using LS.Framework;
using LS.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LS.Framework
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class,new()
    {
        public DbContext _dbcontext = DbContextFactory.DbContext();
        public int Insert(TEntity entity)
        {
            _dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
            
            return _dbcontext.SaveChanges();
        }
        public int Insert(List<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                _dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return _dbcontext.SaveChanges();
        }
        public int Update(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        _dbcontext.Entry(entity).Property(prop.Name).CurrentValue = null;
                    _dbcontext.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }
            return _dbcontext.SaveChanges();
        }
        public int Delete(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Attach(entity);
            _dbcontext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return _dbcontext.SaveChanges();
        }
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entitys = _dbcontext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => _dbcontext.Entry<TEntity>(m).State = EntityState.Deleted);
            return _dbcontext.SaveChanges();
        }
        public TEntity FindEntity(object keyValue)
        {
            return _dbcontext.Set<TEntity>().Find(keyValue);
        }
        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbcontext.Set<TEntity>().FirstOrDefault(predicate);
        }
        public IQueryable<TEntity> IQueryable()
        {
            return _dbcontext.Set<TEntity>();
        }
        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbcontext.Set<TEntity>().Where(predicate);
        }
        public List<TEntity> FindList(string strSql)
        {
            return _dbcontext.Database.SqlQuery<TEntity>(strSql).ToList<TEntity>();
        }
        public List<TEntity> FindList(string strSql, DbParameter[] dbParameter)
        {
            return _dbcontext.Database.SqlQuery<TEntity>(strSql, dbParameter).ToList<TEntity>();
        }
        public List<TEntity> FindList(Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbcontext.Set<TEntity>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToList();
        }
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbcontext.Set<TEntity>().Where(predicate);
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToList();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbcontext.Set<TEntity>().Any(predicate);
        }
    }
}

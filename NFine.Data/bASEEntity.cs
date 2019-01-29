using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Data
{
    public class BaseEntity<TEntity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        //public virtual void Create()
        //{
        //    var entity = this as ICreationAudited;
        //    var loginInfo = OperatorProvider.Provider.CurrentUser;
        //    if (loginInfo != null)
        //    {
        //        if (entity != null) entity.CreatorUserId = loginInfo.UserId;
        //    }

        //    if (entity != null)
        //    {
        //        entity.DeleteMark = false;
        //        entity.CreatorTime = DateTime.Now;
        //    }
        //}
        //public virtual void Modify(int keyValue)
        //{
        //    if (this is IModificationAudited entity)
        //    {
        //        entity.Id = keyValue;
        //        var loginInfo = OperatorProvider.Provider.CurrentUser;
        //        if (loginInfo != null)
        //        {
        //            entity.LastModifyUserId = loginInfo.UserId;
        //        }

        //        entity.LastModifyTime = DateTime.Now;
        //    }
        //}
        //public virtual void Remove()
        //{
        //    var entity = this as IDeleteAudited;
        //    var loginInfo = OperatorProvider.Provider.CurrentUser;
        //    if (loginInfo != null)
        //    {
        //        if (entity != null) entity.DeleteUserId = loginInfo.UserId;
        //    }

        //    if (entity != null)
        //    {
        //        entity.DeleteTime = DateTime.Now;
        //        entity.DeleteMark = true;
        //    }
        //}
    }
}

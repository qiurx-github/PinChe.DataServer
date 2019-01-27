﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LS.Framework.Data 
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            //CallContext：是线程内部唯一的独用的数据槽（一块内存空间）  
            //传递DbContext进去获取实例的信息，在这里进行强制转换。  
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;

            if (dbContext == null)  //线程在内存中没有此上下文  
            {
                //如果不存在上下文 创建一个(自定义)EF上下文  并且放在数据内存中去  
                dbContext = new LsDbContext();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;
        }

        public static DbContext DbContext()
        {
            DbContext dbContext = HttpContext.Current.Items["dbContext"] as DbContext;
            if (dbContext == null)
            {
                dbContext = new LsDbContext();
                HttpContext.Current.Items["dbContext"] = dbContext;
            }
            return dbContext;
        }
    }

}

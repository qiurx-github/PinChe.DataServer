using LS.Framework.Data;
using LS.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Framework.Repository.Interface
{
    public interface ITestRepository:IRepositoryBase<Test>
    {
        void InsertTest();
    }
}

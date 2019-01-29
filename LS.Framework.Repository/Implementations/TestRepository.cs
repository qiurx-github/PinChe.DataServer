using LS.Framework.Data;
using LS.Framework.Models;
using LS.Framework.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Framework.Repository.Implementations
{
    public class TestRepository : RepositoryBase<Test>, ITestRepository
    {
        public void InsertTest()
        {
            Insert(new Test() { Data="ss"});
        }
    }
}

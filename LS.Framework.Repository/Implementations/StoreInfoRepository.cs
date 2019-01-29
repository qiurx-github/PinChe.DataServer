using LS.Framework.Data;
using LS.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Framework.Repository.Implementations
{
    public class StoreInfoRepository: RepositoryBase<StoreInfo>
    {
        public void InsertStoreInfo(StoreInfo storeInfo)
        {
            Insert(storeInfo);
        }
    }
}

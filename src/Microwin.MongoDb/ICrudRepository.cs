using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNeed.MongoDb.Repositories
{
    public interface ICrudRepository<TKey, TVal>
    {
        Task<TKey> Create(TVal model);

        Task CreateOrReplace(TVal model);

        Task CreateOrReplace(IEnumerable<TVal> models);

        Task<TVal> Load(TKey id);

        Task<List<TVal>> LoadAll(IEnumerable<TKey> ids);

        Task<List<TVal>> LoadAll();

        Task Update(TVal model);

        Task Delete(TKey id);

        void PreCreate(TVal model);

        void PreUpdate(TVal model);
    }
}

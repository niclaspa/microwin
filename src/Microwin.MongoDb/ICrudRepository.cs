using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public interface ICrudRepository<TKey, TVal> : IReadRepository<TKey, TVal>
    {
        Task<TKey> Create(TVal model);

        Task CreateOrReplace(TVal model);

        Task CreateOrReplace(IEnumerable<TVal> models);

        Task Update(TVal model);

        Task Delete(TKey id);

        void PreCreate(TVal model);

        void PreUpdate(TVal model);
    }
}

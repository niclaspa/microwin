using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public interface IReadRepository<TKey, TVal>
    {
        Task<TVal> Load(TKey id);

        Task<List<TVal>> LoadAll(IEnumerable<TKey> ids);

        Task<List<TVal>> LoadAll();
    }
}

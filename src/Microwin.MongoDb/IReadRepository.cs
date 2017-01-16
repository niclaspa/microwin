using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public interface IReadRepository<TKey, TVal> where TVal : IDocument<TKey>
    {
        Task<TVal> Load(TKey id);

        Task<List<TVal>> LoadAll(IEnumerable<TKey> ids);

        Task<List<TVal>> LoadAll();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public interface ICachedReadRepository<TKey, TVal> where TVal : IDocument<TKey>
    {
        Task<TVal> Load(TKey id);

        Task<List<TVal>> LoadAll(IEnumerable<TKey> ids);

        Task<List<TVal>> LoadAll();
    }

    public class CachedReadRepository<TKey, TVal> : ICachedReadRepository<TKey, TVal> where TVal : IDocument<TKey>
    {
        private IReadRepository<TKey, TVal> repo;
        private Dictionary<TKey, TVal> cache = new Dictionary<TKey,TVal>();
        bool hasLoadedAll = false;

        public CachedReadRepository(IReadRepository<TKey, TVal> repo)
        {
            if (repo == null) { throw new ArgumentNullException("repo"); }

            this.repo = repo;
        }

        public async Task<TVal> Load(TKey id)
        {
            TVal res;
            if (this.cache.ContainsKey(id))
            {
                res = this.cache[id];
            }
            else
            {
                res = await this.repo.Load(id);
                this.cache[id] = res;
            }

            return res;
        }

        public async Task<List<TVal>> LoadAll(IEnumerable<TKey> ids)
        {
            var idsToLoad = ids.Where(x => !this.cache.ContainsKey(x));
            if (idsToLoad.Count() > 0)
            {
                foreach (var val in await this.repo.LoadAll(idsToLoad))
                {
                    this.cache[val.Id] = val;
                }
            }

            var res = new List<TVal>();
            foreach (var id in ids)
            {
                res.Add(this.cache[id]);
            }

            return res;
        }

        public async Task<List<TVal>> LoadAll()
        {
            if (!this.hasLoadedAll)
            {
                this.hasLoadedAll = true;
                foreach (var val in await this.repo.LoadAll())
                {
                    this.cache[val.Id] = val;
                }
            }

            return this.cache.Values.ToList();
        }
    }
}

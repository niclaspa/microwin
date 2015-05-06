using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNeed.MongoDb.Models
{
    public interface IDocument<T>
    {
        T Id { get; set; }

        int Version { get; set; }
    }
}

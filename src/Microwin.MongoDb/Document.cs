using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public class Document<T> : IDocument<T>
    {
        public T Id { get; set; }

        public int Version { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}

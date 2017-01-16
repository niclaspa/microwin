using System;

namespace Microwin.MongoDb
{
    public interface IDocument<T>
    {
        T Id { get; set; }

        int Version { get; set; }

        DateTime CreatedTime { get; set; }
    }
}

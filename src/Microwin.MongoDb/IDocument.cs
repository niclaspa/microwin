﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwin.MongoDb
{
    public interface IDocument<T>
    {
        T Id { get; set; }

        int Version { get; set; }

        DateTime CreatedTime { get; set; }
    }
}

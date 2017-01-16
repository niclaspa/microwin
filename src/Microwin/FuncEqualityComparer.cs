using System;
using System.Collections.Generic;

namespace Microwin
{
    public class FuncEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> equals;
        private Func<T, int> getHashCode;

        public FuncEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            this.equals = equals;
            this.getHashCode = getHashCode;
        }

        public bool Equals(T x, T y)
        {
            return equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return getHashCode(obj);
        }
    }
}

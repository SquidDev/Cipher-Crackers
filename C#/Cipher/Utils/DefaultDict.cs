using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Utils
{
    public abstract class DefaultDict<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public DefaultDict() : base() { }
        public DefaultDict(IDictionary<TKey, TValue> Dict) : base(Dict) { }

        public TValue GetOrDefault(TKey Key)
        {
            TValue Result;
            if (TryGetValue(Key, out Result))
            {
                return Result;
            }

            Result = GetDefault(Key);
            this[Key] = Result;

            return Result;
        }

        public abstract TValue GetDefault(TKey Key);
    }

    public class BasicDefaultDict<TKey, TValue> : DefaultDict<TKey, TValue>
        where TValue : new()
    {
         public BasicDefaultDict() : base() { }
         public BasicDefaultDict(IDictionary<TKey, TValue> Dict) : base(Dict) { }

         public override TValue GetDefault(TKey Key)
         {
             return new TValue();
         }
    }
}

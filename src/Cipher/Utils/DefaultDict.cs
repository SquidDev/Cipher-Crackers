using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Utils
{
	public static class DefaultDict
	{
		public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> creator)
		{
			TValue result;
			if(!dict.TryGetValue(key, out result)) 
			{
				result = creator(key);
				dict.Add(key, result);
			}

			return result;
		}
		
		public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
			where TValue : new()
		{
			TValue result;
			if(!dict.TryGetValue(key, out result)) 
			{
				result = new TValue();
				dict.Add(key, result);
			}

			return result;
		}
	}

}

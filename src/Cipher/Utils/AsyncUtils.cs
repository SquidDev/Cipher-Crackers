using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Cipher
{
    public static class AsyncUtils
    {
        public static IEnumerable<TRes> RunAsync<TKey, TRes>(this ICollection<TKey> e, Func<TKey, TRes> method)
        {
            List<Task<TRes>> tasks = new List<Task<TRes>>(e.Count);
            foreach (TKey key in e)
            {
                TKey cache = key;
                tasks.Add(Task<TRes>.Run(() => method(cache)));
            }

            Task<TRes>.WaitAll(tasks.ToArray());

            return tasks.Select(x => x.Result);
        }
        
        public static IEnumerable<TRes> RunAsync<TRes>(int size, Func<TRes> method)
        {
        	Task<TRes>[] tasks = new Task<TRes>[size];
        	for(int i = 0; i < size; i++)
            {
        		tasks[i] = Task<TRes>.Run(method);
            }

            Task<TRes>.WaitAll(tasks);

            return tasks.Select(x => x.Result);
        }
    }
}


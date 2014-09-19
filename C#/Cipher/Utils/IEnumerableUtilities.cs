using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Utils
{
    public static class IEnumerableUtilities
    {
        public static int MaxIndex<T>(this IEnumerable<T> Enumerable)
            where T : IComparable<T>
        {
            int MaxIndex = -1;
            T MaxValue = default(T);

            int Index = 0;
            foreach (T Value in Enumerable)
            {
                if (MaxIndex == -1 || Value.CompareTo(MaxValue) > 0)
                {
                    MaxIndex = Index;
                    MaxValue = Value;
                }
                Index++;
            }
            return MaxIndex;
        }

        public static KeyValuePair<int, T> MaxIndexValue<T>(this IEnumerable<T> Enumerable)
            where T : IComparable<T>
        {
            int MaxIndex = -1;
            T MaxValue = default(T);

            int Index = 0;
            foreach (T Value in Enumerable)
            {
                if (MaxIndex == -1 || Value.CompareTo(MaxValue) > 0)
                {
                    MaxIndex = Index;
                    MaxValue = Value;
                }
                Index++;
            }
            return new KeyValuePair<int, T>(MaxIndex, MaxValue); ;
        }

        public static T LastValue<T>(this IList<T> List)
        {
            return List[List.Count - 1];
        }
    }
}

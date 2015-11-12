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

        public static T Max<T>(this IEnumerable<T> e, Comparison<T> comparer)
        {
            T max = default(T);
            bool empty = true;
            foreach (T value in e)
            {
                if (empty || comparer(value, max) > 0)
                {
                    empty = false;
                    max = value;
                }
            }

            return max;
        }

        public static T Max<T>(this IEnumerable<T> e)
            where T : IComparable
        {
            T max = default(T);
            bool empty = true;

            foreach (T value in e)
            {
                if (empty || value.CompareTo(max) > 0)
                {
                    empty = false;
                    max = value;
                }

            }

            return max;
        }
        
        public static T MaxWith<T, TComp>(this IEnumerable<T> e, Func<T, TComp> selector)
            where TComp : IComparable
        {
            T max = default(T);
            TComp maxValue = default(TComp);
            bool empty = true;

            foreach (T value in e)
            {
            	TComp comparable = selector(value);
            	if (empty || comparable.CompareTo(maxValue) > 0)
                {
                    empty = false;
                    max = value;
                    maxValue = comparable;
                }

            }

            return max;
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
            return new KeyValuePair<int, T>(MaxIndex, MaxValue);
            ;
        }

        public static T LastValue<T>(this IList<T> List)
        {
            return List[List.Count - 1];
        }

        public static string PrettyString<T>(this IEnumerable<T> Items)
        {
            return "{" + String.Join(", ", Items) + "}";
        }
    }
}

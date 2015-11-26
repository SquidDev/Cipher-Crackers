using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Ciphers.Keys
{
    public class CachingGridArray : GridArray
    {
        public Dictionary<byte, Vector> cache;

        #region Constructors
        /// <summary>
        /// Create a GridArray from a 2 Dimensional Array
        /// </summary>
        /// <param name="Elements"></param>
        public CachingGridArray(byte[,] Elements)
            : base(Elements)
        {
            CreateCache();
        }

        /// <summary>
        /// Create a GridArray from an array and width and height
        /// </summary>
        /// <param name="Width">Width of the grid</param>
        /// <param name="Height">Height of the grid</param>
        /// <param name="Elements">Values to populate with</param>
        public CachingGridArray(byte Width, byte Height, IList<byte> Elements)
            : base(Width, Height,Elements)
        {
            CreateCache();
        }

        /// <summary>
        /// Create a GridArray from an array and width and height
        /// </summary>
        /// <param name="Width">Width of the grid</param>
        /// <param name="Height">Height of the grid</param>
        /// <param name="Elements">Values to populate with</param>
        public CachingGridArray(byte Width, byte Height, IEnumerable<byte> Elements)
            : base(Width, Height, Elements)
        {
            CreateCache();
        }

        /// <summary>
        /// Create an empty GridArray
        /// </summary>
        /// <param name="Width">Width of the array</param>
        /// <param name="Height">Height of the array</param>
        public CachingGridArray(byte Width, byte Height)
            : base(Width, Height)
        { }
        #endregion
        
        #region Cache
        public void RefreshCache()
        {
            for (byte x = 0; x < Width; x++)
            {
                for (byte y = 0; y < Height; y++)
                {
                    Vector element = cache[Elements[x, y]];
                    element.X = x;
                    element.Y = y;
                }
            }
        }

        protected void CreateCache()
        {
            cache = new Dictionary<byte, Vector>();
            for (byte X = 0; X < Width; X++)
            {
                for (byte Y = 0; Y < Height; Y++)
                {
                    cache.Add(Elements[X, Y], new Vector(X, Y));
                }
            }
        }
        #endregion

        public override void Swap(byte aX, byte aY, byte bX, byte bY)
        {
            byte a = Elements[aX, aY];
            byte b = Elements[bX, bY];
            Elements[aX, aY] = b;
            Elements[bX, bY] = a;

            Vector vector = cache[a];
            vector.X = bX;
            vector.Y = bY;

            vector = cache[b];
            vector.X = aX;
            vector.Y = aY;
        }

        /// <summary>
        /// Get the coordinates of Item
        /// </summary>
        /// <param name="Item">Item to find</param>
        /// <param name="X">X Coordinate</param>
        /// <param name="Y">Y Coordinate</param>
        public override bool IndexOf(byte item, out byte x, out byte y)
        {
            Vector element = cache[item];
            x = element.X;
            y = element.Y;
            return true;
        }

        public void CopyTo(CachingGridArray target)
        {
            if(target.cache == null)
            {
                target.cache = new Dictionary<byte, Vector>();
            }
            base.CopyTo(target);
            foreach(KeyValuePair<byte, Vector> item in cache)
            {
                Vector vec;
                if(target.cache.TryGetValue(item.Key, out vec))
                {
                    vec.X = item.Value.X;
                    vec.Y = item.Value.Y;
                }
                else
                {
                    target.cache.Add(item.Key, new Vector(item.Value.X, item.Value.Y));
                }
            }
        }

        /// <summary>
        /// Internal class for Handling XY coordinates
        /// </summary>
        public class Vector
        {
            public byte X;
            public byte Y;

            public Vector(byte X, byte Y)
            {
                this.X = X;
                this.Y = Y;
            }

            public override bool Equals(object Obj)
            {
                if(Obj is Vector)
                {
                    Vector Vec = (Vector)Obj;
                    return Vec.X == X && Vec.Y == Y;
                }

                return false;
            }
            
            public override int GetHashCode()
			{
				unchecked {
					return 31 * X + Y;
				}
			}


            public override string ToString()
            {
                return String.Format("Vector({0}, {0})", X, Y);
            }
        }
    }
}

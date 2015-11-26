using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Ciphers.Keys
{
    public class CachingGridArray : GridArray
    {
    	public Vector[] Cache;

        public CachingGridArray(byte[,] elements, int cacheSize)
        	: base(elements)
        {
            CreateCache(cacheSize);
        }

        public CachingGridArray(byte width, byte height, IList<byte> elements, int cacheSize)
            : base(width, height,elements)
        {
            CreateCache(cacheSize);
        }

        public CachingGridArray(byte width, byte height, IEnumerable<byte> elements, int cacheSize)
            : base(width, height, elements)
        {
            CreateCache(cacheSize);
        }

        public CachingGridArray(byte width, byte height, int cacheSize)
            : base(width, height)
        {
        	CreateCache(cacheSize);
        }
        
        public void RefreshCache()
        {
            for (byte x = 0; x < Width; x++)
            {
                for (byte y = 0; y < Height; y++)
                {
                    Vector element = Cache[Elements[x, y]];
                    element.X = x;
                    element.Y = y;
                }
            }
        }

        protected void CreateCache(int cacheSize)
        {
        	Cache = new Vector[cacheSize];
            for (byte x = 0; x < Width; x++)
            {
                for (byte y = 0; y < Height; y++)
                {
                	Cache[Elements[x, y]] = new Vector(x, y);
                }
            }
        }

        public override void Swap(byte aX, byte aY, byte bX, byte bY)
        {
            byte a = Elements[aX, aY];
            byte b = Elements[bX, bY];
            Elements[aX, aY] = b;
            Elements[bX, bY] = a;

            Vector aVector = Cache[a];
            aVector.X = bX;
            aVector.Y = bY;

            Vector bVector = Cache[b];
            bVector.X = aX;
            bVector.Y = aY;
        }

        /// <summary>
        /// Get the coordinates of Item
        /// </summary>
        /// <param name="Item">Item to find</param>
        /// <param name="X">X Coordinate</param>
        /// <param name="Y">Y Coordinate</param>
        public override bool IndexOf(byte item, out byte x, out byte y)
        {
            Vector element = Cache[item];
            x = element.X;
            y = element.Y;
            return true;
        }

        public void CopyTo(CachingGridArray target)
        {
        	base.CopyTo(target);
            for(int i = 0; i < Cache.Length; i++)
            {
            	Vector item = Cache[i];
            	if(item == null) 
            	{
            		target.Cache[i] = null;
            	}
            	else 
            	{
	                Vector vec = target.Cache[i];
	                if(vec != null)
	                {
	                    vec.X = item.X;
	                    vec.Y = item.Y;
	                }
	                else
	                {
	                	target.Cache[i] = new Vector(item.X, item.Y);
	                }
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

using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cipher.Ciphers.Keys
{
    /// <summary>
    /// Stores a grid array for keys which use grids
    /// </summary>
    public class GridArray
    {
        public readonly byte[,] Elements;
        public readonly byte Width;
        public readonly byte Height;

        #region Constructors
        /// <summary>
        /// Create a GridArray from a 2 Dimensional Array
        /// </summary>
        /// <param name="Elements"></param>
        public GridArray(byte[,] elements)
        {
            Elements = elements;
            Width = (byte)elements.GetLength(0);
            Height = (byte)elements.GetLength(1);
        }

        /// <summary>
        /// Create a GridArray from an array and width and height
        /// </summary>
        /// <param name="width">Width of the grid</param>
        /// <param name="height">Height of the grid</param>
        /// <param name="elements">Values to populate with</param>
        public GridArray(byte width, byte height, IList<byte> elements)
            : this(width, height)
        {
            for(byte x = 0; x < width; x++)
            {
                for(byte y = 0; y < width; y++)
                {
                    this.Elements[x, y] = elements[y * width + x];
                }
            }
        }

        /// <summary>
        /// Create a GridArray from an array and width and height
        /// </summary>
        /// <param name="width">Width of the grid</param>
        /// <param name="height">Height of the grid</param>
        /// <param name="elements">Values to populate with</param>
        public GridArray(byte width, byte height, IEnumerable<byte> elements)
            : this(width, height)
        {
            int x = 0;
            int y = 0;
            foreach(byte Item in elements)
            {
                this.Elements[x, y] = Item;

                x++;
                if(x >= width)
                {
                    y++;
                    x = 0;
                }
            }
        }

        /// <summary>
        /// Create an empty GridArray
        /// </summary>
        /// <param name="width">Width of the array</param>
        /// <param name="height">Height of the array</param>
        public GridArray(byte width, byte height)
        {
            Elements = new byte[width, height];
            Width = width;
            Height = height;
        }
        #endregion
        
        #region Key manipulation
        /// <summary>
        /// Swap to random rows
        /// </summary>
        public void SwapRows(Random random)
        {
            byte rowA = (byte)random.Next(Height);
            byte rowB = (byte)random.Next(Height);

            if (rowA == rowB) return;
            for (byte offset = 0; offset < Width; offset++)
            {
                Swap(offset, rowA, offset, rowB);
            }
        }

        /// <summary>
        /// Swap two random columns
        /// </summary>
        public void SwapColumns(Random random)
        {
            byte colA = (byte)random.Next(Width);
            byte colB = (byte)random.Next(Width);

            if (colA == colB) return;
            for (byte offset = 0; offset < Height; offset++)
            {
                Swap(colA, offset, colB, offset);
            }
        }

        /// <summary>
        /// Reverse the order of the rows
        /// </summary>
        public void ReverseRows()
        {
            for (byte y = 0; y < Height / 2; y++)
            {
                int yNew = Height - y - 1;
                for (byte x = 0; x < Width; x++)
                {
                    Swap(x, y, x, yNew);
                }
            }
        }

        /// <summary>
        /// Reverse the order of the columns
        /// </summary>
        public void ReverseColumns()
        {
            for (byte x = 0; x < Width / 2; x++)
            {
                int newX = Width - x - 1;
                for (byte y = 0; y < Height; y++)
                {
                    Swap(x, y, newX,  y);
                }
            }
        }

        /// <summary>
        /// Reverse the entire keysquare
        /// </summary>
        public void ReverseSquare()
        {
            int whole = Width * Height;
            int half = whole / 2;
            whole--;
            for(int i = 0; i < half; i++)
            {
                int j = whole - i;
                Swap(i % Width, i / Width, j % Width, j / Width);
            }
        }
        #endregion
        
        #region Utilities
        /// <summary>
        /// Swap two elements in the array
        /// </summary>
        public virtual void Swap(byte aX, byte aY, byte bX, byte bY)
        {
            byte temp = Elements[aX, aY];
            Elements[aX, aY] = Elements[bX, bY];
            Elements[bX, bY] = temp;
        }

        public void Swap(int aX, int aY, int bX, int bY)
        {
            Swap((byte)aX, (byte)aY, (byte)bX, (byte)bY);
        }
        /// <summary>
        /// Swap two random elements in the array
        /// </summary>
        public void Swap(Random random)
        {
            Swap(
                (byte)random.Next(Width), (byte)random.Next(Height),
                (byte)random.Next(Width), (byte)random.Next(Height)
            );
        }

        /// <summary>
        /// Get the coordinates of Item
        /// </summary>
        /// <param name="Item">Item to find</param>
        /// <param name="X">X Coordinate</param>
        /// <param name="Y">Y Coordinate</param>
        public virtual bool IndexOf(byte item, out byte x, out byte y)
        {
            y = 0;
            for(x = 0; x < Width; x ++)
            {
                for(y = 0; y < Height; y++)
                {
                    if(Elements[x, y] == item) return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Fisher yates shuffle
        /// </summary>
        public virtual void Shuffle()
        {
            Random random = MathsUtilities.RandomInstance;
            for(byte i = (byte)(Width * Height - 1); i > 1; i--)
            {
                byte swap = (byte)random.Next(i);
                i--;
                Swap(i % Width, i / Width, swap % Width, swap / Width);
            }
        }

        /// <summary>
        /// Copy array to GridArray
        /// </summary>
        /// <param name="target">GridArray to copy to</param>
        public virtual void CopyTo(GridArray target)
        {
            for(byte x = 0; x < Width; x++)
            {
                for(byte y = 0; y < Height; y++)
                {
                    target.Elements[x, y] = Elements[x, y];
                }
            }
        }
        #endregion

        public override string ToString()
        {
            StringBuilder output = new StringBuilder(Width * Height);
            foreach(byte character in Elements)
            {
                output.Append((char)(character + 'A'));
            }

            return output.ToString();
        }
    }
}

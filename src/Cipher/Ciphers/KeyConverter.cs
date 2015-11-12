using System;
using System.Collections.Generic;
using System.Linq;

using Cipher.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    public interface IKeyConverter<T>
    {
    	T FromString(string key);
    	
    	string ToString(T key);
    }
    
    public class KeyConverters
    {
    	public static readonly IKeyConverter<byte> Letter = new LetterConverter();
    	public static readonly IKeyConverter<byte[]> String = new ArrayConverter<byte>(false, Letter);
    	
    	public static readonly IKeyConverter<int> Integer = new IntConverter();
    	
    	public static readonly IKeyConverter<byte> Byte = new ByteConverter();
    	public static readonly IKeyConverter<byte[]> ByteList = new ArrayConverter<byte>(true, Byte);
    	
    	public static readonly IKeyConverter<Matrix<float>> Matrix = new MatrixConverter();
    }
    
    class LetterConverter : IKeyConverter<byte>
    {
    	public byte FromString(string key)
		{
    		return (byte)(Byte.Parse(key) - 'A');
		}
    	
		public string ToString(byte key)
		{
			return (key + 'A').ToString();
		}
    }
    
    class ByteConverter : IKeyConverter<byte>
    {
		public byte FromString(string key)
		{
			return Byte.Parse(key);
		}
    	
		public string ToString(byte key)
		{
			return key.ToString();
		}
    }
    
    class IntConverter : IKeyConverter<int>
    {
		public int FromString(string key)
		{
			return Int32.Parse(key);
		}
    	
		public string ToString(int key)
		{
			return key.ToString();
		}
    }
    
    class ArrayConverter<T> : IKeyConverter<T[]>
    {
    	private readonly bool withSeparator;
    	private readonly IKeyConverter<T> child;
    	
    	public ArrayConverter(bool withSeparator, IKeyConverter<T> child)
    	{
    		this.withSeparator = withSeparator;
    		this.child = child;
    	}
		public T[] FromString(string key)
		{
			IEnumerable<string> enumerable = withSeparator ? key.Split(';') : key.Select(Char.ToString);
			return enumerable.Select(child.FromString).ToArray();
		}
    	
		public string ToString(T[] key)
		{
			return String.Join(withSeparator ? ";" : "", key.Select(child.ToString));
		}
    }
    
    class MatrixConverter : IKeyConverter<Matrix<float>>
    {
		public Matrix<float> FromString(string key)
		{
			return MatrixExtensions.ReadMatrix(key);
		}
    	
		public string ToString(Matrix<float> key)
		{
			return String.Join(";", key.EnumerateColumns().Select(x => String.Join(",", x.Enumerate())));
		}
    }
}


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testing
{
    public abstract class DataTest
    {
        private TestContext _Context;

        public TestContext TestContext
        {
            get { return _Context; }
            set { _Context = value; }
        }

        public T DataReadElement<T>(string Name)
        {
            return (T)TestContext.DataRow[Name];
        }

        public string DataRead(string Name)
        {
            return TestContext.DataRow[Name].ToString();
        }

        public int DataReadInt(string Name)
        {
            return Int32.Parse(DataRead(Name));
        }

        public byte DataReadByte(string Name)
        {
            return Byte.Parse(DataRead(Name));
        }

        public double DataReadDouble(string Name)
        {
            return Double.Parse(DataRead(Name));
        }
    }
}

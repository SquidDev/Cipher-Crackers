﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cipher.Ciphers.Keys;
using NUnit.Framework;
using Vector = Cipher.Ciphers.Keys.CachingGridArray.Vector;

namespace Testing.Tools
{
    [TestFixture]
    public class GridArrayTest
    {
        [Test]
        [Category("Tools")]
        public void TestCachingGridArray()
        {
            CachingGridArray a = new CachingGridArray(new byte[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            CachingGridArray b = new CachingGridArray(3, 3);

            a.CopyTo(b);

            a.Swap(0, 0, 2, 2);
            Assert.AreEqual(9, a.Elements[0, 0]);
            Assert.AreEqual(1, a.Elements[2, 2]);

            Assert.AreEqual(new Vector(2, 2), a.cache[1]);
            Assert.AreEqual(new Vector(0, 0), a.cache[9]);

            // Check B has not changed.
            Assert.AreEqual(1, b.Elements[0, 0]);
            Assert.AreEqual(9, b.Elements[2, 2]);

            Assert.AreEqual(new Vector(2, 2), b.cache[9]);
            Assert.AreEqual(new Vector(0, 0), b.cache[1]);

            b.CopyTo(a);

            // Check cache updated
            Assert.AreEqual(1, a.Elements[0, 0]);
            Assert.AreEqual(9, a.Elements[2, 2]);

            Assert.AreEqual(1, a.Elements[0, 0]);
            Assert.AreEqual(9, a.Elements[2, 2]);

            Assert.AreEqual(new Vector(2, 2), a.cache[9]);
            Assert.AreEqual(new Vector(0, 0), a.cache[1]);
        }
    }
}

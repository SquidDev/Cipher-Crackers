﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing
{
    public static class AssertUtils
    {
        public static void AssertRoughly(double Expected, double Actual, int DecimalPlaces)
        {
            Assert.AreEqual<double>(Math.Round(Expected, DecimalPlaces), Math.Round(Actual, DecimalPlaces));
        }

        public static void AssertRoughly(double Expected, double Actual, int DecimalPlaces, string Message)
        {
            Assert.AreEqual<double>(Math.Round(Expected, DecimalPlaces), Math.Round(Actual, DecimalPlaces), Message);
        }

        public static void AssertRoughly(double Expected, double Actual, int DecimalPlaces, string Message, params object[] Params)
        {
            Assert.AreEqual<double>(Math.Round(Expected, DecimalPlaces), Math.Round(Actual, DecimalPlaces), Message, Params);
        }

        public static void AssertRoughly(double Expected, double Actual, MidpointRounding Mode)
        {
            Assert.AreEqual<double>(Math.Round(Expected, Mode), Math.Round(Actual, Mode));
        }

        public static void AssertRoughly(double Expected, double Actual, MidpointRounding Mode, string Message)
        {
            Assert.AreEqual<double>(Math.Round(Expected, Mode), Math.Round(Actual, Mode), Message);
        }

        public static void AssertRoughly(double Expected, double Actual, MidpointRounding Mode, string Message, params object[] Params)
        {
            Assert.AreEqual<double>(Math.Round(Expected, Mode), Math.Round(Actual, Mode), Message, Params);
        }
    }
}

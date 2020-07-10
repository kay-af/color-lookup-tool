using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorLookupTool;
using System;
using System.Diagnostics;

namespace ColorLookupTest
{
    [TestClass]
    public class ColorLookupTest
    {
        [TestMethod]
        public void CheckOverloads()
        {
            ColorLookup.Initialize();

            Assert.AreEqual("Red", ColorLookup.Match(0xff0000).colorName);
            Assert.AreEqual("Red", ColorLookup.Match(255,0,0).colorName);
            Assert.AreEqual("Red", ColorLookup.Match("#ff0000").colorName);
        }

        [TestMethod]
        public void CheckColorInformation()
        {
            ColorLookup.Initialize();
            
            Assert.IsTrue(ColorLookup.Match("#f7e7ce").ToString().EndsWith("[#f7e7ce]"));
        }

        [TestMethod]
        public void CheckNearestColorMatch()
        {
            ColorLookup.Initialize();
            
            Assert.AreEqual("Alabaster", ColorLookup.Match("#F2F0E7").colorName);
            Assert.AreEqual("Cherry", ColorLookup.Match(219,50,99).colorName);
        }
    }
}

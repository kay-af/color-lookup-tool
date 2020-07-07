using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorLookupTool;

namespace ColorLookupTest
{
    [TestClass]
    public class ColorLookupTest
    {
        [TestMethod]
        public void CheckOverloads()
        {
            ColorLookup.Initialize();

            Assert.AreEqual("Red", ColorLookup.Match(0xff0000));
            Assert.AreEqual("Red", ColorLookup.Match(255,0,0));
            Assert.AreEqual("Red", ColorLookup.Match("#ff0000"));
        }

        [TestMethod]
        public void CheckNearestColorMatch()
        {
            ColorLookup.Initialize();
            
            Assert.AreEqual("Alabaster", ColorLookup.Match("#F2F0E7"));
            Assert.AreEqual("Cherry", ColorLookup.Match(219,50,99));
        }
    }
}

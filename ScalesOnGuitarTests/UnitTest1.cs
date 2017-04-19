using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalesOnGuitar;
using System.Collections.Generic;

namespace ScalesOnGuitarTests
{
    [TestClass]
    public class TestScale
    {
        [TestMethod]
        public void TestScale1()
        {
            // Arrange
            var s = new Scale("C", ToneMode.Major);

            // Act
            //s.GenerateScale(ToneMode.Major);
            var actual = s.wantedScale;
            var expected = new List<string> { "C", "D", "E", "F", "G", "A", "H" };

            // Assert
            for (int i = actual.Count - 1; i >= 0; i--)
            {
                Assert.AreEqual(actual[i], expected[i]);
            }
        }
    }
}

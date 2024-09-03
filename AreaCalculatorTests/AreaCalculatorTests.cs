using Microsoft.VisualStudio.TestTools.UnitTesting;
using AreaCalculatorNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaCalculatorNS.Tests
{
    [TestClass()]
    public class AreaCalculatorTests
    {
        [TestMethod()]
        public void Circle_RadiusLessThan0()
        {
            Random rnd = new Random();
            double mantissa = (rnd.NextDouble() * 2.0) - 2.0;
            double exponent = Math.Pow(2.0, rnd.Next(-126, 127));
            double radius = mantissa * exponent;
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Circle(radius));
        }

        [TestMethod()]
        public void Circle_RadiusEquals0()
        {
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Circle(0));
        }

        [TestMethod()]
        public void Circle_StandardRadius()
        {
            Random rnd = new Random();
            
            for (int i = 0; i < 20; i++)
            {
                double mantissa = rnd.NextDouble();
                while (mantissa == 0)
                    mantissa = rnd.NextDouble();
                double exponent = Math.Pow(2.0, rnd.Next(-126, 127));
                double radius = mantissa * exponent;
                Assert.AreEqual(Math.Pow(radius, 2) * Math.PI, AreaCalculator.Circle(radius));
            }
        }

        [TestMethod()]
        public void Triangle_StandardSides()
        {
            
            Assert.AreEqual(60, AreaCalculator.Triangle(8, 15, 17), 0.01);
            Assert.AreEqual(1536, AreaCalculator.Triangle(80, 48, 64), 0.01);
            Assert.AreEqual(106.65, AreaCalculator.Triangle(12, 18, 20), 0.01);
            Assert.AreEqual(86.82, AreaCalculator.Triangle(14, 13, 16), 0.01);
        }

        [TestMethod()]
        public void Triangle_ImpossibleSides()
        {

            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Triangle(4, 8, 15));
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Triangle(6, 24, 17));
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Triangle(10, 1, 1));
        }

        [TestMethod()]
        public void Triangle_SidesNotGreaterThan0()
        {

            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Triangle(0, 8, 15));
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Triangle(6, -24, 17));
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => AreaCalculator.Triangle(0, 0, 0));
        }
    }
}
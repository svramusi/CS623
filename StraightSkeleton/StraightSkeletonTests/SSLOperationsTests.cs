using System;

using StraightSkeletonLib;
using MathLib;

using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class SSLOperationsTests
    {
        private LAV listOfActiveVertices;

        [SetUp]
        protected void SetUp()
        {
            listOfActiveVertices = new LAV();
            listOfActiveVertices.Add(new Vertex(2, 6));
            listOfActiveVertices.Add(new Vertex(2, 2));
            listOfActiveVertices.Add(new Vertex(15, 2));
            listOfActiveVertices.Add(new Vertex(15, 6));

            SSLOperations.FindAngleBisectors(listOfActiveVertices);
        }

        [Test]
        public void TestAngleBisectors()
        {
            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(1).AngleBisector.GetX(), 3));
            Assert.AreEqual(5.293, Math.Round(listOfActiveVertices.Get(1).AngleBisector.GetY(), 3));

            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(2).AngleBisector.GetX(), 3));
            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(2).AngleBisector.GetY(), 3));

            Assert.AreEqual(14.293, Math.Round(listOfActiveVertices.Get(3).AngleBisector.GetX(), 3));
            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(3).AngleBisector.GetY(), 3));

            Assert.AreEqual(14.293, Math.Round(listOfActiveVertices.Get(4).AngleBisector.GetX(), 3));
            Assert.AreEqual(5.293, Math.Round(listOfActiveVertices.Get(4).AngleBisector.GetY(), 3));
        }

        [Test]
        public void TestClosestIntersection()
        {
            Intersection intersection = SSLOperations.GetClosestIntersection(listOfActiveVertices.GetStart());
            Assert.AreEqual(4, Math.Round(intersection.GetX()));
            Assert.AreEqual(4, Math.Round(intersection.GetY()));

            Assert.AreEqual(listOfActiveVertices.Get(2), intersection.GetVA());
            Assert.AreEqual(listOfActiveVertices.GetStart(), intersection.GetVB());

            Assert.AreEqual(2, Math.Round(intersection.Distance));
        }
    }
}

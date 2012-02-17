using System;

using StraightSkeletonLib;
using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class VertexTests
    {
        [Test]
        public void TestIntegerXY()
        {
            Vertex v = new Vertex(0, 1);
            Assert.AreEqual(0, v.GetX());
            Assert.AreEqual(1, v.GetY());
        }

        [Test]
        public void TestDoubleXY()
        {
            Vertex v = new Vertex(0.1, 1.1);
            Assert.AreEqual(0.1, v.GetX());
            Assert.AreEqual(1.1, v.GetY());
        }

        [Test]
        public void TestNullPrevAndNextNeighbor()
        {
            Vertex v = new Vertex(0, 1);
            Assert.IsNull(v.GetPrevVertex());
            Assert.IsNull(v.GetNextVertex());
        }

        [Test]
        public void TestSetNextVertex()
        {
            Vertex v1 = new Vertex(0, 1);
            Vertex v2 = new Vertex(1, 1);

            Assert.IsNull(v1.GetNextVertex());

            v1.SetNextVertex(v2);

            Assert.AreEqual(v2, v1.GetNextVertex());
        }

        [Test]
        public void TestSetPrevVertex()
        {
            Vertex v1 = new Vertex(0, 1);
            Vertex v2 = new Vertex(1, 1);

            Assert.IsNull(v1.GetPrevVertex());

            v1.SetPrevVertex(v2);

            Assert.AreEqual(v2, v1.GetPrevVertex());
        }

        [Test]
        public void TestNullComparison()
        {
            Vertex v1 = new Vertex(0.1234, 0.1234);
            Vertex v2 = null;

            Assert.IsFalse(v1.Equals(v2));
        }

        [Test]
        public void TestInvalidObjectComparison()
        {
            Vertex v1 = new Vertex(0.1234, 0.1234);
            LAV lav = new LAV();

            Assert.IsFalse(v1.Equals(lav));
        }

        [Test]
        public void TestEqualVertices()
        {
            Vertex v1 = new Vertex(0.1234, 0.1234);
            Vertex v2 = new Vertex(0.1234, 0.1234);

            Assert.IsTrue(v1.Equals(v2));
        }

        [Test]
        public void TestNonEqualVertices()
        {
            Vertex v1 = new Vertex(0.1234, 0.1234);
            Vertex v2 = new Vertex(0.1234, 0.1134);

            Assert.IsFalse(v1.Equals(v2));
        }

        [Test]
        public void TestAngleBisector()
        {
            Vertex v1 = new Vertex(1, 1);
            Vertex bisector = new Vertex(2, 3);

            v1.AngleBisector = bisector;

            Assert.AreEqual(2, bisector.GetX());
            Assert.AreEqual(3, bisector.GetY());

        }
    }
}

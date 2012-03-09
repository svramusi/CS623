using System;

using StraightSkeletonLib;
using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class VertexTests
    {
        [Test]
        public void TestDeepCopy()
        {
            Vertex v = new Vertex(0, 1);
            Vertex bisector = new Vertex(100, 100);

            v.SetProcessed();
            v.Type = Vertex.VertexType.Split;
            v.AngleBisector = bisector;

            Vertex v2 = new Vertex(v);

            Assert.IsTrue(v2.Processed);
            Assert.AreEqual(Vertex.VertexType.Split, v2.Type);
            Assert.AreEqual(bisector, v2.AngleBisector);
        }

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
        public void TestLineSegmentEdges()
        {
            Vertex v1 = new Vertex(0, 1);
            Vertex v2 = new Vertex(0, 0);
            Vertex v3 = new Vertex(1, 0);

            Assert.IsNull(v2.PrevLineSegment);
            Assert.IsNull(v2.NextLineSegment);

            v2.SetPrevVertex(v1);
            v2.SetNextVertex(v3);

            Assert.AreEqual(new LineSegment(v1, v2), v2.PrevLineSegment);
            Assert.AreEqual(new LineSegment(v2, v3), v2.NextLineSegment);
        }


        [Test]
        public void TestSetLineSegmentEdges()
        {
            Vertex v1 = new Vertex(0, 1);
            Vertex v2 = new Vertex(0, 0);
            Vertex v3 = new Vertex(1, 0);

            LineSegment ls1 = new LineSegment(new Vertex(1, 1), new Vertex(3, 3));
            LineSegment ls2 = new LineSegment(new Vertex(10, 10), new Vertex(30, 30));

            Assert.IsNull(v2.PrevLineSegment);
            Assert.IsNull(v2.NextLineSegment);

            v2.SetPrevVertex(v1);
            v2.SetNextVertex(v3);

            Assert.AreEqual(new LineSegment(v1, v2), v2.PrevLineSegment);
            Assert.AreEqual(new LineSegment(v2, v3), v2.NextLineSegment);

            v2.PrevLineSegment = ls1;
            v2.NextLineSegment = ls2;

            Assert.AreEqual(ls1, v2.PrevLineSegment);
            Assert.AreEqual(ls2, v2.NextLineSegment);
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

        [Test]
        public void TestProcessed()
        {
            Vertex v = new Vertex(0, 0);
            Assert.IsFalse(v.Processed);

            v.SetProcessed();
            Assert.IsTrue(v.Processed);
        }

        [Test]
        public void TestType()
        {
            Vertex v = new Vertex(0, 0);
            Assert.AreEqual(Vertex.VertexType.Undefined, v.Type);

            v.Type = Vertex.VertexType.Split;

            Assert.AreEqual(Vertex.VertexType.Split, v.Type);
        }
    }
}

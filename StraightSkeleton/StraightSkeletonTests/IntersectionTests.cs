using System;

using StraightSkeletonLib;

using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class IntersectionTests
    {
        LineSegment ls1;
        LineSegment ls2;

        [SetUp]
        public void SetUp()
        {
            ls1 = new LineSegment(new Vertex(0, 0), new Vertex(0, 1));
            ls2 = new LineSegment(new Vertex(0, 0), new Vertex(1, 0));
        }

        [Test]
        public void TestIntersectionInit()
        {
            Vertex vA = new Vertex(2, 6);
            Vertex vB = new Vertex(2, 2);

            Intersection intersection = new Intersection(4, 4, vA, vB, Vertex.VertexType.Edge, ls1, ls2);

            Assert.AreEqual(4, intersection.GetX());
            Assert.AreEqual(4, intersection.GetY());

            Assert.AreEqual(vA, intersection.GetVA());
            Assert.AreEqual(vB, intersection.GetVB());

            Assert.AreEqual(2, intersection.Distance);
        }

        [Test]
        public void TestEqualIntersections()
        {
            Intersection intersection1 = new Intersection(4, 4, new Vertex(2, 6), new Vertex(2, 6), Vertex.VertexType.Edge, ls1, ls2);
            Intersection intersection2 = new Intersection(4, 4, new Vertex(2, 6), new Vertex(2, 6), Vertex.VertexType.Edge, ls1, ls2);
            Assert.AreEqual(intersection1, intersection2);
        }

        [Test]
        public void TestGetType()
        {
            Intersection intersection = new Intersection(4, 4, new Vertex(2, 6), new Vertex(2, 6), Vertex.VertexType.Edge, ls1, ls2);
            Assert.AreEqual(Vertex.VertexType.Edge, intersection.Type);
        }


        [Test]
        public void TestGetLineSegments()
        {
            Intersection intersection = new Intersection(4, 4, new Vertex(2, 6), new Vertex(2, 6), Vertex.VertexType.Edge, ls1, ls2);
            Assert.AreEqual(ls1, intersection.GetLSVA());
            Assert.AreEqual(ls2, intersection.GetLSVB());
        }
    }
}

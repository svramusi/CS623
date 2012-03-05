using System;

using StraightSkeletonLib;

using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class IntersectionTests
    {
        [Test]
        public void TestIntersectionInit()
        {
            Vertex vA = new Vertex(2, 6);
            Vertex vB = new Vertex(2, 2);

            Intersection intersection = new Intersection(4, 4, vA, vB, Vertex.VertexType.Edge);

            Assert.AreEqual(4, intersection.GetX());
            Assert.AreEqual(4, intersection.GetY());

            Assert.AreEqual(vA, intersection.GetVA());
            Assert.AreEqual(vB, intersection.GetVB());

            Assert.AreEqual(2, intersection.Distance);
        }

        [Test]
        public void TestEqualIntersections()
        {
            Intersection intersection1 = new Intersection(4, 4, new Vertex(2, 6), new Vertex(2, 6), Vertex.VertexType.Edge);
            Intersection intersection2 = new Intersection(4, 4, new Vertex(2, 6), new Vertex(2, 6), Vertex.VertexType.Edge);
            Assert.AreEqual(intersection1, intersection2);
        }

        [Test]
        public void TestGetType()
        {
            Intersection intersection = new Intersection(4, 4, new Vertex(2, 6), new Vertex(2, 6), Vertex.VertexType.Edge);
            Assert.AreEqual(Vertex.VertexType.Edge, intersection.Type);
        }
    }
}

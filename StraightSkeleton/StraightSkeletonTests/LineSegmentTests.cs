using System;

using StraightSkeletonLib;

using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class LineSegmentTests
    {
        Vertex v1;
        Vertex v2;

        [SetUp]
        protected void SetUp()
        {
            v1 = new Vertex(0, 1);
            v2 = new Vertex(2, 3);
        }

        [Test]
        public void TestLineSegment()
        {
            LineSegment ls = new LineSegment(v1, v2);

            Assert.AreEqual(0, ls.Start.GetX());
            Assert.AreEqual(1, ls.Start.GetY());
            Assert.AreEqual(2, ls.End.GetX());
            Assert.AreEqual(3, ls.End.GetY());
        }

        [Test]
        public void TestEqualSegments()
        {
            LineSegment ls1 = new LineSegment(v1, v2);
            LineSegment ls2 = new LineSegment(v1, v2);

            Assert.AreEqual(ls1, ls2);
        }
    }
}

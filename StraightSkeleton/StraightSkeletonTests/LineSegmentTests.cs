using System;

using StraightSkeletonLib;

using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class LineSegmentTests
    {
        [Test]
        public void TestLineSegment()
        {
            LineSegment ls = new LineSegment(0, 1, 2, 3);
            Assert.AreEqual(0, ls.Start.X);
            Assert.AreEqual(1, ls.Start.Y);
            Assert.AreEqual(2, ls.End.X);
            Assert.AreEqual(3, ls.End.Y);
        }

        [Test]
        public void TestEqualSegments()
        {
            LineSegment ls1 = new LineSegment(0, 1, 2, 3);
            LineSegment ls2 = new LineSegment(0, 1, 2, 3);

            Assert.AreEqual(ls1, ls2);
        }
    }
}

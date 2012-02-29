using System;

using StraightSkeletonLib;

using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class IntersectionQueueTests
    {
        private IntersectionQueue q;

        [SetUp]
        protected void SetUp()
        {
            q = new IntersectionQueue();
        }

        [Test]
        public void BasicOperationTest()
        {
            q.PushDouble(10);
            Assert.AreEqual(10, q.GetMinDouble());
        }

        [Test]
        public void PushTwoMinFirstTest()
        {
            q.PushDouble(10);
            q.PushDouble(100);

            Assert.AreEqual(10, q.GetMinDouble());
            Assert.AreEqual(100, q.GetMinDouble());
        }

        [Test]
        public void PushTwoMaxFirstTest()
        {
            q.PushDouble(100);
            q.PushDouble(10);

            Assert.AreEqual(10, q.GetMinDouble());
            Assert.AreEqual(100, q.GetMinDouble());
        }

        [Test]
        public void PushThreeMinFirstTest()
        {
            q.PushDouble(1);
            q.PushDouble(2);
            q.PushDouble(3);

            Assert.AreEqual(1, q.GetMinDouble());
            Assert.AreEqual(2, q.GetMinDouble());
            Assert.AreEqual(3, q.GetMinDouble());
        }

        [Test]
        public void PushThreeMinFirstTest2()
        {
            q.PushDouble(1);
            q.PushDouble(3);
            q.PushDouble(2);

            Assert.AreEqual(1, q.GetMinDouble());
            Assert.AreEqual(2, q.GetMinDouble());
            Assert.AreEqual(3, q.GetMinDouble());
        }

        [Test]
        public void PushFourMaxFirstTest()
        {
            q.PushDouble(4);
            q.PushDouble(3);;
            q.PushDouble(2);
            q.PushDouble(1);

            Assert.AreEqual(1, q.GetMinDouble());
            Assert.AreEqual(2, q.GetMinDouble());
            Assert.AreEqual(3, q.GetMinDouble());
            Assert.AreEqual(4, q.GetMinDouble());
        }
        
        [Test]
        public void PushFourMaxFirstThenTwoMinTest()
        {
            q.PushDouble(4);
            q.PushDouble(3);
            q.PushDouble(2);
            q.PushDouble(1);

            q.PushDouble(5);
            q.PushDouble(6);

            Assert.AreEqual(1, q.GetMinDouble());
            Assert.AreEqual(2, q.GetMinDouble());
            Assert.AreEqual(3, q.GetMinDouble());
            Assert.AreEqual(4, q.GetMinDouble());
            Assert.AreEqual(5, q.GetMinDouble());
            Assert.AreEqual(6, q.GetMinDouble());
        }
        
        [Test]
        public void PushTwentyMinFirstTest()
        {
            for (int i = 1; i <= 20; i++)
                q.PushDouble(i);

            for (int i = 1; i <= 20; i++)
                Assert.AreEqual(i, q.GetMinDouble());
        }

        [Test]
        public void PushTwentyMaxFirstTest()
        {
            for (int i = 20; i > 0; i--)
                q.PushDouble(i);

            for (int i = 1; i <= 20; i++)
                Assert.AreEqual(i, q.GetMinDouble());
        }

        [Test]
        public void PushOneHundredMinFirstTest()
        {
            for (int i = 1; i <= 100; i++)
                q.PushDouble(i);

            for (int i = 1; i <= 100; i++)
                Assert.AreEqual(i, q.GetMinDouble());
        }

        [Test]
        public void PushOneHundredMaxFirstTest()
        {
            for (int i = 100; i > 0; i--)
                q.PushDouble(i);

            for (int i = 1; i <= 100; i++)
                Assert.AreEqual(i, q.GetMinDouble());
        }

        [Test]
        public void PushTwoFiftyMinThenTwoFiftyMaxTest()
        {
            for (int i = 1; i <= 250; i++)
                q.PushDouble(i);

            for (int i = 500; i > 250; i--)
                q.PushDouble(i);

            for (int i = 1; i <= 500; i++)
                Assert.AreEqual(i, q.GetMinDouble());
        }

        [Test]
        public void PushTwoFiftyMaxThenTwoFiftyMinTest()
        {
            for (int i = 500; i > 250; i--)
                q.PushDouble(i);

            for (int i = 1; i <= 250; i++)
                q.PushDouble(i);

            for (int i = 1; i <= 500; i++)
                Assert.AreEqual(i, q.GetMinDouble());
        }

        [Test]
        public void PushIntersectionsTest()
        {
            q.Push(new Intersection(0, 0, new Vertex(0, 0), new Vertex(0, 0), 10));
            q.Push(new Intersection(0, 0, new Vertex(0, 0), new Vertex(0, 0), 20));
            q.Push(new Intersection(0, 0, new Vertex(0, 0), new Vertex(0, 0), 30));

            Assert.AreEqual(new Intersection(0, 0, new Vertex(0, 0), new Vertex(0, 0), 10), q.GetMin());
        }

        [Test]
        public void IsEmptyTest()
        {
            Assert.IsTrue(q.IsEmpty());
            q.PushDouble(0);
            Assert.IsFalse(q.IsEmpty());
        }
    }
}

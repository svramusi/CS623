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
            q.Push(10);
            Assert.AreEqual(10, q.GetLast());
            Assert.AreEqual(10, q.GetMin());
        }

        [Test]
        public void PushTwoMinFirstTest()
        {
            q.Push(10);
            q.Push(100);

            Assert.AreEqual(10, q.GetMin());
            Assert.AreEqual(100, q.GetMin());
        }

        [Test]
        public void PushTwoMaxFirstTest()
        {
            q.Push(100);
            q.Push(10);

            Assert.AreEqual(10, q.GetMin());
            Assert.AreEqual(100, q.GetMin());
        }

        [Test]
        public void PushThreeMinFirstTest()
        {
            q.Push(1);
            q.Push(2);
            q.Push(3);

            Assert.AreEqual(1, q.GetMin());
            Assert.AreEqual(2, q.GetMin());
            Assert.AreEqual(3, q.GetMin());
        }

        [Test]
        public void PushThreeMinFirstTest2()
        {
            q.Push(1);
            q.Push(3);
            q.Push(2);

            Assert.AreEqual(1, q.GetMin());
            Assert.AreEqual(2, q.GetMin());
            Assert.AreEqual(3, q.GetMin());
        }

        [Test]
        public void PushFourMaxFirstTest()
        {
            q.Push(4);
            q.Push(3);;
            q.Push(2);
            q.Push(1);

            Assert.AreEqual(1, q.GetMin());
            Assert.AreEqual(2, q.GetMin());
            Assert.AreEqual(3, q.GetMin());
            Assert.AreEqual(4, q.GetMin());
        }
        
        [Test]
        public void PushFourMaxFirstThenTwoMinTest()
        {
            q.Push(4);
            q.Push(3);
            q.Push(2);
            q.Push(1);

            q.Push(5);
            q.Push(6);

            Assert.AreEqual(1, q.GetMin());
            Assert.AreEqual(2, q.GetMin());
            Assert.AreEqual(3, q.GetMin());
            Assert.AreEqual(4, q.GetMin());
            Assert.AreEqual(5, q.GetMin());
            Assert.AreEqual(6, q.GetMin());
        }

        [Test]
        public void PushTenMinFirstTest()
        {
            q.Push(1);
            q.Push(2);

            q.Push(3);
            q.Push(4);
            Assert.AreEqual(2, q.GetLast());

            q.Push(5);
            q.Push(6);

            Assert.AreEqual(3, q.GetLast());
            q.Push(7);
            q.Push(8);

            Assert.AreEqual(4, q.GetLast());
            q.Push(9);
            q.Push(10);
        }

        [Test]
        public void PushTwentyMinFirstTest()
        {
            for (int i = 1; i <= 20; i++)
                q.Push(i);

            for (int i = 1; i <= 20; i++)
                Assert.AreEqual(i, q.GetMin());
        }

        [Test]
        public void PushTwentyMaxFirstTest()
        {
            for (int i = 20; i > 0; i--)
                q.Push(i);

            for (int i = 1; i <= 20; i++)
                Assert.AreEqual(i, q.GetMin());
        }

        [Test]
        public void PushOneHundredMinFirstTest()
        {
            for (int i = 1; i <= 100; i++)
                q.Push(i);

            for (int i = 1; i <= 100; i++)
                Assert.AreEqual(i, q.GetMin());
        }

        [Test]
        public void PushOneHundredMaxFirstTest()
        {
            for (int i = 100; i > 0; i--)
                q.Push(i);

            for (int i = 1; i <= 100; i++)
                Assert.AreEqual(i, q.GetMin());
        }

        [Test]
        public void PushTwoFiftyMinThenTwoFiftyMaxTest()
        {
            for (int i = 1; i <= 250; i++)
                q.Push(i);

            for (int i = 500; i > 250; i--)
                q.Push(i);

            for (int i = 1; i <= 500; i++)
                Assert.AreEqual(i, q.GetMin());
        }

        [Test]
        public void PushTwoFiftyMaxThenTwoFiftyMinTest()
        {
            for (int i = 500; i > 250; i--)
                q.Push(i);

            for (int i = 1; i <= 250; i++)
                q.Push(i);

            for (int i = 1; i <= 500; i++)
                Assert.AreEqual(i, q.GetMin());
        }
    }
}

using System;

using StraightSkeletonLib;
using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class SLAVTests
    {
        private SLAV slav;

        private Vertex v1;
        private Vertex v2;
        private Vertex v3;
        private Vertex v4;
        private Vertex v5;


        [SetUp]
        protected void SetUp()
        {
            slav = new SLAV();

            v1 = new Vertex(0, 3);
            v2 = new Vertex(2, 0);
            v3 = new Vertex(4, 2);
            v4 = new Vertex(6, 0);
            v5 = new Vertex(8, 3);

            slav.Insert(v1);
            slav.Insert(v2);
            slav.Insert(v3);
            slav.Insert(v4);
            slav.Insert(v5);
        }

        [Test]
        public void TestBreakAndCreateNew()
        {
            //NEED TO CONTINUE HERE!!!
            LAV lav = slav.Get(0);
            SSLOperations.SetVertexType(lav);
            SSLOperations.ComputeAngleBisectors(lav);

            Assert.AreEqual(v1, lav.Get(1));
            Assert.AreEqual(v2, lav.Get(2));
            Assert.AreEqual(v3, lav.Get(3));
            Assert.AreEqual(Vertex.VertexType.Split, v3.Type);

            Assert.AreEqual(v4, lav.Get(4));
            Assert.AreEqual(v5, lav.Get(5));

            slav.BreakAndCreateNew(v3, 0);

            lav = slav.Get(0);

            Assert.AreEqual(v1, lav.Get(1));
            Assert.AreEqual(v2, lav.Get(2));
            Assert.AreEqual(v3, lav.Get(3));
            Assert.AreEqual(v1, v3.GetNextVertex());
        }
    }
}

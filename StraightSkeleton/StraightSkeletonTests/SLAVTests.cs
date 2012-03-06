using System;

using StraightSkeletonLib;
using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class SLAVTests
    {
        [Test]
        public void TestBreakAndCreateNew()
        {
            SLAV slav = new SLAV();

            Vertex v1 = new Vertex(0, 3);
            Vertex v2 = new Vertex(2, 0);
            Vertex v3 = new Vertex(4, 2);
            Vertex v4 = new Vertex(6, 0);
            Vertex v5 = new Vertex(8, 3);

            slav.Insert(v1);
            slav.Insert(v2);
            slav.Insert(v3);
            slav.Insert(v4);
            slav.Insert(v5);

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

        [Test]
        public void TestQuad()
        {
            SLAV slav = new SLAV();

            Vertex v1 = new Vertex(0, 6);
            Vertex v2 = new Vertex(0, 0);
            Vertex v3 = new Vertex(2, 0);
            Vertex v4 = new Vertex(4, 2);
            Vertex v5 = new Vertex(6, 2);
            Vertex v6 = new Vertex(9, 0);
            Vertex v7 = new Vertex(5, 6);
            Vertex v8 = new Vertex(3.5, 4);
            Vertex v9 = new Vertex(3, 6);
            Vertex v10 = new Vertex(0, 6);

            slav.Insert(v1);
            slav.Insert(v2);
            slav.Insert(v3);
            slav.Insert(v4);
            slav.Insert(v5);
            slav.Insert(v6);
            slav.Insert(v7);
            slav.Insert(v8);
            slav.Insert(v9);
            slav.Insert(v10);


            LAV lav = slav.Get(0);
            SSLOperations.SetVertexType(lav);
            SSLOperations.ComputeAngleBisectors(lav);

            Assert.AreEqual(v1, lav.Get(1));
            Assert.AreEqual(v2, lav.Get(2));
            Assert.AreEqual(v3, lav.Get(3));
            Assert.AreEqual(v4, lav.Get(4));
            Assert.AreEqual(v5, lav.Get(5));
            Assert.AreEqual(v6, lav.Get(6));
            Assert.AreEqual(v7, lav.Get(7));
            Assert.AreEqual(v8, lav.Get(8));
            Assert.AreEqual(v9, lav.Get(9));
            Assert.AreEqual(v10, lav.Get(10));

            slav.BreakAndCreateNew(v8, 0);
        }
    }
}

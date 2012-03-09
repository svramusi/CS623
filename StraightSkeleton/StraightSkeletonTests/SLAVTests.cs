using System;

using StraightSkeletonLib;
using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class SLAVTests
    {
        [Test]
        public void TestSLAVInsertSingleList()
        {
            SLAV slav = new SLAV();

            Vertex v1 = new Vertex(0, 3);
            slav.Insert(v1, 0);

            LAV lav = slav.Get(0);
            Assert.AreEqual(v1, lav.GetStart());
        }

        [Test]
        public void TestSLAVInsertDoubleList()
        {
            SLAV slav = new SLAV();

            Vertex v1 = new Vertex(0, 3);
            slav.Insert(v1, 0);

            LAV lav = slav.Get(0);
            Assert.AreEqual(v1, lav.GetStart());

            
            Vertex v2 = new Vertex(1, 3);
            slav.Insert(v2, 1);

            lav = slav.Get(1);
            Assert.AreEqual(v2, lav.GetStart());


            lav = slav.Get(0);
            Assert.AreEqual(v1, lav.GetStart());
        }

        [Test]
        public void TestCount()
        {
            SLAV slav = new SLAV();

            slav.Insert(new Vertex(0, 3), 0);
            slav.Insert(new Vertex(0, 3), 1);
            slav.Insert(new Vertex(0, 3), 2);

            Assert.AreEqual(3, slav.Count);
        }

        [Test]
        public void TestBreakAndCreateNew()
        {
            SLAV slav = new SLAV();

            //NEED TO MAINTAIN THIS LIST FOR TESTING PURPOSES 
            LAV localLav = new LAV();

            Vertex v1 = new Vertex(0, 3);
            Vertex v2 = new Vertex(2, 0);
            Vertex v3 = new Vertex(4, 2);
            Vertex v4 = new Vertex(6, 0);
            Vertex v5 = new Vertex(8, 3);

            localLav.Add(v1);
            localLav.Add(v2);
            localLav.Add(v3);
            localLav.Add(v4);
            localLav.Add(v5);

            SSLOperations.SetVertexType(localLav);
            SSLOperations.ComputeAngleBisectors(localLav);



            slav.Insert(v1, 0);
            slav.Insert(v2, 0);
            slav.Insert(v3, 0);
            slav.Insert(v4, 0);
            slav.Insert(v5, 0);

            SSLOperations.SetVertexType(slav.Get(0));
            SSLOperations.ComputeAngleBisectors(slav.Get(0));

            LAV lav = slav.Get(0);
            Assert.AreEqual(v1, lav.Get(1));
            Assert.AreEqual(v2, lav.Get(2));
            Assert.AreEqual(v3, lav.Get(3));
            Assert.AreEqual(Vertex.VertexType.Split, lav.Get(3).Type);
            Assert.AreEqual(v4, lav.Get(4));
            Assert.AreEqual(v5, lav.Get(5));

            slav.BreakAndCreateNew(v3, 0);

            lav = slav.Get(0);

            Assert.AreEqual(v1, lav.Get(1));
            Assert.AreEqual(v2, lav.Get(2));
            Assert.AreEqual(new Vertex(4, 3), lav.Get(3));
            Assert.AreEqual(v1, lav.Get(3).GetNextVertex());


            lav = slav.Get(1);

            Assert.AreEqual(new Vertex(4, 3), lav.Get(1));
            Assert.AreEqual(v4, lav.Get(2));
            Assert.AreEqual(v5, lav.Get(3));

            Assert.AreEqual(new Vertex(4, 3), lav.Get(3).GetNextVertex());
        }

        [Test]
        public void TestQuad()
        {
            SLAV slav = new SLAV();

            //NEED TO MAINTAIN THIS LIST FOR TESTING PURPOSES 
            LAV localLav = new LAV();

            Vertex v1 = new Vertex(0, 6);
            Vertex v2 = new Vertex(0, 0);
            Vertex v3 = new Vertex(2, 0);
            Vertex v4 = new Vertex(4, 2);
            Vertex v5 = new Vertex(6, 2);
            Vertex v6 = new Vertex(9, 0);
            Vertex v7 = new Vertex(5, 6);
            Vertex v8 = new Vertex(3.5, 4);
            Vertex v9 = new Vertex(3, 6);

            localLav.Add(v1);
            localLav.Add(v2);
            localLav.Add(v3);
            localLav.Add(v4);
            localLav.Add(v5);
            localLav.Add(v6);
            localLav.Add(v7);
            localLav.Add(v8);
            localLav.Add(v9);

            slav.Insert(v1, 0);
            slav.Insert(v2, 0);
            slav.Insert(v3, 0);
            slav.Insert(v4, 0);
            slav.Insert(v5, 0);
            slav.Insert(v6, 0);
            slav.Insert(v7, 0);
            slav.Insert(v8, 0);
            slav.Insert(v9, 0);


            SSLOperations.SetVertexType(localLav);
            SSLOperations.ComputeAngleBisectors(localLav);


            SSLOperations.SetVertexType(slav.Get(0));
            SSLOperations.ComputeAngleBisectors(slav.Get(0));

            LAV lav = slav.Get(0);
            Assert.AreEqual(v1, lav.Get(1));
            Assert.AreEqual(v2, lav.Get(2));
            Assert.AreEqual(v3, lav.Get(3));
            Assert.AreEqual(v4, lav.Get(4));
            Assert.AreEqual(v5, lav.Get(5));
            Assert.AreEqual(v6, lav.Get(6));
            Assert.AreEqual(v7, lav.Get(7));
            Assert.AreEqual(v8, lav.Get(8));
            Assert.AreEqual(v9, lav.Get(9));

            slav.BreakAndCreateNew(v8, 0);


            lav = slav.Get(0);

            Assert.AreEqual(v1, lav.Get(1));
            Assert.AreEqual(v2, lav.Get(2));
            Assert.AreEqual(v3, lav.Get(3));
            Assert.AreEqual(v4, lav.Get(4));
            Assert.AreEqual(3.30, Math.Round(lav.Get(5).GetX(), 2));
            Assert.AreEqual(3.02, Math.Round(lav.Get(5).GetY(), 2));
            Assert.AreEqual(v9, lav.Get(6));
            Assert.AreEqual(v1, lav.Get(6).GetNextVertex());


            lav = slav.Get(1);

            Assert.AreEqual(3.30, Math.Round(lav.Get(1).GetX(), 2));
            Assert.AreEqual(3.02, Math.Round(lav.Get(1).GetY(), 2));
            Assert.AreEqual(v5, lav.Get(2));
            Assert.AreEqual(v6, lav.Get(3));
            Assert.AreEqual(v7, lav.Get(4));
            Assert.AreEqual(3.30, Math.Round(lav.Get(4).GetNextVertex().GetX(), 2));
        }
    }
}

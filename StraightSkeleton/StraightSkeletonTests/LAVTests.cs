using System;

using StraightSkeletonLib;
using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class LAVTests
    {
        private LAV listOfActiveVertices;

        
        [SetUp]
        protected void SetUp()
        {
            listOfActiveVertices = new LAV();
        }
        

        [Test]
        public void TestEmptyLAV()
        {
            Assert.IsNull(this.listOfActiveVertices.GetStart());
        }

        [Test]
        public void TestAddVertexToList()
        {
            Vertex v = new Vertex(0.0, 1.1);
            listOfActiveVertices.Add(v);

            Vertex startVertex = listOfActiveVertices.GetStart();
            Assert.AreEqual(0.0, startVertex.GetX());
            Assert.AreEqual(1.1, startVertex.GetY());

            Assert.IsNull(startVertex.GetPrevVertex());
            Assert.IsNull(startVertex.GetNextVertex());
        }

        [Test]
        public void TestAddTwoVertexToList()
        {
            Vertex v1 = new Vertex(0.0, 0.0);
            listOfActiveVertices.Add(v1);

            Vertex v2 = new Vertex(1.1, 1.1);
            listOfActiveVertices.Add(v2);

            Vertex startVertex = listOfActiveVertices.GetStart();
            Assert.AreEqual(0.0, startVertex.GetX());
            Assert.AreEqual(0.0, startVertex.GetY());

            Assert.AreEqual(v2, startVertex.GetNextVertex());
            Assert.AreEqual(v2, startVertex.GetPrevVertex());
        }

        [Test]
        public void TestAddThreeVertexToList()
        {
            Vertex v1 = new Vertex(0.0, 0.0);
            listOfActiveVertices.Add(v1);

            Vertex v2 = new Vertex(1.1, 1.1);
            listOfActiveVertices.Add(v2);

            Vertex v3 = new Vertex(2.2, 2.2);
            listOfActiveVertices.Add(v3);

            Vertex startVertex = listOfActiveVertices.GetStart();
            Assert.AreEqual(0.0, startVertex.GetX());
            Assert.AreEqual(0.0, startVertex.GetY());

            Assert.AreEqual(v2, startVertex.GetNextVertex());
            Assert.AreEqual(v3, startVertex.GetPrevVertex());
        }

        [Test]
        public void TestForEach()
        {
            Vertex v1 = new Vertex(0, 0);
            listOfActiveVertices.Add(v1);

            Vertex v2 = new Vertex(1, 1);
            listOfActiveVertices.Add(v2);

            Vertex v3 = new Vertex(2, 2);
            listOfActiveVertices.Add(v3);

            int count = 0;
            foreach (Vertex v in listOfActiveVertices)
            {
                Assert.AreEqual(Convert.ToDouble(count), v.GetX());
                Assert.AreEqual(Convert.ToDouble(count), v.GetY());

                count++;
            }
        }

        [Test]
        public void TestGet()
        {
            Vertex v1 = new Vertex(0, 0);
            listOfActiveVertices.Add(v1);

            Vertex v2 = new Vertex(1, 1);
            listOfActiveVertices.Add(v2);

            Vertex v3 = new Vertex(2, 2);
            listOfActiveVertices.Add(v3);

            Vertex v4 = new Vertex(3, 3);
            listOfActiveVertices.Add(v4);

            Vertex v5 = new Vertex(4, 4);
            listOfActiveVertices.Add(v5);

            Vertex v6 = new Vertex(5, 5);
            listOfActiveVertices.Add(v6);


            Vertex getV = listOfActiveVertices.Get(2);
            Assert.AreEqual(v2, getV);

            getV = listOfActiveVertices.Get(5);
            Assert.AreEqual(v5, getV);
        }

        [Test]
        public void TestLength()
        {
            listOfActiveVertices.Add(new Vertex(0, 0));
            listOfActiveVertices.Add(new Vertex(1, 1));

            Assert.AreEqual(2, listOfActiveVertices.Length);
        }

        [Test]
        public void TestGetNextOnLastElement()
        {
            Vertex v1 = new Vertex(0, 0);
            listOfActiveVertices.Add(v1);

            Vertex v2 = new Vertex(1, 1);
            listOfActiveVertices.Add(v2);

            Vertex lastVertex = listOfActiveVertices.Get(listOfActiveVertices.Length);
            Assert.AreEqual(v1, lastVertex.GetNextVertex());
        }

        [Test]
        public void TestReplace()
        {
            listOfActiveVertices.Add(new Vertex(1, 1));
            listOfActiveVertices.Add(new Vertex(2, 2));
            listOfActiveVertices.Add(new Vertex(3, 3));
            listOfActiveVertices.Add(new Vertex(4, 4));
            listOfActiveVertices.Add(new Vertex(5, 5));
            listOfActiveVertices.Add(new Vertex(6, 6));

            listOfActiveVertices.Insert(new Vertex(9, 9), listOfActiveVertices.Get(3), listOfActiveVertices.Get(4));

            Assert.AreEqual(1, listOfActiveVertices.Get(1).GetX());
            Assert.AreEqual(2, listOfActiveVertices.Get(2).GetX());
            Assert.AreEqual(9, listOfActiveVertices.Get(3).GetX());
            Assert.AreEqual(5, listOfActiveVertices.Get(4).GetX());
            Assert.AreEqual(6, listOfActiveVertices.Get(5).GetX());
        }

        [Test]
        public void TestReplaceHead()
        {
            listOfActiveVertices.Add(new Vertex(1, 1));
            listOfActiveVertices.Add(new Vertex(2, 2));
            listOfActiveVertices.Add(new Vertex(3, 3));
            listOfActiveVertices.Add(new Vertex(4, 4));
            listOfActiveVertices.Add(new Vertex(5, 5));
            listOfActiveVertices.Add(new Vertex(6, 6));

            listOfActiveVertices.Insert(new Vertex(9, 9), listOfActiveVertices.Get(1), listOfActiveVertices.Get(2));

            Assert.AreEqual(9, listOfActiveVertices.Get(1).GetX());
            Assert.AreEqual(3, listOfActiveVertices.Get(2).GetX());
            Assert.AreEqual(4, listOfActiveVertices.Get(3).GetX());
            Assert.AreEqual(5, listOfActiveVertices.Get(4).GetX());
            Assert.AreEqual(6, listOfActiveVertices.Get(5).GetX());
        }

        [Test]
        public void TestReplaceTail()
        {
            listOfActiveVertices.Add(new Vertex(1, 1));
            listOfActiveVertices.Add(new Vertex(2, 2));
            listOfActiveVertices.Add(new Vertex(3, 3));
            listOfActiveVertices.Add(new Vertex(4, 4));
            listOfActiveVertices.Add(new Vertex(5, 5));
            listOfActiveVertices.Add(new Vertex(6, 6));

            listOfActiveVertices.Insert(new Vertex(9, 9), listOfActiveVertices.Get(5), listOfActiveVertices.Get(6));

            Assert.AreEqual(1, listOfActiveVertices.Get(1).GetX());
            Assert.AreEqual(2, listOfActiveVertices.Get(2).GetX());
            Assert.AreEqual(3, listOfActiveVertices.Get(3).GetX());
            Assert.AreEqual(4, listOfActiveVertices.Get(4).GetX());
            Assert.AreEqual(9, listOfActiveVertices.Get(5).GetX());
        }

        [Test]
        public void TestReplaceWrapAround()
        {
            listOfActiveVertices.Add(new Vertex(1, 1));
            listOfActiveVertices.Add(new Vertex(2, 2));
            listOfActiveVertices.Add(new Vertex(3, 3));
            listOfActiveVertices.Add(new Vertex(4, 4));
            listOfActiveVertices.Add(new Vertex(5, 5));
            listOfActiveVertices.Add(new Vertex(6, 6));

            listOfActiveVertices.Insert(new Vertex(9, 9), listOfActiveVertices.Get(6), listOfActiveVertices.Get(1));

            Assert.AreEqual(2, listOfActiveVertices.Get(1).GetX());
            Assert.AreEqual(3, listOfActiveVertices.Get(2).GetX());
            Assert.AreEqual(4, listOfActiveVertices.Get(3).GetX());
            Assert.AreEqual(5, listOfActiveVertices.Get(4).GetX());
            Assert.AreEqual(9, listOfActiveVertices.Get(5).GetX());
        }
    }
}

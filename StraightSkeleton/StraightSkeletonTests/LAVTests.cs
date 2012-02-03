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
    }
}

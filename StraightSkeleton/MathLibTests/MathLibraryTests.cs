using System;

using MathLib;
using StraightSkeletonLib;

using NUnit.Framework;

namespace MathLibTests
{
    [TestFixture]
    public class MathLibraryTests
    {
        [Test]
        public void DistanceBetweenVerticesTests()
        {
            Vertex v1;
            Vertex v2;

            v1 = new Vertex(0, 0);
            v2 = new Vertex(1, 0);
            Assert.AreEqual(1.0, MathLibrary.GetDistanceBetweenVertices(v1, v2));

            v1 = new Vertex(0, 0);
            v2 = new Vertex(2, 0);
            Assert.AreEqual(2.0, MathLibrary.GetDistanceBetweenVertices(v1, v2));

            v1 = new Vertex(1, 5);
            v2 = new Vertex(-2, 1);
            Assert.AreEqual(5, MathLibrary.GetDistanceBetweenVertices(v1, v2));

            v1 = new Vertex(-2, -3);
            v2 = new Vertex(-4, 4);
            Assert.AreEqual(7.28011, Math.Round(MathLibrary.GetDistanceBetweenVertices(v1, v2), 5));
        }

        [Test]
        public void AngleBetweenVerticesTests()
        {
            Vertex v1;
            Vertex v2;
            Vertex v3;

            v1 = new Vertex(0, 3);
            v2 = new Vertex(0, 0);
            v3 = new Vertex(4, 0);

            Assert.AreEqual(90, MathLibrary.GetAngleBetweenVerticese(v1, v2, v3));
        }
    }
}

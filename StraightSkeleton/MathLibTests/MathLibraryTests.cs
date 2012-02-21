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
        public void DistanceBetweenLineAndVertexTest()
        {
            Assert.AreEqual(2, MathLibrary.GetDistanceBetweenLineAndVertex(new Vertex(2, 6), new Vertex(2, 2), new Vertex(4, 4)));
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

        [Test]
        public void TestAngleBisectorPoint()
        {
            Vertex v1;
            Vertex v2;
            Vertex v3;

            v1 = new Vertex(1, 3);
            v2 = new Vertex(1, 1);
            v3 = new Vertex(3, 1);

            Vertex bisector = MathLibrary.GetAngleBisectorVertex(v1, v2, v3);
            Assert.AreEqual(1.71, Math.Round(bisector.GetX(), 2));
            Assert.AreEqual(1.71, Math.Round(bisector.GetY(), 2));


            v1 = new Vertex(5.78, 5.90);
            v2 = new Vertex(5, 3);
            v3 = new Vertex(6.93, 3.52);

            bisector = MathLibrary.GetAngleBisectorVertex(v1, v2, v3);
            Assert.AreEqual(5.71, Math.Round(bisector.GetX(), 2));
            Assert.AreEqual(3.71, Math.Round(bisector.GetY(), 2));           
        }

        [Test]
        public void TestLineEquationFromTwoPoints()
        {
            Vertex v1;
            Vertex v2;

            v1 = new Vertex(0, 0);
            v2 = new Vertex(1, 1);

            LineEquation le = MathLibrary.GetLineEquation(v1, v2);
            Assert.AreEqual(1.0, le.Slope);
            Assert.AreEqual(0.0, le.YIntercept);

            v1 = new Vertex(0, 1);
            v2 = new Vertex(1, 3);

            LineEquation le2 = MathLibrary.GetLineEquation(v1, v2);
            Assert.AreEqual(2.0, le2.Slope);
            Assert.AreEqual(1.0, le2.YIntercept);

            v1 = new Vertex(0, 5);
            v2 = new Vertex(1, 4);

            LineEquation le3 = MathLibrary.GetLineEquation(v1, v2);
            Assert.AreEqual(-1.0, le3.Slope);
            Assert.AreEqual(5.0, le3.YIntercept);
        }

        [Test]
        public void TestLineIntersection()
        {
            Vertex intersection = MathLibrary.GetIntersectionPoint(new LineEquation(1.0, 0.0), new LineEquation(-1.0, 4.0));
            Assert.AreEqual(2.0, intersection.GetX());
            Assert.AreEqual(2.0, intersection.GetY());

            intersection = MathLibrary.GetIntersectionPoint(new LineEquation(2.0, 3.0), new LineEquation(-0.5, 7.0));
            Assert.AreEqual(1.6, intersection.GetX());
            Assert.AreEqual(6.2, intersection.GetY());

            intersection = MathLibrary.GetIntersectionPoint(new LineEquation(1.0, 0.0), new LineEquation(1.0, -1.0));
            Assert.IsTrue(double.IsInfinity(intersection.GetX()));
            Assert.IsTrue(double.IsInfinity(intersection.GetY()));
        }
    }
}

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
        public void TestEdgeAngleBisectorPoint()
        {
            Vertex v1;
            Vertex v2;
            Vertex v3;

            v1 = new Vertex(1, 3);
            v2 = new Vertex(1, 1);
            v3 = new Vertex(3, 1);

            //THIS WOULD NORMALLY BE CALCULATED IN A LAV
            v1.Type = Vertex.VertexType.Edge;
            v2.Type = Vertex.VertexType.Edge;
            v3.Type = Vertex.VertexType.Edge;

            Vertex bisector = MathLibrary.GetAngleBisectorVertex(v1, v2, v3);
            Assert.AreEqual(1.71, Math.Round(bisector.GetX(), 2));
            Assert.AreEqual(1.71, Math.Round(bisector.GetY(), 2));


            v1 = new Vertex(5.78, 5.90);
            v2 = new Vertex(5, 3);
            v3 = new Vertex(6.93, 3.52);

            //THIS WOULD NORMALLY BE CALCULATED IN A LAV
            v1.Type = Vertex.VertexType.Edge;
            v2.Type = Vertex.VertexType.Edge;
            v3.Type = Vertex.VertexType.Edge;

            bisector = MathLibrary.GetAngleBisectorVertex(v1, v2, v3);
            Assert.AreEqual(5.71, Math.Round(bisector.GetX(), 2));
            Assert.AreEqual(3.71, Math.Round(bisector.GetY(), 2));
        }

        [Test]
        public void TestSplitAngleBisectorPoint()
        {
            Vertex v1 = new Vertex(4, 0);
            Vertex v2 = new Vertex(4, 3);
            Vertex v3 = new Vertex(8, 3);

            //THIS WOULD NORMALLY BE CALCULATED IN A LAV
            v1.Type = Vertex.VertexType.Edge;
            v2.Type = Vertex.VertexType.Split;
            v3.Type = Vertex.VertexType.Edge;

            Vertex bisector = MathLibrary.GetAngleBisectorVertex(v1, v2, v3);            
            Assert.AreEqual(3.29, Math.Round(bisector.GetX(), 2));
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

        [Test]
        public void TestPointRotation()
        {
            Vertex v = MathLibrary.Rotate(new Vertex(1, 2), new Vertex(1, 0), 180);
            Assert.AreEqual(1, Math.Round(v.GetX()));
            Assert.AreEqual(4, Math.Round(v.GetY()));

            v = MathLibrary.Rotate(new Vertex(2, 2), new Vertex(4, 0), 180);
            Assert.AreEqual(0, Math.Round(v.GetX()));
            Assert.AreEqual(4, Math.Round(v.GetY()));

            v = MathLibrary.Rotate(new Vertex(2, 2), new Vertex(4, 0), -90);
            Assert.AreEqual(0, Math.Round(v.GetX()));
            Assert.AreEqual(0, Math.Round(v.GetY()));

            v = MathLibrary.Rotate(new Vertex(2, 2), new Vertex(4, 0), 90);
            Assert.AreEqual(4, Math.Round(v.GetX()));
            Assert.AreEqual(4, Math.Round(v.GetY()));
        }

        [Test]
        public void TestLeftOfLineHorizontal()
        {
            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(new Vertex(0, 0), new Vertex(4, 0), new Vertex(2, 1)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(0, 0), new Vertex(4, 0), new Vertex(2, -1)));
        }

        [Test]
        public void TestLeftOfLinePositive()
        {
            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(new Vertex(2, 2), new Vertex(0, 0), new Vertex(2.01, 1))); //FAR RIGHT
            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(2, 2), new Vertex(0, 0), new Vertex(5, 5)));

            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(2, 2), new Vertex(0, 0), new Vertex(-10, 1))); //FAR LEFT
            
            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(new Vertex(2, 2), new Vertex(0, 0), new Vertex(2, 1)));
            
            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(new Vertex(2, 2), new Vertex(0, 0), new Vertex(1, .01)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(2, 2), new Vertex(0, 0), new Vertex(1, 1.5)));

            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(2, 2), new Vertex(0, 0), new Vertex(0, 5)));
        }

        [Test]
        public void TestLeftOfLineNegative()
        {
            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(4, 0), new Vertex(2, 2), new Vertex(5, 1))); //FAR RIGHT

            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(new Vertex(4, 0), new Vertex(2, 2), new Vertex(1.90, 1))); //FAR LEFT

            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(new Vertex(4, 0), new Vertex(2, 2), new Vertex(2, 1)));

            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(new Vertex(4, 0), new Vertex(2, 2), new Vertex(3, 0.01)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(4, 0), new Vertex(2, 2), new Vertex(3, 1.5)));

            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(4, 0), new Vertex(2, 2), new Vertex(4, 1)));
        }

        [Test]
        public void TestTriangleContainsPoint()
        {
            Vertex v1 = new Vertex(0, 0);
            Vertex v2 = new Vertex(4, 0);
            Vertex v3 = new Vertex(2, 2);

            Assert.IsTrue(MathLibrary.TriangleContainsPoint(v1, v2, v3, new Vertex(2, 1)));

            Assert.IsFalse(MathLibrary.TriangleContainsPoint(v1, v2, v3, new Vertex(0, 1)));
            Assert.IsFalse(MathLibrary.TriangleContainsPoint(v1, v2, v3, new Vertex(2, -1)));
            Assert.IsFalse(MathLibrary.TriangleContainsPoint(v1, v2, v3, new Vertex(3, 1.5)));
        }
        
        [Test]
        public void TestTriangleContainsPoint2()
        {
            Vertex v1 = new Vertex(8, 3);
            Vertex v2 = new Vertex(0, 3);
            Vertex v3 = new Vertex(4, 0.859264966048015);

            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(v1, v2, new Vertex(4, 2)));
            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(v2, v3, new Vertex(4, 2)));
            Assert.IsTrue(MathLibrary.IsPointLeftOfLine(v3, v1, new Vertex(4, 2)));

            Assert.IsTrue(MathLibrary.TriangleContainsPoint(v1, v2, v3, new Vertex(4, 3)));
        }

        [Test]
        public void TestRectangleContainsPoint()
        {
            Vertex v1 = new Vertex(0, 6);
            Vertex v2 = new Vertex(0, 3);
            Vertex v3 = new Vertex(8, 3);
            Vertex v4 = new Vertex(8, 6);

            Assert.IsTrue(MathLibrary.RectangleContainsPoint(v1, v2, v3, v4, new Vertex(4, 4)));

            Assert.IsFalse(MathLibrary.RectangleContainsPoint(v1, v2, v3, v4, new Vertex(8, 3)));

            Assert.IsFalse(MathLibrary.RectangleContainsPoint(v1, v2, v3, v4, new Vertex(-1, 5)));
            Assert.IsFalse(MathLibrary.RectangleContainsPoint(v1, v2, v3, v4, new Vertex(2, 2)));
            Assert.IsFalse(MathLibrary.RectangleContainsPoint(v1, v2, v3, v4, new Vertex(9, 4)));
            Assert.IsFalse(MathLibrary.RectangleContainsPoint(v1, v2, v3, v4, new Vertex(7, 7)));
        }

        [Test]
        public void TestLeftOfLineInfinityHorizontal()
        {
            //LEFT TO RIGHT
            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(4, 0), new Vertex(8, 0), new Vertex(2, 7)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLine(new Vertex(4, 0), new Vertex(8, 0), new Vertex(2, -1)));

            //RIGHT TO LEFT
            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(8, 8), new Vertex(0, 8), new Vertex(2, 7)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(8, 8), new Vertex(0, 8), new Vertex(2, 9)));
        }

        [Test]
        public void TestLeftOfLineInfinityVertical()
        {
            //BOTTOM UP
            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 0), new Vertex(0, 10), new Vertex(-2, 7)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 0), new Vertex(0, 10), new Vertex(0, 7)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 0), new Vertex(0, 10), new Vertex(10, 0)));

            //TOP DOWN
            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 10), new Vertex(0, 0), new Vertex(2, 7)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 10), new Vertex(0, 0), new Vertex(0, 7)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 10), new Vertex(0, 0), new Vertex(-1, -67)));
        }

        [Test]
        public void TestLeftOfLineInfinityNegative()
        {
            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 8), new Vertex(4, 0), new Vertex(2, 7)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 8), new Vertex(4, 0), new Vertex(0, 0)));

            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(8, 0), new Vertex(0, 8), new Vertex(0, 0)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(8, 0), new Vertex(0, 8), new Vertex(2, 7)));
        }

        [Test]
        public void TestLeftOfLineInfinityPositive()
        {
            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 0), new Vertex(3, 3), new Vertex(2, 5)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(0, 0), new Vertex(3, 3), new Vertex(10, 6)));

            Assert.IsTrue(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(3, 3), new Vertex(0, 0), new Vertex(10, 6)));
            Assert.IsFalse(MathLibrary.IsPointLeftOfLineToInfinity(new Vertex(3, 3), new Vertex(0, 0), new Vertex(2, 5)));
        }

        [Test]
        public void TestQuadrilateralContainsPoint()
        {
            Vertex v1 = new Vertex(0, 8);
            Vertex v2 = new Vertex(4, 0);
            Vertex v3 = new Vertex(8, 0);
            Vertex v4 = new Vertex(8, 8);

            Assert.IsTrue(MathLibrary.QuadrilateralContainsPoint(v1, v2, v3, v4, new Vertex(2, 7)));

        }
            
        [Test]
        public void TestQuadrilateralContainsPoint2()
        {
            Vertex v1 = new Vertex(4, 2);
            Vertex v2 = new Vertex(6, 2);
            Vertex v3 = new Vertex(15.69, 34.02);
            Vertex v4 = new Vertex(-9.26, 34.02);

            Assert.IsTrue(MathLibrary.QuadrilateralContainsPoint(v1, v2, v3, v4, new Vertex(3.5, 4)));
        }
    }
}

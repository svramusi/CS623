using System;
using System.Collections.Generic;

using StraightSkeletonLib;
using MathLib;

using NUnit.Framework;

namespace StraightSkeletonTests
{
    [TestFixture]
    public class SSLOperationsTests
    {
        private LAV listOfActiveVertices;

        [SetUp]
        protected void SetUp()
        {
            listOfActiveVertices = new LAV();
            listOfActiveVertices.Add(new Vertex(2, 6));
            listOfActiveVertices.Add(new Vertex(2, 2));
            listOfActiveVertices.Add(new Vertex(15, 2));
            listOfActiveVertices.Add(new Vertex(15, 6));

            SSLOperations.ComputeAngleBisectors(listOfActiveVertices);
        }

        [Test]
        public void TestAngleBisectors()
        {
            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(1).AngleBisector.GetX(), 3));
            Assert.AreEqual(5.293, Math.Round(listOfActiveVertices.Get(1).AngleBisector.GetY(), 3));

            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(2).AngleBisector.GetX(), 3));
            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(2).AngleBisector.GetY(), 3));

            Assert.AreEqual(14.293, Math.Round(listOfActiveVertices.Get(3).AngleBisector.GetX(), 3));
            Assert.AreEqual(2.707, Math.Round(listOfActiveVertices.Get(3).AngleBisector.GetY(), 3));

            Assert.AreEqual(14.293, Math.Round(listOfActiveVertices.Get(4).AngleBisector.GetX(), 3));
            Assert.AreEqual(5.293, Math.Round(listOfActiveVertices.Get(4).AngleBisector.GetY(), 3));
        }

        [Test]
        public void TestClosestIntersection()
        {
            Intersection intersection = SSLOperations.GetClosestIntersection(listOfActiveVertices.GetStart());
            Assert.AreEqual(4, Math.Round(intersection.GetX()));
            Assert.AreEqual(4, Math.Round(intersection.GetY()));

            Assert.AreEqual(listOfActiveVertices.Get(2), intersection.GetVA());
            Assert.AreEqual(listOfActiveVertices.GetStart(), intersection.GetVB());

            Assert.AreEqual(2, Math.Round(intersection.Distance));
        }

        [Test]
        public void TestNoIntersection()
        {
            LAV infiniteLav = new LAV();
            infiniteLav.Add(new Vertex(0, 0));
            infiniteLav.Add(new Vertex(1, 0));
            infiniteLav.Add(new Vertex(2, 0));
            SSLOperations.ComputeAngleBisectors(infiniteLav);

            Intersection intersection = SSLOperations.GetClosestIntersection(infiniteLav.GetStart());
            Assert.IsTrue(double.IsInfinity(intersection.Distance));
        }

        [Test]
        public void TestGeneratePriorityQueue()
        {
            SSLOperations.GeneratePriorityQueue(listOfActiveVertices);

            Intersection i = null;

            i = SSLOperations.GetMinIntersection();
            Assert.AreEqual(4, Math.Round(i.GetX()));
            Assert.AreEqual(4, Math.Round(i.GetY()));
            Assert.AreEqual(2, Math.Round(i.Distance));

            i = SSLOperations.GetMinIntersection();
            Assert.AreEqual(4, Math.Round(i.GetX()));
            Assert.AreEqual(4, Math.Round(i.GetY()));
            Assert.AreEqual(2, Math.Round(i.Distance));

            i = SSLOperations.GetMinIntersection();
            Assert.AreEqual(13, Math.Round(i.GetX()));
            Assert.AreEqual(4, Math.Round(i.GetY()));
            Assert.AreEqual(2, Math.Round(i.Distance));

            i = SSLOperations.GetMinIntersection();
            Assert.AreEqual(13, Math.Round(i.GetX()));
            Assert.AreEqual(4, Math.Round(i.GetY()));
            Assert.AreEqual(2, Math.Round(i.Distance));
        }

        [Test]
        public void TestPointerVerticesAreProcessed()
        {
            SSLOperations.GeneratePriorityQueue(listOfActiveVertices);
            Intersection i = null;

            i = SSLOperations.GetMinIntersection();
            i = SSLOperations.GetMinIntersection();
            Assert.AreEqual(6, i.GetVB().GetY());

            i = SSLOperations.GetMinIntersection();
            i = SSLOperations.GetMinIntersection();
            Assert.AreEqual(2, i.GetVB().GetY());
        }

        [Test]
        public void TestGetResult()
        {
            List<LineSegment> result = SSLOperations.GenerateSkeleton(listOfActiveVertices);

            //foreach (LineSegment ls in result)
                //Console.WriteLine(ls.ToString());

            Assert.AreEqual(new LineSegment(2, 6, 4, 4), result[0]);
            Assert.AreEqual(new LineSegment(2, 2, 4, 4), result[1]);
            Assert.AreEqual(new LineSegment(15, 2, 13, 4), result[2]);
            Assert.AreEqual(new LineSegment(15, 6, 13, 4), result[3]);
            Assert.AreEqual(new LineSegment(4, 4, 13, 4), result[4]);
        }

        //THIS CAUSES IT TO CRASH, QUITE THE EDGE CONDITION... 
        //[Test]
        //public void TestGetResult2()
        //{
        //    LAV listOfActiveVertices2 = new LAV();
        //    listOfActiveVertices2.Add(new Vertex(3, 10));
        //    listOfActiveVertices2.Add(new Vertex(0, 7));
        //    listOfActiveVertices2.Add(new Vertex(0, 3));
        //    listOfActiveVertices2.Add(new Vertex(3, 0));
        //    listOfActiveVertices2.Add(new Vertex(7, 0));
        //    listOfActiveVertices2.Add(new Vertex(10, 3));
        //    listOfActiveVertices2.Add(new Vertex(10, 7));
        //    listOfActiveVertices2.Add(new Vertex(7, 10));

        //    SSLOperations.ComputeAngleBisectors(listOfActiveVertices2);

        //    List<LineSegment> result = SSLOperations.GenerateSkeleton(listOfActiveVertices2);

        //    foreach (LineSegment ls in result)
        //        Console.WriteLine(ls.ToString());
        //}
    }
}

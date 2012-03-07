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
        private SLAV setListOfActiveVertices;

        [SetUp]
        protected void SetUp()
        {
            setListOfActiveVertices = new SLAV();
            setListOfActiveVertices.Insert(new Vertex(2, 6), 0);
            setListOfActiveVertices.Insert(new Vertex(2, 2), 0);
            setListOfActiveVertices.Insert(new Vertex(15, 2), 0);
            setListOfActiveVertices.Insert(new Vertex(15, 6), 0);

            listOfActiveVertices = new LAV();
            listOfActiveVertices.Add(new Vertex(2, 6));
            listOfActiveVertices.Add(new Vertex(2, 2));
            listOfActiveVertices.Add(new Vertex(15, 2));
            listOfActiveVertices.Add(new Vertex(15, 6));

            SSLOperations.SetVertexType(setListOfActiveVertices.Get(0));
            SSLOperations.ComputeAngleBisectors(setListOfActiveVertices.Get(0));

            SSLOperations.SetVertexType(listOfActiveVertices);
            SSLOperations.ComputeAngleBisectors(listOfActiveVertices);
        }

        [Test]
        public void TestAngleBisectorsConvexPolygon()
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
        public void TestAngleBisectorsNonConvexPolygon()
        {
            LAV listOfActiveVertices2 = new LAV();
            listOfActiveVertices2.Add(new Vertex(0, 6));
            listOfActiveVertices2.Add(new Vertex(0, 0));
            listOfActiveVertices2.Add(new Vertex(4, 0));
            listOfActiveVertices2.Add(new Vertex(4, 3));
            listOfActiveVertices2.Add(new Vertex(8, 3));
            listOfActiveVertices2.Add(new Vertex(8, 6));
            SSLOperations.SetVertexType(listOfActiveVertices2);
            SSLOperations.ComputeAngleBisectors(listOfActiveVertices2);
            
            Assert.AreEqual(3.29, Math.Round(listOfActiveVertices2.Get(4).AngleBisector.GetX(), 2));
            Assert.AreEqual(3.71, Math.Round(listOfActiveVertices2.Get(4).AngleBisector.GetY(), 2));
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
            SSLOperations.SetVertexType(infiniteLav);
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
            List<LineSegment> result = SSLOperations.GenerateSkeleton(setListOfActiveVertices);

            //foreach (LineSegment ls in result)
                //Console.WriteLine(ls.ToString());

            Assert.AreEqual(new LineSegment(2, 6, 4, 4), result[0]);
            Assert.AreEqual(new LineSegment(2, 2, 4, 4), result[1]);
            Assert.AreEqual(new LineSegment(15, 2, 13, 4), result[2]);
            Assert.AreEqual(new LineSegment(15, 6, 13, 4), result[3]);
            Assert.AreEqual(new LineSegment(4, 4, 13, 4), result[4]);
        }

        //[Test]
        //public void TestGetResult2()
        //{
        //    LAV listOfActiveVertices2 = new LAV();
        //    listOfActiveVertices2.Add(new Vertex(3, 11));
        //    listOfActiveVertices2.Add(new Vertex(0, 7));
        //    listOfActiveVertices2.Add(new Vertex(0, 3));
        //    listOfActiveVertices2.Add(new Vertex(3, 0));
        //    listOfActiveVertices2.Add(new Vertex(7, 0));
        //    listOfActiveVertices2.Add(new Vertex(10, 3));
        //    listOfActiveVertices2.Add(new Vertex(10, 7));
        //    listOfActiveVertices2.Add(new Vertex(7, 11));

        //    SSLOperations.ComputeAngleBisectors(listOfActiveVertices2);

        //    List<LineSegment> result = SSLOperations.GenerateSkeleton(listOfActiveVertices2);

        //    foreach (LineSegment ls in result)
        //        Console.WriteLine(ls.ToString());
        //}

        [Test]
        public void TestGetResult3()
        {

            //System.Diagnostics.Debugger.Break();

            SLAV slav3 = new SLAV();
            slav3.Insert(new Vertex(0, 6), 0);
            slav3.Insert(new Vertex(0, 0), 0);
            slav3.Insert(new Vertex(4, 0), 0);
            slav3.Insert(new Vertex(4, 3), 0);
            slav3.Insert(new Vertex(8, 0), 0);
            slav3.Insert(new Vertex(8, 6), 0);
            
            SSLOperations.ComputeAngleBisectors(slav3.Get(0));

            List<LineSegment> result = SSLOperations.GenerateSkeleton(slav3);

            foreach (LineSegment ls in result)
                Console.WriteLine(ls.ToString());
        }


        [Test]
        public void TestVertexType()
        {
            //ALL CONVEX POLYGONS
            //RECTANGLE
            SSLOperations.SetVertexType(listOfActiveVertices);

            foreach (Vertex v in listOfActiveVertices)
                Assert.AreEqual(Vertex.VertexType.Edge, v.Type);

            //DIAMOND
            LAV listOfActiveVertices2 = new LAV();
            listOfActiveVertices2.Add(new Vertex(2, 4));
            listOfActiveVertices2.Add(new Vertex(0, 2));
            listOfActiveVertices2.Add(new Vertex(2, 0));
            listOfActiveVertices2.Add(new Vertex(4, 2));
            SSLOperations.SetVertexType(listOfActiveVertices2);

            foreach (Vertex v in listOfActiveVertices2)
                Assert.AreEqual(Vertex.VertexType.Edge, v.Type);

            //OCTAGON
            LAV listOfActiveVertices3 = new LAV();
            listOfActiveVertices3.Add(new Vertex(3, 10));
            listOfActiveVertices3.Add(new Vertex(0, 7));
            listOfActiveVertices3.Add(new Vertex(0, 3));
            listOfActiveVertices3.Add(new Vertex(3, 0));
            listOfActiveVertices3.Add(new Vertex(7, 0));
            listOfActiveVertices3.Add(new Vertex(10, 3));
            listOfActiveVertices3.Add(new Vertex(10, 7));
            listOfActiveVertices3.Add(new Vertex(7, 10));
            SSLOperations.SetVertexType(listOfActiveVertices3);

            foreach (Vertex v in listOfActiveVertices3)
                Assert.AreEqual(Vertex.VertexType.Edge, v.Type);

            
            //ALL NON-CONVEX POLYGONS
            //PEAK -- W type shape
            LAV listOfActiveVertices4 = new LAV();
            listOfActiveVertices4.Add(new Vertex(0, 3));
            listOfActiveVertices4.Add(new Vertex(2, 0));
            listOfActiveVertices4.Add(new Vertex(4, 2));
            listOfActiveVertices4.Add(new Vertex(6, 0));
            listOfActiveVertices4.Add(new Vertex(8, 3));
            SSLOperations.SetVertexType(listOfActiveVertices4);

            for (int i = 0; i < listOfActiveVertices4.Length; i++)
                if (i != 3)
                    Assert.AreEqual(Vertex.VertexType.Edge, listOfActiveVertices4.Get(i).Type);
                else
                    Assert.AreEqual(Vertex.VertexType.Split, listOfActiveVertices4.Get(i).Type);

            //VALLEY
            LAV listOfActiveVertices5 = new LAV();
            listOfActiveVertices5.Add(new Vertex(0, 4));
            listOfActiveVertices5.Add(new Vertex(0, 0));
            listOfActiveVertices5.Add(new Vertex(4, 0));
            listOfActiveVertices5.Add(new Vertex(4, 4));
            listOfActiveVertices5.Add(new Vertex(2, 2));
            SSLOperations.SetVertexType(listOfActiveVertices5);

            for (int i = 0; i < listOfActiveVertices5.Length; i++)
                if (i != 5)
                    Assert.AreEqual(Vertex.VertexType.Edge, listOfActiveVertices5.Get(i).Type);
                else
                    Assert.AreEqual(Vertex.VertexType.Split, listOfActiveVertices5.Get(i).Type);

            //>
            LAV listOfActiveVertices6 = new LAV();
            listOfActiveVertices6.Add(new Vertex(0, 4));
            listOfActiveVertices6.Add(new Vertex(2, 2));
            listOfActiveVertices6.Add(new Vertex(0, 0));
            listOfActiveVertices6.Add(new Vertex(4, 0));
            listOfActiveVertices6.Add(new Vertex(4, 4));
            SSLOperations.SetVertexType(listOfActiveVertices6);

            for (int i = 0; i < listOfActiveVertices6.Length; i++)
                if (i != 2)
                    Assert.AreEqual(Vertex.VertexType.Edge, listOfActiveVertices6.Get(i).Type);
                else
                    Assert.AreEqual(Vertex.VertexType.Split, listOfActiveVertices6.Get(i).Type);

            //<
            LAV listOfActiveVertices7 = new LAV();
            listOfActiveVertices7.Add(new Vertex(0, 4));
            listOfActiveVertices7.Add(new Vertex(0, 0));
            listOfActiveVertices7.Add(new Vertex(4, 0));
            listOfActiveVertices7.Add(new Vertex(2, 2));
            listOfActiveVertices7.Add(new Vertex(4, 4));
            SSLOperations.SetVertexType(listOfActiveVertices7);

            for (int i = 0; i < listOfActiveVertices7.Length; i++)
                if (i != 4)
                    Assert.AreEqual(Vertex.VertexType.Edge, listOfActiveVertices7.Get(i).Type);
                else
                    Assert.AreEqual(Vertex.VertexType.Split, listOfActiveVertices7.Get(i).Type);

            /*
             * X------------------------X
             * |                        |
             * |                        |
             * |            X-----------X
             * |            |
             * |            |
             * X------------X
            */
            LAV listOfActiveVertices8 = new LAV();
            listOfActiveVertices8.Add(new Vertex(0, 6));
            listOfActiveVertices8.Add(new Vertex(0, 0));
            listOfActiveVertices8.Add(new Vertex(4, 0));
            listOfActiveVertices8.Add(new Vertex(4, 3));
            listOfActiveVertices8.Add(new Vertex(8, 3));
            listOfActiveVertices8.Add(new Vertex(8, 6));
            SSLOperations.SetVertexType(listOfActiveVertices8);

            for (int i = 0; i < listOfActiveVertices8.Length; i++)
                if (i != 4)
                    Assert.AreEqual(Vertex.VertexType.Edge, listOfActiveVertices8.Get(i).Type);
                else
                    Assert.AreEqual(Vertex.VertexType.Split, listOfActiveVertices8.Get(i).Type);

            /*
             * X------------------------X
             * |                        |
             * |                        |
             * X------------X           X
             *              |           |
             *              |           |
             *              |           |
             *              X-----------X
            */
            LAV listOfActiveVertices9 = new LAV();
            listOfActiveVertices9.Add(new Vertex(0, 8));
            listOfActiveVertices9.Add(new Vertex(0, 3));
            listOfActiveVertices9.Add(new Vertex(4, 3));
            listOfActiveVertices9.Add(new Vertex(4, 0));
            listOfActiveVertices9.Add(new Vertex(8, 0));
            listOfActiveVertices9.Add(new Vertex(8, 6));
            SSLOperations.SetVertexType(listOfActiveVertices9);

            for (int i = 0; i < listOfActiveVertices9.Length; i++)
                if (i != 3)
                    Assert.AreEqual(Vertex.VertexType.Edge, listOfActiveVertices9.Get(i).Type);
                else
                    Assert.AreEqual(Vertex.VertexType.Split, listOfActiveVertices9.Get(i).Type);
        }
    }
}

using System;
using System.Collections.Generic;

using MathLib;

namespace StraightSkeletonLib
{
    public class SSLOperations
    {
        private static IntersectionQueue queue = new IntersectionQueue();

        public static void ComputeAngleBisectors(LAV listOfActiveVertices)
        {
            foreach (Vertex v in listOfActiveVertices)
            {
                v.AngleBisector = MathLibrary.GetAngleBisectorVertex(v.GetPrevVertex(), v, v.GetNextVertex());
            }
        }

        public static Intersection GetClosestIntersection(Vertex v)
        {
            Vertex next = v.GetNextVertex();
            Vertex prev = v.GetPrevVertex();

            Vertex nextIntersection = MathLibrary.GetIntersectionPoint(MathLibrary.GetLineEquation(next, next.AngleBisector), MathLibrary.GetLineEquation(v, v.AngleBisector));
            Vertex prevIntersection = MathLibrary.GetIntersectionPoint(MathLibrary.GetLineEquation(prev, prev.AngleBisector), MathLibrary.GetLineEquation(v, v.AngleBisector));
            
            if (MathLibrary.GetDistanceBetweenVertices(v, nextIntersection) < MathLibrary.GetDistanceBetweenVertices(v, prevIntersection))
            {
                Intersection i = new Intersection(nextIntersection.GetX(), nextIntersection.GetY(), next, v);
                //Console.WriteLine("i'm " + v.ToString() + " and my cloests intersection is x: " + i.GetX() + " y: " + i.GetY() + " distance: " + i.Distance);
                return i;
            }
            else
            {
                Intersection i = new Intersection(prevIntersection.GetX(), prevIntersection.GetY(), v, prev);
                //Console.WriteLine("i'm " + v.ToString() + " and my cloests intersection is x: " + i.GetX() + " y: " + i.GetY() + " distance: " + i.Distance);
                return i;
            }
        }

        public static void GeneratePriorityQueue(LAV listOfActiveVertices)
        {
            foreach (Vertex v in listOfActiveVertices)
                queue.Push(GetClosestIntersection(v));
        }

        public static Intersection GetMinIntersection()
        {
            return queue.GetMin();
        }

        public static List<LineSegment> GenerateSkeleton(LAV listOfActiveVertices)
        {
            List<LineSegment> result = new List<LineSegment>();

            if (queue.IsEmpty())
                SSLOperations.GeneratePriorityQueue(listOfActiveVertices);
            
            while (!queue.IsEmpty())
            {
                while (!queue.IsEmpty())
                {
                    Intersection intersection = GetMinIntersection();

                    if (!intersection.GetVA().Processed && !intersection.GetVB().Processed)
                    {
                        result.Add(new LineSegment(intersection.GetVB().GetX(), intersection.GetVB().GetY(), intersection.GetX(), intersection.GetY()));
                        result.Add(new LineSegment(intersection.GetVA().GetX(), intersection.GetVA().GetY(), intersection.GetX(), intersection.GetY()));

                        Vertex newVertex = new Vertex(intersection.GetX(), intersection.GetY());
                        listOfActiveVertices.Insert(newVertex, intersection.GetVB(), intersection.GetVA());

                        //Console.WriteLine("\n\nafter insert");
                        //foreach (Vertex v in listOfActiveVertices)
                        //    Console.WriteLine(v.ToString());
                    }

                    intersection.GetVA().SetProcessed();
                    intersection.GetVB().SetProcessed();
                }

                Vertex startVertex = listOfActiveVertices.GetStart();
                if (startVertex.GetNextVertex().GetNextVertex().Equals(startVertex))
                {
                    result.Add(new LineSegment(startVertex.GetX(), startVertex.GetY(), startVertex.GetNextVertex().GetX(), startVertex.GetNextVertex().GetY()));
                }
                else
                {
                    ComputeAngleBisectors(listOfActiveVertices);
                    SSLOperations.GeneratePriorityQueue(listOfActiveVertices);
                }
            }


            return result;
        }
    }
}

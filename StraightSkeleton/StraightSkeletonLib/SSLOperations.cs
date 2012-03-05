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

            if (MathLibrary.GetDistanceBetweenLineAndVertex(v, next, nextIntersection) < MathLibrary.GetDistanceBetweenLineAndVertex(v, prev, prevIntersection))
            {
                Intersection i = new Intersection(nextIntersection.GetX(), nextIntersection.GetY(), next, v, v.Type);
                //Console.WriteLine("i'm " + v.ToString() + " and my cloests intersection is x: " + i.GetX() + " y: " + i.GetY() + " distance: " + i.Distance);
                return i;
            }
            else
            {
                Intersection i = new Intersection(prevIntersection.GetX(), prevIntersection.GetY(), v, prev, v.Type);
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

                    if (intersection.Type == Vertex.VertexType.Undefined)
                    {
                        throw new Exception("Undefined vertex type.");
                    }

                    if (!intersection.GetVA().Processed && !intersection.GetVB().Processed)
                    {
                        if (intersection.Type == Vertex.VertexType.Edge)
                        {
                            result.Add(new LineSegment(intersection.GetVB().GetX(), intersection.GetVB().GetY(), intersection.GetX(), intersection.GetY()));
                            result.Add(new LineSegment(intersection.GetVA().GetX(), intersection.GetVA().GetY(), intersection.GetX(), intersection.GetY()));

                            Vertex newVertex = new Vertex(intersection.GetX(), intersection.GetY());
                            listOfActiveVertices.Insert(newVertex, intersection.GetVB(), intersection.GetVA());

                            //Console.WriteLine("\n\nafter insert");
                            //foreach (Vertex v in listOfActiveVertices)
                            //    Console.WriteLine(v.ToString());
                        }
                        else
                        {
                        }
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

        public static void SetVertexType(LAV listOfActiveVertices)
        {
            Vertex next = null;
            Vertex prev = null;

            double nextX = 0;
            double nextY = 0;

            double prevX = 0;
            double prevY = 0;

            double myX = 0;
            double myY = 0;

            foreach (Vertex v in listOfActiveVertices)
            {
                myX = v.GetX();
                myY = v.GetY();

                next = v.GetNextVertex();
                prev = v.GetPrevVertex();

                nextX = next.GetX();
                nextY = next.GetY();

                prevX = prev.GetX();
                prevY = prev.GetY();

                if (nextX == myX)
                {
                    nextX += 0.01;
                }

                if (nextY == myY)
                {
                    nextY -= 0.01;
                }

                if (prevX == myX)
                {
                    prevX -= 0.01;
                }

                if (prevY == myY)
                {
                    prevY -= 0.01;
                }

                if ((prevY < myY && prevX < myX) && (nextY < myY && myX < nextX)) //PEAK 
                {
                    v.Type = Vertex.VertexType.Split;
                }
                else if ((prevY > myY && myX < prevX) && (nextY > myY && nextX < myX)) //VALLEY
                {
                    v.Type = Vertex.VertexType.Split;
                }
                else if ((prevY > myY && myX > prevX) && (myY > nextY && myX > nextX)) //>
                {
                    v.Type = Vertex.VertexType.Split;
                }
                else if ((prevY < myY && prevX > myX) && (myY < nextY && nextX > myX)) //<
                {
                    v.Type = Vertex.VertexType.Split;
                }
                else
                {
                    v.Type = Vertex.VertexType.Edge;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace StraightSkeletonLib
{
    public class SSLOperations
    {
        private static IntersectionQueue queue = new IntersectionQueue();

        public static void ComputeAngleBisectors(LAV listOfActiveVertices)
        {
            foreach (Vertex v in listOfActiveVertices)
            {
                v.AngleBisector = MathLibrary.GetAngleBisectorVertex(v.PrevLineSegment, v.NextLineSegment);

                if (v.AngleBisector == null)
                {
                    v.AngleBisector = new Vertex(double.PositiveInfinity, double.PositiveInfinity);
                }
                else
                {
                    if (v.Type == Vertex.VertexType.Split)
                    {
                        v.AngleBisector = MathLibrary.Rotate(v, v.AngleBisector, 180);
                    }
                }
            }
        }

        public static Intersection GetClosestIntersection(Vertex v)
        {
            Vertex next = v.GetNextVertex();
            Vertex prev = v.GetPrevVertex();

            Vertex nextIntersection = MathLibrary.GetIntersectionPoint(new LineSegment(next, next.AngleBisector), new LineSegment(v, v.AngleBisector));
            Vertex prevIntersection = MathLibrary.GetIntersectionPoint(new LineSegment(prev, prev.AngleBisector), new LineSegment(v, v.AngleBisector));

            Vertex.VertexType type = Vertex.VertexType.Undefined;

            if (MathLibrary.GetDistanceBetweenLineAndVertex(v, next, nextIntersection) < MathLibrary.GetDistanceBetweenLineAndVertex(v, prev, prevIntersection))
            {
                if (v.Type == Vertex.VertexType.Split || next.Type == Vertex.VertexType.Split)
                    type = Vertex.VertexType.Split;
                else
                    type = Vertex.VertexType.Edge;

                Intersection i = new Intersection(nextIntersection.GetX(), nextIntersection.GetY(), next, v, type, next.NextLineSegment, v.PrevLineSegment);
                return i;
            }
            else
            {
                if (v.Type == Vertex.VertexType.Split || prev.Type == Vertex.VertexType.Split)
                    type = Vertex.VertexType.Split;
                else
                    type = Vertex.VertexType.Edge;

                Intersection i = new Intersection(prevIntersection.GetX(), prevIntersection.GetY(), v, prev, type, prev.PrevLineSegment, v.NextLineSegment);
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

        public static List<LineSegment> GenerateSkeleton(SLAV setListOfActiveVertices)
        {
            List<LineSegment> result = new List<LineSegment>();
            int lavIndex = 0;

            if (queue.IsEmpty())
                SSLOperations.GeneratePriorityQueue(setListOfActiveVertices.Get(lavIndex));

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
                        double intersectionX = Math.Round(intersection.GetX(), 2);
                        double intersectionY = Math.Round(intersection.GetY(), 2);

                        Vertex intersectionVertex = new Vertex(intersectionX, intersectionY);

                        if (intersection.Type == Vertex.VertexType.Edge || intersection.Type == Vertex.VertexType.Split)
                        {
                            result.Add(new LineSegment(intersection.GetVB(), intersectionVertex));
                            result.Add(new LineSegment(intersection.GetVA(), intersectionVertex));

                            setListOfActiveVertices.Get(lavIndex).Insert(intersectionVertex, intersection.GetVB(), intersection.GetVA());

                            intersectionVertex.NextLineSegment = intersection.GetLSVA();
                            intersectionVertex.PrevLineSegment = intersection.GetLSVB();

                            SetVertexType(intersectionVertex);
                            intersectionVertex.AngleBisector = MathLibrary.GetAngleBisectorVertex(intersectionVertex.PrevLineSegment, intersectionVertex.NextLineSegment);

                            if (intersectionVertex.AngleBisector == null) //THE LINES ARE PARALLEL
                            {
                                intersectionVertex.AngleBisector = MathLibrary.Rotate(intersectionVertex, MathLibrary.GetAngleBisectorVertex(new LineSegment(intersectionVertex, intersection.GetVB()), new LineSegment(intersectionVertex, intersection.GetVA())), 180);
                            }
                        }

                        intersection.GetVA().SetProcessed();
                        intersection.GetVB().SetProcessed();
                    }
                }

                Vertex startVertex = setListOfActiveVertices.Get(lavIndex).GetStart();
                if (startVertex.GetNextVertex().GetNextVertex().Equals(startVertex))
                {
                    result.Add(new LineSegment(startVertex, startVertex.GetNextVertex()));
                }
                else
                {
                    SSLOperations.GeneratePriorityQueue(setListOfActiveVertices.Get(lavIndex));
                }
            }

            return result;
        }

        public static void SetVertexType(Vertex v)
        {
            Vertex next = null;
            Vertex prev = null;

            double nextX = 0;
            double nextY = 0;

            double prevX = 0;
            double prevY = 0;

            double myX = 0;
            double myY = 0;

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
        
        public static void SetVertexType(LAV listOfActiveVertices)
        {
            foreach (Vertex v in listOfActiveVertices)
            {
                SetVertexType(v);
            }
        }
    }
}

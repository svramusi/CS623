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
                v.AngleBisector = MathLibrary.GetAngleBisectorVertex(v.GetPrevLineSegment(), v.GetNextLineSegment());

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

                Intersection i = new Intersection(nextIntersection.GetX(), nextIntersection.GetY(), next, v, type, next.GetNextLineSegment(), v.GetPrevLineSegment());
                //Console.WriteLine("i'm " + v.ToString() + " and my cloests intersection is x: " + i.GetX() + " y: " + i.GetY() + " distance: " + i.Distance);
                return i;
            }
            else
            {
                if (v.Type == Vertex.VertexType.Split || prev.Type == Vertex.VertexType.Split)
                    type = Vertex.VertexType.Split;
                else
                    type = Vertex.VertexType.Edge;

                Intersection i = new Intersection(prevIntersection.GetX(), prevIntersection.GetY(), v, prev, type, prev.GetPrevLineSegment(), v.GetNextLineSegment());
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

                            SetVertexType(intersectionVertex);
                            intersectionVertex.AngleBisector = MathLibrary.GetAngleBisectorVertex(intersection.GetVB().GetPrevLineSegment(), intersection.GetVA().GetNextLineSegment());

                            if (intersectionVertex.AngleBisector == null) //THE LINES ARE PARALLEL
                            {
                                intersectionVertex.AngleBisector = MathLibrary.Rotate(intersectionVertex, MathLibrary.GetAngleBisectorVertex(new LineSegment(intersectionVertex, intersection.GetVB()), new LineSegment(intersectionVertex, intersection.GetVA())), 180);
                            }
                        }
                            /*
                        else if (intersection.Type == Vertex.VertexType.Split)
                        {
                            result.Add(new LineSegment(intersection.GetVB(), intersectionVertex));
                        }
                             * */

                        intersection.GetVA().SetProcessed();
                        intersection.GetVB().SetProcessed();
                    }



                    Console.WriteLine(intersection);
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


            /*
            int lavIndex = 0;

            while (lavIndex < setListOfActiveVertices.Count)
            {

                if (queue.IsEmpty())
                    SSLOperations.GeneratePriorityQueue(setListOfActiveVertices.Get(lavIndex));

                /-*
                foreach (Vertex v in setListOfActiveVertices.Get(lavIndex))
                {
                    if (v.Type == Vertex.VertexType.Split)
                        setListOfActiveVertices.BreakAndCreateNew(v, lavIndex);
                }
                 * *-/

                while (!queue.IsEmpty())
                {
                    while (!queue.IsEmpty())
                    {
                        Intersection intersection = GetMinIntersection();

                        if (intersection.Type == Vertex.VertexType.Undefined)
                        {
                            throw new Exception("Undefined vertex type.");
                        }

                        
                        if(intersection.Type == Vertex.VertexType.Split)
                        {
                            if (intersection.GetVA().Type == Vertex.VertexType.Split)
                            {
                                setListOfActiveVertices.BreakAndCreateNew(intersection.GetVA(), lavIndex);
                            }
                            else if (intersection.GetVB().Type == Vertex.VertexType.Split)
                            {
                                setListOfActiveVertices.BreakAndCreateNew(intersection.GetVB(), lavIndex);
                            }
                        }
                        

                        if (!intersection.GetVA().Processed && !intersection.GetVB().Processed)
                        {
                            if (intersection.Type == Vertex.VertexType.Edge)
                            {
                                result.Add(new LineSegment(intersection.GetVB().GetX(), intersection.GetVB().GetY(), intersection.GetX(), intersection.GetY()));
                                result.Add(new LineSegment(intersection.GetVA().GetX(), intersection.GetVA().GetY(), intersection.GetX(), intersection.GetY()));

                                Vertex newVertex = new Vertex(intersection.GetX(), intersection.GetY());
                                setListOfActiveVertices.Get(lavIndex).Insert(newVertex, intersection.GetVB(), intersection.GetVA());

                                SetVertexType(newVertex);
                                newVertex.AngleBisector = MathLibrary.GetAngleBisectorVertex(newVertex.GetPrevVertex(), newVertex, newVertex.GetNextVertex());

                                //Console.WriteLine("\n\nafter insert");
                                //foreach (Vertex v in listOfActiveVertices)
                                //    Console.WriteLine(v.ToString());
                            }

                            intersection.GetVA().SetProcessed();
                            intersection.GetVB().SetProcessed();
                        }

                    }

                    Vertex startVertex = setListOfActiveVertices.Get(lavIndex).GetStart();
                    if (startVertex.GetNextVertex().GetNextVertex().Equals(startVertex))
                    {
                        result.Add(new LineSegment(startVertex.GetX(), startVertex.GetY(), startVertex.GetNextVertex().GetX(), startVertex.GetNextVertex().GetY()));
                    }
                    else
                    {
                        SSLOperations.GeneratePriorityQueue(setListOfActiveVertices.Get(lavIndex));
                    }
                }

                lavIndex++;
            }

            */

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

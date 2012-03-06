using System;

using StraightSkeletonLib;

//SOME MATH CALCULATIONS TAKEN FROM http://people.sc.fsu.edu/~jburkardt/cpp_src/geometry/geometry.cpp 

namespace MathLib
{
    public class MathLibrary
    {
        public static double GetDistanceBetweenVertices(Vertex v1, Vertex v2)
        {  
            double xDistance = Math.Abs(v1.GetX() - v2.GetX());
            double yDistance = Math.Abs(v1.GetY() - v2.GetY());
            return GetDistanceOfVertex(xDistance, yDistance);
        }
      
        //V2 ALWAYS NEEDS TO BE THE MIDDLE VERTEX
        public static double GetAngleBetweenVerticese(Vertex v1, Vertex v2, Vertex v3)
        {
            double a = MathLibrary.GetDistanceBetweenVertices(v1, v2);
            double b = MathLibrary.GetDistanceBetweenVertices(v2, v3);
            double c = MathLibrary.GetDistanceBetweenVertices(v1, v3);

            return RadiansToDegrees(Math.Acos(((a * a) + (b * b) - (c * c)) / (2 * a * b)));
        }

        private static double RadiansToDegrees(double rad)
        {
            return rad * (180.0 / Math.PI);
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        private static double GetDistanceOfVertex(double x, double y)
        {
            return Math.Sqrt((x * x) + (y * y));
        }

        public static Vertex GetAngleBisectorVertex(Vertex prev, Vertex current, Vertex next)
        {
            if (current.Type == Vertex.VertexType.Undefined)
                throw new Exception("Undefined vertex type.");

            double bisectorX = 0;
            double bisectorY = 0;

            double distance12 = GetDistanceBetweenVertices(prev, current);
            double distance32 = GetDistanceBetweenVertices(next, current);

            bisectorX = (prev.GetX() - current.GetX()) / distance12;
            bisectorY = (prev.GetY() - current.GetY()) / distance12;

            bisectorX = bisectorX + ((next.GetX() - current.GetX()) / distance32);
            bisectorY = bisectorY + ((next.GetY() - current.GetY()) / distance32);

            bisectorX = bisectorX * 0.5;
            bisectorY = bisectorY * 0.5;

            double distance = GetDistanceOfVertex(bisectorX, bisectorY);

            bisectorX = current.GetX() + (bisectorX / distance);
            bisectorY = current.GetY() + (bisectorY / distance);

            Vertex bisectorVertex = new Vertex(bisectorX, bisectorY);            
            if (current.Type == Vertex.VertexType.Split)
                bisectorVertex = Rotate(current, bisectorVertex, 180);

            return bisectorVertex;
        }

        public static LineEquation GetLineEquation(Vertex v1, Vertex v2)
        {
            double slope = (v1.GetY() - v2.GetY()) / (v1.GetX() - v2.GetX());
            double yIntercept = v1.GetY() - (slope * v1.GetX());

            return new LineEquation(slope, yIntercept);
        }

        public static Vertex GetIntersectionPoint(LineEquation line1, LineEquation line2)
        {
            double combinedSlope = line1.Slope - line2.Slope;
            double combinedYIntercept = line2.YIntercept - line1.YIntercept;
            
            double xInteresection = combinedYIntercept / combinedSlope;
            double yIntersection = line1.GetY(xInteresection);

            return new Vertex(xInteresection, yIntersection);
        }

        public static double GetDistanceBetweenLineAndVertex(Vertex endPoint1, Vertex endPoint2, Vertex point)
        {
            double bottom = ((endPoint2.GetX() - endPoint1.GetX()) * (endPoint2.GetX() - endPoint1.GetX())) + ((endPoint2.GetY() - endPoint1.GetY()) * (endPoint2.GetY() - endPoint1.GetY()));
            double dot = ((point.GetX() - endPoint1.GetX()) * (endPoint2.GetX() - endPoint1.GetX())) + ((point.GetY() - endPoint1.GetY()) * (endPoint2.GetY() - endPoint1.GetY()));

            double t = dot / bottom;

            double newX = endPoint1.GetX() + t * (endPoint2.GetX() - endPoint1.GetX());
            double newY = endPoint1.GetY() + t * (endPoint2.GetY() - endPoint1.GetY());
            
            double retVal = Math.Sqrt(((point.GetX() - newX) * (point.GetX() - newX)) + ((point.GetY() - newY) * (point.GetY() - newY)));

            if (double.IsNaN(retVal))
                return double.PositiveInfinity;
            else
                return retVal;
        }

        public static Vertex Rotate(Vertex pivotPoint, Vertex point, double deg)
        {            
            double angle = DegreesToRadians(deg);

            double x = pivotPoint.GetX() + Math.Cos(angle) * (point.GetX() - pivotPoint.GetX()) - Math.Sin(angle) * (point.GetY() - pivotPoint.GetY());
            double y = pivotPoint.GetY() + Math.Sin(angle) * (point.GetX() - pivotPoint.GetX()) + Math.Cos(angle) * (point.GetY() - pivotPoint.GetY());

            return new Vertex(x, y);
        }

        public static bool IsPointLeftOfLine(Vertex v1, Vertex v2, Vertex point)
        {
            LineEquation lineEquation = GetLineEquation(v1, v2);
            double lineY = lineEquation.GetY(point.GetX());

            Vertex left = null;
            Vertex right = null;

            if (v1.GetX() < v2.GetX())
            {
                left = v1;
                right = v2;
            }
            else
            {
                left = v2;
                right = v1;
            }
            
            if (lineEquation.Slope < 0) //NEGATIVE
            {
                if (point.GetX() > right.GetX()) //FAR RIGHT
                {
                    return false;
                }
                else if (point.GetX() >= left.GetX() && point.GetX() <= right.GetX()) //BETWEEN
                {
                    if (v1.GetX() < v2.GetX()) //LEFT TO RIGHT
                    {
                        if (point.GetY() >= lineY)
                            return true;
                        else
                            return false;
                    }
                    else //RIGHT TO LEFT
                    {
                        if (point.GetY() <= lineY)
                            return true;
                        else
                            return false;
                    }
                }
                else if (point.GetX() < left.GetX()) // FAR LEFT
                {
                    if (point.GetY() > right.GetY() && point.GetY() < left.GetY())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else if (lineEquation.Slope > 0) //POSITIVE
            {
                if (point.GetX() < left.GetX()) //FAR LEFT
                {
                    return false;
                }
                else if (point.GetX() <= right.GetX() && point.GetX() >= left.GetX()) //BETWEEN
                {
                    if (v2.GetX() < v1.GetX()) //RIGHT TO LEFT
                    {
                        if (point.GetY() <= lineY)
                            return true;
                        else
                            return false;
                    }
                    else //LEFT TO RIGHT
                    {
                        if (point.GetY() >= lineY)
                            return true;
                        else
                            return false;
                    }
                }
                else if (point.GetX() > right.GetX()) //FAR RIGHT
                {
                    if (point.GetY() > left.GetY() && point.GetY() < right.GetY())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else //HORIZONTAL OR VERTICAL
            {
                if (v1.GetX() == v2.GetX()) //VERTICAL
                {
                    if (v2.GetY() > v1.GetY()) //GOING UP
                    {
                        if (point.GetX() < v1.GetX() && point.GetY() > v1.GetY() && point.GetY() < v2.GetY())
                            return true;
                        else
                            return false;
                    }
                    else //GOING DOWN
                    {
                        if (point.GetX() > v1.GetY() && point.GetY() < v1.GetY() && point.GetY() > v2.GetY())
                            return true;
                        else
                            return false;
                    }
                }
                else //HORIZONTAL
                {
                    if (v1.GetX() < v2.GetX()) //LEFT TO RIGHT
                    {
                        if (point.GetY() >= v1.GetY() && point.GetX() > v1.GetX() && point.GetX() < v2.GetX())
                            return true;
                        else
                            return false;
                    }
                    else //RIGHT TO LEFT
                    {
                        if (point.GetY() <= v1.GetY() && point.GetX() < v1.GetX() && point.GetX() > v2.GetX())
                            return true;
                        else
                            return false;
                    }
                }
            }
        }

        //POINTS MUST BE COUNTER CLOCKWISE
        public static bool TriangleContainsPoint(Vertex v1, Vertex v2, Vertex v3, Vertex point)
        {
            bool side1 = IsPointLeftOfLine(v1, v2, point);
            bool side2 = IsPointLeftOfLine(v2, v3, point);
            bool side3 = IsPointLeftOfLine(v3, v1, point);
            
            if (side1 && side2 && side3)
                return true;
            else
                return false;
        }

        //POINTS MUST BE COUNTER CLOCKWISE
        public static bool RectangleContainsPoint(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vertex point)
        {
            bool side1 = IsPointLeftOfLine(v1, v2, point);
            bool side2 = IsPointLeftOfLine(v2, v3, point);
            bool side3 = IsPointLeftOfLine(v3, v4, point);
            bool side4 = IsPointLeftOfLine(v4, v1, point);

            if (side1 && side2 && side3 && side4)
                return true;
            else
                return false;
        }

        public static bool IsPointLeftOfLineToInfinity(Vertex v1, Vertex v2, Vertex point)
        {
            LineEquation lineEquation = GetLineEquation(v1, v2);
            double lineY = lineEquation.GetY(point.GetX());
            
            if (double.IsInfinity(lineEquation.Slope)) //VERTICAL
            {
                if (v1.GetY() < v2.GetY()) //BOTTOM UP
                {
                    if (point.GetX() < v1.GetX())
                        return true;
                    else
                        return false;
                }
                else //TOP DOWN
                {
                    if (point.GetX() > v1.GetX())
                        return true;
                    else
                        return false;
                }
            }
            else if (lineEquation.Slope < 0) //NEGATIVE
            {
                if (v1.GetX() < v2.GetX())
                {
                    if (point.GetY() > lineY)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (point.GetY() < lineY)
                        return true;
                    else
                        return false;
                }
            }
            else if (lineEquation.Slope > 0) //POSITIVE
            {
                if (v1.GetX() < v2.GetX())
                {
                    if (point.GetY() > lineY)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (point.GetY() < lineY)
                        return true;
                    else
                        return false;
                }
            }
            else if (lineEquation.Slope == 0)
            {
                if (v1.GetX() < v2.GetX()) //LEFT TO RIGHT
                {
                    if (point.GetY() > v1.GetY())
                        return true;
                    else
                        return false;
                }
                else //RIGHT TO LEFT
                {
                    if (point.GetY() < v1.GetY())
                        return true;
                    else
                        return false;
                }
            }
            else
                return false;
        }

        //POINTS MUST BE COUNTER CLOCKWISE
        public static bool QuadrilateralContainsPoint(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vertex point)
        {
            bool side1 = IsPointLeftOfLineToInfinity(v1, v2, point);
            bool side2 = IsPointLeftOfLineToInfinity(v2, v3, point);
            bool side3 = IsPointLeftOfLineToInfinity(v3, v4, point);
            bool side4 = IsPointLeftOfLineToInfinity(v4, v1, point);

            if (side1 && side2 && side3 && side4)
                return true;
            else
                return false;
        }
    }
}

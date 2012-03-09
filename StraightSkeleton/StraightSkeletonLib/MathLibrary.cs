using System;

//SOME MATH CALCULATIONS TAKEN FROM http://people.sc.fsu.edu/~jburkardt/cpp_src/geometry/geometry.cpp 

namespace StraightSkeletonLib
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

        public static Vertex GetAngleBisectorVertex(LineSegment ls1, LineSegment ls2)
        {
            //if (ls1.Start.Type == Vertex.VertexType.Undefined || ls1.End.Type == Vertex.VertexType.Undefined || ls2.Start.Type == Vertex.VertexType.Undefined || ls2.Start.Type == Vertex.VertexType.Undefined)
                //throw new Exception("Undefined vertex type.");

            Vertex intersection = GetIntersectionPoint(ls1, ls2);

            //THE LINES ARE PARALLEL
            if (double.IsInfinity(intersection.GetX()) || double.IsNaN(intersection.GetX()) || double.IsInfinity(intersection.GetY()) || double.IsNaN(intersection.GetY()))
                return null;

            //ls1 = prev
            //ls2 = next
            //intersection = current

            Vertex maxLine1;
            Vertex maxLine2;

            if (GetDistanceBetweenVertices(ls1.Start, intersection) >= GetDistanceBetweenVertices(ls1.End, intersection))
                maxLine1 = ls1.Start;
            else
                maxLine1 = ls1.End;

            if (GetDistanceBetweenVertices(ls2.Start, intersection) >= GetDistanceBetweenVertices(ls2.End, intersection))
                maxLine2 = ls2.Start;
            else
                maxLine2 = ls2.End;


            double bisectorX = 0;
            double bisectorY = 0;

            double distance12 = GetDistanceBetweenVertices(maxLine1, intersection);
            double distance32 = GetDistanceBetweenVertices(maxLine2, intersection);

            bisectorX = (maxLine1.GetX() - intersection.GetX()) / distance12;
            bisectorY = (maxLine1.GetY() - intersection.GetY()) / distance12;

            bisectorX = bisectorX + ((maxLine2.GetX() - intersection.GetX()) / distance32);
            bisectorY = bisectorY + ((maxLine2.GetY() - intersection.GetY()) / distance32);

            bisectorX = bisectorX * 0.5;
            bisectorY = bisectorY * 0.5;

            double distance = GetDistanceOfVertex(bisectorX, bisectorY);

            bisectorX = intersection.GetX() + (bisectorX / distance);
            bisectorY = intersection.GetY() + (bisectorY / distance);

            Vertex bisectorVertex = new Vertex(bisectorX, bisectorY);
            
            //TAKE CARE OF THIS LATER -- THERE IS NO WAY TO DETERMINE SPLITS BASED ON LINE SEGMENTS
            //if (current.Type == Vertex.VertexType.Split)
                //bisectorVertex = Rotate(current, bisectorVertex, 180);

            return bisectorVertex;
        }

        /*
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
        */        

        public static LineEquation GetLineEquation(Vertex v1, Vertex v2)
        {
            double slope = (v1.GetY() - v2.GetY()) / (v1.GetX() - v2.GetX());
            double yIntercept = v1.GetY() - (slope * v1.GetX());

            return new LineEquation(slope, yIntercept);
        }

        public static Vertex GetIntersectionPoint(LineSegment ls1, LineSegment ls2)
        {
            LineEquation le1 = GetLineEquation(ls1.Start, ls1.End);
            LineEquation le2 = GetLineEquation(ls2.Start, ls2.End);

            if (double.IsInfinity(le1.Slope)) //VERTICAL LINE
            {
                double x = ls1.Start.GetX();
                return new Vertex(x, le2.GetY(x));
            }
            else if (double.IsInfinity(le2.Slope)) //VERTICAL LINE
            {
                double x = ls2.Start.GetX();
                return new Vertex(x, le1.GetY(x));
            }
            else
            {
                return GetIntersectionPoint(le1, le2);
            }
        }

        public static Vertex GetIntersectionPoint(LineEquation le1, LineEquation le2)
        {
            double combinedSlope = le1.Slope - le2.Slope;
            double combinedYIntercept = le2.YIntercept - le1.YIntercept;

            double xInteresection = combinedYIntercept / combinedSlope;
            double yIntersection = le1.GetY(xInteresection);

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

            double pointX = Math.Round(point.GetX(), 5);
            double pointY = Math.Round(point.GetY(), 5);

            double v1X = Math.Round(v1.GetX(), 5);
            double v1Y = Math.Round(v1.GetY(), 5);

            double v2X = Math.Round(v2.GetX(), 5);
            double v2Y = Math.Round(v2.GetY(), 5);

            double leftX = Math.Round(left.GetX(), 5);
            double leftY= Math.Round(left.GetY(), 5);

            double rightX = Math.Round(right.GetX(), 5);
            double rightY = Math.Round(right.GetY(), 5);
            
            if (lineEquation.Slope < 0) //NEGATIVE
            {
                if (pointX > rightX) //FAR RIGHT
                {
                    return false;
                }
                else if (pointX >= leftX && pointX <= rightX) //BETWEEN
                {
                    if (v1X < v2X) //LEFT TO RIGHT
                    {
                        if (pointY >= lineY)
                            return true;
                        else
                            return false;
                    }
                    else //RIGHT TO LEFT
                    {
                        if (pointY <= lineY)
                            return true;
                        else
                            return false;
                    }
                }
                else if (pointX < leftX) // FAR LEFT
                {
                    if (pointY > rightY && pointY < leftY)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else if (lineEquation.Slope > 0) //POSITIVE
            {
                if (pointX < leftX) //FAR LEFT
                {
                    return false;
                }
                else if (pointX <= rightX && pointX >= leftX) //BETWEEN
                {
                    if (v2X < v1X) //RIGHT TO LEFT
                    {
                        if (pointY <= lineY)
                            return true;
                        else
                            return false;
                    }
                    else //LEFT TO RIGHT
                    {
                        if (pointY >= lineY)
                            return true;
                        else
                            return false;
                    }
                }
                else if (pointX > rightX) //FAR RIGHT
                {
                    if (pointY > leftY && pointY < rightY)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else //HORIZONTAL OR VERTICAL
            {
                if (v1X == v2X) //VERTICAL
                {
                    if (v2Y > v1Y) //GOING UP
                    {
                        if (pointX < v1X && pointY > v1Y && pointY < v2Y)
                            return true;
                        else
                            return false;
                    }
                    else //GOING DOWN
                    {
                        if (pointX > v1Y && pointY < v1Y && pointY > v2Y)
                            return true;
                        else
                            return false;
                    }
                }
                else //HORIZONTAL
                {
                    if (v1X < v2X) //LEFT TO RIGHT
                    {
                        if (pointY >= v1Y && pointX > v1X && pointX < v2X)
                            return true;
                        else
                            return false;
                    }
                    else //RIGHT TO LEFT
                    {
                        if (pointY <= v1Y && pointX < v1X && pointX > v2X)
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
            bool side1 = IsPointLeftOfLineToInfinity(v1, v2, point);
            bool side2 = IsPointLeftOfLineToInfinity(v2, v3, point);
            bool side3 = IsPointLeftOfLineToInfinity(v3, v1, point);
            
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
                    if (point.GetX() <= v1.GetX())
                        return true;
                    else
                        return false;
                }
                else //TOP DOWN
                {
                    if (point.GetX() >= v1.GetX())
                        return true;
                    else
                        return false;
                }
            }
            else if (lineEquation.Slope < 0) //NEGATIVE
            {
                if (v1.GetX() < v2.GetX())
                {
                    if (point.GetY() >= lineY)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (point.GetY() <= lineY)
                        return true;
                    else
                        return false;
                }
            }
            else if (lineEquation.Slope > 0) //POSITIVE
            {
                if (v1.GetX() < v2.GetX())
                {
                    if (point.GetY() >= lineY)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (point.GetY() <= lineY)
                        return true;
                    else
                        return false;
                }
            }
            else if (lineEquation.Slope == 0)
            {
                if (v1.GetX() < v2.GetX()) //LEFT TO RIGHT
                {
                    if (point.GetY() >= v1.GetY())
                        return true;
                    else
                        return false;
                }
                else //RIGHT TO LEFT
                {
                    if (point.GetY() <= v1.GetY())
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

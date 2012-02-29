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

        private static double GetDistanceOfVertex(double x, double y)
        {
            return Math.Sqrt((x * x) + (y * y));
        }

        public static Vertex GetAngleBisectorVertex(Vertex v1, Vertex v2, Vertex v3)
        {
            double bisectorX = 0;
            double bisectorY = 0;

            double distance12 = GetDistanceBetweenVertices(v1, v2);
            double distance32 = GetDistanceBetweenVertices(v3, v2);

            bisectorX = (v1.GetX() - v2.GetX()) / distance12;
            bisectorY = (v1.GetY() - v2.GetY()) / distance12;

            bisectorX = bisectorX + ((v3.GetX() - v2.GetX()) / distance32);
            bisectorY = bisectorY + ((v3.GetY() - v2.GetY()) / distance32);

            bisectorX = bisectorX * 0.5;
            bisectorY = bisectorY * 0.5;

            double distance = GetDistanceOfVertex(bisectorX, bisectorY);

            bisectorX = v2.GetX() + (bisectorX / distance);
            bisectorY = v2.GetY() + (bisectorY / distance);

            return new Vertex(bisectorX, bisectorY);
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
    }
}

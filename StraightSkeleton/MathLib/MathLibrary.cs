using System;

using StraightSkeletonLib;

namespace MathLib
{
    public class MathLibrary
    {
        public static double GetDistanceBetweenVertices(Vertex v1, Vertex v2)
        {
            double xDistance = Math.Abs(v1.GetX() - v2.GetX());
            double yDistance = Math.Abs(v1.GetY() - v2.GetY());
            double cSquared = (xDistance * xDistance) + (yDistance * yDistance);

            return Math.Sqrt(cSquared);
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
    }
}

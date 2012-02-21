using System;

using MathLib;

namespace StraightSkeletonLib
{
    public class SSLOperations
    {
        public static void FindAngleBisectors(LAV listOfActiveVertices)
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
                return new Intersection(nextIntersection.GetX(), nextIntersection.GetY(), next, v);
            else
                return new Intersection(prevIntersection.GetX(), prevIntersection.GetY(), v, prev);
        }
    }
}

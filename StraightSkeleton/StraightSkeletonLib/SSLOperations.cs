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

        public static Vertex GetClosestIntersection(Vertex v)
        {
            Vertex next = v.GetNextVertex();
            Vertex prev = v.GetPrevVertex();

            Vertex nextIntersection = MathLibrary.GetIntersectionPoint(MathLibrary.GetLineEquation(next, next.AngleBisector), MathLibrary.GetLineEquation(v, v.AngleBisector));
            Vertex prevIntersection = MathLibrary.GetIntersectionPoint(MathLibrary.GetLineEquation(prev, prev.AngleBisector), MathLibrary.GetLineEquation(v, v.AngleBisector));

            if (MathLibrary.GetDistanceBetweenVertices(v, nextIntersection) < MathLibrary.GetDistanceBetweenVertices(v, prevIntersection))
                return nextIntersection;
            else
                return prevIntersection;
        }
    }
}

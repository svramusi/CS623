using System;
using System.Collections.Generic;

using MathLib;

namespace StraightSkeletonLib
{
    public class SLAV
    {
        private List<LAV> list;

        public SLAV()
        {
            list = new List<LAV>();

            LAV lav = new LAV();
            list.Add(lav);
        }

        public void Insert(Vertex v)
        {
            list[0].Add(v);
        }

        public LAV Get(int index)
        {
            return list[index];
        }

        public void BreakAndCreateNew(Vertex v, int lavIndex)
        {
            LAV lav = list[lavIndex];

            LineEquation line1 = null;
            LineEquation line2 = null;

            Vertex nextVertex = null;
            Vertex intersection = null;

            foreach (Vertex currentVertex in lav)
            {
                nextVertex = currentVertex.GetNextVertex();
                
                line1 = MathLibrary.GetLineEquation(currentVertex, currentVertex.AngleBisector);
                line2 = MathLibrary.GetLineEquation(nextVertex, nextVertex.AngleBisector);
                intersection = MathLibrary.GetIntersectionPoint(line1, line2);

                if (double.IsInfinity(intersection.GetX())) //IF THE BISECTORS DON'T INTERSECT, IT MUST BE A QUADRALATERAL
                {
                    double distanceCurrent = MathLibrary.GetDistanceBetweenVertices(currentVertex, v);
                    double distanceNext = MathLibrary.GetDistanceBetweenVertices(nextVertex, v);

                    double maxDistance = 0;

                    if (distanceCurrent >= distanceNext)
                        maxDistance = distanceCurrent * 10; //MAKE IT A LITTLE BIGGER JUST TO BE SURE
                    else
                        maxDistance = distanceNext * 10; //MAKE IT A LITTLE BIGGER JUST TO BE SURE

                    Vertex rectCurrent = new Vertex(line1.GetX(maxDistance + currentVertex.GetY()), maxDistance + currentVertex.GetY());
                    Vertex rectNext = new Vertex(line2.GetX(maxDistance + nextVertex.GetY()), maxDistance + nextVertex.GetY());
                    
                    if (MathLibrary.RectangleContainsPoint(currentVertex, nextVertex, rectNext, rectCurrent, v))
                        Console.WriteLine("I FOUND IT!!!");

                }
                else
                {                    
                    if (MathLibrary.TriangleContainsPoint(currentVertex, nextVertex, intersection, v.AngleBisector))
                        Console.WriteLine("I FOUND IT!!!");
                }
            }
        }
    }
}

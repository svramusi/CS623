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
        }

        public void Insert(Vertex v, int index)
        {
            if (index > list.Count-1)
            {
                if (index == list.Count)
                    list.Add(new LAV());
                else
                    throw new Exception("ERROR --> Invalid SLAV index!");
            }

            list[index].Add(new Vertex(v)); //PERFORM DEEP COPY
        }

        public LAV Get(int index)
        {
            return list[index];
        }

        public void BreakAndCreateNew(Vertex v, int lavIndex)
        {
            LAV lav = this.Get(lavIndex);

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
                
                //IF THE BISECTORS DON'T INTERSECT, OR INTERSECT BEHIND THE LINE, IT MUST BE A QUADRALATERAL
                if ((currentVertex.GetX() < nextVertex.GetX() && (intersection.GetY() < currentVertex.GetY() && intersection.GetY() < nextVertex.GetY())) || 
                    (currentVertex.GetX() > nextVertex.GetX() && (intersection.GetY() > currentVertex.GetY() && intersection.GetY() > nextVertex.GetY())) ||
                    double.IsInfinity(intersection.GetX()))
                {
                    Console.WriteLine("rect - current: " + currentVertex);

                    double distanceCurrent = MathLibrary.GetDistanceBetweenVertices(currentVertex, v);
                    double distanceNext = MathLibrary.GetDistanceBetweenVertices(nextVertex, v);

                    double maxDistance = 0;

                    if (distanceCurrent >= distanceNext)
                        maxDistance = distanceCurrent * 10; //MAKE IT A LITTLE BIGGER JUST TO BE SURE
                    else
                        maxDistance = distanceNext * 10; //MAKE IT A LITTLE BIGGER JUST TO BE SURE

                    Vertex rectCurrent = new Vertex(line1.GetX(maxDistance + currentVertex.GetY()), maxDistance + currentVertex.GetY());
                    Vertex rectNext = new Vertex(line2.GetX(maxDistance + nextVertex.GetY()), maxDistance + nextVertex.GetY());
                    
                    if (MathLibrary.QuadrilateralContainsPoint(currentVertex, nextVertex, rectNext, rectCurrent, v))
                    {
                        Console.WriteLine("i'm " + currentVertex + " and my rect contains point: " + v);
                        
                        Vertex splitVertex = null;
                        Vertex referenceVertex = null;
                        Vertex counterVertex = lav.GetStart();

                        while (splitVertex == null && !(counterVertex.GetNextVertex().Equals(lav.GetStart())))
                        {
                            if (counterVertex.Equals(v))
                                splitVertex = counterVertex;
                            else if (counterVertex.Equals(currentVertex))
                                referenceVertex = counterVertex;

                            counterVertex = counterVertex.GetNextVertex();
                        }

                        counterVertex = lav.GetStart();
                        while (referenceVertex == null && !(counterVertex.GetNextVertex().Equals(lav.GetStart())))
                        {
                            if (counterVertex.Equals(v))
                                splitVertex = counterVertex;
                            else if (counterVertex.Equals(currentVertex))
                                referenceVertex = counterVertex;

                            counterVertex = counterVertex.GetNextVertex();
                        }

                        Vertex splitNext = splitVertex.GetNextVertex();
                        Vertex splitPrev = splitVertex.GetPrevVertex();

                        Vertex refNext = referenceVertex.GetNextVertex();
                        Vertex refPrev = referenceVertex.GetPrevVertex();

                        Vertex newVertex1 = new Vertex(v.AngleBisector.GetX(), v.AngleBisector.GetY());
                        newVertex1.SetNextVertex(splitNext);
                        newVertex1.SetPrevVertex(referenceVertex);

                        referenceVertex.SetNextVertex(newVertex1);
                        splitVertex.GetNextVertex().SetPrevVertex(newVertex1);



                        Vertex newVertex2 = new Vertex(v.AngleBisector.GetX(), v.AngleBisector.GetY());
                        newVertex2.SetNextVertex(refNext);
                        newVertex2.SetPrevVertex(splitPrev);

                        splitVertex.GetPrevVertex().SetNextVertex(newVertex2);
                        referenceVertex.GetNextVertex().SetPrevVertex(newVertex2);

                        Console.WriteLine("adding : " + newVertex2 + " to " + (lavIndex + 1));
                        this.Insert(newVertex2, lavIndex + 1);
                        Vertex counterVertex2 = newVertex2.GetNextVertex();
                        while (!counterVertex2.Equals(newVertex2))
                        {
                            Console.WriteLine("adding : " + counterVertex2 + " to " + (lavIndex + 1));
                            this.Insert(counterVertex2, lavIndex + 1);
                            counterVertex2 = counterVertex2.GetNextVertex();
                        }

                        break;
                    }

                }
                else
                {
                    if (MathLibrary.TriangleContainsPoint(currentVertex, nextVertex, intersection, v.AngleBisector))
                    {
                        bool matchFound = false;
                        foreach (Vertex matchVertex in lav)
                        {
                            if (matchFound)
                            {
                                this.Insert(matchVertex, lavIndex + 1);
                            }

                            if (matchVertex.Equals(v))
                            {
                                Vertex newVertex = new Vertex(v.AngleBisector.GetX(), v.AngleBisector.GetY());
                                newVertex.SetPrevVertex(matchVertex.GetPrevVertex());
                                newVertex.SetNextVertex(nextVertex);
                                
                                matchVertex.GetPrevVertex().SetNextVertex(newVertex);
                                
                                this.Insert(newVertex, lavIndex + 1);

                                matchFound = true;
                            }
                        }

                        break;
                    }
                }
            }
        }
    }
}

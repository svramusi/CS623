using System;
using System.Collections.Generic;

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

            foreach (Vertex currentVertex in lav)
            {
                if (currentVertex.Equals(v))
                {
                    
                    break;
                }
            }
        }
    }
}

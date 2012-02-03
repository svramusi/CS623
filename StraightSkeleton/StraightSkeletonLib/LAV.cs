using System;

namespace StraightSkeletonLib
{
    public class LAV
    {
        private Vertex startVertex;
        private Vertex endVertex;

        public LAV()
        {
            this.startVertex = null;
            this.endVertex = null;
        }

        public Vertex GetStart()
        {
            return startVertex;
        }

        public void Add(Vertex v)
        {
            if (startVertex == null)
            {
                startVertex = v;
            }
            else
            {
                endVertex.SetNextVertex(v);
                v.SetPrevVertex(endVertex);
            }

            endVertex = v;
            if(!startVertex.Equals(endVertex))
                startVertex.SetPrevVertex(endVertex);
        }
    }
}

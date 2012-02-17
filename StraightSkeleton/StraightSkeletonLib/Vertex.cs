using System;

namespace StraightSkeletonLib
{
    public class Vertex
    {
        private double x;
        private double y;

        private Vertex prevVertex;
        private Vertex nextVertex;
        private Vertex bisectorVertex;

        public Vertex(double x, double y)
        {
            this.x = x;
            this.y = y;

            prevVertex = null;
            nextVertex = null;
        }

        public double GetX()
        {
            return this.x;
        }

        public double GetY()
        {
            return this.y;
        }

        public Vertex GetPrevVertex()
        {
            return this.prevVertex;
        }

        public Vertex GetNextVertex()
        {
            return this.nextVertex;
        }

        public void SetNextVertex(Vertex v)
        {
            this.nextVertex = v;
        }

        public void SetPrevVertex(Vertex v)
        {
            this.prevVertex = v;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            if((this.x == ((Vertex)obj).GetX()) && (this.y == ((Vertex)obj).GetY()))
                return true;
            else
                return false;
        }

        public Vertex AngleBisector
        {
            get { return this.bisectorVertex; }
            set { this.bisectorVertex = value; }
        }
    }
}

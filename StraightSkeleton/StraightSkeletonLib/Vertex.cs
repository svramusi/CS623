using System;

namespace StraightSkeletonLib
{
    public class Vertex
    {
        private bool processed; 

        private double x;
        private double y;

        private Vertex prevVertex;
        private Vertex nextVertex;
        private Vertex bisectorVertex;

        private LineSegment prevLS;
        private LineSegment nextLS;

        private VertexType type;

        public enum VertexType { Undefined, Edge, Split };

        public Vertex(double x, double y)
        {
            this.x = x;
            this.y = y;

            this.prevVertex = null;
            this.nextVertex = null;
            this.bisectorVertex = null;

            this.prevLS = null;
            this.nextLS = null;                 

            this.processed = false;
            this.type = VertexType.Undefined;
        }

        public Vertex(Vertex v)
        {
            this.x = v.GetX();
            this.y = v.GetY();

            this.prevVertex = null;
            this.nextVertex = null;

            this.bisectorVertex = v.bisectorVertex;
            this.processed = v.Processed;
            this.type = v.Type;
        }

        public VertexType Type
        {
            set { this.type = value; }
            get { return this.type; }
        }

        public bool Processed
        {
            get { return this.processed; }
        }

        public void SetProcessed()
        {
            this.processed = true;
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

            this.nextLS = new LineSegment(this, v);
        }

        public void SetPrevVertex(Vertex v)
        {
            this.prevVertex = v;

            this.prevLS = new LineSegment(v, this);
        }

        public LineSegment GetPrevLineSegment()
        {
            return this.prevLS;
        }

        public LineSegment GetNextLineSegment()
        {
            return this.nextLS;
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

        public override string ToString()
        {
            return "x: " + this.x + " y: " + this.y;
        }

        public Vertex AngleBisector
        {
            get { return this.bisectorVertex; }
            set { this.bisectorVertex = value; }
        }
    }
}

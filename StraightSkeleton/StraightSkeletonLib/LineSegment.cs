using System;

namespace StraightSkeletonLib
{
    public class LineSegment
    {
        private Vertex start;
        private Vertex end;

        public LineSegment(Vertex start, Vertex end)
        {
            this.start = start;
            this.end = end;
        }

        public Vertex Start
        {
            get { return this.start; }
        }

        public Vertex End
        {
            get { return this.end; }
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            LineSegment comp = (LineSegment)obj;

            if(this.Start.Equals(comp.Start) && this.End.Equals(comp.End))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return "Start: x: " + Start.GetX().ToString() + " y: " + Start.GetY().ToString() + " End: x: " + End.GetX().ToString() + " y: " + End.GetY().ToString();
        }

    }
}

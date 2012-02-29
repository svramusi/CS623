using System;

namespace StraightSkeletonLib
{
    public class LineSegment
    {
        private Point start;
        private Point end;

        public LineSegment(double x1, double y1, double x2, double y2)
        {
            this.start = new Point(x1, y1);
            this.end = new Point(x2, y2);
        }

        public Point Start
        {
            get { return this.start; }
        }

        public Point End
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
            return "Start: x: " + Start.X.ToString() + " y: " + Start.Y.ToString() + " End: x: " + End.X.ToString() + " y: " + End.Y.ToString();
        }

    }

    public class Point
    {
        private double x;
        private double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get { return this.x; }
        }

        public double Y
        {
            get { return this.y; }
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            Point comp = (Point)obj;

            if(Math.Round(this.X) == Math.Round(comp.X) && Math.Round(this.Y) == Math.Round(comp.Y))
                return true;
            else
                return false;
        }
    }
}

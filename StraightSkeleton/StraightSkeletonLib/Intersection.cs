using System;

using MathLib;

namespace StraightSkeletonLib
{
    public class Intersection
    {
        private double x;
        private double y;
        private double distance;

        private Vertex vA;
        private Vertex vB;

        public Intersection(double x, double y, Vertex vA, Vertex vB)
        {
            this.x = x;
            this.y = y;

            this.vA = vA;
            this.vB = vB;

            this.distance = MathLibrary.GetDistanceBetweenLineAndVertex(vA, vB, new Vertex(x, y));
        }

        public double Distance
        {
            get { return this.distance; }
        }

        public double GetX()
        {
            return this.x;
        }

        public double GetY()
        {
            return this.y;
        }

        public Vertex GetVA()
        {
            return this.vA;
        }

        public Vertex GetVB()
        {
            return this.vB;
        }
    }
}

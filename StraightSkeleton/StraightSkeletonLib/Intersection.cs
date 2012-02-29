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

        //THIS IS FOR UNIT TESTING ONLY, DONT FEEL LIKE DOING THE DISTANCE CALCULATIONS
        public Intersection(double x, double y, Vertex vA, Vertex vB, double distance)
        {
            this.x = x;
            this.y = y;

            this.vA = vA;
            this.vB = vB;

            this.distance = distance;
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            Intersection comp = (Intersection)obj;

            if(this.distance == comp.distance && this.GetX() == comp.GetX() && this.GetY() == comp.GetY() && this.GetVA().Equals(comp.GetVA()) && this.GetVB().Equals(comp.GetVB()))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return "x: " + this.x + " y: " + this.y + " VA x: " + vA.GetX() + " y: " + vA.GetY() + " VB x: " + vB.GetX() + " y: " + vB.GetY();
        }
    }
}

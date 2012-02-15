using System;

namespace MathLib
{
    public class LineEquation
    {
        private double slope;
        private double yIntercept;

        public LineEquation(double slope, double yIntercept)
        {
            this.slope = slope;
            this.yIntercept = yIntercept;
        }

        public double Slope
        {
            get { return this.slope; }
        }

        public double YIntercept
        {
            get { return this.yIntercept; }
        }

        public double GetY(double xPoint)
        {
            return (this.slope * xPoint) + this.yIntercept;
        }
    }
}

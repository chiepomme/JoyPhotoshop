using System;

namespace JoyPhotoshop
{
    struct DeltaAngle
    {
        double degree;

        DeltaAngle(double degree) => this.degree = degree;
        public static DeltaAngle FromDegree(double degree) => new DeltaAngle(degree);
        public static DeltaAngle FromRadian(double radian) => new DeltaAngle(MathAngle.ToDegree(radian * 180 / Math.PI));

        public double ToRadian() => MathAngle.ToRadian(degree);
        public double ToDegree() => degree;

        public override string ToString() => $"DeltaAngle({degree}deg)";
        public override bool Equals(object obj)
        {
            if (!(obj is DeltaAngle)) return false;
            return this == (DeltaAngle)obj;
        }
        public override int GetHashCode() => degree.GetHashCode();

        public static Angle operator +(Angle a, DeltaAngle d) => Angle.FromDegree(a.ToDegree() + d.degree);
        public static Angle operator -(Angle a, DeltaAngle d) => Angle.FromDegree(a.ToDegree() - d.degree);
        public static DeltaAngle operator +(DeltaAngle a, DeltaAngle b) => new DeltaAngle(a.degree + b.degree);
        public static DeltaAngle operator -(DeltaAngle a, DeltaAngle b) => new DeltaAngle(a.degree - b.degree);

        public static DeltaAngle operator *(DeltaAngle a, double v) => new DeltaAngle(a.degree * v);
        public static DeltaAngle operator /(DeltaAngle a, double v) => new DeltaAngle(a.degree / v);

        public static bool operator ==(DeltaAngle a, DeltaAngle b) => Math.Abs(a.degree - b.degree) < 0.0001;
        public static bool operator !=(DeltaAngle a, DeltaAngle b) => !(a == b);
    }
}

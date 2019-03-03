using System;
using System.Numerics;

namespace JoyPhotoshop
{
    struct Angle
    {
        double degree;

        Angle(double degree) => this.degree = MathAngle.NormalizeDegree(degree);
        public static Angle FromDegree(double degree) => new Angle(degree);
        public static Angle FromRadian(double radian) => new Angle(MathAngle.ToDegree(radian));
        public static Angle FromVector(Vector2 vector)
        {
            if (vector.X == 0 && vector.Y == 0) return FromDegree(0);

            var angle = FromRadian(Math.Atan2(vector.Y, vector.X));
            angle = FromDegree(360) - (angle - FromDegree(90));
            return angle;
        }

        public double ToRadian() => MathAngle.ToRadian(degree);
        public double ToDegree() => degree;
        public Vector2 ToVector()
        {
            var radian = ToRadian();
            return new Vector2((float)Math.Sin(radian), (float)Math.Cos(radian));
        }

        public override string ToString() => $"Angle({degree}deg)";
        public override bool Equals(object obj)
        {
            if (!(obj is Angle)) return false;
            return this == (Angle)obj;
        }
        public override int GetHashCode() => degree.GetHashCode();

        public static DeltaAngle operator -(Angle a, Angle b) => DeltaAngle.FromDegree(a.degree - b.degree);
        public static bool operator ==(Angle a, Angle b) => Math.Abs(a.degree - b.degree) < 0.0001;
        public static bool operator !=(Angle a, Angle b) => !(a == b);
    }
}

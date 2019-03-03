using System;

namespace JoyPhotoshop
{
    static class MathAngle
    {
        public static double ToRadian(double degree) => degree * Math.PI / 180;
        public static double ToDegree(double radian) => radian * 180 / Math.PI;
        public static double NormalizeDegree(double degree)
        {
            while (degree >= 360) degree -= 360;
            while (degree < 0) degree += 360;
            return degree;
        }
    }
}

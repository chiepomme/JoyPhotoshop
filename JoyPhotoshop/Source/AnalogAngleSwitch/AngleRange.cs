namespace JoyPhotoshop
{
    class AngleRange
    {
        public readonly Angle startAngle;
        public readonly DeltaAngle deltaAngle;

        AngleRange(Angle startAngle, DeltaAngle deltaAngle)
        {
            this.startAngle = startAngle;
            this.deltaAngle = deltaAngle;
        }

        public static AngleRange CreateFromStartAngle(Angle startAngle, DeltaAngle deltaAngle)
            => new AngleRange(startAngle, deltaAngle);
        public static AngleRange CreateFromCenterAngle(Angle centerAngle, DeltaAngle deltaAngleOneSide)
            => new AngleRange(centerAngle - deltaAngleOneSide, deltaAngleOneSide * 2);
        public static AngleRange FromCenterDegree(double centerDegree, double deltaDegreeOneSide)
            => CreateFromCenterAngle(Angle.FromDegree(centerDegree), DeltaAngle.FromDegree(deltaDegreeOneSide));

        public bool IsInRange(Angle angle)
        {
            // StartAngle を原点としたときの距離で判断する
            var translation = Angle.FromDegree(0) - startAngle;
            var translatedAngle = angle + translation;

            if (deltaAngle.ToDegree() >= 0)
            {
                return translatedAngle.ToDegree() <= deltaAngle.ToDegree();
            }
            else
            {
                return translatedAngle.ToDegree() >= deltaAngle.ToDegree();
            }
        }
    }
}

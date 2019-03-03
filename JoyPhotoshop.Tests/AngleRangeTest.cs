using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoyPhotoshop.Tests
{
    [TestClass]
    public class AngleRangeTest
    {
        [TestMethod]
        public void 正しく角度の判定ができる()
        {
            AngleRange angleRange;

            angleRange = AngleRange.CreateFromStartAngle(Angle.FromDegree(0), DeltaAngle.FromDegree(90));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(0)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(45)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(90)));

            angleRange = AngleRange.CreateFromStartAngle(Angle.FromDegree(90), DeltaAngle.FromDegree(90));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(90)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(135)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(180)));

            angleRange = AngleRange.CreateFromStartAngle(Angle.FromDegree(270), DeltaAngle.FromDegree(90));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(270)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(315)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(360)));

            angleRange = AngleRange.CreateFromStartAngle(Angle.FromDegree(270), DeltaAngle.FromDegree(180));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(270)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(315)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(360)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(0)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(45)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(90)));


            angleRange = AngleRange.CreateFromStartAngle(Angle.FromDegree(0), DeltaAngle.FromDegree(-90));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(-0)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(-45)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(-90)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(360)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(315)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(270)));

            angleRange = AngleRange.CreateFromStartAngle(Angle.FromDegree(90), DeltaAngle.FromDegree(-90));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(90)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(45)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(0)));

            angleRange = AngleRange.CreateFromStartAngle(Angle.FromDegree(90), DeltaAngle.FromDegree(-180));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(90)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(45)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(0)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(360)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(315)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(270)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(-0)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(-45)));
            Assert.IsTrue(angleRange.IsInRange(Angle.FromDegree(-90)));
        }
    }
}

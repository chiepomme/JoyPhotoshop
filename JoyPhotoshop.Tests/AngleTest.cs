using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace JoyPhotoshop.Tests
{
    [TestClass]
    public class AngleTest
    {
        [TestMethod]
        public void Vector2から正しい角度を作れる()
        {
            Assert.AreEqual(0, Angle.FromVector(new Vector2(0, 1)).ToDegree());
            Assert.AreEqual(45, Angle.FromVector(new Vector2(1, 1)).ToDegree());
            Assert.AreEqual(90, Angle.FromVector(new Vector2(1, 0)).ToDegree());
            Assert.AreEqual(135, Angle.FromVector(new Vector2(1, -1)).ToDegree());
            Assert.AreEqual(180, Angle.FromVector(new Vector2(0, -1)).ToDegree());
            Assert.AreEqual(225, Angle.FromVector(new Vector2(-1, -1)).ToDegree());
            Assert.AreEqual(270, Angle.FromVector(new Vector2(-1, 0)).ToDegree());
            Assert.AreEqual(315, Angle.FromVector(new Vector2(-1, 1)).ToDegree());
        }

        [TestMethod]
        public void 角度から正しいVector2を取れる()
        {
            Vector2 expect;
            Vector2 actual;

            expect = Vector2.Normalize(new Vector2(0, 1));
            actual = Angle.FromDegree(0).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);

            expect = Vector2.Normalize(new Vector2(1, 1));
            actual = Angle.FromDegree(45).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);

            expect = Vector2.Normalize(new Vector2(1, 0));
            actual = Angle.FromDegree(90).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);

            expect = Vector2.Normalize(new Vector2(1, -1));
            actual = Angle.FromDegree(135).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);

            expect = Vector2.Normalize(new Vector2(0, -1));
            actual = Angle.FromDegree(180).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);

            expect = Vector2.Normalize(new Vector2(-1, -1));
            actual = Angle.FromDegree(225).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);

            expect = Vector2.Normalize(new Vector2(-1, 0));
            actual = Angle.FromDegree(270).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);

            expect = Vector2.Normalize(new Vector2(-1, 1));
            actual = Angle.FromDegree(315).ToVector();
            Assert.AreEqual(expect.X, actual.X, 0.001);
            Assert.AreEqual(expect.Y, actual.Y, 0.001);
        }
    }
}

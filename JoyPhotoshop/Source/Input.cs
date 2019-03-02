using Microsoft.DirectX.DirectInput;
using System.Numerics;

namespace JoyPhotoshop
{
    static class Input
    {
        static byte[] prevButtons;
        static byte[] currentButtons;

        static int[] prevPov;
        static int[] currentPov;

        public static Vector2 StickPos { get; private set; }
        public static Vector3 Gyro { get; private set; }

        const int Up = 1;
        public static bool UpPress => GetButtonPress(Up);
        public static bool UpDown => GetButtonDown(Up);
        public static bool UpUp => GetButtonUp(Up);

        const int Right = 2;
        public static bool RightPress => GetButtonPress(Right);
        public static bool RightDown => GetButtonDown(Right);
        public static bool RightUp => GetButtonUp(Right);

        const int Down = 0;
        public static bool DownPress => GetButtonPress(Down);
        public static bool DownDown => GetButtonDown(Down);
        public static bool DownUp => GetButtonUp(Down);

        const int Left = 3;
        public static bool LeftPress => GetButtonPress(Left);
        public static bool LeftDown => GetButtonDown(Left);
        public static bool LeftUp => GetButtonUp(Left);

        const int L = 6;
        public static bool LPress => GetButtonPress(L);
        public static bool LDown => GetButtonDown(L);
        public static bool LUp => GetButtonUp(L);

        const int ZL = 7;
        public static bool ZLPress => GetButtonPress(ZL);
        public static bool ZLDown => GetButtonDown(ZL);
        public static bool ZLUp => GetButtonUp(ZL);

        const int Minus = 8;
        public static bool MinusPress => GetButtonPress(Minus);
        public static bool MinusDown => GetButtonDown(Minus);
        public static bool MinusUp => GetButtonUp(Minus);

        const int SL = 5;
        public static bool SLPress => GetButtonPress(SL);
        public static bool SLDown => GetButtonDown(SL);
        public static bool SLUp => GetButtonUp(SL);

        const int SR = 4;
        public static bool SRPress => GetButtonPress(SR);
        public static bool SRDown => GetButtonDown(SR);
        public static bool SRUp => GetButtonUp(SR);

        const int Stick = 11;
        public static bool StickPress => GetButtonPress(Stick);
        public static bool StickDown => GetButtonDown(Stick);
        public static bool StickUp => GetButtonUp(Stick);

        const int Photo = 13;
        public static bool PhotoPress => GetButtonPress(Photo);
        public static bool PhotoDown => GetButtonDown(Photo);
        public static bool PhotoUp => GetButtonUp(Photo);

        internal static void UpdateState(JoystickState state)
        {
            prevButtons = currentButtons;
            currentButtons = state.GetButtons();

            prevPov = currentPov;
            currentPov = state.GetPointOfView();

            StickPos = new Vector2(ConvertAxisToFloat(state.X), ConvertAxisToFloat(state.Y));

            var slider = state.GetSlider();
            Gyro = new Vector3(ConvertAxisToFloat(slider[0]), ConvertAxisToFloat(slider[1]), ConvertAxisToFloat(state.Rz));
        }

        internal static void Reset()
        {
            prevButtons = currentButtons = null;
            prevPov = currentPov = null;

            StickPos = Vector2.Zero;
        }

        static float ConvertAxisToFloat(int value)
        {
            var value01 = (float)value / ushort.MaxValue;
            return (value01 * 2) - 1;
        }

        static bool GetPrevButtonPress(int index)
        {
            if (prevButtons == null) return false;
            return prevButtons[index] >= 128;
        }

        public static bool GetButtonPress(int index)
        {
            if (currentButtons == null) return false;
            return currentButtons[index] >= 128;
        }

        public static bool GetButtonDown(int index)
        {
            if (prevButtons == null || currentButtons == null) return false;
            return GetPrevButtonPress(index) == false && GetButtonPress(index) == true;
        }

        public static bool GetButtonUp(int index)
        {
            if (prevButtons == null || currentButtons == null) return false;
            return GetPrevButtonPress(index) == true && GetButtonPress(index) == false;
        }
    }
}

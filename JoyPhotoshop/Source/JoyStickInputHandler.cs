using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace JoyPhotoshop
{
    class JoyStickInputHandler
    {
        double totalRotationAmount;
        double totalZoomAmount;

        const int maxInputs = 64;
        readonly PInvoke.INPUT[] inputs = new PInvoke.INPUT[maxInputs];
        int inputCount;

        void AddInput(PInvoke.INPUT input)
        {
            inputs[inputCount] = input;
            inputCount++;
        }

        void AddInput(IList<PInvoke.INPUT> inputs)
        {
            for (var i = 0; i < inputs.Count; i++)
            {
                this.inputs[inputCount + i] = inputs[i];
            }

            inputCount += inputs.Count;
        }

        public void Handle(double deltaSec)
        {
            inputCount = 0;

            if (Input.SLDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.CONTROL, PInvoke.VK.MENU, PInvoke.VK.KEY_Z));
            }

            if (Input.SRDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.CONTROL, PInvoke.VK.SHIFT, PInvoke.VK.KEY_Z));
            }

            if (Input.DownDown)
            {
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.SPACE));
            }

            if (Input.DownUp)
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.SPACE));
            }

            if (Input.UpDown)
            {
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.MENU));

                totalRotationAmount = 0;
                totalZoomAmount = 0;
            }

            if (Input.UpPress)
            {
                totalZoomAmount += Input.Gyro.X * deltaSec * 5000;
                if (Math.Abs(totalZoomAmount) > 50)
                {
                    AddInput(PInvoke.INPUT.CreateMouseWheel((int)totalZoomAmount));
                    totalZoomAmount = 0;
                }

                totalRotationAmount += Input.Gyro.Y * deltaSec * 300;
                var rotationCount = (int)(totalRotationAmount / 5);
                totalRotationAmount = totalRotationAmount % 5;

                for (var i = 0; i < Math.Abs(rotationCount); i++)
                {
                    var key = Math.Sign(rotationCount) > 0 ? PInvoke.VK.F14 : PInvoke.VK.F13;
                    AddInput(PInvoke.INPUT.CreateKeyDown(key));
                    AddInput(PInvoke.INPUT.CreateKeyUp(key));
                }
            }

            if (Input.UpUp)
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.MENU));
            }

            if (Input.RightDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.OEM_6)); // ]
            }

            if (Input.LeftDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.OEM_4)); // [
            }

            if (Input.ZLDown)
            {
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.KEY_E));
            }

            if (Input.ZLUp)
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.KEY_E));
            }

            if (Input.LDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.KEY_B));
            }

            if (Input.MinusDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.CONTROL, PInvoke.VK.KEY_S));
            }

            PInvoke.SendInput(inputCount, inputs, Marshal.SizeOf<PInvoke.INPUT>());
        }
    }
}

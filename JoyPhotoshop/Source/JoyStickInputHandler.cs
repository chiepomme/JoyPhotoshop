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

        readonly INotifier notifier;
        readonly AnalogAngleSwitch angleSwitch = new AnalogAngleSwitch();

        public JoyStickInputHandler(INotifier notifier)
        {
            this.notifier = notifier;

            angleSwitch.Add("ブラシ", Angle.FromDegree(0));
            angleSwitch.Add("指先", Angle.FromDegree(-60));
            angleSwitch.Add("覆い焼き", Angle.FromDegree(60));

            angleSwitch.Add("回転", Angle.FromDegree(180 + 60));
            angleSwitch.Add("手のひら", Angle.FromDegree(180));
            angleSwitch.Add("ズーム", Angle.FromDegree(180 - 60));
        }

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

            angleSwitch.Update(Input.StickPos);

            if (Input.SLDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.CONTROL, PInvoke.VK.MENU, PInvoke.VK.KEY_Z));
                notifier.Notify("元に戻す");
            }

            if (Input.SRDown)
            {
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.CONTROL, PInvoke.VK.SHIFT, PInvoke.VK.KEY_Z));
                notifier.Notify("やり直す");
            }

            if (Input.DownDown)
            {

            }

            if (Input.DownUp)
            {

            }

            if (Input.UpDown)
            {
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.LMENU));
                notifier.Notify("ALT");

                totalRotationAmount = 0;
                totalZoomAmount = 0;
            }

            /* どこかで何かを使ってジャイロ復活させたいな・・・という気持ちで残っています
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
            */

            if (Input.UpUp)
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.LMENU));
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
                notifier.Notify("消しゴム");
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.KEY_E));
            }

            if (Input.ZLUp)
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.KEY_E));
            }

            if (Input.LDown)
            {
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.SHIFT));
                notifier.Notify("SHIFT");
            }

            if (Input.LUp)
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.SHIFT));
            }

            if (Input.MinusDown)
            {
                notifier.Notify("保存");
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.CONTROL, PInvoke.VK.KEY_S));
            }

            if (angleSwitch.IsDown("ブラシ"))
            {
                notifier.Notify("ブラシ");
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.KEY_B));
            }

            if (angleSwitch.IsDown("覆い焼き"))
            {
                notifier.Notify("覆い焼き");
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.KEY_O));
            }

            // デフォだと割当たっていないので割り当ててください
            if (angleSwitch.IsDown("指先"))
            {
                notifier.Notify("指先");
                AddInput(PInvoke.INPUT.CreateInputStroke(PInvoke.VK.KEY_N));
            }

            if (angleSwitch.IsDown("回転"))
            {
                notifier.Notify("回転");
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.KEY_R));
            }

            if (angleSwitch.IsUp("回転"))
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.KEY_R));
            }

            if (angleSwitch.IsDown("ズーム"))
            {
                notifier.Notify("ズーム");
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.KEY_Z));
            }

            if (angleSwitch.IsUp("ズーム"))
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.KEY_Z));
            }

            if (angleSwitch.IsDown("手のひら"))
            {
                notifier.Notify("手のひら");
                AddInput(PInvoke.INPUT.CreateKeyDown(PInvoke.VK.SPACE));
            }

            if (angleSwitch.IsUp("手のひら"))
            {
                AddInput(PInvoke.INPUT.CreateKeyUp(PInvoke.VK.SPACE));
            }

            PInvoke.SendInput(inputCount, inputs, Marshal.SizeOf<PInvoke.INPUT>());
        }
    }
}

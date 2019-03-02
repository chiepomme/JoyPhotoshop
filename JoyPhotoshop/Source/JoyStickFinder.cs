using Microsoft.DirectX.DirectInput;
using System.Collections.Generic;

namespace JoyPhotoshop
{
    class JoyStickFinder
    {
        public List<JoyStickDescriptor> FindJoySticks()
        {
            var joySticks = new List<JoyStickDescriptor>();

            foreach (DeviceInstance deviceInstance in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly))
            {
                using (var device = new Device(deviceInstance.InstanceGuid))
                {
                    if (device.DeviceInformation.InstanceName != "vJoy Device") continue;
                    joySticks.Add(new JoyStickDescriptor(device.DeviceInformation.InstanceGuid, device.DeviceInformation.InstanceName));
                }
            }

            return joySticks;
        }
    }
}

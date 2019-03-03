using Microsoft.DirectX.DirectInput;
using System;
using System.Windows.Forms;

namespace JoyPhotoshop
{
    class JoyStickHolder : IDisposable
    {
        readonly Control parent;

        public Device Current { get; private set; }

        public JoyStickHolder(Control parent)
        {
            this.parent = parent;
        }

        public void Hold(JoyStickDescriptor descriptor)
        {
            if (Current != null && Current.DeviceInformation.InstanceGuid == descriptor.guid) return;

            Release();

            Current = new Device(descriptor.guid);
            Current.SetCooperativeLevel(parent, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
            Current.SetDataFormat(DeviceDataFormat.Joystick);
            Current.Acquire();
        }

        public void ReleaseIfNotAttatched()
        {
            if (Current != null && !Current.Caps.Attatched)
            {
                Release();
            }
        }

        public void Release()
        {
            if (Current == null) return;
            Current.Unacquire();
            Current.Dispose();
            Current = null;
        }

        public void Dispose()
        {
            Release();
        }
    }
}

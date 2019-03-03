using System;
using System.Linq;
using System.Windows.Forms;

namespace JoyPhotoshop
{
    public partial class MainWindow : Form, INotifier
    {
        readonly JoyStickHolder joyStick;
        readonly JoyStickFinder joyStickFinder = new JoyStickFinder();
        readonly JoyStickInputHandler inputHandler;

        readonly NotificationWindow notificationWindow;

        DateTime lastUpdatedAt = DateTime.Now;

        public MainWindow()
        {
            joyStick = new JoyStickHolder(this);
            inputHandler = new JoyStickInputHandler(this);
            notificationWindow = new NotificationWindow(this);

            InitializeComponent();
            RefreshList();

            if (deviceComboBox.Items.Count > 0)
            {
                deviceComboBox.SelectedIndex = 0;
                joyStick.Hold((JoyStickDescriptor)deviceComboBox.SelectedItem);
            }
        }

        void OnItemSelected(object _, EventArgs __) => joyStick.Hold((JoyStickDescriptor)deviceComboBox.SelectedItem);

        void OnClosed(object sender, FormClosedEventArgs e)
        {
            joyStick.Dispose();
            notificationWindow.Dispose();
        }

        void RefreshDeviceListClicked(object _, EventArgs __) => RefreshList();

        void RefreshList()
        {
            deviceComboBox.Items.Clear();
            deviceComboBox.Items.AddRange(joyStickFinder.FindJoySticks().ToArray());

            if (deviceComboBox.Items.Count > 0 && joyStick.Current != null)
            {
                foreach (var descriptorInList in deviceComboBox.Items.OfType<JoyStickDescriptor>())
                {
                    if (descriptorInList.guid == joyStick.Current.DeviceInformation.InstanceGuid)
                    {
                        deviceComboBox.SelectedItem = descriptorInList;
                    }
                }
            }
        }

        void OnTick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            HandleJoyStickInput((now - lastUpdatedAt).TotalSeconds);
            lastUpdatedAt = now;
        }

        void HandleJoyStickInput(double deltaSec)
        {
            joyStick.ReleaseIfNotAttatched();

            if (joyStick.Current == null)
            {
                Input.Reset();
            }
            else
            {
                joyStick.Current.Poll();
                Input.UpdateState(joyStick.Current.CurrentJoystickState);
                inputHandler.Handle(deltaSec);
            }
        }

        public void Notify(string message)
        {
            notificationWindow.SetMessageAndShow(message);
        }
    }
}

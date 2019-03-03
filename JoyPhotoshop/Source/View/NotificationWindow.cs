using System.Drawing;
using System.Windows.Forms;

namespace JoyPhotoshop
{
    public class NotificationWindow : Form
    {
        const double DefaultOpacity = 0.4;

        readonly Form parent;

        readonly Label label;
        readonly Timer repositionTimer;
        readonly Timer hideTimer;

        public NotificationWindow(Form parent)
        {
            this.parent = parent;

            TopMost = true;
            Size = new Size(200, 100);
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            Opacity = 0;
            ShowInTaskbar = false;

            label = new Label { Parent = this };

            repositionTimer = new Timer();
            repositionTimer.Tick += OnRepositionTimerTick;
            repositionTimer.Interval = 5;
            repositionTimer.Start();

            hideTimer = new Timer();
            hideTimer.Tick += OnHideTimerTick;
            hideTimer.Interval = 500;

            Show(parent);
        }

        void OnRepositionTimerTick(object _, System.EventArgs __)
        {
            MoveToCursor();
        }

        void OnHideTimerTick(object _, System.EventArgs __)
        {
            hideTimer.Stop();
            label.Text = "";
            Opacity = 0;
        }

        public void SetMessageAndShow(string message)
        {
            Opacity = 0;

            label.Text = message;
            Size = label.PreferredSize;
            MoveToCursor();

            Opacity = DefaultOpacity;

            if (hideTimer.Enabled)
            {
                hideTimer.Stop();
            }

            hideTimer.Start();
        }

        void MoveToCursor()
        {
            var pos = MousePosition;
            pos.Offset(0, -40);
            Location = pos;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            label.Dispose();
            repositionTimer.Dispose();
        }
    }
}

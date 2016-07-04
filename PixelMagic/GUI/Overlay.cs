using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;
using PixelMagic.Rotation;

// ReSharper disable once CheckNamespace

namespace PixelMagic.GUI
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public partial class Overlay : Form
    {
        private static Overlay overlay;
        private static bool shown;
        private Point firstPoint;
        private bool mouseIsDown;

        public Overlay()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Black;
            TransparencyKey = Color.Black;
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        public static event EventHandler onLocationChanged;

        public static void updateLabels()
        {
            if (overlay == null)
                return;

            overlay.AoELabel.Text = frmMain.combatRoutine.Type == CombatRoutine.RotationType.SingleTarget ? "Single-Target ON!" : "AoE-Target ON!";
        }

        public static bool showOverlay(Point p)
        {
            if (ConfigFile.DisableOverlay)
            {
                Log.Write("Overlay has been forced off, it will not show", Color.Gray);
                return false;
            }

            overlay = new Overlay {Location = new Point(p.X, p.Y)};
            overlay.Show();
            shown = true;
            return shown;
        }

        private static void onLocationChange(EventArgs args)
        {
            var handler = onLocationChanged;
            handler?.Invoke(null, args);
        }

        public static void closeOverlay()
        {
            overlay.Close();
            shown = false;
        }

        public static void setLocation(Point p)
        {
            overlay.Location = p;
        }

        public static Point getLocation()
        {
            return overlay.Location;
        }

        public static bool isShown()
        {
            return overlay != null && shown;
        }

        private void lblMain_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void lblMain_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            onLocationChange(e);
        }

        private void lblMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                // Get the difference between the two points
                var xDiff = firstPoint.X - e.Location.X;
                var yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                var x = Location.X - xDiff;
                var y = Location.Y - yDiff;
                Location = new Point(x, y);
            }
        }

        internal static void hideOverlay()
        {
            overlay.Hide();
            shown = false;
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
        }
    }
}
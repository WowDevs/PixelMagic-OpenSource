using PixelMagic.Rotation;
using System;
using System.Drawing;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.GUI
{
    public partial class Overlay : Form
    {
        private static Overlay overlay;
        private static bool shown;
        private bool mouseIsDown = false;
        private Point firstPoint;
        public static event EventHandler onLocationChanged;

        public Overlay()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Black;
            TransparencyKey = Color.Black;
        }
        
        public static void updateLabels()
        {
            if (overlay == null)
                return;        

            if (frmMain.combatRoutine.Type == CombatRoutine.RotationType.SingleTarget)
            {
                overlay.AoELabel.Text = "Single-Target ON!";
            }
            else
            {
                overlay.AoELabel.Text = "AoE-Target ON!";
            }
        }
        
        public static bool showOverlay(Point p)
        {
            if (ConfigFile.DisableOverlay)
            {
                Log.Write("Overlay has been forced off, it will not show", Color.Gray);
                return false;
            }

            overlay = new Overlay();
            overlay.Location = new Point(p.X, p.Y);
            overlay.Show();
            shown = true;
            return shown;
        }

        private static void onLocationChange(EventArgs args)
        {
            EventHandler handler = onLocationChanged;
            if (handler != null)
            {
                handler(null, args);
            }

        }

        public static void closeOverlay()
        {
            overlay.Close();
            shown = false;
        }

        public static void setLocation(Point p)
        {
            if (p != null)
                overlay.Location = p;
        }

        public static Point getLocation()
        {
            return overlay.Location;
        }

        public static bool isShown()
        {

            return (overlay != null && shown) ? true : false;
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
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }
        private void LabelAoE_click(object sender, EventArgs e)
        {
        
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
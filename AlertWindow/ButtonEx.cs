using System.Drawing;
using System.Windows.Forms;

namespace Allison.AlertWindow.Windows.Forms
{
    internal class ButtonEx : System.Windows.Forms.Control
    {

        public ButtonEx()
        {
            MouseLeave += ButtonEx_MouseLeave;
            MouseEnter += ButtonEx_MouseEnter;
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            this.mouseOver = false;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Color c = default(Color);
            if (this.mouseOver)
            {
                c = Color.Gray;
            }
            else
            {
                c = Color.FromArgb(64, 64, 64);
            }

            using (SolidBrush sb = new SolidBrush(c))
            {
                pevent.Graphics.DrawString("×", this.Font, sb, -2, 1);
            }
        }

        private bool mouseOver;
        private void ButtonEx_MouseEnter(object sender, System.EventArgs e)
        {
            this.mouseOver = true;
            this.Invalidate();
        }

        private void ButtonEx_MouseLeave(object sender, System.EventArgs e)
        {
            this.mouseOver = false;
            this.Invalidate();
        }

    }
}

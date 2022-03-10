using MetroFramework;
using MetroFramework.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp6.src
{
    public class LabelCustom : MetroLabel
    {
        private Color _borderColor = MetroColors.White;
        private Color _onHoverBorderColor = MetroColors.White;
        private Color _labelColor = MetroColors.Magenta;
        private Color _onHoverLabelColor = MetroColors.White;
        private Color _textColor = MetroColors.White;
        private Color _onHoverTextColor = MetroColors.Magenta;

        private bool _isHovering;
        private int _borderThickness = 0;
        private int _borderThicknessByTwo = 0;

        public LabelCustom()
        {
            DoubleBuffered = true;
            MouseEnter += (sender, e) => { _isHovering = true; Invalidate(); };
            MouseLeave += (sender, e) => { _isHovering = false; Invalidate(); };

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush brush = new SolidBrush(_isHovering ? _onHoverBorderColor : _borderColor);

            //Border
            g.FillRectangle(brush, 0, 0, Height, Height);
            g.FillRectangle(brush, Width - Height, 0, Height, Height);
            g.FillRectangle(brush, Height / 2, 0, Width - Height, Height);

            brush.Dispose();
            brush = new SolidBrush(_isHovering ? _onHoverLabelColor : _labelColor);

            //Inner part. Label itself
            g.FillRectangle(brush, _borderThicknessByTwo, _borderThicknessByTwo, Height - _borderThickness,
                Height - _borderThickness);
            g.FillRectangle(brush, (Width - Height) + _borderThicknessByTwo, _borderThicknessByTwo,
                Height - _borderThickness, Height - _borderThickness);
            g.FillRectangle(brush, Height / 2 + _borderThicknessByTwo, _borderThicknessByTwo,
                Width - Height - _borderThickness, Height - _borderThickness);

            brush.Dispose();
            brush = new SolidBrush(_isHovering ? _onHoverTextColor : _textColor);

            //Label Text
            SizeF stringSize = g.MeasureString(Text, Font);
            g.DrawString(Text, Font, brush, (20), (Height - stringSize.Height) / 2);
        }
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        public Color OnHoverBorderColor { get => _onHoverBorderColor; set { _onHoverBorderColor = value; Invalidate(); } }
        public Color LabelColor { get => _labelColor; set { _labelColor = value; Invalidate(); } }
        public Color OnHoverLabelColor { get => _onHoverLabelColor; set { _onHoverLabelColor = value; Invalidate(); } }
        public Color TextColor { get => _textColor; set { _textColor = value; Invalidate(); } }
        public Color OnHoverTextColor { get => _onHoverTextColor; set { _onHoverTextColor = value; Invalidate(); } }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp6.src
{
    class LabelButtonCustom : Label
    {
        private Color _borderColor = MetroFramework.MetroColors.White;
        private Color _onHoverBorderColor = MetroFramework.MetroColors.White;
        private Color _labelButtonColor = MetroFramework.MetroColors.Magenta;
        private Color _onHoverLabelButtonColor = MetroFramework.MetroColors.White;
        private Color _textColor = MetroFramework.MetroColors.Magenta;
        private Color _onHoverTextColor = MetroFramework.MetroColors.White;

        private bool _isHovering;
        private int _borderThickness = 0;
        private int _borderThicknessByTwo = 0;

        public LabelButtonCustom()
        {
            DoubleBuffered = true;
            MouseEnter += (sender, e) => { _isHovering = true; Invalidate(); };
            MouseLeave += (sender, e) => { _isHovering = false; Invalidate(); };
            TextAlign = ContentAlignment.MiddleLeft;

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
            brush = new SolidBrush(_isHovering ? _onHoverLabelButtonColor : _labelButtonColor);

            //Inner part. LabelButton itself
            g.FillRectangle(brush, _borderThicknessByTwo, _borderThicknessByTwo, Height - _borderThickness,
                Height - _borderThickness);
            g.FillRectangle(brush, (Width - Height) + _borderThicknessByTwo, _borderThicknessByTwo,
                Height - _borderThickness, Height - _borderThickness);
            g.FillRectangle(brush, Height / 2 + _borderThicknessByTwo, _borderThicknessByTwo,
                Width - Height - _borderThickness, Height - _borderThickness);

            brush.Dispose();
            brush = new SolidBrush(_isHovering ? _onHoverTextColor : _textColor);

            //LabelButton Text
            SizeF stringSize = g.MeasureString(Text, Font);
            g.DrawString(Text, Font, brush, (Width - stringSize.Width) / 2, (Height - stringSize.Height) / 2);
        }
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        public Color OnHoverBorderColor { get => _onHoverBorderColor; set { _onHoverBorderColor = value; Invalidate(); } }
        public Color LabelButtonColor { get => _labelButtonColor; set { _labelButtonColor = value; Invalidate(); } }
        public Color OnHoverLabelButtonColor { get => _onHoverLabelButtonColor; set { _onHoverLabelButtonColor = value; Invalidate(); } }
        public Color TextColor { get => _textColor; set { _textColor = value; Invalidate(); } }
        public Color OnHoverTextColor { get => _onHoverTextColor; set { _onHoverTextColor = value; Invalidate(); } }
    }
}

using MetroFramework;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsFormsApp6.src
{
    public class CheckBoxCustom : CheckBox
    {
        private Color _borderColor = MetroColors.White;
        private Color _onHoverBorderColor = MetroColors.White;
        private Color _checkBoxColor = MetroColors.Magenta;
        private Color _onHoverCheckBoxColor = MetroColors.White;
        private Color _textColor = MetroColors.White;
        private Color _onHoverTextColor = MetroColors.Magenta;


        private bool _isHovering;
        private int _borderThickness = 0;
        private int _borderThicknessByTwo = 0;

        public CheckBoxCustom()
        {
            DoubleBuffered = true;
            MouseEnter += (sender, e) => { _isHovering = true; Invalidate(); };
            MouseLeave += (sender, e) => { _isHovering = false; Invalidate(); };
            this.FlatStyle = FlatStyle.Standard;
            this.FlatAppearance.BorderSize = 0;

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
            brush = new SolidBrush(_isHovering ? _onHoverCheckBoxColor : _checkBoxColor);

            //Inner part. CheckBox itself
            g.FillRectangle(brush, _borderThicknessByTwo, _borderThicknessByTwo, Height - _borderThickness,
                Height - _borderThickness);
            g.FillRectangle(brush, (Width - Height) + _borderThicknessByTwo, _borderThicknessByTwo,
                Height - _borderThickness, Height - _borderThickness);
            g.FillRectangle(brush, Height / 2 + _borderThicknessByTwo, _borderThicknessByTwo,
                Width - Height - _borderThickness, Height - _borderThickness);

            brush.Dispose();
            brush = new SolidBrush(_isHovering ? _onHoverTextColor : _textColor);

            //CheckBox Text
            SizeF stringSize = g.MeasureString(Text, Font);
            g.DrawString(Text, Font, brush, (10), (Height - stringSize.Height) / 2);
        }
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }
        public Color OnHoverBorderColor { get => _onHoverBorderColor; set { _onHoverBorderColor = value; Invalidate(); } }
        public Color CheckBoxColor { get => _checkBoxColor; set { _checkBoxColor = value; Invalidate(); } }
        public Color OnHoverCheckBoxColor { get => _onHoverCheckBoxColor; set { _onHoverCheckBoxColor = value; Invalidate(); } }
        public Color TextColor { get => _textColor; set { _textColor = value; Invalidate(); } }
        public Color OnHoverTextColor { get => _onHoverTextColor; set { _onHoverTextColor = value; Invalidate(); } }


    }
}

using Opulos.Core.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp6.src;
namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {

        SplitContainer splitPane = new SplitContainer { Orientation = Orientation.Vertical, Dock = DockStyle.Fill, BorderStyle = BorderStyle.Fixed3D, SplitterWidth = 6 };
        TabControl tc = new TabControl { Dock = DockStyle.Fill };
        AP1 ap1 = new AP1();
        AP2 ap2 = new AP2();
        AP3 ap3 = new AP3();
        RandevuMenu ap4 = new RandevuMenu();
        TabPage tp1 = new TabPage("Test 1");
        TabPage tp2 = new TabPage("Test 2");
        TabPage tp3 = new TabPage("Test 3");
        TabPage tp4 = new TabPage("Test 4");
        //---
        CheckBox cbOnlyOneOpen = new CheckBox { Text = "Only One Open", AutoSize = true };
        CheckBox cbFillLastOpened = new CheckBox { Text = "Fill Last Opened", AutoSize = true };
        CheckBox cbResetFillOnClose = new CheckBox { Text = "Reset Fill On Close", AutoSize = true };
        CheckBox cbFillModeGrowOnly = new CheckBox { Text = "Fill Mode Grow Only", AutoSize = true };
        //---
        CheckBox cbFillWidth = new CheckBox { Text = "Fill Width", Checked = true };
        CheckBox cbFillHeight = new CheckBox { Text = "Fill Height", Checked = true };
        CheckBox cbGrowAndShrink = new CheckBox { Text = "Grow and Shrink", Checked = true };
        CheckBox cbControlMinimumWidthIsItsPreferredWidth = new CheckBox { Text = "Control Minimum Width Is Its Preferred Width", AutoSize = true, Checked = true };
        CheckBox cbControlMinimumHeightIsItsPreferredHeight = new CheckBox { Text = "Control Minimum Height Is Its Preferred Height", AutoSize = true, Checked = true };
        CheckBox cbMouseGrabRequiresPositiveFillWeight = new CheckBox { Text = "Mouse Grab Requires Positive Fill Weight", AutoSize = true, Checked = true };
        CheckBox cbAllowMouseResize = new CheckBox { Text = "Allow Mouse Resize", AutoSize = true, Checked = true };
        CheckBox cbEnabled = new CheckBox { Text = "Enabled", Checked = true, AutoSize = true };
        //---
        // Open/Close Animation Options:
        CheckBox cbAnimateOpen = new CheckBox { Text = "Animate Open Millis", AutoSize = true, Checked = true };
        NumericUpDown nudAnimateOpen = new NumericUpDown { Minimum = 0, Maximum = 9999, Increment = 50, DecimalPlaces = 0, Value = 300 };
        CheckBox cbAnimateClose = new CheckBox { Text = "Animate Close Millis", AutoSize = true, Checked = true };
        NumericUpDown nudAnimateClose = new NumericUpDown { Minimum = 0, Maximum = 9999, Increment = 50, DecimalPlaces = 0, Value = 300 };
        //---
        // Resize Bar Options:
        CheckBox cbResizeBarsFadeProximity = new CheckBox { Text = "Fade Proximity", Checked = true };
        NumericUpDown nudResizeBarsFadeProximity = new NumericUpDown { Minimum = 0, Maximum = 99, Increment = 5, DecimalPlaces = 0, Value = 30 };
        CheckBox cbResizeBarsFadeInMillis = new CheckBox { Text = "Fade-In Millis", Checked = true };
        NumericUpDown nudResizeBarsFadeInMillis = new NumericUpDown { Minimum = 0, Maximum = 9999, Increment = 50, DecimalPlaces = 0, Value = 800 };
        CheckBox cbResizeBarsFadeOutMillis = new CheckBox { Text = "Fade-Out Millis", Checked = true };
        NumericUpDown nudResizeBarsFadeOutMillis = new NumericUpDown { Minimum = 0, Maximum = 9999, Increment = 50, DecimalPlaces = 0, Value = 800 };
        CheckBox cbResizeBarsKeepFocusAfterMouseDrag = new CheckBox { Text = "Keep Focus After Mouse Drag" };
        CheckBox cbResizeBarsKeepFocusOnClick = new CheckBox { Text = "Keep Focus On Click", Checked = true };
        CheckBox cbResizeBarsKeepFocusIfControlOutOfView = new CheckBox { Text = "Keep Focus If Control Out Of View", Checked = true };
        CheckBox cbResizeBarsTabStop = new CheckBox { Text = "Tab Stop", Checked = true };
        CheckBox cbResizeBarsStayVisibleIfFocused = new CheckBox { Text = "Stay Visible If Focused", Checked = true };
        CheckBox cbResizeBarsStayInViewOnMouseDrag = new CheckBox { Text = "Stay In View On Mouse Drag", Checked = true };
        CheckBox cbResizeBarsStayInViewOnArrowKey = new CheckBox { Text = "Stay In View On Arrow Key", Checked = true };
        CheckBox cbShowPartiallyVisibleResizeBars = new CheckBox { Text = "Show Partially Visible" };
        CheckBox cbResizeBarsArrowKeyDelta = new CheckBox { Text = "Arrow Key Delta", Checked = true };
        NumericUpDown nudResizeBarsArrowKeyDelta = new NumericUpDown { AutoSize = true, DecimalPlaces = 0, Minimum = 0, Maximum = 99, Value = 10 };
        //---
        // Add Options:
        Button btnAdd = new Button { Text = "Add", AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoSize = true };
        CheckBox cbOpenOnAdd = new CheckBox { Text = "Open on Add", AutoSize = true };
        CheckBox cbFillWt = new CheckBox { Text = "Fill Wt", AutoSize = true, Checked = true };
        NumericUpDown nudFillWt = new NumericUpDown { DecimalPlaces = 1, Minimum = 0, Maximum = 10, Value = 1, Increment = 1, AutoSize = true };
        CheckBox cbAddResizeBars = new CheckBox { Text = "Add Resize Bars", Checked = true, AutoSize = true };
        Label lbCheckBoxMargin = new Label { Text = "Check Box Margin", AutoSize = true };
        NumericUpDown nudCheckBoxMargin = new NumericUpDown { DecimalPlaces = 0, Minimum = 0, Maximum = 99, Value = 5, AutoSize = true };
        Label lbContentMargin = new Label { Text = "Content Margin", AutoSize = true };
        NumericUpDown nudContentMargin = new NumericUpDown { DecimalPlaces = 0, Minimum = 0, Maximum = 99, Value = 5, AutoSize = true };
        Label lbContentPadding = new Label { Text = "Content Padding", AutoSize = true };
        NumericUpDown nudContentPadding = new NumericUpDown { DecimalPlaces = 0, Minimum = 0, Maximum = 99, Value = 5, AutoSize = true };
        Label lbContentBackColor = new Label { Text = "Content Back Color", AutoSize = true };
        Button btnContentBackColor = new Button { Text = "Choose...", AutoSize = true };
        //---
        FlowLayoutPanel options = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true };
        ColorDialog dialogColor = new ColorDialog();

        public Form1()
        {
            InitializeComponent();

            Text = "Accordion Demo";
            ClientSize = new System.Drawing.Size(1000, 700);
            splitPane.Size = new System.Drawing.Size(1000, 1);
            splitPane.SplitterDistance = 400;
            splitPane.FixedPanel = FixedPanel.Panel1;


            SuspendLayout();
            GroupBox gbAdd = CreateGBox("Add Options",
                                        btnAdd, CreateHPanel(btnAdd, cbFillWt, nudFillWt, cbOpenOnAdd),
                                        new Label(), cbAddResizeBars,
                                        lbCheckBoxMargin, nudCheckBoxMargin,
                                        lbContentMargin, nudContentMargin,
                                        lbContentPadding, nudContentPadding,
                                        lbContentBackColor, btnContentBackColor);

            //---
            options.Controls.Add(cbOnlyOneOpen);
            options.Controls.Add(cbFillLastOpened);
            options.Controls.Add(cbResetFillOnClose);
            options.Controls.Add(cbFillModeGrowOnly);
            options.Controls.Add(CreateHPanel(cbFillWidth, cbFillHeight, cbGrowAndShrink));
            options.Controls.Add(cbControlMinimumWidthIsItsPreferredWidth);
            options.Controls.Add(cbControlMinimumHeightIsItsPreferredHeight);
            options.Controls.Add(cbMouseGrabRequiresPositiveFillWeight);
            options.Controls.Add(cbAllowMouseResize);
            options.Controls.Add(cbEnabled);
            //---
            // Open/Close Animation Options:
            options.Controls.Add(CreateHPanel(cbAnimateOpen, nudAnimateOpen));
            options.Controls.Add(CreateHPanel(cbAnimateClose, nudAnimateClose));
            // Resize Bar Options:
            options.Controls.Add(CreateGBox("Resize Bar Options",
                                    cbResizeBarsFadeProximity, nudResizeBarsFadeProximity,
                                    cbResizeBarsFadeInMillis, nudResizeBarsFadeInMillis,
                                    cbResizeBarsFadeOutMillis, nudResizeBarsFadeOutMillis,
                                    new Label(), cbResizeBarsKeepFocusAfterMouseDrag,
                                    new Label(), cbResizeBarsKeepFocusOnClick,
                                    new Label(), cbResizeBarsKeepFocusIfControlOutOfView,
                                    new Label(), cbResizeBarsTabStop,
                                    new Label(), cbResizeBarsStayVisibleIfFocused,
                                    new Label(), cbResizeBarsStayInViewOnMouseDrag,
                                    new Label(), cbResizeBarsStayInViewOnArrowKey,
                                    new Label(), cbShowPartiallyVisibleResizeBars,
                                    cbResizeBarsArrowKeyDelta, nudResizeBarsArrowKeyDelta
                                ));
            options.Controls.Add(gbAdd);
            //---
            splitPane.Panel1.Controls.Add(options);
            splitPane.Panel2.Controls.Add(tc);
            tc.TabPages.Add(tp4);
            tc.TabPages.Add(tp1);
            tc.TabPages.Add(tp2);
            tc.TabPages.Add(tp3);
            tp1.Controls.Add(ap1);
            tp2.Controls.Add(ap2);
            tp3.Controls.Add(ap3);
            tp4.Controls.Add(ap4);

            //---

            Accordion[] accs = new[] { ap1.acc, ap2.acc, ap3.acc, ap4.acc };

            cbOnlyOneOpen.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.OpenOneOnly = cbOnlyOneOpen.Checked;
                    if (cbOnlyOneOpen.Checked)
                        a.Close(null);
                }
            };
            cbFillLastOpened.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.FillLastOpened = cbFillLastOpened.Checked;
            };
            cbFillModeGrowOnly.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.FillModeGrowOnly = cbFillModeGrowOnly.Checked;
            };
            cbResetFillOnClose.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.FillResetOnCollapse = cbResetFillOnClose.Checked;
            };
            cbFillWidth.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.FillWidth = cbFillWidth.Checked;
                    a.PerformLayout();
                    a.PerformLayout();
                }
            };
            cbFillHeight.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.FillHeight = cbFillHeight.Checked;
                    a.PerformLayout();
                    a.PerformLayout();
                }
            };
            cbGrowAndShrink.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.GrowAndShrink = cbGrowAndShrink.Checked;
                    a.PerformLayout();
                    a.PerformLayout();
                }
            };
            cbControlMinimumWidthIsItsPreferredWidth.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.ControlMinimumWidthIsItsPreferredWidth = cbControlMinimumWidthIsItsPreferredWidth.Checked;
                    a.PerformLayout();
                    a.PerformLayout();
                    a.PerformLayout();
                }
            };
            cbControlMinimumHeightIsItsPreferredHeight.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.ControlMinimumHeightIsItsPreferredHeight = cbControlMinimumHeightIsItsPreferredHeight.Checked;
                    a.PerformLayout();
                    a.PerformLayout();
                }
            };
            cbMouseGrabRequiresPositiveFillWeight.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.GrabRequiresPositiveFillWeight = cbMouseGrabRequiresPositiveFillWeight.Checked;
                }
            };
            cbAllowMouseResize.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.AllowMouseResize = cbAllowMouseResize.Checked;
            };
            cbEnabled.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.Enabled = cbEnabled.Checked;
            };
            cbAnimateOpen.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.AnimateOpenMillis = (cbAnimateOpen.Checked ? (int)nudAnimateOpen.Value : 0);
                }
                nudAnimateOpen.Enabled = cbAnimateOpen.Checked;
            };
            cbAnimateClose.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.AnimateCloseMillis = (cbAnimateClose.Checked ? (int)nudAnimateClose.Value : 0);
                }
                nudAnimateClose.Enabled = cbAnimateClose.Checked;
            };
            nudAnimateClose.ValueChanged += delegate
            {
                foreach (var a in accs)
                    a.AnimateCloseMillis = (int)nudAnimateClose.Value;
            };
            nudAnimateOpen.ValueChanged += delegate
            {
                foreach (var a in accs)
                    a.AnimateOpenMillis = (int)nudAnimateOpen.Value;
            };
            //---
            // Resize Bar Options:
            cbResizeBarsFadeInMillis.CheckedChanged += delegate
            {
                bool b = cbResizeBarsFadeInMillis.Checked;
                nudResizeBarsFadeInMillis.Enabled = b;
                foreach (var a in accs)
                    a.ResizeBarsFadeInMillis = (b ? (int)nudResizeBarsFadeInMillis.Value : 0);
            };
            cbResizeBarsFadeOutMillis.CheckedChanged += delegate
            {
                bool b = cbResizeBarsFadeOutMillis.Checked;
                nudResizeBarsFadeOutMillis.Enabled = b;
                foreach (var a in accs)
                    a.ResizeBarsFadeOutMillis = (b ? (int)nudResizeBarsFadeOutMillis.Value : 0);
            };
            cbResizeBarsFadeProximity.CheckedChanged += delegate
            {
                bool b = cbResizeBarsFadeProximity.Checked;
                nudResizeBarsFadeProximity.Enabled = b;
                foreach (var a in accs)
                {
                    a.ResizeBarsFadeProximity = (b ? (int)nudResizeBarsFadeProximity.Value : 0);
                    foreach (var rb in a.ResizeBars)
                        rb.Visible = (a.ResizeBarsFadeProximity == 0);
                }
            };
            nudResizeBarsFadeInMillis.ValueChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsFadeInMillis = (int)nudResizeBarsFadeInMillis.Value;
            };
            nudResizeBarsFadeOutMillis.ValueChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsFadeOutMillis = (int)nudResizeBarsFadeOutMillis.Value;
            };
            nudResizeBarsFadeProximity.ValueChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.ResizeBarsFadeProximity = (int)nudResizeBarsFadeProximity.Value;
                    foreach (var rb in a.ResizeBars)
                        rb.Visible = (a.ResizeBarsFadeProximity == 0);
                }
            };
            cbResizeBarsKeepFocusAfterMouseDrag.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsKeepFocusAfterMouseDrag = cbResizeBarsKeepFocusAfterMouseDrag.Checked;
            };
            cbResizeBarsKeepFocusOnClick.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsKeepFocusOnClick = cbResizeBarsKeepFocusOnClick.Checked;
            };
            cbResizeBarsKeepFocusIfControlOutOfView.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsKeepFocusIfControlOutOfView = cbResizeBarsKeepFocusIfControlOutOfView.Checked;
            };

            cbResizeBarsTabStop.CheckedChanged += delegate
            {
                foreach (var a in accs)
                {
                    a.ResizeBarsTabStop = cbResizeBarsTabStop.Checked;
                    foreach (Control rb in a.ResizeBars)
                        rb.TabStop = a.ResizeBarsTabStop;
                }
            };
            cbResizeBarsStayVisibleIfFocused.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsStayVisibleIfFocused = cbResizeBarsStayVisibleIfFocused.Checked;
            };
            cbResizeBarsStayInViewOnMouseDrag.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsStayInViewOnMouseDrag = cbResizeBarsStayInViewOnMouseDrag.Checked;
            };
            cbResizeBarsStayInViewOnArrowKey.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsStayInViewOnArrowKey = cbResizeBarsStayInViewOnArrowKey.Checked;
            };
            cbShowPartiallyVisibleResizeBars.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ShowPartiallyVisibleResizeBars = cbShowPartiallyVisibleResizeBars.Checked;
            };
            cbResizeBarsArrowKeyDelta.CheckedChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsArrowKeyDelta = cbResizeBarsArrowKeyDelta.Checked ? (int)nudResizeBarsArrowKeyDelta.Value : 0;
            };
            nudResizeBarsArrowKeyDelta.ValueChanged += delegate
            {
                foreach (var a in accs)
                    a.ResizeBarsArrowKeyDelta = (int)nudResizeBarsArrowKeyDelta.Value;
            };
            //---
            btnContentBackColor.Click += delegate
            {
                var result = dialogColor.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    lbContentBackColor.BackColor = dialogColor.Color;
                }
            };

            //---
            cbFillWt.CheckedChanged += delegate
            {
                nudFillWt.Enabled = cbFillWt.Checked;
            };
            tc.SelectedIndexChanged += (o, e) =>
            {
                bool b = (tc.SelectedTab == tp1);
                gbAdd.Enabled = b;
            };
            int count = 10;
            btnAdd.Click += delegate
            {
                Control cc = new TextBox { Text = "" + count, Dock = DockStyle.Fill, Multiline = true };
                Padding cbMargin = new Padding((int)nudCheckBoxMargin.Value);
                Padding contentMargin = new Padding((int)nudContentMargin.Value);
                Padding contentPadding = new Padding((int)nudContentPadding.Value);
                Color? contentBackColor = null;
                if (lbContentBackColor.BackColor != lbContentBackColor.Parent.BackColor)
                    contentBackColor = lbContentBackColor.BackColor;
                ap1.acc.Add(cc, "Some control " + count, "", (cbFillWt.Checked ? Convert.ToDouble(nudFillWt.Value) : 0), cbOpenOnAdd.Checked, addResizeBar: cbAddResizeBars.Checked, checkboxMargin: cbMargin, contentPadding: contentPadding, contentMargin: contentMargin, contentBackColor: contentBackColor);
                count++;
            };
            //---
            cbOnlyOneOpen.Checked = false;
            cbOnlyOneOpen.Checked = true;
            Controls.Add(splitPane);
            ResumeLayout(true);

        }




        private static GroupBox CreateGBox(String title, params Control[] arr)
        {
            GB gb = new GB { Text = title, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            VFLP p = new VFLP();
            gb.Controls.Add(p);
            for (int i = 0; i < arr.Length; i += 2)
            {
                var c = CreateHPanel(arr[i], arr[i + 1]);
                p.Controls.Add(c);
            }
            return gb;
        }

        private class GB : GroupBox
        {
            public override Size GetPreferredSize(Size proposedSize)
            {
                var c = Controls[0];
                Size s = c.GetPreferredSize(proposedSize);
                var m = c.Margin;
                var p = this.Padding;
                s.Height += m.Vertical + p.Vertical;
                s.Width += m.Horizontal + p.Horizontal;
                s.Height += DisplayRectangle.Y;
                return s;
            }
        }

        private static Panel CreateHPanel(params Control[] arr)
        {
            return new HFLP(arr);
        }

        private class VFLP : FlowLayoutPanel
        {
            public VFLP()
            {
                FlowDirection = FlowDirection.TopDown;
                WrapContents = false;
                Dock = DockStyle.Fill;
                AutoSize = true;
                AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
            public override Size GetPreferredSize(Size proposedSize)
            {
                Size s = Size.Empty;
                foreach (Control c in Controls)
                {
                    var ps = c.PreferredSize;
                    var m = c.Margin;
                    ps.Width += m.Horizontal;
                    ps.Height += m.Vertical;
                    s.Height += ps.Height;
                    if (ps.Width > s.Width)
                        s.Width = ps.Width;
                }
                Padding p = this.Padding;
                s.Width += p.Horizontal;
                s.Height += p.Vertical;
                return s;
            }
        }

        private class HFLP : FlowLayoutPanel
        {
            public HFLP(Control[] arr)
            {
                AutoSize = true;
                FlowDirection = FlowDirection.LeftToRight;
                WrapContents = false;
                foreach (Control c in arr)
                {
                    c.AutoSize = true;
                    c.Anchor = AnchorStyles.None;
                    Controls.Add(c);
                }
            }
            public override Size GetPreferredSize(Size proposedSize)
            {
                Size ps = Size.Empty;
                foreach (Control c in Controls)
                {
                    Size s = c.PreferredSize;
                    Padding m = c.Margin;
                    s.Width += m.Horizontal;
                    s.Height += m.Vertical;
                    ps.Width += s.Width;
                    if (s.Height > ps.Height)
                        ps.Height = s.Height;
                }

                return ps;
            }
        }

    }

    internal abstract class AccordionPanel : Panel
    {

        public Accordion acc = new Accordion();

        public AccordionPanel()
        {
            Dock = DockStyle.Fill;
            Controls.Add(acc);
        }

    }


    internal class AP1 : AccordionPanel
    {

        // radio buttons TabStop is false by default... weird.
        RadioButton rbOption1 = new RadioButton { Text = "Very Very Very Long Option Text 1", AutoSize = true, TabStop = true };
        RadioButton rbOption2 = new RadioButton { Text = "Option 2", AutoSize = true, TabStop = true };

        public AP1()
        {

            acc.Insets = new Padding(10);
            acc.ContentPadding = new Padding(10);
            acc.ContentMargin = new Padding(0);
            acc.CheckBoxMargin = new Padding(0);

            TableLayoutPanel p = new TableLayoutPanel { Dock = DockStyle.Fill };
            p.Margin = new Padding(0);
            p.BackColor = System.Drawing.Color.LightBlue;
            p.Controls.Add(rbOption1);
            p.Controls.Add(rbOption2);
            p.Controls.Add(new TextBox { Dock = DockStyle.Fill }); //, Anchor = (AnchorStyles) (AnchorStyles.Left | AnchorStyles.Right) });
            Size s = p.GetPreferredSize(Size.Empty);
            Control c = new TextBox { Text = "Some text information.", Dock = DockStyle.Fill, Multiline = true };
            acc.Add(c, "Some control", "Tooltip 1", 1, true);
            acc.Add(p, "Radio Options", "ToolTip 2");

            UserControl panel = new UserControl { Margin = Padding.Empty, Padding = Padding.Empty, Dock = DockStyle.Fill };
            var cb = new CheckBox { Text = "Check Box 1", Margin = Padding.Empty };
            //cb.BackColor = Color.Red;
            panel.Controls.Add(cb);
            Color? blue = null;
            acc.Add(panel, "Check Box", contentBackColor: blue, open: true);
        }
        private static void onHover(object sender, EventArgs e)
        {
            if (sender is TableLayoutPanel)
            {
                TableLayoutPanel userC = (TableLayoutPanel)sender;
                userC.BackColor = Color.Black;
                userC.ForeColor = Color.White;

            }
        }
        private static void offHover(object sender, EventArgs e)
        {
            if (sender is TableLayoutPanel)
            {
                TableLayoutPanel userC = (TableLayoutPanel)sender;
                userC.ForeColor = Color.Black;
                userC.BackColor = Color.White;

            }
        }
    }

    internal class AP2 : AccordionPanel
    {
        // Old: can be used to reproduce the layout problem if the Form WindowState == Minimized is not ignored.
        // New: No longer happens with the custom AccordionLayoutEngine.
        public AP2()
        {

            DataGridView dgv = new DataGridView() { AutoSize = true, Dock = DockStyle.Fill };
            dgv.Columns.Add("Column1", "Column1");
            acc.Add(dgv, "DGV", fillWt: 1, open: true);
        }
    }


    internal class AP3 : AccordionPanel
    {

        class TextBox2 : TextBox
        {

            public TextBox2()
            {
                MinimumSize = new Size(100, 5);
                MaximumSize = new Size(300, 90);
                Multiline = true;
            }

            public override Size GetPreferredSize(Size proposedSize)
            {
                return new Size(120, 40);
            }
        }

        public AP3()
        {

            this.Size = new Size(700, 400);
            this.Padding = new Padding(10);
            this.BackColor = Color.LightBlue;

            acc.SuspendLayout();
            acc.BackColor = Color.LightYellow;
            acc.Padding = new Padding(5, 10, 15, 20);
            String text1 = "Minimum Size = (100, 5)\r\nMaximum Size = (300, 90)\r\nPreferred Size = (120, 40)\r\nUncheck ControlMinimumHeightIsItsPreferredHeight to allow to shrink to the minimum height.";
            TextBox2 tb1 = new TextBox2 { Text = text1, Dock = DockStyle.Fill, Margin = new Padding(1, 2, 3, 4), ScrollBars = ScrollBars.Vertical };
            acc.Add(tb1, "Min and Max Constraints (fillWt:1.0)", fillWt: 1.0, contentBackColor: Color.Red, contentMargin: new Padding(10), addResizeBar: true);

            String text2 = "This textbox's height will grow and shrink slower than the above textbox's height if the above textbox hasn't reached its limits.";
            acc.Add(new TextBox { Text = text2, Multiline = true, Dock = DockStyle.Fill }, "No Constraints (fillWt:0.5)", fillWt: 0.5, contentBackColor: Color.LightGreen, resizeBarMargin: new Padding(100));
            acc.ResumeLayout();
        }
    }

    internal class AP4 : AccordionPanel
    {

        public AP4()
        {
            List<CheckBoxCustom> checkboxList = new List<CheckBoxCustom>();

            Accordion accNested = new Accordion() { Dock = DockStyle.Fill };
            checkboxList.Add(accNested.Add(CreateControl(), "Child1"));
            checkboxList.Add(accNested.Add(CreateControl(), "Child2"));
            //accNested.AutoSize = false;

            Padding? margin = new Padding(35);

            Accordion accParent = acc;
            checkboxList.Add(accParent.Add(CreateControl(), "Parent1", contentMargin: margin));
            checkboxList.Add(accParent.Add(accNested, "Parent2")); //, contentBackColor:Color.LightBlue));
                                                                   //checkboxList.Add(accParent.Add(CreateControl(), "Parent2", contentMargin:margin));
            checkboxList.Add(accParent.Add(CreateControl(), "Parent3", contentMargin: margin));
            //accParent.AutoSize = false;
            //accParent.FillHeight = false;                Controls.Add(acc);
        }

        private static Control CreateControl(Control tb = null)
        {
            UC fp1 = new UC { Dock = DockStyle.Fill };

            if (tb == null)
            {
                tb = new TextBox { Multiline = true, Dock = DockStyle.Fill };
                //tb = new Button { Text = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly };
            }
            fp1.Controls.Add(tb);
            return fp1;
        }

        private class UC : UserControl
        { //FlowLayoutPanel {
            public override Size GetPreferredSize(Size proposedSize)
            {
                Control c = Controls[0];
                Padding m = c.Margin;
                Padding p = Padding;
                Size s = c.PreferredSize;
                s.Height += (m.Vertical + p.Vertical);
                s.Width += (m.Horizontal + p.Horizontal);
                return s;
            }
        }
    }
    internal class RandevuMenu : AccordionPanel
    {


        public RandevuMenu()
        {
            List<CheckBoxCustom> checkboxList = new List<CheckBoxCustom>();
            DateTime date = DateTimeOffset.Now.Date;

            //accNested.AutoSize = false;
            Padding? margin = new Padding(15);
            Accordion accParent = acc;
            Accordion accNested = new Accordion() { Dock = DockStyle.Fill };
            TableLayoutPanel panelAcc = null;
            for (int i = 0; i < 15; i++)
            {
                checkboxList.Add(accParent.Add(CreateControl(panelAcc, date: date.AddDays(i)), date.AddDays(i).ToLongDateString(), contentMargin: margin));
            }
            //accParent.AutoSize = false;
            //accParent.FillHeight = false;
            Controls.Add(acc);
        }


        private static Control CreateControl(TableLayoutPanel panelAcc, DateTime date = default(DateTime), Control tb = null)
        {
            UC fp1 = new UC { Dock = DockStyle.Fill };
            panelAcc = new TableLayoutPanel { Dock = DockStyle.Fill };
            //var db =  DatabaseHandler.Singleton ;
            //List<Musteri> temp;
            if (tb == null)
            {
                //tb = new TextBox { Multiline = true, Dock = DockStyle.Fill };
                /*temp = db.GetMusteris();
                foreach(var i in temp)
                {
                                tb = new LabelCustom { Text = i.ad+" "+i.soyad,Name = i.musteriID.ToString(), Dock = DockStyle.Fill };
                                tb.Click += new EventHandler(btnClick);
                                panelAcc.Controls.Add(tb);
                
                }*/



            }

            fp1.Controls.Add(panelAcc);
            return fp1;
        }
        private static void btnClick3(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = (Button)sender;
            }
        }
        private static void btnClick(object sender, EventArgs e)
        {
            if (sender is LabelCustom)
            {
                LabelCustom button = (LabelCustom)sender;
                Form3.label1.Name = button.Name;
                Form3.label1.Text = button.Text;
            }

        }

        private class UC : UserControl
        { //FlowLayoutPanel {
            public override Size GetPreferredSize(Size proposedSize)
            {
                Control c = Controls[0];
                Padding m = c.Margin;
                Padding p = Padding;
                Size s = c.PreferredSize;
                s.Height += (m.Vertical + p.Vertical);
                s.Width += (m.Horizontal + p.Horizontal);
                return s;
            }
        }
    }
}


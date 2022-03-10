using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Opulos.Core.UI;
using RandevuSistemi;
using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp6.model;
using WindowsFormsApp6.src;

namespace WindowsFormsApp6
{
    public partial class Form3 : MetroForm
    {
        public static Label label1 = new Label();
        //private List<Odeme> Odemes;
        string[] alfabe = new string[] { "A", "B", "C", "Ç", "D", "E", "F", "G", "H", "İ", "I", "J", "K", "L", "M", "N", "O", "Ö", "P", "R", "S", "Ş", "T", "U", "Ü", "V", "Y", "Z" };
        OdemeMenu odemeMenu;
        RandevuMenu1 randevuMenu;

        Musteri Musteri;
        List<Epilasyon> Epilasyons;
        List<Seans> Seans;
        List<Taksit> Taksits;
        List<SeansEpilasyonMap> SeansEpilasyons;
        List<TaksitEpilasyonMap> TaksitEpilasyons;


        public Form3()
        {
            label1.Hide();
            label1.TextChanged += new EventHandler(metroLabel3_TextChanged);
            InitializeComponent();


            metroTabControl2.SelectedIndexChanged += new EventHandler(metroTabControl2_SelectedIndexChanged);
            metroTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
            metroGrid1.Columns["Randevudate"].DefaultCellStyle.Format = "dd.MM.yyyy";
            metroGrid1.Sort(metroGrid1.Columns["Randevudate"], System.ComponentModel.ListSortDirection.Descending);
            metroGrid3.Columns["OdemeTarih"].DefaultCellStyle.Format = "dd.MM.yyyy";
            foreach (var i in alfabe)
            {
                MetroTabPage ekle = new MetroTabPage();
                ekle.AllowDrop = true;
                ekle.AutoScroll = true;
                ekle.BackColor = System.Drawing.SystemColors.ActiveBorder;
                ekle.HorizontalScrollbar = true;
                ekle.HorizontalScrollbarBarColor = true;
                ekle.HorizontalScrollbarHighlightOnWheel = false;
                ekle.HorizontalScrollbarSize = 10;
                ekle.Location = new System.Drawing.Point(4, 19);
                ekle.Name = i;
                ekle.Size = new System.Drawing.Size(212, 485);
                ekle.Text = i;
                ekle.VerticalScrollbar = true;
                ekle.VerticalScrollbarBarColor = true;
                ekle.VerticalScrollbarHighlightOnWheel = false;
                ekle.VerticalScrollbarSize = 10;
                ekle.Style = MetroColorStyle.Magenta;
                ekle.Theme = MetroThemeStyle.Light;
                ekle.UseStyleColors = true;
                this.metroTabControl2.Controls.Add(ekle);
                odemeMenu = new OdemeMenu();
                this.metroTabPage2.Controls.Add(this.odemeMenu);
                this.odemeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
                this.odemeMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.odemeMenu.Location = new System.Drawing.Point(4, 4);
                this.odemeMenu.Name = "odemeMenu1";
                this.odemeMenu.Size = new System.Drawing.Size(232, 456);
                this.odemeMenu.TabIndex = 2;
                randevuMenu = new RandevuMenu1();
                this.metroTabPage1.Controls.Add(this.randevuMenu);
                this.randevuMenu.AutoSize = true;
                this.randevuMenu.Dock = System.Windows.Forms.DockStyle.Fill;
                this.randevuMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.randevuMenu.ForeColor = System.Drawing.Color.DodgerBlue;
                this.randevuMenu.Location = new System.Drawing.Point(4, 4);
                this.randevuMenu.Name = "randevuMenu";
                this.randevuMenu.Size = new System.Drawing.Size(232, 456);
                this.randevuMenu.TabIndex = 2;
            }
        }
        DataTable tablo = new DataTable();


        private void labelButtonCustom1_Click(object sender, EventArgs e)
        {
            MusteriEkle ekle = new MusteriEkle();
            ekle.Show();
        }

        private void labelButtonCustom2_Click(object sender, EventArgs e)
        {

            if (Kontrol())
            {
                LabelButtonCustom label = (LabelButtonCustom)sender;
                RandevuEkle ekle = new RandevuEkle();
                ekle.Show();

            }
            else
            {

                string message = "Lütfen Müşteri Seçtikten Sonra Tekrar Deneyiniz";
                string caption = "Müşteri Seçilmedi";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MetroMessageBox.Show(Owner, message: message, caption, buttons);

            }

        }

        private bool Kontrol()
        {
            try
            {
                Convert.ToInt32(Form3.label1.Name);
                return true;
            }
            catch
            {

                return false;
            }

        }
        private void labelButtonCustom5_Click(object sender, EventArgs e)
        {
            if (Kontrol())
            {
                MusteriDuzenle duzenle = new MusteriDuzenle();
                duzenle.Show();

            }
            else
            {

                string message = "Lütfen Müşteri Seçtikten Sonra Tekrar Deneyiniz";
                string caption = "Müşteri Seçilmedi";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MetroMessageBox.Show(Owner, message: message, caption, buttons, 120);

            }
        }

        private void cihazEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cihaz cihaz = new Cihaz();
            cihaz.Show();
        }

        private void cihazTipiEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CihazTipi cihaz = new CihazTipi();
            cihaz.Show();
        }




        private void metroLabel3_TextChanged(object sender, EventArgs e)
        {
            this.metroTabPage1.Controls.Clear();
            this.metroTabPage2.Controls.Clear();
            randevuMenu = new RandevuMenu1();
            this.metroTabPage1.Controls.Add(this.randevuMenu);
            this.randevuMenu.AutoSize = true;
            this.randevuMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.randevuMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.randevuMenu.ForeColor = System.Drawing.Color.DodgerBlue;
            this.randevuMenu.Location = new System.Drawing.Point(4, 4);
            this.randevuMenu.Name = "randevuMenu";
            this.randevuMenu.Size = new System.Drawing.Size(232, 456);
            this.randevuMenu.TabIndex = 2;

            odemeMenu = new OdemeMenu();
            this.metroTabPage2.Controls.Add(this.odemeMenu);
            this.odemeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.odemeMenu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.odemeMenu.Location = new System.Drawing.Point(4, 4);
            this.odemeMenu.Name = "odemeMenu1";
            this.odemeMenu.Size = new System.Drawing.Size(232, 456);
            this.odemeMenu.TabIndex = 2;
            try
            {

                DatabaseHandler db = DatabaseHandler.Singleton;
                Musteri = db.GetMusteriByID(Convert.ToInt32(label1.Name));
                metroLabel1.Text = Musteri.ad + " " + Musteri.soyad;
                metroLabel3.Text = Musteri.telefon;
                metroGrid1.Rows.Clear();
                metroGrid3.Rows.Clear();
                Seans = db.GetSeansByMustriID(Musteri.musteriID);
                Taksits = db.GetTaksitsByMusteriID(Musteri.musteriID);
                Epilasyons = db.GetEpilasyonByMusteriID(Musteri.musteriID);
                SeansEpilasyons = new List<SeansEpilasyonMap>();
                foreach (var i in Epilasyons)
                {
                    SeansEpilasyons.AddRange(db.GetSeansEpilasyonMapsByEpilasyonID(i.epilasyonID));

                }
                TaksitEpilasyons = new List<TaksitEpilasyonMap>();
                foreach (var i in Epilasyons)
                {
                    TaksitEpilasyons.AddRange(db.GetTaksitEpilasyonMapsByEpilasyonID(i.epilasyonID));
                }
                foreach (var i in Seans)
                {
                    model.Cihaz cihaz = db.GetCihazByID(i.cihazID);
                    object[] row = new object[] { i.seansBaslangicTarihi.ToString("dd.MM.yyy"),( i.isChooseSeansTime? i.seansBaslangicTarihi.ToShortTimeString():"Saat Seçilmedi")
                        , cihaz.cihazAdi,i.isCompleted };

                    metroGrid1.Rows.Add(row);
                    metroGrid1.Rows[metroGrid1.Rows.Count - 2].Tag = i.seansID;
                    foreach (DataGridViewCell a in metroGrid1.Rows[metroGrid1.Rows.Count - 2].Cells)
                    {
                        a.ToolTipText = "Vücut Bölgeleri ";
                    }

                    foreach (var j in db.GetSeansVucutbolgeMapsBySeansID(i.seansID))
                    {
                        foreach (DataGridViewCell k in metroGrid1.Rows[metroGrid1.Rows.Count - 2].Cells)
                            k.ToolTipText += "\n   " + db.GetVucutBolgeByID(j.vucutBolgeID).vucutBolge + " " +
                                j.seansNo.ToString() + ".Seans";
                    }
                    //metroGrid1.Rows[metroGrid1.Rows.Count - 2]


                }
                foreach (var i in Taksits)
                {
                    object[] row = new object[] {i.odemeTarihi.ToString("dd.MM.yyy"),
                        Epilasyons.Find(x => x.epilasyonID==(TaksitEpilasyons.Find(y => y.taksitID == i.taksitID).epilasyonID)).toplamTutar.ToString(),
                        TaksitEpilasyons.Find(y => y.taksitID==i.taksitID).kalanTutar.ToString(),
                        i.ucret.ToString(),i.aciklama,i.isComleted };
                    metroGrid3.Rows.Add(row);
                    metroGrid3.Rows[metroGrid3.Rows.Count - 2].Tag = i.taksitID;

                }
            }
            catch
            {

            }
        }

        private void randevuListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandevuListele listele = new RandevuListele();
            listele.Show();
        }


        private void müşteriDüzenlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusteriDuzenle duzenle = new MusteriDuzenle();
            duzenle.Show();
        }

        private void vucutBölgesiEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VucutBolgeEkleme ekleme = new VucutBolgeEkleme();
            ekleme.Show();
        }

        private void düzenlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VucutBolgeDuzenleme duzenleme = new VucutBolgeDuzenleme();
            duzenleme.Show();
        }



        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroTabControl2.SelectedIndex = 0;
            metroTabControl2.SelectedTab.Controls.Clear();
            if (metroTextBox1.Text.Equals("") || metroTextBox1.Text.Equals(" ")) return;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                List<Musteri> musteris = new List<Musteri>();
                string tempk = metroTextBox1.Text.ToUpper();
                musteris.AddRange(db.GetMusteriByArama(tempk));
                for (int i = 0; i < musteris.Count; i++)
                {
                    MetroButton temp = new MetroButton { Text = musteris[i].ad + " " + musteris[i].soyad, Name = musteris[i].musteriID.ToString(), Location = new Point(10, 0 + (i * 25)), Size = new Size(177, 20), FontSize = MetroButtonSize.Tall, FontWeight = MetroButtonWeight.Regular };
                    temp.Click += new EventHandler(btnClick);
                    metroTabControl2.SelectedTab.Controls.Add(temp);
                }
            }
            catch
            {

            }
        }




        private void metroGrid3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = e.RowIndex;
            if (rowIndex == -1) return;
            MetroGrid tempG = (MetroGrid)sender;
            DataGridViewRow row = tempG.Rows[rowIndex];
            if (row.Tag == null) return;
            Taksit temp;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                temp = db.GetTaksitByID(Convert.ToInt32(row.Tag));
            }
            catch
            {
                temp = new Taksit();
            }
            OdemeDuzenle odeme = new OdemeDuzenle(temp);
            odeme.Show();
        }

        private void btnClick(object sender, EventArgs e)
        {
            if (sender is MetroButton)
            {
                MetroButton button = (MetroButton)sender;
                Form3.label1.Name = button.Name;

                if (Form3.label1.Text.Length < 20)
                    Form3.label1.Text += "+";
                else
                    Form3.label1.Text = "+";

            }

        }
        private void metroTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroTabControl2.SelectedIndex == 0) return;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                List<Musteri> musteris = db.GetMusteriByFirstHarf(metroTabControl2.SelectedTab.Name);
                metroTabControl2.SelectedTab.Controls.Clear();
                for (int i = 0; i < musteris.Count; i++)
                {
                    MetroButton temp = new MetroButton { Text = musteris[i].ad + " " + musteris[i].soyad, Name = musteris[i].musteriID.ToString(), Location = new Point(10, 0 + (i * 25)), Size = new Size(177, 20), FontSize = MetroButtonSize.Tall, FontWeight = MetroButtonWeight.Regular };
                    temp.Click += new EventHandler(btnClick);
                    metroTabControl2.SelectedTab.Controls.Add(temp);
                }
            }
            catch
            {

            }
        }

        private void metroGrid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex == -1) return;
            MetroGrid tempG = (MetroGrid)sender;
            DataGridViewRow row = tempG.Rows[rowIndex];
            if (row.Tag == null) return;
            Seans temp;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                temp = db.GetSeansByID(Convert.ToInt32(row.Tag));
            }
            catch
            {
                temp = new Seans();
            }
            RandevuDuzenle randevu = new RandevuDuzenle(temp);
            randevu.Show();

        }


        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)

            {
                // Then Do your Thang

                if (metroTextBox1.Text.Equals("") || metroTextBox1.Text.Equals(" ")) return;
                metroTabControl2.SelectedIndex = 0;
                metroTabControl2.SelectedTab.Controls.Clear();
                try
                {
                    DatabaseHandler db = DatabaseHandler.Singleton;
                    List<Musteri> musteris = new List<Musteri>();
                    string tempk = metroTextBox1.Text.ToUpper();
                    musteris.AddRange(db.GetMusteriByArama(tempk));
                    for (int i = 0; i < musteris.Count; i++)
                    {
                        MetroButton temp = new MetroButton { Text = musteris[i].ad + " " + musteris[i].soyad, Name = musteris[i].musteriID.ToString(), Location = new Point(10, 0 + (i * 25)), Size = new Size(177, 20), FontSize = MetroButtonSize.Tall, FontWeight = MetroButtonWeight.Regular };
                        temp.Click += new EventHandler(btnClick);
                        metroTabControl2.SelectedTab.Controls.Add(temp);
                    }
                }
                catch
                {

                }
            }
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }


        private void labelButtonCustom3_Click_1(object sender, EventArgs e)
        {
            RandevuListele randevu = new RandevuListele();
            randevu.Show();
        }

        private void labelButtonCustom4_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void labelButtonCustom6_Click(object sender, EventArgs e)
        {
            Son3AyGelmeyenler son3 = new Son3AyGelmeyenler();
            son3.Show();
        }

        private void labelButtonCustom7_Click(object sender, EventArgs e)
        {
            OdemeGecikenler odeme = new OdemeGecikenler();
            odeme.Show();
        }

    }

    internal class RandevuMenu1 : AccordionPanel
    {


        public RandevuMenu1()
        {
            List<CheckBoxCustom> checkboxList = new List<CheckBoxCustom>();
            DateTime date = DateTime.Today;

            //accNested.AutoSize = false;
            Padding? margin = new Padding(15);
            Accordion accParent = acc;
            Accordion accNested = new Accordion() { Dock = DockStyle.Fill };
            TableLayoutPanel panelAcc = null;
            for (int i = 0; i < 30; i++)
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
            try
            {

                DatabaseHandler db = DatabaseHandler.Singleton;
                List<Seans> temp;
                if (tb == null)
                {
                    temp = db.GetSeansByDate(date, date.AddDays(1));
                    Musteri musteri;
                    foreach (var i in temp)
                    {
                        musteri = db.GetMusteriByID(i.musteriID);
                        tb = new LabelCustom
                        {
                            Text = (i.isChooseSeansTime ? i.seansBaslangicTarihi.ToShortTimeString() : "") + " " + db.GetCihazByID(i.cihazID).cihazAdi + " - " +
                            musteri.ad + " " + musteri.soyad,
                            Name = i.musteriID.ToString() + "." + i.seansID,
                            Dock = DockStyle.Fill
                        };
                        tb.Click += new EventHandler(btnClick);
                        SeansEpilasyonMap seansEpilasyon = db.GetSeansEpilasyonMapsBySeansID(i.seansID);
                        Epilasyon epilasyon = db.GetEpilasyonByID(seansEpilasyon.epilasyonID);
                        MetroToolTip toolTip = new MetroToolTip();
                        string tempVB = "Ödeme Ayrıntıları\n  " + musteri.ad + " " + musteri.soyad
                            + "\n  Cihaz : " + db.GetCihazByID(i.cihazID).cihazAdi
                            + "\n  Seans Tarihi : " + (i.isChooseSeansTime ? i.seansBaslangicTarihi.ToShortTimeString() : "Saat Seçilmedi")
                            + "\n  Vücut Bölgeleri ";
                        foreach (var j in db.GetSeansVucutbolgeMapsBySeansID(i.seansID))
                        {
                            tempVB += "\n  " + db.GetVucutBolgeByID(j.vucutBolgeID).vucutBolge + j.seansNo.ToString()
                                + ".Seans";
                        }
                        tempVB += "\n   ";

                        toolTip.SetToolTip(tb, tempVB + "\n"

                            );
                        panelAcc.Controls.Add(tb);

                    }



                }
            }
            catch
            {

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
                string[] temp = button.Name.ToString().Split('.');
                Form3.label1.Name = temp[0];
                Form3.label1.Text = button.Text;
                Seans seans;
                try
                {
                    DatabaseHandler db = DatabaseHandler.Singleton;
                    seans = db.GetSeansByID(Convert.ToInt32(temp[1]));
                    RandevuDuzenle randevu = RandevuDuzenle(seans);
                    randevu.Show();
                }
                catch
                {
                    seans = new Seans();
                }
            }

        }

        private static RandevuDuzenle RandevuDuzenle(Seans seans)
        {
            return new RandevuDuzenle(seans);
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

    internal class OdemeMenu : AccordionPanel
    {


        public OdemeMenu()
        {
            List<CheckBoxCustom> checkboxList = new List<CheckBoxCustom>();
            DateTime date = DateTime.Today;

            //accNested.AutoSize = false;
            Padding? margin = new Padding(15);
            Accordion accParent = acc;
            Accordion accNested = new Accordion() { Dock = DockStyle.Fill };
            TableLayoutPanel panelAcc = null;
            for (int i = 0; i < 30; i++)
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
            try
            {

                DatabaseHandler db = DatabaseHandler.Singleton;
                Musteri temp;
                List<Taksit> taksits;
                if (tb == null)
                {
                    //tb = new TextBox { Multiline = true, Dock = DockStyle.Fill };
                    taksits = db.GetTaksitByDate(date, date.AddDays(1));
                    foreach (var i in taksits)
                    {
                        if (i.isComleted != true)
                        {

                            temp = db.GetMusteriByID(i.musteriID);
                            tb = new LabelCustom { Text = temp.ad + " " + temp.soyad + " " + i.aciklama, Name = temp.musteriID.ToString() + "." + i.taksitID.ToString(), Dock = DockStyle.Fill };
                            TaksitEpilasyonMap taksitEpilasyon = db.GetTaksitEpilasyonMapsByTaksitID(i.taksitID);
                            Epilasyon epilasyon = db.GetEpilasyonByID(taksitEpilasyon.epilasyonID);
                            MetroToolTip toolTip = new MetroToolTip();
                            toolTip.SetToolTip(tb, "Ödeme Ayrıntıları\n  " + temp.ad + " " + temp.soyad
                                + "\n  Toplam Tutar : " + epilasyon.toplamTutar.ToString()
                                + "\n  Kalan Tutar : " + taksitEpilasyon.kalanTutar.ToString() + "\n"

                                );
                            tb.Click += new EventHandler(btnClick);

                            panelAcc.Controls.Add(tb);
                        }

                    }



                }
            }
            catch
            {

            }
            fp1.Controls.Add(panelAcc);
            return fp1;
        }
        private static void btnClick(object sender, EventArgs e)
        {
            if (sender is LabelCustom)
            {
                LabelCustom button = (LabelCustom)sender;
                string[] temp = button.Name.Split('.');
                Form3.label1.Name = temp[0];
                Form3.label1.Text = button.Text;
                try
                {
                    DatabaseHandler db = DatabaseHandler.Singleton;
                    OdemeDuzenle odeme = new OdemeDuzenle(db.GetTaksitByID(Convert.ToInt32(temp[1])));
                    odeme.Show();
                }
                catch
                {

                }

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

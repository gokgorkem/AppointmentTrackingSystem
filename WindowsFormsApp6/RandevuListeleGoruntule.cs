using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp6;
using WindowsFormsApp6.model;

namespace RandevuSistemi
{
    public partial class RandevuListeleGoruntule : MetroForm
    {
        private static Seans seans1;
        private static bool saatSecimi;
        List<VucutBolge> vucutBolges;
        List<SeansVucutbolgeMap> seansVucutbolge;
        Epilasyon epilasyon;
        Musteri musteri;
        internal static Seans Seans1 { get => seans1; set => seans1 = value; }
        public static Label label;
        public RandevuListeleGoruntule(Seans seans)
        {
            label = new Label();
            label.Hide();
            label.TextChanged += new EventHandler(label_tex);
            InitializeComponent();
            seans1 = seans;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                vucutBolges = new List<VucutBolge>();
                musteri = db.GetMusteriByID(seans1.musteriID);
                epilasyon = db.GetEpilasyonByID(
                    db.GetSeansEpilasyonMapsBySeansID(seans1.seansID).epilasyonID);
                seansVucutbolge = db.GetSeansVucutbolgeMapsBySeansID(seans1.seansID);
                for (int i = 0; i < seansVucutbolge.Count; i++)
                {
                    MetroLabel label = new MetroLabel();
                    var vb = db.GetVucutBolgeByID(seansVucutbolge[i].vucutBolgeID);
                    if (i % 2 == 0)
                    {
                        label.Location = new System.Drawing.Point(558, 168 + ((i / 2) * 35));
                        label.Name = vb.vucutBolgeID.ToString();
                        label.Text = vb.vucutBolge + " " +
                            seansVucutbolge[i].seansNo.ToString() + ".Seans";
                    }
                    else
                    {
                        label.Location = new System.Drawing.Point(689, 168 + ((i / 2) * 35));
                        label.Name = vb.vucutBolgeID.ToString();
                        label.Text = vb.vucutBolge + " " +
                            seansVucutbolge[i].seansNo.ToString() + ".Seans";
                    }
                    label.AutoSize = true;
                    label.Style = MetroFramework.MetroColorStyle.Black;
                    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    label.Theme = MetroFramework.MetroThemeStyle.Light;
                    label.FontWeight = MetroLabelWeight.Regular;
                    label.UseStyleColors = true;
                    vucutBolges.Add(vb);
                    this.Controls.Add(label);
                }
                metroLabel2.Text = musteri.ad + " " + musteri.soyad;
                metroLabel4.Text = musteri.telefon;
                metroLabel5.Text = db.GetCihazByID(seans1.cihazID).cihazAdi;
                metroButton3.Text = seans1.isChooseSeansTime ? Seans1.seansBaslangicTarihi.ToShortTimeString().ToString() : "Saat Belirlenmedi";
                metroCheckBox1.Checked = seans1.isCompleted;
                metroDateTime1.Value = seans1.seansBaslangicTarihi;
                metroLabel7.Text = db.GetSeansByCihazID(seans1.cihazID, metroDateTime1.Value, metroDateTime1.Value.AddDays(1)).Count.ToString();

            }
            catch
            {

            }
        }

        private void label_tex(object sender, EventArgs e)
        {
            metroButton3.Text = Seans1.seansBaslangicTarihi.ToShortTimeString();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

            Seans1.isCompleted = metroCheckBox1.Checked;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                db.UpdateDB(Seans1);
            }
            catch { }
            RandevuListele.label.Text += "+";
            this.Close();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {

            RandevuSaatListeleme randevuSaat = new RandevuSaatListeleme(Convert.ToDateTime(metroDateTime1.Text), seans1.cihazID);
            randevuSaat.Show();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                metroLabel7.Text = db.GetSeansByCihazID(seans1.cihazID, metroDateTime1.Value, metroDateTime1.Value.AddDays(1)).Count.ToString();
            }
            catch { }

        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {

            this.metroLabel7.Text = "KONTROL EDİLMEDİ";
        }
    }
}

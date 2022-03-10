using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Microsoft.EntityFrameworkCore.Internal;
using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class RandevuEkle : MetroForm
    {
        public static DateTime startTimes;
        public static DateTime endTimes;
        public static bool saatSecimi;

        model.Musteri musteri;
        Epilasyon epilasyon;
        Seans seansEkle;
        Taksit taksitEkle;
        TaksitEpilasyonMap taksitEpilasyon;
        SeansEpilasyonMap seansEpilasyon;
        model.Cihaz cihaz;

        List<MetroCheckBox> vucut;
        List<model.Cihaz> cihazs;
        List<model.VucutBolge> vucutBolges;
        bool listele = false;
        public RandevuEkle()
        {
            InitializeComponent();
            metroPanel1.Hide();
            vucut = new List<MetroCheckBox>();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                musteri = db.GetMusteriByID(Convert.ToInt32(Form3.label1.Name));
                cihazs = db.GetCihazs();
                foreach (var i in cihazs)
                {
                    metroComboBox4.Items.Add(i.cihazAdi);

                }
                vucutBolges = db.GetVucutBolges();
                for (int i = 0; i < vucutBolges.Count; i++)
                {
                    MetroCheckBox temp = new MetroCheckBox();
                    temp.AutoSize = true;
                    temp.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
                    temp.Location = new System.Drawing.Point(24, 150 + (i * 25));
                    temp.Name = vucutBolges[i].vucutBolgeID.ToString();
                    temp.Size = new System.Drawing.Size(129, 19);
                    temp.Text = vucutBolges[i].vucutBolge.ToString();
                    temp.Style = MetroFramework.MetroColorStyle.Black;
                    temp.Theme = MetroFramework.MetroThemeStyle.Light;
                    temp.FontWeight = MetroCheckBoxWeight.Regular;
                    temp.UseSelectable = true;
                    vucut.Add(temp);
                    this.Controls.Add(temp);
                }
                musteriSoyadi.Text = musteri.ad + " " + musteri.soyad;

            }
            catch
            {

            }
        }


        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (!listele)
            {

                string messageT = "Lütfen Randevu Listele Butonuna Tıklayınız";
                String captionT = "Seans Özellikleri Seçilmedi";
                DialogResult resultT = MetroMessageBox.Show(Owner, messageT, captionT);
                return;
            }
            seansEkle = new Seans()
            {
                musteriID = musteri.musteriID,
                cihazID = cihaz.cihazID,
                seansBaslangicTarihi = Convert.ToDateTime(Convert.ToDateTime(this.metroDateTime1.Text).ToShortDateString()),
                seansBitisTarihi = Convert.ToDateTime(Convert.ToDateTime(this.metroDateTime1.Text).ToShortDateString()),
                isChooseSeansTime = saatSecimi
            };
            if (saatSecimi)
            {
                seansEkle.seansBaslangicTarihi = startTimes;
                seansEkle.seansBitisTarihi = endTimes;
            }
            taksitEkle = new Taksit()
            {
                musteriID = musteri.musteriID,
                odemeTarihi = Convert.ToDateTime(Convert.ToDateTime(this.metroDateTime1.Text).ToShortDateString()),
                ucret = 0,
                aciklama = "1.Ödeme"

            };
            epilasyon = new Epilasyon()
            {
                musteriID = musteri.musteriID,
                cihazID = cihaz.cihazID,
                seansSayisi = Convert.ToByte(metroComboBox2.Text),
                toplamTutar = (float)Convert.ToDouble(metroTextBox1.Text.ToString().Replace(",", "."))

            };
            taksitEpilasyon = new TaksitEpilasyonMap()
            {
                kalanTutar = (float)Convert.ToDouble(metroTextBox1.Text.ToString().Replace(",", "."))
            };
            seansEpilasyon = new SeansEpilasyonMap();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;

                epilasyon.epilasyonID = db.InsertDB(epilasyon);

                seansEkle.seansID = db.InsertDB(seansEkle);

                taksitEkle.taksitID = db.InsertDB(taksitEkle);

                taksitEpilasyon.taksitID = taksitEkle.taksitID;
                taksitEpilasyon.epilasyonID = epilasyon.epilasyonID;
                taksitEpilasyon.taksitEpilasyonMapID = db.InsertDB(taksitEpilasyon);

                seansEpilasyon.seansID = seansEkle.seansID;
                seansEpilasyon.epilasyonID = epilasyon.epilasyonID;
                seansEpilasyon.seansEpilasyonMapID = db.InsertDB(seansEpilasyon);
                foreach (var i in vucut)
                {
                    if (i.Checked)
                    {
                        SeansVucutbolgeMap seansVucutbolgeMap = new SeansVucutbolgeMap()
                        {
                            seansID = seansEkle.seansID,
                            vucutBolgeID = Convert.ToInt16(i.Name),
                            seansNo = 1
                        };
                        db.InsertDB(seansVucutbolgeMap);
                    }
                }

            }
            catch
            {

            }
            Form3.label1.Text += "+";
            this.Close();

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void metroButton_Click(object sender, EventArgs e)
        {
            MetroButton button = (MetroButton)sender;
            DateTime date = Convert.ToDateTime(metroDateTime1.Text);
            RandevuSaat randevuSaat = new RandevuSaat(date, cihaz.cihazID);
            randevuSaat.Show();
        }



        private void metroButton3_Click(object sender, EventArgs e)
        {
            string messageT;
            string captionT;
            if (metroComboBox2.SelectedIndex == -1)
            {
                messageT = "Lütfen Seans Sayısını Seçiniz";
                captionT = "Seans Sayısını Seçiniz ";
                DialogResult resultT = MetroMessageBox.Show(Owner, messageT, captionT);
                return;
            }
            if (metroComboBox4.SelectedIndex == -1)
            {
                messageT = "Lütfen Randevu Cihazını Seçiniz";
                captionT = "Cihaz Seçiniz ";
                DialogResult resultT = MetroMessageBox.Show(Owner, messageT, captionT);
                return;
            }
            if (!vucut.Any(x => x.Checked == true))
            {
                messageT = "En Az Bir Bölge Seçiniz";
                captionT = "Vucut Bölgesi Seçiniz";
                DialogResult resultT = MetroMessageBox.Show(Owner, messageT, captionT);
                metroPanel1.Hide();
                return;
            }
            string tutar = this.metroTextBox1.Text;

            if (metroTextBox1.Text.Count() == 0 || metroTextBox1.Text.Equals("0"))
            {
                messageT = "Lütfen Paket Tutarını Giriniz";
                captionT = "Tutar Giriniz";
                DialogResult resultT = MetroMessageBox.Show(Owner, messageT, captionT);
                return;
            }
            listele = true;
            metroPanel1.Show();
            string seans = this.metroComboBox2.Text;
            int cihaz = this.metroComboBox4.SelectedIndex;
            this.cihaz = cihazs[cihaz];

            startTimes = new DateTime();
            endTimes = new DateTime();

            this.Controls.Clear();
            this.InitializeComponent();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                foreach (var i in cihazs)
                {
                    metroComboBox4.Items.Add(i.cihazAdi);
                }
            }
            catch
            {

            }
            foreach (var i in vucut)
            {
                this.Controls.Add(i);
            }
            this.metroComboBox2.Text = seans;
            this.metroComboBox4.SelectedIndex = cihaz;
            metroTextBox1.Text = tutar;
            musteriSoyadi.Text = musteri.ad + " " + musteri.soyad;

        }

        private void RandevuEkle_Load(object sender, EventArgs e)
        {

        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroLabel7_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as MetroTextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            this.metroLabel2.Text = "KONTROL EDİLMEDİ";
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                metroLabel2.Text = db.GetSeansByCihazID(cihaz.cihazID,metroDateTime1.Value,metroDateTime1.Value.AddDays(1)).Count.ToString();
            }
            catch { }
        }
    }
}

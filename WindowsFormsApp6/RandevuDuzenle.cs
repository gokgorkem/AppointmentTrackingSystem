using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using RandevuSistemi;
using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class RandevuDuzenle : MetroForm
    {
        private static Seans seans1;
        private static bool saatSecimi;
        List<VucutBolge> vucutBolges;
        List<SeansVucutbolgeMap> seansVucutbolge;
        Epilasyon epilasyon;
        Musteri musteri;
        internal static Seans Seans1 { get => seans1; set => seans1 = value; }
        public static Label label;
        public RandevuDuzenle(Seans seans)
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
                if (!seans1.isChooseSeansTime)
                {
                    seans1.seansBaslangicTarihi = Convert.ToDateTime(metroDateTime1.Value.ToShortDateString());
                    seans1.seansBitisTarihi = Convert.ToDateTime(metroDateTime1.Value.ToShortDateString());

                }
                db.UpdateDB(Seans1);
                if (seans1.isCompleted
                    && db.GetOSeansSSeansMapsByOncekiSeansID(seans1.seansID) == null)
                {
                    List<SeansVucutbolgeMap> tempSVB = new List<SeansVucutbolgeMap>();
                    foreach (var i in seansVucutbolge)
                    {
                        if (i.seansNo < epilasyon.seansSayisi)
                        {
                            tempSVB.Add(new SeansVucutbolgeMap()
                            {
                                vucutBolgeID = i.vucutBolgeID,
                                seansNo = Convert.ToByte(i.seansNo + 1)
                            });
                        }
                    }
                    if (tempSVB.Count > 0)
                    {
                        Seans tempSeans = new Seans()
                        {
                            musteriID = seans1.musteriID,
                            cihazID = seans1.cihazID,
                            seansBaslangicTarihi = seans1.seansBaslangicTarihi,
                            seansBitisTarihi = seans1.seansBitisTarihi
                        };
                        tempSeans.seansID = db.InsertDB(tempSeans);
                        db.InsertDB(new OSeansSSeansMap()
                        {
                            oncekiSeansID = seans1.seansID,
                            sonrakiSeansID = tempSeans.seansID
                        });
                        SeansEpilasyonMap seansEpilasyon = new SeansEpilasyonMap()
                        {
                            seansID = tempSeans.seansID,
                            epilasyonID = epilasyon.epilasyonID
                        };
                        db.InsertDB(seansEpilasyon);
                        foreach (var i in tempSVB)
                        {
                            i.seansID = tempSeans.seansID;
                            db.InsertDB(i);
                        }
                        RandevuDuzenle randevu = new RandevuDuzenle(db.GetSeansByID(tempSeans.seansID));
                        randevu.Show();
                    }
                }
                else if (!seans1.isCompleted
                   && db.GetOSeansSSeansMapsByOncekiSeansID(seans1.seansID) == null)
                {
                    foreach (var i in db.GetTaksitEpilasyonMapsByEpilasyonID(epilasyon.epilasyonID))
                    {
                        if (db.GetOTaksitSTaksitMapByOncekiTaksitID(i.taksitID) == null)
                        {
                            Taksit taksitTemp = db.GetTaksitByID(i.taksitID);
                            taksitTemp.odemeTarihi = seans1.seansBaslangicTarihi;
                            db.UpdateDB(taksitTemp);
                        }
                    }
                }
            }
            catch { }
            Form3.label1.Text += "+";
            this.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }


        private void metroButton3_Click(object sender, EventArgs e)
        {
            RandevuSaatDuzenleme randevuSaat = new RandevuSaatDuzenleme(Convert.ToDateTime(metroDateTime1.Text), seans1.cihazID);
            randevuSaat.Show();
        }

        private void metroLabel7_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel9_Click(object sender, EventArgs e)
        {

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            PaketDuzenleme paket = new PaketDuzenleme(epilasyon);
            paket.Show();
            this.Close();
        }

        private void metroDateTime1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            this.metroLabel7.Text = "KONTROL EDİLMEDİ";

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
    }
}

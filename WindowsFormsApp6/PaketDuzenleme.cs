using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp6;
using WindowsFormsApp6.model;

namespace RandevuSistemi
{
    public partial class PaketDuzenleme : MetroForm
    {
        Epilasyon Epilasyon;
        List<Seans> Seans;
        List<Taksit> Taksits;
        Musteri Musteri;
        WindowsFormsApp6.model.Cihaz Cihaz;
        List<WindowsFormsApp6.model.Cihaz> Cihazs;
        List<VucutBolge> VucutBolges;
        List<SeansVucutbolgeMap> SeansVucutBolgesMax;
        List<SeansVucutbolgeMap> SeansVucutbolges;
        List<SeansEpilasyonMap> SeansEpilasyons;
        List<TaksitEpilasyonMap> TaksitEpilasyons;

        List<MetroCheckBox> mCBVucutBolges;
        public PaketDuzenleme(Epilasyon epilasyon)
        {
            InitializeComponent();
            Epilasyon = epilasyon;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                Cihazs = db.GetCihazs();
                VucutBolges = db.GetVucutBolges();
                Musteri = db.GetMusteriByID(Epilasyon.musteriID);
                SeansEpilasyons = db.GetSeansEpilasyonMapsByEpilasyonID(Epilasyon.epilasyonID);
                Seans = new List<Seans>();
                SeansVucutbolges = new List<SeansVucutbolgeMap>();
                foreach (var i in SeansEpilasyons)
                {
                    Seans.Add(db.GetSeansByID(i.seansID));
                    SeansVucutbolges.AddRange(db.GetSeansVucutbolgeMapsBySeansID(i.seansID));
                }
                TaksitEpilasyons = db.GetTaksitEpilasyonMapsByEpilasyonID(Epilasyon.epilasyonID);
                Taksits = new List<Taksit>();
                foreach (var i in TaksitEpilasyons)
                {
                    Taksits.Add(db.GetTaksitByID(i.taksitID));
                }
                SeansVucutBolgesMax = new List<SeansVucutbolgeMap>();
                foreach (var i in VucutBolges)
                {
                    if (SeansVucutbolges.Any(s => s.vucutBolgeID == i.vucutBolgeID))
                    {
                        SeansVucutBolgesMax.Add(SeansVucutbolges.FindAll(x => x.vucutBolgeID == i.vucutBolgeID)
                            .Aggregate((x1, x2) => x1.seansNo > x2.seansNo ? x1 : x2));
                    }

                }
                Cihaz = db.GetCihazByID(Seans[0].cihazID);

                foreach (var i in Seans)
                {
                    object[] row = new object[] { i.seansBaslangicTarihi.ToString("dd.MM.yyy"),( i.isChooseSeansTime? i.seansBaslangicTarihi.ToShortTimeString():"Saat Seçilmedi")
                        , Cihaz.cihazAdi,i.isCompleted };

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


                }

                foreach (var i in Taksits)
                {
                    object[] row = new object[] {i.odemeTarihi.ToString("dd.MM.yyy"),
                        TaksitEpilasyons.Find(y => y.taksitID==i.taksitID).kalanTutar.ToString(),
                        i.ucret.ToString(),i.aciklama,i.isComleted };
                    metroGrid2.Rows.Add(row);
                    metroGrid2.Rows[metroGrid2.Rows.Count - 2].Tag = i.taksitID;

                }
            }
            catch { }
            mCBVucutBolges = new List<MetroCheckBox>();
            for (int i = 0; i < VucutBolges.Count; i++)
            {
                MetroCheckBox temp = new MetroCheckBox();
                temp.Size = new System.Drawing.Size(129, 19);
                temp.Location = new System.Drawing.Point(24, 155 + (i * 25));
                temp.Style = MetroFramework.MetroColorStyle.Black;
                temp.Theme = MetroFramework.MetroThemeStyle.Light;
                temp.FontWeight = MetroCheckBoxWeight.Regular;
                temp.AutoSize = true;
                temp.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
                temp.Name = VucutBolges[i].vucutBolgeID.ToString();
                temp.TabIndex = 9;
                temp.Text = VucutBolges[i].vucutBolge;
                temp.UseSelectable = true;
                if (SeansVucutBolgesMax.Any(s => s.vucutBolgeID == VucutBolges[i].vucutBolgeID))
                {
                    temp.Text += " " + SeansVucutBolgesMax.Find(x => x.vucutBolgeID == VucutBolges[i].vucutBolgeID).seansNo.ToString() + ".Seans";
                    temp.Checked = true;
                }
                Controls.Add(temp);
                mCBVucutBolges.Add(temp);
            }
            metroLabel7.Text = Musteri.ad + " " + Musteri.soyad;
            metroLabel4.Text = Cihaz.cihazAdi;
            metroComboBox1.Text = Epilasyon.seansSayisi.ToString();
            metroTextBox1.Text = Epilasyon.toplamTutar.ToString();


        }

        private void metroLabel3_Click(object sender, System.EventArgs e)
        {

        }

        private void metroGrid2_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void PaketDuzenleme_Load(object sender, System.EventArgs e)
        {

        }

        private void metroGrid1_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void metroTabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void metroLabel5_Click(object sender, System.EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, System.EventArgs e)
        {


            float ucret;
            try
            {
                ucret = (float)Convert.ToDouble(metroTextBox1.Text.ToString().Replace(",", "."));

            }
            catch
            {
                return;
            }
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                Seans tempSeans = Seans.FindLast(x => x.isCompleted == false);
                List<SeansVucutbolgeMap> tempSeansVucutbolges = SeansVucutbolges.FindAll(x => x.seansID == tempSeans.seansID);
                foreach (var i in mCBVucutBolges)
                {
                    if (i.Checked)
                    {
                        if (!tempSeansVucutbolges.Any(x => x.vucutBolgeID == Convert.ToInt16(i.Name)))
                        {
                            SeansVucutbolgeMap tempSeansVucutbolgeDB = new SeansVucutbolgeMap()
                            {
                                seansID = tempSeans.seansID,
                                vucutBolgeID = Convert.ToInt16(i.Name),
                                seansNo = 1
                            };
                            db.InsertDB(tempSeansVucutbolgeDB);
                        }
                    }
                    else if (tempSeansVucutbolges.Any(x => x.vucutBolgeID == Convert.ToInt16(i.Name)))
                    {

                        SeansVucutbolgeMap tempSeansVucutbolgeDB = tempSeansVucutbolges.Find(x => x.vucutBolgeID == Convert.ToInt16(i.Name));
                        tempSeansVucutbolgeDB.isDeleted = true;
                        db.UpdateDB(tempSeansVucutbolgeDB);
                    }
                }
                float fark = ucret - Epilasyon.toplamTutar;
                foreach (var i in TaksitEpilasyons)
                {
                    i.kalanTutar += fark;
                    db.UpdateDB(i);
                }
                Epilasyon.toplamTutar += fark;
                Epilasyon.seansSayisi = Convert.ToByte(metroComboBox1.Text);
                db.UpdateDB(Epilasyon);
            }
            catch { }
            Form3.label1.Text += "+";
            this.Close();
        }

        private void metroButton2_Click(object sender, System.EventArgs e)
        {
            this.Close();
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
    }
}

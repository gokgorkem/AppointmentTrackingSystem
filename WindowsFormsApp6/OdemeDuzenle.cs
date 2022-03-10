using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using RandevuSistemi.model;
using System;
using System.Windows.Forms;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class OdemeDuzenle : MetroForm
    {
        Taksit taksit;
        Epilasyon epilasyon;
        Musteri musteri;
        TaksitEpilasyonMap taksitEpilasyonMap;
        bool kontrol;

        public OdemeDuzenle(Taksit taksit)
        {
            InitializeComponent();
            this.taksit = taksit;
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                musteri = db.GetMusteriByID(taksit.musteriID);
                epilasyon = db.GetEpilasyonByID(db.GetTaksitEpilasyonMapsByTaksitID(taksit.taksitID).epilasyonID);
                taksitEpilasyonMap = db.GetTaksitEpilasyonMapsByTaksitID(taksit.taksitID);
                metroLabel2.Text = musteri.ad + " " + musteri.soyad;
                metroLabel3.Text = musteri.telefon;
                metroLabel4.Text = epilasyon.toplamTutar.ToString();
                metroLabel7.Text = taksitEpilasyonMap.kalanTutar.ToString();
                metroTextBox1.Text = taksit.ucret.ToString();
                metroDateTime1.Value = taksit.odemeTarihi;
                metroCheckBox1.Checked = taksit.isComleted;
                kontrol = taksit.isComleted;
                if (taksit.isComleted)
                    metroCheckBox1.Enabled = false;
            }
            catch
            {

            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            float ucret;
            float oUcret = taksit.ucret;
            try
            {
                ucret = (float)Convert.ToDouble(metroTextBox1.Text.ToString().Replace(",", "."));

            }
            catch
            {
                return;
            }
            if (taksitEpilasyonMap.kalanTutar < (ucret - oUcret))
            {
                string messageT = "Girilen Tutar Kalan Ödemeden Fazla olamaz";
                string captionT = "Tutar Yanlış Tekrar Giriniz ";
                DialogResult resultT = MetroMessageBox.Show(Owner, messageT, captionT);
                return;
            }

            string message = "Müşteri Adı Soyad : " + metroLabel2.Text +
                "\nMüşteri Telefon : " + metroLabel3.Text +
                "\nToplam Tutar : " + metroLabel4.Text +
                "\nKalan Ödeme : " + metroLabel7.Text +
                "\nÖdeme Tutarı : " + metroTextBox1.Text +
                "\nÖdeme Tarihi : " + metroDateTime1.Text +
                "\nÖdeme Durumu : ";
            string caption = "Bu Ödemeyi Kaydetmek İster Misiniz ?";
            message += metroCheckBox1.Checked ? "Ödendi" : "Ödenmedi";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MetroMessageBox.Show(Owner, message, caption, buttons, 200);
            if (result == DialogResult.Yes)
            {
                string[] tempAciklama = taksit.aciklama.Split('.');
                try
                {
                    DatabaseHandler db = DatabaseHandler.Singleton;
                    taksit.odemeTarihi = Convert.ToDateTime(metroDateTime1.Text);
                    taksit.ucret = ucret;
                    taksit.isComleted = metroCheckBox1.Checked;
                    db.UpdateDB(taksit);
                    if (db.GetOTaksitSTaksitMapByOncekiTaksitID(taksit.taksitID) == null && taksitEpilasyonMap.kalanTutar - (ucret - oUcret) == 0)
                    {
                        taksitEpilasyonMap.kalanTutar -= (ucret - oUcret);
                        db.UpdateDB(taksitEpilasyonMap);

                    }
                    else if (taksit.isComleted && db.GetOTaksitSTaksitMapByOncekiTaksitID(taksit.taksitID) == null && !kontrol)
                    {
                        taksitEpilasyonMap.kalanTutar -= taksit.ucret;
                        db.UpdateDB(taksitEpilasyonMap);
                        Taksit tempTaksit = new Taksit()
                        {
                            musteriID = taksit.musteriID,
                            odemeTarihi = db.GetSeansByEpilasyonIDLast(epilasyon.epilasyonID),
                            aciklama = (Convert.ToInt32(tempAciklama[0]) + 1).ToString() + ".Ödeme",
                            ucret = 0,
                            isComleted = false

                        };
                        tempTaksit.taksitID = db.InsertDB(tempTaksit);
                        TaksitEpilasyonMap tempTaksitEpilasyon = new TaksitEpilasyonMap()
                        {
                            taksitID = tempTaksit.taksitID,
                            epilasyonID = epilasyon.epilasyonID,
                            kalanTutar = taksitEpilasyonMap.kalanTutar
                        };
                        db.InsertDB(tempTaksitEpilasyon);
                        OTaksitSTaksitMap tempOTaksitSTaksit = new OTaksitSTaksitMap()
                        {
                            oncekiTaksitID = taksit.taksitID,
                            sonrakiTaksitID = tempTaksit.taksitID
                        };
                        db.InsertDB(tempOTaksitSTaksit);
                        OdemeDuzenle odeme = new OdemeDuzenle(db.GetTaksitByID(tempTaksit.taksitID));
                        odeme.Show();
                    }
                    else if (taksit.isComleted && db.GetOTaksitSTaksitMapByOncekiTaksitID(taksit.taksitID) == null && kontrol &&
                        db.GetOTaksitSTaksitMapByOncekiTaksitID(taksit.taksitID) == null)
                    {
                        taksitEpilasyonMap.kalanTutar -= (ucret - oUcret);
                        db.UpdateDB(taksitEpilasyonMap);
                        Taksit tempTaksit = new Taksit()
                        {
                            musteriID = taksit.musteriID,
                            odemeTarihi = db.GetSeansByEpilasyonIDLast(epilasyon.epilasyonID),
                            aciklama = (Convert.ToInt32(tempAciklama[0]) + 1).ToString() + ".Ödeme",
                            ucret = 0,
                            isComleted = false

                        };
                        tempTaksit.taksitID = db.InsertDB(tempTaksit);
                        TaksitEpilasyonMap tempTaksitEpilasyon = new TaksitEpilasyonMap()
                        {
                            taksitID = tempTaksit.taksitID,
                            epilasyonID = epilasyon.epilasyonID,
                            kalanTutar = taksitEpilasyonMap.kalanTutar
                        };
                        db.InsertDB(tempTaksitEpilasyon);
                        OTaksitSTaksitMap tempOTaksitSTaksit = new OTaksitSTaksitMap()
                        {
                            oncekiTaksitID = taksit.taksitID,
                            sonrakiTaksitID = tempTaksit.taksitID
                        };
                        db.InsertDB(tempOTaksitSTaksit);
                        OdemeDuzenle odeme = new OdemeDuzenle(db.GetTaksitByID(tempTaksit.taksitID));
                        odeme.Show();
                    }
                    else if (oUcret != taksit.ucret && db.GetOTaksitSTaksitMapByOncekiTaksitID(taksit.taksitID) != null)
                    {
                        taksitEpilasyonMap.kalanTutar -= (ucret - oUcret);
                        db.UpdateDB(taksitEpilasyonMap);

                        int tempID = taksit.taksitID;
                        while (db.GetOTaksitSTaksitMapByOncekiTaksitID(tempID) != null)
                        {
                            OTaksitSTaksitMap sTaksitMap = db.GetOTaksitSTaksitMapByOncekiTaksitID(tempID);
                            tempID = sTaksitMap.sonrakiTaksitID;
                            TaksitEpilasyonMap taksitTemp = db.GetTaksitEpilasyonMapsByTaksitID(tempID);
                            taksitTemp.kalanTutar -= (ucret - oUcret);
                            db.UpdateDB(taksitTemp);
                        }
                    }
                }
                catch
                {

                }
                Form3.label1.Text += "+";
            }
            this.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void metroTextBox1_Validated(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
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

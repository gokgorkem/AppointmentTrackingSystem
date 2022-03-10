using MetroFramework;
using MetroFramework.Forms;
using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class MusteriDuzenle : MetroForm
    {
        Musteri musteri;
        public MusteriDuzenle()
        {
            InitializeComponent();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                musteri = db.GetMusteriByID(Convert.ToInt32(Form3.label1.Name));
                metroTextBox1.Text = musteri.ad;
                metroTextBox2.Text = musteri.soyad;
                metroTextBox3.Text = musteri.telefon;
            }
            catch
            {

            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

            string message = "Müşteri Adı : " + metroTextBox1.Text +
                "\nMüşteri Soyadı : " + metroTextBox2.Text +
                "\nTelefon : " + metroTextBox3.Text;
            string caption = "Bu Müşteriyi Silmek İster Misiniz ?";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MetroMessageBox.Show(Owner, message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                musteri.ad = metroTextBox1.Text;
                musteri.soyad = metroTextBox2.Text;
                musteri.telefon = metroTextBox3.Text;
                try
                {
                    DatabaseHandler db = DatabaseHandler.Singleton;
                    db.UpdateDB(musteri);
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

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {

            string message = "Müşteri Adı : " + musteri.ad +
                "\nMüşteri Soyadı : " + musteri.soyad +
                "\nTelefon : " + musteri.telefon;
            string caption = "Bu Müşteriyi Silmek İstermisiniz ?";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MetroMessageBox.Show(Owner, message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                musteri.isDeleted = true;
                try
                {
                    DatabaseHandler db = DatabaseHandler.Singleton;
                    db.UpdateDB(musteri);
                    List<Epilasyon> epilasyons = db.GetEpilasyonByMusteriID(musteri.musteriID);
                    List<Seans> seans = db.GetSeansByMustriID(musteri.musteriID);
                    List<Taksit> taksits = db.GetTaksitsByMusteriID(musteri.musteriID);
                    foreach (var i in epilasyons)
                    {
                        foreach (var j in db.GetTaksitEpilasyonMapsByEpilasyonID(i.epilasyonID))
                        {
                            j.IsDeleted = true;
                            db.UpdateDB(j);
                        }
                        foreach (var j in db.GetSeansEpilasyonMapsByEpilasyonID(i.epilasyonID))
                        {

                            j.IsDeleted = true;
                            db.UpdateDB(j);
                        }
                        i.isDeleted = true;
                        db.UpdateDB(i);
                    }
                    foreach (var i in seans)
                    {
                        i.isDeleted = true;
                        db.UpdateDB(i);
                        foreach (var j in db.GetSeansVucutbolgeMapsBySeansID(i.seansID))
                        {
                            j.isDeleted = true;
                            db.UpdateDB(j);
                        }
                        try
                        {
                            OSeansSSeansMap oSeansS = db.GetOSeansSSeansMapsByOncekiSeansID(i.seansID);
                            oSeansS.IsDeleted = true;
                            db.UpdateDB(oSeansS);

                        }
                        catch { }

                    }
                    foreach (var i in taksits)
                    {
                        i.isDeleted = true;
                        db.UpdateDB(i);
                        try
                        {
                            OTaksitSTaksitMap oTaksitS = db.GetOTaksitSTaksitMapByOncekiTaksitID(i.taksitID);
                            oTaksitS.IsDeleted = true;
                            db.UpdateDB(oTaksitS);
                        }
                        catch { }
                    }
                }
                catch
                {

                }
                Form3.label1.Text += "+";
            }
            this.Close();
        }
    }
}

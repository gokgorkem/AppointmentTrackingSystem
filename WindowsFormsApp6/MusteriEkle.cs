using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class MusteriEkle : MetroForm
    {
        public MusteriEkle()
        {
            InitializeComponent();
        }


        private void metroButton1_Click(object sender, EventArgs e)
        {
            int ID = 0;
            Musteri musteri = new Musteri();

            string message = "Müşteri Adı : " + metroTextBox1.Text +
                "\nMüşteri Soyadı : " + metroTextBox2.Text +
                "\nTelefon : " + metroTextBox3.Text;
            string caption = "Bu Müşteriyi Kaydetmek İster Misiniz ?";

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
                    ID = db.InsertDB(musteri);
                }
                catch
                {

                }
                Form3.label1.Name = Convert.ToString(ID);
                Form3.label1.Text = Convert.ToString(ID);
                RandevuEkle randevu = new RandevuEkle();
                randevu.Show();
                this.Close();
            }

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

    }
}

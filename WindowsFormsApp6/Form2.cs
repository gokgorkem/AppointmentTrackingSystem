using MetroFramework.Forms;
using System;
using System.Drawing;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();

        }
        public void degistir(string text)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillEllipse(myBrush, new Rectangle(0, 0, 200, 300));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
            float kazanç = 0;

            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                foreach (var i in db.GetTaksitByDate(Convert.ToDateTime(metroDateTime1.Text),
                    Convert.ToDateTime(metroDateTime2.Text)))
                {

                    if (i.isComleted)
                    {
                        Musteri musteri = db.GetMusteriByID(i.musteriID);
                        object[] row = new object[] { "Ödeme", musteri.ad + " " + musteri.soyad + " " + i.aciklama, i.ucret.ToString(), i.odemeTarihi };
                        metroGrid1.Rows.Add(row);
                        kazanç += i.ucret;
                    }
                }
                metroLabel2.Text = kazanç.ToString();
            }
            catch
            {

            }
        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}

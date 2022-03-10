using MetroFramework.Controls;
using MetroFramework.Forms;
using RandevuSistemi;
using System;
using System.Windows.Forms;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class RandevuListele : MetroForm
    {
        public static Label label;
        public RandevuListele()
        {
            label = new Label();
            label.TextChanged += new EventHandler(labelTextChanged);
            label.Hide();
            InitializeComponent();


        }

        private void labelTextChanged(object sender, EventArgs e)
        {
            if (label.Text.Length >= 20)
            {
                label.Text = "";
            }
            this.metroButton1_Click(sender, e);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroGrid1.Rows.Clear();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                foreach (var i in db.GetSeansByDate(Convert.ToDateTime(metroDateTime1.Text),
                    Convert.ToDateTime(metroDateTime2.Text).AddDays(1)))
                {
                    Musteri musteri = db.GetMusteriByID(i.musteriID);
                    model.Cihaz cihaz = db.GetCihazByID(i.cihazID);
                    object[] row = new object[] { musteri.ad + " " + musteri.soyad,musteri.telefon,
                        cihaz.cihazAdi,i.isCompleted,i.seansBaslangicTarihi.ToString("dd.MM.yyyy"),
                        i.isChooseSeansTime?i.seansBaslangicTarihi.ToShortTimeString():"Saat Seçilmedi"};
                    metroGrid1.Rows.Add(row);
                    metroGrid1.Rows[metroGrid1.Rows.Count - 2].Tag = i.seansID.ToString();

                }
            }
            catch
            {

            }
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            RandevuListeleGoruntule randevu = new RandevuListeleGoruntule(temp);
            randevu.Show();
        }
    }
}

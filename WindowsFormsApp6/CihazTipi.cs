using MetroFramework.Forms;
using System;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class CihazTipi : MetroForm
    {
        public CihazTipi()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                model.CihazTipi cihaz = new model.CihazTipi();
                cihaz.cihazTipi = metroTextBox1.Text;
                db.InsertDB(cihaz);
            }
            catch
            {

            }
            this.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}

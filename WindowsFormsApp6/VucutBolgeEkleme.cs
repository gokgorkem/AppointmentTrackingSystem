using MetroFramework.Forms;
using System;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class VucutBolgeEkleme : MetroForm
    {
        public VucutBolgeEkleme()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                model.VucutBolge vucut = new model.VucutBolge();
                vucut.vucutBolge = metroTextBox1.Text;
                db.InsertDB(vucut);
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

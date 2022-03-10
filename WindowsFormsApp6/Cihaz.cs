using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class Cihaz : MetroForm
    {
        List<model.CihazTipi> cihazs;
        public Cihaz()
        {
            InitializeComponent();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                cihazs = db.GetCihazTipis();
                foreach (var i in cihazs)
                {
                    metroComboBox1.Items.Add(i.cihazTipi);
                }
            }
            catch
            {

            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            model.Cihaz cihaz = new model.Cihaz();
            DatabaseHandler db = DatabaseHandler.Singleton;
            try
            {
                foreach (var i in cihazs)
                {
                    if (i.cihazTipi == metroComboBox1.Text)
                    {
                        cihaz.cihaztipiID = i.cihazTipiID;

                    }
                }
                cihaz.cihazAdi = metroTextBox1.Text;
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

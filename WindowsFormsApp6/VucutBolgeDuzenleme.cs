using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class VucutBolgeDuzenleme : MetroForm
    {
        List<model.VucutBolge> vucutBolges;
        public VucutBolgeDuzenleme()
        {
            InitializeComponent();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                vucutBolges = db.GetVucutBolges();
                foreach (var i in vucutBolges)
                {
                    metroComboBox1.Items.Add(i.vucutBolge);
                }
            }
            catch
            {

            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                List<VucutBolge> vucuts = db.GetVucutBolges();
                foreach (var i in vucuts)
                {
                    if (i.vucutBolge == metroComboBox1.Text)
                    {
                        i.vucutBolge = metroTextBox1.Text;
                        db.UpdateDB(i);
                        break;
                    }
                }
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

        [Obsolete]
        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroTextBox1.PromptText = metroComboBox1.Text;

        }
    }
}

using Calendar;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp6.model;

namespace RandevuSistemi
{
    public partial class RandevuSaatListeleme : MetroForm
    {
        List<Appointment> m_Appointments;
        public RandevuSaatListeleme(DateTime date, int ID)
        {
            InitializeComponent();
            m_Appointments = new List<Appointment>();
            dayView1.Renderer = new Office11Renderer();
            dayView1.StartDate = date;
            dayView1.NewAppointment += new NewAppointmentEventHandler(dayView2_NewAppointment);
            dayView1.SelectionChanged += new EventHandler(dayView2_SelectionChanged);
            dayView1.ResolveAppointments += new ResolveAppointmentsEventHandler(this.dayView2_ResolveAppointments);
            try
            {
                Appointment tek;
                DatabaseHandler db = DatabaseHandler.Singleton;
                foreach (var i in db.GetSeansByCihazID(ID, date, date.AddDays(1)))
                {
                    if (i.isChooseSeansTime)
                    {
                        tek = new Appointment();
                        tek.StartDate = i.seansBaslangicTarihi;
                        tek.EndDate = i.seansBitisTarihi;
                        tek.Locked = true;
                        tek.BorderColor = Color.Magenta;
                        var tempM = db.GetMusteriByID(i.musteriID);
                        var tempC = db.GetCihazByID(i.cihazID);
                        tek.Title = tempM.ad + " " + tempM.soyad + " " + tempC.cihazAdi;
                        m_Appointments.Add(tek);

                    }
                }
            }
            catch
            {

            }
        }


        private void dayView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dayView1.Selection == SelectionType.DateRange)
            {
                metroLabel3.Text = dayView1.SelectionStart.ToString();
                metroLabel4.Text = dayView1.SelectionEnd.ToString();
            }
            else if (dayView1.Selection == SelectionType.Appointment)
            {
                metroLabel3.Text = dayView1.SelectedAppointment.StartDate.ToString();
                metroLabel4.Text = dayView1.SelectedAppointment.EndDate.ToString();
            }
        }
        void dayView2_NewAppointment(object sender, NewAppointmentEventArgs args)
        {
            Appointment m_Appointment = new Appointment();

            m_Appointment.StartDate = args.StartDate;
            m_Appointment.EndDate = args.EndDate;
            m_Appointment.Title = args.Title;

            m_Appointments.Add(m_Appointment);
        }

        private void dayView2_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            List<Appointment> m_Apps = new List<Appointment>();

            foreach (Appointment m_App in m_Appointments)
                if ((m_App.StartDate >= args.StartDate) &&
                    (m_App.StartDate <= args.EndDate))
                    m_Apps.Add(m_App);

            args.Appointments = m_Apps;
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {

            string message = "  Seans Başlangıç Tarihi : " + metroLabel3.Text +
                             "\n  Seans Bitiş Tarihi : " + metroLabel4.Text;
            string caption = "Bu Seans Saatlerini Ayarlamak İstermisiniz ?";

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MetroMessageBox.Show(Owner, message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Closes the parent form.
                RandevuListeleGoruntule.Seans1.seansBaslangicTarihi = Convert.ToDateTime(metroLabel3.Text);
                RandevuListeleGoruntule.Seans1.seansBitisTarihi = Convert.ToDateTime(metroLabel4.Text);
                RandevuListeleGoruntule.Seans1.isChooseSeansTime = true;
                RandevuListeleGoruntule.label.Text += "+";
                this.Close();
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                this.Close();
            }
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}

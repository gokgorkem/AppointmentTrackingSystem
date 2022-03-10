using Calendar;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsFormsApp6.model;

namespace WindowsFormsApp6
{
    public partial class RandevuSaat : MetroForm
    {
        List<Appointment> m_Appointments;
        public RandevuSaat(DateTime date, int ID)
        {
            InitializeComponent();
            m_Appointments = new List<Appointment>();
            dayView2.Renderer = new Office11Renderer();
            dayView2.StartDate = date;
            dayView2.NewAppointment += new NewAppointmentEventHandler(dayView2_NewAppointment);
            dayView2.SelectionChanged += new EventHandler(dayView2_SelectionChanged);
            dayView2.ResolveAppointments += new ResolveAppointmentsEventHandler(this.dayView2_ResolveAppointments);
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
            if (dayView2.Selection == SelectionType.DateRange)
            {
                metroLabel3.Text = dayView2.SelectionStart.ToString();
                metroLabel4.Text = dayView2.SelectionEnd.ToString();
            }
            else if (dayView2.Selection == SelectionType.Appointment)
            {
                metroLabel3.Text = dayView2.SelectedAppointment.StartDate.ToString();
                metroLabel4.Text = dayView2.SelectedAppointment.EndDate.ToString();
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
        private void metroButton1_Click(object sender, EventArgs e)
        {
            RandevuEkle.startTimes = Convert.ToDateTime(metroLabel3.Text);
            RandevuEkle.endTimes = Convert.ToDateTime(metroLabel4.Text);
            RandevuEkle.saatSecimi = true;
            this.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}

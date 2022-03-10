using MetroFramework.Forms;
using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using WindowsFormsApp6.model;

namespace RandevuSistemi
{
    public partial class Son3AyGelmeyenler : MetroForm
    {
        List<Son3Ay> sons;
        public Son3AyGelmeyenler()
        {
            InitializeComponent();
            try
            {
                sons = new List<Son3Ay>();
                DatabaseHandler db = DatabaseHandler.Singleton;
                sons = db.GetSon3Ay(DateTime.Today.AddMonths(-3));
                foreach (var i in sons)
                {
                    Musteri musteri = db.GetMusteriByID(i.musteriID);
                    object[] row = new object[] { musteri.ad + " " + musteri.soyad, musteri.telefon, i.sonTarih.ToLongDateString() };
                    metroGrid1.Rows.Add(row);
                }
            }
            catch
            {

            }
        }
    }
}

using MetroFramework.Forms;
using System;
using WindowsFormsApp6.model;

namespace RandevuSistemi
{
    public partial class OdemeGecikenler : MetroForm
    {
        public OdemeGecikenler()
        {
            InitializeComponent();
            try
            {
                DatabaseHandler db = DatabaseHandler.Singleton;
                foreach (var i in db.GetTaksitByPastDate(DateTime.Today))
                {
                    var temp = db.GetMusteriByID(i.musteriID);
                    object[] row = new object[] {temp.ad+" "+temp.soyad,temp.telefon,
                        i.aciklama,i.ucret.ToString(),i.odemeTarihi
                    };
                    metroGrid1.Rows.Add(row);
                }
            }
            catch
            {

            }
        }
    }
}

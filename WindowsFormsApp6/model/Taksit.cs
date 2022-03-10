using System;

namespace WindowsFormsApp6.model
{
    public class Taksit
    {
        public int taksitID;
        public int musteriID;
        public float ucret;
        public DateTime odemeTarihi;
        public string aciklama;
        public bool isComleted;
        public bool isSending;
        public bool isDeleted;
        public DateTime createdDate;
        public DateTime modifyTime;
    }
}

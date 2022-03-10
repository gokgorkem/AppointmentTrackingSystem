using System;

namespace WindowsFormsApp6.model
{
    public class Seans
    {
        public int seansID;
        public int musteriID;
        public int cihazID;
        public DateTime seansBaslangicTarihi;
        public DateTime seansBitisTarihi;
        public bool isCompleted;
        public bool isChooseSeansTime;
        public bool isSending;
        public bool isDeleted;
        public DateTime createdDate;
        public DateTime modifyTime;
    }
}

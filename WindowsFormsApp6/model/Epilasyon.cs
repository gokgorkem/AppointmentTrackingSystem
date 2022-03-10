using System;

namespace WindowsFormsApp6.model
{
    public class Epilasyon
    {
        public int epilasyonID;
        public int musteriID;
        public int cihazID;
        public byte seansSayisi;
        public float toplamTutar;
        public bool isDeleted;
        public DateTime createdDate;
        public DateTime modifyTime;
    }
}

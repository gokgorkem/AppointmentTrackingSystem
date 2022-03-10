using System;

namespace RandevuSistemi.model
{
    public class TaksitEpilasyonMap
    {
        public int taksitEpilasyonMapID;
        public int taksitID;
        public int epilasyonID;
        public float kalanTutar;
        public bool IsDeleted;
        public DateTime CreatedDate;
        public DateTime ModifyTime;
    }
}

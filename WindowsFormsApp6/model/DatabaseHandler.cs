using RandevuSistemi.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WindowsFormsApp6.model
{
    class DatabaseHandler
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["WindowsFormsApp6.Properties.Settings.Connection"].ConnectionString;
        private static DatabaseHandler singleton;
        private static SqlConnection sqlConnection;

        public SqlConnection SqlConnetionFactory
        {
            get { return sqlConnection; }
        }

        private DatabaseHandler() { }

        public static DatabaseHandler Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new DatabaseHandler();

                sqlConnection = new SqlConnection(connectionString);
                return singleton;
            }
        }

        #region Cihaz

        public int InsertDB(Cihaz cihaz)
        {
            int ID;
            string sqlQuery = "Insert into dbo.Cihaz (CihazAdı, CihazTipiID )" +
                                                " Values(@CihazAdi, @CihazTipiID  );" +
                                                "SET @CihazID = SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            try
            {
                command.Parameters.AddWithValue("@CihazAdi", cihaz.cihazAdi);
                command.Parameters.AddWithValue("@CihazTipiID", cihaz.cihaztipiID);
                command.Parameters.AddWithValue("@CihazID", cihaz.cihaztipiID).Direction = ParameterDirection.Output;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@CihazID"].Value;

            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public Cihaz GetCihazByID(int ID)
        {
            Cihaz Result = new Cihaz();

            string sqlQuery = string.Format("select * from dbo.Cihaz" +
                                                " where dbo.Cihaz.CihazID = {0} ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        Result.cihazAdi = dataReader["CihazAdı"].ToString();
                        Result.cihaztipiID = Convert.ToInt32(dataReader["CihazTipiID"]);
                    }
                }

            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Cihaz> GetCihazs()
        {
            List<Cihaz> Result = new List<Cihaz>();
            Cihaz cihaz;
            string sqlQuery = "select * from dbo.Cihaz ;";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();


            try
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        cihaz = new Cihaz();
                        cihaz.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        cihaz.cihazAdi = dataReader["CihazAdı"].ToString();
                        cihaz.cihaztipiID = Convert.ToInt32(dataReader["CihazTipiID"]);
                        Result.Add(cihaz);
                    }
                }
                command.Dispose();
                sqlConnection.Close();

            }
            catch
            {

            }

            return Result;
        }

        public int UpdateDB(Cihaz cihaz)
        {

            string sqlQuery = "Update dbo.Cihaz set CihazAdi=@CihazAdi, CihazTipiID=@CihazTipiID " +
                                                "  where CihazID=@CihazID ;";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@CihazID", cihaz.cihazID);
            command.Parameters.AddWithValue("@CihazAdi", cihaz.cihazAdi);
            command.Parameters.AddWithValue("@CihazTipiID", cihaz.cihaztipiID);
            sqlConnection.Open();
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                var commandResult = command.ExecuteScalar();
                if (commandResult != null)
                {
                    ID = Convert.ToInt32(commandResult);
                }
                else
                {
                    ID = cihaz.cihazID;
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region CihazTipi

        public int InsertDB(CihazTipi cihazTipi)
        {
            int ID;
            string sqlQuery = "Insert into dbo.CihazTipi (CihazTipi)" +
                                                    " Values(@CihazTipi );" +
                                                "SET @CihazTipiID = SCOPE_IDENTITY()";
            try
            {
                SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
                command.Parameters.AddWithValue("@CihazTipi", cihazTipi.cihazTipi);
                command.Parameters.AddWithValue("@CihazTipiID", cihazTipi.cihazTipiID).Direction = ParameterDirection.Output;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@CihazTipiID"].Value;
                command.Dispose();
                sqlConnection.Close();

            }
            catch
            {
                ID = 0;
            }
            return ID;
        }

        public CihazTipi GetCihazTipi(int ID)
        {
            CihazTipi Result = new CihazTipi();

            string sqlQuery = String.Format("select * from dbo.Cihaz" +
                                                " where dbo.Cihaz.CihazID = {0} ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.cihazTipiID = Convert.ToInt32(dataReader["CihazTipiID"]);
                        Result.cihazTipi = dataReader["CihazTipi"].ToString();
                    }
                }

            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<CihazTipi> GetCihazTipis()
        {
            List<CihazTipi> Result = new List<CihazTipi>();
            CihazTipi cihazTipi;
            string sqlQuery = "select * from dbo.CihazTipi;";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        cihazTipi = new CihazTipi();
                        cihazTipi.cihazTipiID = Convert.ToInt32(dataReader["CihazTipiID"]);
                        cihazTipi.cihazTipi = dataReader["CihazTipi"].ToString();

                        Result.Add(cihazTipi);
                    }
                }

            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public int UpdateDB(CihazTipi cihazTipi)
        {
            string sqlQuery = "Update dbo.Cihaz set CihazTipi=@CihazTipi " +
                                                "  where CihazTipiID=@CihazTipiID ;";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@CihazTipiID", cihazTipi.cihazTipiID);
            command.Parameters.AddWithValue("@CihazTipi", cihazTipi.cihazTipi);
            sqlConnection.Open();
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                var commandResult = command.ExecuteScalar();
                if (commandResult != null)
                {
                    ID = Convert.ToInt32(commandResult);
                }
                else
                {
                    ID = cihazTipi.cihazTipiID;
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region Epilasyon

        public int InsertDB(Epilasyon epilasyon)
        {
            int ID = 0;
            string sqlQuery = "Insert into dbo.Epilasyon (MusteriID,ToplamTutar,SeansSayisi,CihazID )" +
                                                    " Values(@MusteriID,@ToplamTutar,@SeansSayisi,@CihazID);" +
                                                "SET @EpilasyonID = SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@MusteriID", epilasyon.musteriID);
            command.Parameters.AddWithValue("@CihazID", epilasyon.cihazID);
            command.Parameters.AddWithValue("@ToplamTutar", epilasyon.toplamTutar);
            command.Parameters.AddWithValue("@SeansSayisi", epilasyon.seansSayisi);
            command.Parameters.AddWithValue("@EpilasyonID", epilasyon.epilasyonID).Direction = ParameterDirection.Output;
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@EpilasyonID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public Epilasyon GetEpilasyonByID(int ID)
        {
            Epilasyon Result = new Epilasyon();

            string sqlQuery = String.Format("select * from dbo.Epilasyon" +
                                                " where dbo.Epilasyon.EpilasyonID = {0} ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        Result.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        Result.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        Result.seansSayisi = Convert.ToByte(dataReader["SeansSayisi"]);
                        Result.toplamTutar = (float)Convert.ToDouble(dataReader["ToplamTutar"]);
                        Result.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                    }
                }

            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Epilasyon> GetEpilasyons()
        {
            List<Epilasyon> Result = new List<Epilasyon>();
            Epilasyon epilasyon;

            string sqlQuery = "select * from dbo.Epilasyon where IsDeleted = 0 ;";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        epilasyon = new Epilasyon();
                        epilasyon.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        epilasyon.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        epilasyon.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        epilasyon.seansSayisi = Convert.ToByte(dataReader["SeansSayisi"]);
                        epilasyon.toplamTutar = (float)Convert.ToDouble(dataReader["ToplamTutar"]);
                        epilasyon.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        epilasyon.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        epilasyon.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.Add(epilasyon);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public int UpdateDB(Epilasyon epilasyon)
        {
            string sqlQuery = "Update dbo.Epilasyon set " +
                "MusteriID=@MusteriID," +
                "CihazID=@CihazID," +
                "SeansSayisi=@SeansSayisi," +
                "ToplamTutar=@ToplamTutar," +
                "CreatedDate=@CreatedDate," +
                "ModifyTime=@ModifyTime," +
                "IsDeleted=@IsDeleted " +
                                                "  where EpilasyonID=@EpilasyonID ;";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@EpilasyonID", epilasyon.epilasyonID);
            command.Parameters.AddWithValue("@MusteriID", epilasyon.musteriID);
            command.Parameters.AddWithValue("@CihazID", epilasyon.cihazID);
            command.Parameters.AddWithValue("@SeansSayisi", epilasyon.seansSayisi);
            command.Parameters.AddWithValue("@ToplamTutar", epilasyon.toplamTutar);
            command.Parameters.AddWithValue("@CreatedDate", epilasyon.createdDate);
            command.Parameters.AddWithValue("@ModifyTime", DateTime.Now);
            command.Parameters.AddWithValue("@IsDeleted", epilasyon.isDeleted);
            sqlConnection.Open();
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public List<Epilasyon> GetEpilasyonByMusteriID(int ID)
        {
            List<Epilasyon> Result = new List<Epilasyon>();
            Epilasyon epilasyon;

            string sqlQuery = String.Format("select * from dbo.Epilasyon where IsDeleted = 0 and MusteriID = {0} ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        epilasyon = new Epilasyon();
                        epilasyon.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        epilasyon.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        epilasyon.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        epilasyon.seansSayisi = Convert.ToByte(dataReader["SeansSayisi"]);
                        epilasyon.toplamTutar = (float)Convert.ToDouble(dataReader["ToplamTutar"]);
                        epilasyon.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        epilasyon.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        epilasyon.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.Add(epilasyon);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        #endregion

        #region Musteri

        public int InsertDB(Musteri musteri)
        {

            string sqlQuery = "Insert into dbo.Musteri (Ad,Soyad,Telefon)" +
                                                    " Values(@Ad,@Soyad,@Telefon );" +
                                                "SET @MusteriID = SCOPE_IDENTITY()";
            int ID;
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@Ad", musteri.ad.ToUpper());
            command.Parameters.AddWithValue("@Soyad", musteri.soyad.ToUpper());
            command.Parameters.AddWithValue("@Telefon", musteri.telefon);
            command.Parameters.AddWithValue("@MusteriID", musteri.musteriID).Direction = ParameterDirection.Output;
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@MusteriID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public Musteri GetMusteriByID(int ID)
        {
            Musteri Result = new Musteri();

            string sqlQuery = String.Format("select * from dbo.Musteri " +
                                                "where MusteriID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        Result.ad = dataReader["Ad"].ToString();
                        Result.soyad = dataReader["Soyad"].ToString();
                        Result.telefon = dataReader["Telefon"].ToString();
                        Result.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Musteri> GetMusteris()
        {
            List<Musteri> Result = new List<Musteri>();
            Musteri musteri;

            string sqlQuery = "select * from dbo.Musteri where IsDeleted = 0 ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        musteri = new Musteri();
                        musteri.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        musteri.ad = dataReader["Ad"].ToString();
                        musteri.soyad = dataReader["Soyad"].ToString();
                        musteri.telefon = dataReader["Telefon"].ToString();
                        musteri.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        musteri.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        musteri.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(musteri);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Musteri> GetMusteriByFirstHarf(String harf)
        {
            List<Musteri> Result = new List<Musteri>();
            Musteri musteri;

            string sqlQuery = String.Format("select * from dbo.Musteri" +
                                                " where IsDeleted = 0 and (Musteri.Ad like '{0}%' or Musteri.Ad like '{1}%' ) ;", harf, harf.ToLower());

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();


            try
            {

                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        musteri = new Musteri();
                        musteri.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        musteri.ad = dataReader["Ad"].ToString();
                        musteri.soyad = dataReader["Soyad"].ToString();
                        musteri.telefon = dataReader["Telefon"].ToString();
                        musteri.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        musteri.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        musteri.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(musteri);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Musteri> GetMusteriByArama(String harf)
        {
            List<Musteri> Result = new List<Musteri>();
            Musteri musteri;

            string sqlQuery = String.Format("select * from dbo.Musteri" +
                                                " where IsDeleted = 0 and (Musteri.Ad like '{0}%' or Musteri.Ad like '{1}%' or  Musteri.Soyad like '{0}%' or Musteri.Soyad like '{1}%' or  Musteri.Telefon like '{0}%' or Musteri.Telefon like '{1}%')  ;", harf.ToUpper(), harf.ToLower());

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();


            try
            {

                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        musteri = new Musteri();
                        musteri.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        musteri.ad = dataReader["Ad"].ToString();
                        musteri.soyad = dataReader["Soyad"].ToString();
                        musteri.telefon = dataReader["Telefon"].ToString();
                        musteri.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        musteri.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        musteri.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(musteri);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public int UpdateDB(Musteri musteri)
        {
            string sqlQuery = "Update dbo.Musteri set " +
                      "Ad=@Ad," +
                      "Soyad=@Soyad," +
                      "Telefon=@Telefon," +
                      "IsDeleted=@IsDeleted," +
                      "CreatedDate=@CreatedDate," +
                      "ModifyTime=@ModifyTime" +
                                                        "  where MusteriID=@MusteriID ;";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@MusteriID", musteri.musteriID);
            command.Parameters.AddWithValue("@Ad", musteri.ad);
            command.Parameters.AddWithValue("@Soyad", musteri.soyad);
            command.Parameters.AddWithValue("@Telefon", musteri.telefon);
            command.Parameters.AddWithValue("@IsDeleted", musteri.isDeleted);
            command.Parameters.AddWithValue("@CreatedDate", musteri.createdDate);
            command.Parameters.AddWithValue("@ModifyTime", DateTime.Now);
            sqlConnection.Open();
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region OSeansSSeansMap
        public int InsertDB(OSeansSSeansMap map)
        {
            int ID;
            string sqlQuery = "Insert into dbo.OSeansSSeansMap (OncekiSeansID,SonrakiSeansID)" +
                                                    " Values(@OncekiSeansID,@SonrakiSeansID );" +
                                                "SET @OSeansSSeansMapID = SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@OncekiSeansID", map.oncekiSeansID);
            command.Parameters.AddWithValue("@SonrakiSeansID", map.sonrakiSeansID);
            command.Parameters.AddWithValue("@OSeansSSeansMapID", map.oSeansSSeansMapID).Direction = ParameterDirection.Output;
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@OSeansSSeansMapID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public OSeansSSeansMap GetOSeansSSeansMapByID(int ID)
        {
            OSeansSSeansMap Result = new OSeansSSeansMap();

            string sqlQuery = String.Format("select * from dbo.OSeansSSeansMap" +
                                                " where dbo.OSeansSSeansMap.OSeansSSeansMapID = {0} and IsDeleted = 0  ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.oSeansSSeansMapID = Convert.ToInt32(dataReader["OSeansSSeansMapID"]);
                        Result.oncekiSeansID = Convert.ToInt32(dataReader["OncekiSeansID"]);
                        Result.sonrakiSeansID = Convert.ToInt32(dataReader["SonrakiSeansID"]);
                        Result.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<OSeansSSeansMap> GetOSeansSSeansMaps()
        {
            List<OSeansSSeansMap> Result = new List<OSeansSSeansMap>();
            OSeansSSeansMap map;

            string sqlQuery = "select * from dbo.OSeansSSeansMap where IsDeleted = 0  ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new OSeansSSeansMap();
                        map.oSeansSSeansMapID = Convert.ToInt32(dataReader["OSeansSSeansMapID"]);
                        map.oncekiSeansID = Convert.ToInt32(dataReader["OncekiSeansID"]);
                        map.sonrakiSeansID = Convert.ToInt32(dataReader["SonrakiSeansID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(map);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public OSeansSSeansMap GetOSeansSSeansMapsByOncekiSeansID(int ID)
        {
            OSeansSSeansMap map = null;

            string sqlQuery = String.Format("select * from dbo.OSeansSSeansMap" +
                                                " where dbo.OSeansSSeansMap.OncekiSeansID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    map = new OSeansSSeansMap();
                    while (dataReader.Read())
                    {
                        map.oSeansSSeansMapID = Convert.ToInt32(dataReader["OSeansSSeansMapID"]);
                        map.oncekiSeansID = Convert.ToInt32(dataReader["OncekiSeansID"]);
                        map.sonrakiSeansID = Convert.ToInt32(dataReader["SonrakiSeansID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return map;
        }

        public int UpdateDB(OSeansSSeansMap map)
        {
            string sqlQuery = "update OSeansSSeansMap set " +
                            "OncekiSeansID=@OncekiSeansID," +
                            "SonrakiSeansID=@SonrakiSeansID," +
                            "IsDeleted=@IsDeleted," +
                            "CreatedDate=@CreatedDate," +
                            "ModifyTime=@ModifyTime" +
                                                              "  where OSeansSSeansMapID=@OSeansSSeansMapID ;";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@OSeansSSeansMapID", map.oSeansSSeansMapID);
            command.Parameters.AddWithValue("@OncekiSeansID", map.oncekiSeansID);
            command.Parameters.AddWithValue("@SonrakiSeansID", map.sonrakiSeansID);
            command.Parameters.AddWithValue("@IsDeleted", map.IsDeleted);
            command.Parameters.AddWithValue("@CreatedDate", map.CreatedDate);
            command.Parameters.AddWithValue("@ModifyTime", map.ModifyTime);
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region OTaksitSTaksitMap
        public int InsertDB(OTaksitSTaksitMap map)
        {
            int ID;
            string sqlQuery = "Insert into dbo.OTaksitSTaksitMap (OncekiTaksitID,SonrakiTaksitID)" +
                                                    " Values(@OncekiTaksitID,@SonrakiTaksitID );" +
                                                "SET @OTaksitSTaksitMapID = SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@OncekiTaksitID", map.oncekiTaksitID);
            command.Parameters.AddWithValue("@SonrakiTaksitID", map.sonrakiTaksitID);
            command.Parameters.AddWithValue("@OTaksitSTaksitMapID", map.oTaksitSTaksitMapID).Direction = ParameterDirection.Output;
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@OTaksitSTaksitMapID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public OTaksitSTaksitMap GetOTaksitSTaksitMapByID(int ID)
        {
            OTaksitSTaksitMap Result = new OTaksitSTaksitMap();

            string sqlQuery = String.Format("select * from dbo.OTaksitSTaksitMap" +
                                                " where dbo.OTaksitSTaksitMap.OTaksitSTaksitMapID = {0} and IsDeleted = 0  ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.oTaksitSTaksitMapID = Convert.ToInt32(dataReader["OTaksitSTaksitMapID"]);
                        Result.oncekiTaksitID = Convert.ToInt32(dataReader["OncekiTaksitID"]);
                        Result.sonrakiTaksitID = Convert.ToInt32(dataReader["SonrakiTaksitID"]);
                        Result.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<OTaksitSTaksitMap> GetOTaksitSTaksitMaps()
        {
            List<OTaksitSTaksitMap> Result = new List<OTaksitSTaksitMap>();
            OTaksitSTaksitMap map;

            string sqlQuery = "select * from dbo.OTaksitSTaksitMap where IsDeleted = 0  ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new OTaksitSTaksitMap();
                        map.oTaksitSTaksitMapID = Convert.ToInt32(dataReader["OTaksitSTaksitMapID"]);
                        map.oncekiTaksitID = Convert.ToInt32(dataReader["OncekiTaksitID"]);
                        map.sonrakiTaksitID = Convert.ToInt32(dataReader["SonrakiTaksitID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(map);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public OTaksitSTaksitMap GetOTaksitSTaksitMapByOncekiTaksitID(int ID)
        {
            OTaksitSTaksitMap map = null;

            string sqlQuery = String.Format("select * from dbo.OTaksitSTaksitMap" +
                                                " where dbo.OTaksitSTaksitMap.OncekiTaksitID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new OTaksitSTaksitMap();
                        map.oTaksitSTaksitMapID = Convert.ToInt32(dataReader["OTaksitSTaksitMapID"]);
                        map.oncekiTaksitID = Convert.ToInt32(dataReader["OncekiTaksitID"]);
                        map.sonrakiTaksitID = Convert.ToInt32(dataReader["SonrakiTaksitID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return map;
        }

        public OTaksitSTaksitMap GetOTaksitSTaksitMapBySonrakiTaksitID(int ID)
        {
            OTaksitSTaksitMap map = null;

            string sqlQuery = String.Format("select * from dbo.OTaksitSTaksitMap" +
                                                " where dbo.OTaksitSTaksitMap.SonrakiTaksitID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new OTaksitSTaksitMap();
                        map.oTaksitSTaksitMapID = Convert.ToInt32(dataReader["OTaksitSTaksitMapID"]);
                        map.oncekiTaksitID = Convert.ToInt32(dataReader["OncekiTaksitID"]);
                        map.sonrakiTaksitID = Convert.ToInt32(dataReader["SonrakiTaksitID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return map;
        }

        public int UpdateDB(OTaksitSTaksitMap map)
        {
            string sqlQuery = "update OTaksitSTaksitMap set " +
                            "OncekiTaksitID=@OncekiTaksitID," +
                            "SonrakiTaksitID=@SonrakiTaksitID," +
                            "IsDeleted=@IsDeleted," +
                            "CreatedDate=@CreatedDate," +
                            "ModifyTime=@ModifyTime" +
                                                              "  where OTaksitSTaksitMapID=@OTaksitSTaksitMapID ;";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@OSeansSSeansMapID", map.oTaksitSTaksitMapID);
            command.Parameters.AddWithValue("@OncekiTaksitID", map.oncekiTaksitID);
            command.Parameters.AddWithValue("@SonrakiTaksitID", map.sonrakiTaksitID);
            command.Parameters.AddWithValue("@IsDeleted", map.IsDeleted);
            command.Parameters.AddWithValue("@CreatedDate", map.CreatedDate);
            command.Parameters.AddWithValue("@ModifyTime", map.ModifyTime);
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region Seans

        public int InsertDB(Seans seans)
        {
            int ID;
            string sqlQuery = "Insert into dbo.Seans (MusteriID,CihazID,SeansBitisTarihi,SeansBaslangicTarihi,IsCompleted,IsChooseSeansTime)" +
                                                    " Values(@MusteriID,@CihazID,@SeansBitisTarihi,@SeansBaslangicTarihi,@IsCompleted,@IsChooseSeansTime);" +
                                                "SET @SeansID = SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@MusteriID", seans.musteriID);
            command.Parameters.AddWithValue("@CihazID", seans.cihazID);
            command.Parameters.AddWithValue("@SeansBitisTarihi", seans.seansBitisTarihi);
            command.Parameters.AddWithValue("@SeansBaslangicTarihi", seans.seansBaslangicTarihi);
            command.Parameters.AddWithValue("@IsCompleted", seans.isCompleted);
            command.Parameters.AddWithValue("@IsChooseSeansTime", seans.isChooseSeansTime);
            command.Parameters.AddWithValue("@SeansID", seans.seansID).Direction = ParameterDirection.Output;
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@SeansID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public Seans GetSeansByID(int ID)
        {
            Seans Result = new Seans();

            string sqlQuery = String.Format("select * from dbo.Seans" +
                                                " where dbo.Seans.SeansID = {0} and IsDeleted = 0  ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        Result.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        Result.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        Result.seansBaslangicTarihi = Convert.ToDateTime(dataReader["SeansBaslangicTarihi"]);
                        Result.seansBitisTarihi = Convert.ToDateTime(dataReader["SeansBitisTarihi"]);
                        Result.isCompleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        Result.isChooseSeansTime = Convert.ToBoolean(dataReader["IsChooseSeansTime"]);
                        Result.isSending = Convert.ToBoolean(dataReader["IsSending"]);
                        Result.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public DateTime GetSeansByEpilasyonIDLast(int ID)
        {
            DateTime Result = DateTime.Now;

            string sqlQuery = String.Format("select LAST_VALUE(SeansBaslangicTarihi)over( order by  Seans.MusteriID) from Seans " +
                                                    " inner join SeansEpilasyonMap on SeansEpilasyonMap.SeansID = Seans.SeansID" +
                                                    " inner join Epilasyon on Epilasyon.EpilasyonID = SeansEpilasyonMap.EpilasyonID" +
                                                    " where Epilasyon.EpilasyonID = {0} ", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result = Convert.ToDateTime(dataReader[0]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Seans> GetSeans()
        {
            List<Seans> Result = new List<Seans>();
            Seans seans;

            string sqlQuery = "select * from dbo.Seans where IsDeleted = 0 order by SeansBaslangicTarihi asc";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        seans = new Seans();
                        seans.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        seans.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        seans.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        seans.seansBaslangicTarihi = Convert.ToDateTime(dataReader["SeansBaslangicTarihi"]);
                        seans.seansBitisTarihi = Convert.ToDateTime(dataReader["SeansBitisTarihi"]);
                        seans.isCompleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        seans.isChooseSeansTime = Convert.ToBoolean(dataReader["IsChooseSeansTime"]);
                        seans.isSending = Convert.ToBoolean(dataReader["IsSending"]);
                        seans.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        seans.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        seans.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(seans);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public int UpdateDB(Seans seans)
        {
            string sqlQuery = "Update dbo.Seans set " +
                         "MusteriID=@MusteriID," +
                         "CihazID=@CihazID," +
                         "SeansBaslangicTarihi=@SeansBaslangicTarihi," +
                         "SeansBitisTarihi=@SeansBitisTarihi," +
                         "IsCompleted=@IsCompleted," +
                         "IsChooseSeansTime=@IsChooseSeansTime," +
                         "IsSending=@IsSending," +
                         "IsDeleted=@IsDeleted," +
                         "CreatedDate=@CreatedDate," +
                         "ModifyTime=@ModifyTime" +
                                                           "  where SeansID=@SeansID ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@SeansID", seans.seansID);
            command.Parameters.AddWithValue("@MusteriID", seans.musteriID);
            command.Parameters.AddWithValue("@CihazID", seans.cihazID);
            command.Parameters.AddWithValue("@SeansBaslangicTarihi", seans.seansBaslangicTarihi);
            command.Parameters.AddWithValue("@SeansBitisTarihi", seans.seansBitisTarihi);
            command.Parameters.AddWithValue("@IsCompleted", seans.isCompleted);
            command.Parameters.AddWithValue("@IsChooseSeansTime", seans.isChooseSeansTime);
            command.Parameters.AddWithValue("@IsSending", seans.isSending);
            command.Parameters.AddWithValue("@IsDeleted", seans.isDeleted);
            command.Parameters.AddWithValue("@CreatedDate", seans.createdDate);
            command.Parameters.AddWithValue("@ModifyTime", DateTime.Now);
            sqlConnection.Open();
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public List<Seans> GetSeansByMustriID(int musteriID)
        {
            List<Seans> Result = new List<Seans>();
            Seans seans;

            string sqlQuery = String.Format("select * from dbo.Seans" +
                                                " where  " +
                                                "dbo.Seans.MusteriID =  {0} and IsDeleted = 0  order by SeansBaslangicTarihi asc"
                                                , musteriID);
            try
            {
                SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
                sqlConnection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                try
                {

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            seans = new Seans();
                            seans.seansID = Convert.ToInt32(dataReader["SeansID"]);
                            seans.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                            seans.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                            seans.seansBaslangicTarihi = Convert.ToDateTime(dataReader["SeansBaslangicTarihi"]);
                            seans.seansBitisTarihi = Convert.ToDateTime(dataReader["SeansBitisTarihi"]);
                            seans.isChooseSeansTime = Convert.ToBoolean(dataReader["IsChooseSeansTime"]);
                            seans.isCompleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                            seans.isSending = Convert.ToBoolean(dataReader["IsSending"]);
                            seans.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                            seans.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                            seans.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                            Result.Add(seans);
                        }
                    }
                }
                catch
                {

                }
                command.Dispose();
                sqlConnection.Close();
            }
            catch
            {

            }
            return Result;
        }

        public List<Seans> GetSeansByDate(DateTime startDate, DateTime endDate)
        {
            List<Seans> Result = new List<Seans>();
            Seans seans;

            string sqlQuery = String.Format("select * from dbo.Seans" +
                                                " where  IsDeleted = 0 and SeansBaslangicTarihi " +
                                                "between '{0}' and '{1}'  order by SeansBaslangicTarihi asc "
                                                , startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        seans = new Seans();
                        seans.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        seans.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        seans.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        seans.seansBaslangicTarihi = Convert.ToDateTime(dataReader["SeansBaslangicTarihi"]);
                        seans.seansBitisTarihi = Convert.ToDateTime(dataReader["SeansBitisTarihi"]);
                        seans.isCompleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        seans.isChooseSeansTime = Convert.ToBoolean(dataReader["IsChooseSeansTime"]);
                        seans.isSending = Convert.ToBoolean(dataReader["IsSending"]);
                        seans.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        seans.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        seans.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(seans);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Seans> GetSeansByCihazID(int cihazID, DateTime startDate, DateTime endDate)
        {
            List<Seans> Result = new List<Seans>();
            Seans seans;

            string sqlQuery = String.Format("select * from dbo.Seans" +
                                                " where CihazID = {0} and IsDeleted = 0  and " +
                                                " SeansBaslangicTarihi " +
                                                "between '{1}' and '{2}' order by SeansBaslangicTarihi asc;"
                                                , cihazID, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();


            try
            {

                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        seans = new Seans();
                        seans.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        seans.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        seans.cihazID = Convert.ToInt32(dataReader["CihazID"]);
                        seans.seansBaslangicTarihi = Convert.ToDateTime(dataReader["SeansBaslangicTarihi"]);
                        seans.seansBitisTarihi = Convert.ToDateTime(dataReader["SeansBitisTarihi"]);
                        seans.isCompleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        seans.isChooseSeansTime = Convert.ToBoolean(dataReader["IsChooseSeansTime"]);
                        seans.isSending = Convert.ToBoolean(dataReader["IsSending"]);
                        seans.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        seans.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        seans.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(seans);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        #endregion

        #region SeansEpilasyonMap

        public int InsertDB(SeansEpilasyonMap map)
        {
            int ID;
            string sqlQuery = "Insert into dbo.SeansEpilasyonMap (EpilasyonID,SeansID)" +
                                                    " Values(@EpilasyonID,@SeansID );" +
                                                "SET @SeansEpilasyonMapID = SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@SeansID", map.seansID);
            command.Parameters.AddWithValue("@EpilasyonID", map.epilasyonID);
            command.Parameters.AddWithValue("@SeansEpilasyonMapID", map.seansEpilasyonMapID).Direction = ParameterDirection.Output;
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@SeansEpilasyonMapID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public SeansEpilasyonMap GetSeansEpilasyonMapByID(int ID)
        {
            SeansEpilasyonMap Result = new SeansEpilasyonMap();

            string sqlQuery = String.Format("select * from dbo.SeansEpilasyonMap" +
                                                " where dbo.SeansEpilasyonMap.SeansEpilasyonMapID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.seansEpilasyonMapID = Convert.ToInt32(dataReader["SeansEpilasyonMapID"]);
                        Result.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        Result.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        Result.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<SeansEpilasyonMap> GetSeansEpilasyonMaps()
        {
            List<SeansEpilasyonMap> Result = new List<SeansEpilasyonMap>();
            SeansEpilasyonMap map;

            string sqlQuery = "select * from dbo.SeansEpilasyonMap and IsDeleted = 0 ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new SeansEpilasyonMap();
                        map.seansEpilasyonMapID = Convert.ToInt32(dataReader["SeansEpilasyonMapID"]);
                        map.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        map.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(map);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<SeansEpilasyonMap> GetSeansEpilasyonMapsByEpilasyonID(int ID)
        {
            List<SeansEpilasyonMap> Result = new List<SeansEpilasyonMap>();
            SeansEpilasyonMap map;

            string sqlQuery = String.Format("select * from dbo.SeansEpilasyonMap" +
                                                " where dbo.SeansEpilasyonMap.EpilasyonID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new SeansEpilasyonMap();
                        map.seansEpilasyonMapID = Convert.ToInt32(dataReader["SeansEpilasyonMapID"]);
                        map.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        map.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(map);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public SeansEpilasyonMap GetSeansEpilasyonMapsBySeansID(int ID)
        {
            SeansEpilasyonMap map = new SeansEpilasyonMap();

            string sqlQuery = String.Format("select * from dbo.SeansEpilasyonMap" +
                                                " where dbo.SeansEpilasyonMap.SeansID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map.seansEpilasyonMapID = Convert.ToInt32(dataReader["SeansEpilasyonMapID"]);
                        map.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        map.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return map;
        }

        public int UpdateDB(SeansEpilasyonMap map)
        {
            string sqlQuery = "update SeansEpilasyonMap set " +
                            "SeansID=@SeansID," +
                            "EpilasyonID=@EpilasyonID," +
                            "IsDeleted=@IsDeleted," +
                            "CreatedDate=@CreatedDate," +
                            "ModifyTime=@ModifyTime" +
                                                              "  where SeansEpilasyonMapID=@SeansEpilasyonMapID ;";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@SeansEpilasyonMapID", map.seansEpilasyonMapID);
            command.Parameters.AddWithValue("@SeansID", map.seansID);
            command.Parameters.AddWithValue("@EpilasyonID", map.epilasyonID);
            command.Parameters.AddWithValue("@IsDeleted", map.IsDeleted);
            command.Parameters.AddWithValue("@CreatedDate", map.CreatedDate);
            command.Parameters.AddWithValue("@ModifyTime", map.ModifyTime);
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region Taksit

        public int InsertDB(Taksit taksit)
        {
            int ID;
            string sqlQuery = "Insert into dbo.Taksit (MusteriID,Ucret,OdemeTarihi,Aciklama,IsCompleted)" +
                                                    " Values(@MusteriID,@Ucret,@OdemeTarihi,@Aciklama,@IsCompleted);" +
                                                " SET @TaksitID = SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);

            command.Parameters.AddWithValue("@MusteriID", taksit.musteriID);
            command.Parameters.AddWithValue("@Ucret", taksit.ucret);
            command.Parameters.AddWithValue("@OdemeTarihi", taksit.odemeTarihi);
            command.Parameters.AddWithValue("@Aciklama", taksit.aciklama);
            command.Parameters.AddWithValue("@IsCompleted", taksit.isComleted);
            command.Parameters.AddWithValue("@TaksitID", taksit.taksitID).Direction = ParameterDirection.Output;

            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@TaksitID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();

            return ID;
        }

        public Taksit GetTaksitByID(int ID)
        {
            Taksit Result = new Taksit();

            string sqlQuery = String.Format("select * from dbo.Taksit" +
                                                " where dbo.Taksit.TaksitID = {0} and IsDeleted = 0  ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        Result.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        Result.ucret = (float)Convert.ToDouble(dataReader["Ucret"]);
                        Result.odemeTarihi = Convert.ToDateTime(dataReader["OdemeTarihi"]);
                        Result.aciklama = Convert.ToString(dataReader["Aciklama"]);
                        Result.isComleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        Result.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Taksit> GetTaksits()
        {
            List<Taksit> Result = new List<Taksit>();
            Taksit taksit;

            string sqlQuery = "select * from dbo.Taksit where IsDeleted = 0 ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        taksit = new Taksit();
                        taksit.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        taksit.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        taksit.ucret = (float)Convert.ToDouble(dataReader["Ucret"]);
                        taksit.odemeTarihi = Convert.ToDateTime(dataReader["OdemeTarihi"]);
                        taksit.aciklama = Convert.ToString(dataReader["Aciklama"]);
                        taksit.isComleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        taksit.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        taksit.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        taksit.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(taksit);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public int UpdateDB(Taksit taksit)
        {
            string sqlQuery = "Update dbo.Taksit set " +
                            " MusteriID=@MusteriID," +
                            " Ucret=@Ucret," +
                            " OdemeTarihi=@OdemeTarihi," +
                            " Aciklama=@Aciklama," +
                            " IsCompleted=@IsCompleted," +
                            " IsSending=@IsSending," +
                            " IsDeleted=@IsDeleted," +
                            " CreatedDate=@CreatedDate," +
                            "ModifyTime=@ModifyTime " +
                                                              "  where TaksitID=@TaksitID ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@TaksitID", taksit.taksitID);
            command.Parameters.AddWithValue("@MusteriID", taksit.musteriID);
            command.Parameters.AddWithValue("@Ucret", taksit.ucret);
            command.Parameters.AddWithValue("@OdemeTarihi", taksit.odemeTarihi);
            command.Parameters.AddWithValue("@Aciklama", taksit.aciklama);
            command.Parameters.AddWithValue("@IsCompleted", taksit.isComleted);
            command.Parameters.AddWithValue("@IsSending", taksit.isSending);
            command.Parameters.AddWithValue("@IsDeleted", taksit.isDeleted);
            command.Parameters.AddWithValue("@CreatedDate", taksit.createdDate);
            command.Parameters.AddWithValue("@ModifyTime", DateTime.Now);
            sqlConnection.Open();
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public List<Taksit> GetTaksitsByMusteriID(int ID)
        {

            List<Taksit> Result = new List<Taksit>();
            Taksit taksit;

            string sqlQuery = string.Format("select * from dbo.Taksit"
                                                + " where  MusteriID={0} and IsDeleted = 0  ", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();
            command.Parameters.AddWithValue("@MusteriID", ID);
            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        taksit = new Taksit();
                        taksit.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        taksit.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        taksit.ucret = (float)Convert.ToDouble(dataReader["Ucret"]);
                        taksit.odemeTarihi = Convert.ToDateTime(dataReader["OdemeTarihi"]);
                        taksit.aciklama = Convert.ToString(dataReader["Aciklama"]);
                        taksit.isComleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        taksit.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        taksit.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        taksit.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(taksit);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Taksit> GetTaksitByDate(DateTime startDate, DateTime endDate)
        {
            List<Taksit> Result = new List<Taksit>();
            Taksit taksit;

            string sqlQuery = String.Format("select * from dbo.Taksit" +
                                                " where IsDeleted = 0 and OdemeTarihi " +
                                                "between '{0}' and '{1}' order by OdemeTarihi asc"
                                                , startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        taksit = new Taksit();
                        taksit.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        taksit.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        taksit.ucret = (float)Convert.ToDouble(dataReader["Ucret"]);
                        taksit.odemeTarihi = Convert.ToDateTime(dataReader["OdemeTarihi"]);
                        taksit.aciklama = Convert.ToString(dataReader["Aciklama"]);
                        taksit.isComleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        taksit.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        taksit.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        taksit.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(taksit);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<Taksit> GetTaksitByPastDate(DateTime startDate)
        {
            List<Taksit> Result = new List<Taksit>();
            Taksit taksit;

            string sqlQuery = String.Format("select * from dbo.Taksit" +
                                                " where IsDeleted = 0 and OdemeTarihi " +
                                                "<'{0}' and IsCompleted=0 order by OdemeTarihi,MusteriID asc"
                                                , startDate.ToString("yyyy-MM-dd"));

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        taksit = new Taksit();
                        taksit.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        taksit.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        taksit.ucret = (float)Convert.ToDouble(dataReader["Ucret"]);
                        taksit.odemeTarihi = Convert.ToDateTime(dataReader["OdemeTarihi"]);
                        taksit.aciklama = Convert.ToString(dataReader["Aciklama"]);
                        taksit.isComleted = Convert.ToBoolean(dataReader["IsCompleted"]);
                        taksit.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        taksit.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        taksit.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(taksit);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        #endregion

        #region TaksitEpilasyonMap

        public int InsertDB(TaksitEpilasyonMap map)
        {
            int ID;
            string sqlQuery = "Insert into dbo.TaksitEpilasyonMap (EpilasyonID,TaksitID,KalanTutar)" +
                                                    " Values(@EpilasyonID,@TaksitID,@KalanTutar );" +
                                                "SET @TaksitEpilasyonMapID = SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@TaksitID", map.taksitID);
            command.Parameters.AddWithValue("@EpilasyonID", map.epilasyonID);
            command.Parameters.AddWithValue("@KalanTutar", map.kalanTutar);
            command.Parameters.AddWithValue("@TaksitEpilasyonMapID", map.taksitEpilasyonMapID).Direction = ParameterDirection.Output;
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@TaksitEpilasyonMapID"].Value;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public TaksitEpilasyonMap GetTaksitEpilasyonMapByID(int ID)
        {
            TaksitEpilasyonMap Result = new TaksitEpilasyonMap();

            string sqlQuery = String.Format("select * from dbo.TaksitEpilasyonMap" +
                                                " where dbo.TaksitEpilasyonMap.TaksitEpilasyonMapID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.taksitEpilasyonMapID = Convert.ToInt32(dataReader["TaksitEpilasyonMapID"]);
                        Result.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        Result.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        Result.kalanTutar = (float)Convert.ToDouble(dataReader["KalanTutar"]);
                        Result.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<TaksitEpilasyonMap> GetTaksitEpilasyonMaps()
        {
            List<TaksitEpilasyonMap> Result = new List<TaksitEpilasyonMap>();
            TaksitEpilasyonMap map;

            string sqlQuery = "select * from dbo.TaksitEpilasyonMap where IsDeleted = 0 ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new TaksitEpilasyonMap();
                        map.taksitEpilasyonMapID = Convert.ToInt32(dataReader["TaksitEpilasyonMapID"]);
                        map.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        map.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        map.kalanTutar = (float)Convert.ToDouble(dataReader["KalanTutar"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(map);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<TaksitEpilasyonMap> GetTaksitEpilasyonMapsByEpilasyonID(int ID)
        {
            List<TaksitEpilasyonMap> Result = new List<TaksitEpilasyonMap>();
            TaksitEpilasyonMap map;

            string sqlQuery = String.Format("select * from dbo.TaksitEpilasyonMap" +
                                                " where dbo.TaksitEpilasyonMap.EpilasyonID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map = new TaksitEpilasyonMap();
                        map.taksitEpilasyonMapID = Convert.ToInt32(dataReader["TaksitEpilasyonMapID"]);
                        map.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        map.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        map.kalanTutar = (float)Convert.ToDouble(dataReader["KalanTutar"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(map);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public TaksitEpilasyonMap GetTaksitEpilasyonMapsByTaksitID(int ID)
        {
            TaksitEpilasyonMap map = new TaksitEpilasyonMap();

            string sqlQuery = String.Format("select * from dbo.TaksitEpilasyonMap" +
                                                " where dbo.TaksitEpilasyonMap.TaksitID = {0} and IsDeleted = 0 ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        map.taksitEpilasyonMapID = Convert.ToInt32(dataReader["TaksitEpilasyonMapID"]);
                        map.taksitID = Convert.ToInt32(dataReader["TaksitID"]);
                        map.epilasyonID = Convert.ToInt32(dataReader["EpilasyonID"]);
                        map.kalanTutar = (float)Convert.ToDouble(dataReader["KalanTutar"]);
                        map.IsDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        map.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        map.ModifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return map;
        }

        public int UpdateDB(TaksitEpilasyonMap map)
        {
            string sqlQuery = "update TaksitEpilasyonMap set " +
                            "TaksitID=@TaksitID," +
                            "EpilasyonID=@EpilasyonID," +
                            "KalanTutar=@KalanTutar," +
                            "IsDeleted=@IsDeleted," +
                            "CreatedDate=@CreatedDate," +
                            "ModifyTime=@ModifyTime" +
                                                              "  where TaksitEpilasyonMapID=@TaksitEpilasyonMapID ;";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@TaksitEpilasyonMapID", map.taksitEpilasyonMapID);
            command.Parameters.AddWithValue("@TaksitID", map.taksitID);
            command.Parameters.AddWithValue("@EpilasyonID", map.epilasyonID);
            command.Parameters.AddWithValue("@KalanTutar", map.kalanTutar);
            command.Parameters.AddWithValue("@IsDeleted", map.IsDeleted);
            command.Parameters.AddWithValue("@CreatedDate", map.CreatedDate);
            command.Parameters.AddWithValue("@ModifyTime", map.ModifyTime);
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region VucutBolge
        public int InsertDB(VucutBolge vucutBolge)
        {
            int ID;
            string sqlQuery = "Insert into dbo.VucutBolge (VucutBolge)" +
                                                    " Values(@VucutBolge );";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@VucutBolge", vucutBolge.vucutBolge);
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = GetVucutBolges().Last<VucutBolge>().vucutBolgeID;
                command.Dispose();
                sqlConnection.Close();
            }
            catch
            {
                ID = 0;
            }
            return ID;
        }

        public VucutBolge GetVucutBolgeByID(int ID)
        {
            VucutBolge Result = new VucutBolge();

            string sqlQuery = String.Format("select * from dbo.VucutBolge" +
                                                " where dbo.VucutBolge.VucutBolgeID = {0} and IsDeleted = 0  ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.vucutBolgeID = Convert.ToInt16(dataReader["VucutBolgeID"]);
                        Result.vucutBolge = Convert.ToString(dataReader["VucutBolge"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<VucutBolge> GetVucutBolges()
        {
            List<VucutBolge> Result = new List<VucutBolge>();
            VucutBolge vucutBolge;

            string sqlQuery = "select * from dbo.VucutBolge where IsDeleted = 0  order by VucutBolge asc";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        vucutBolge = new VucutBolge();
                        vucutBolge.vucutBolgeID = Convert.ToInt16(dataReader["VucutBolgeID"]);
                        vucutBolge.vucutBolge = Convert.ToString(dataReader["VucutBolge"]);
                        Result.Add(vucutBolge);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public int UpdateDB(VucutBolge vucutBolge)
        {
            string sqlQuery = "update VucutBolge set " +
                            "VucutBolge=@VucutBolge" +
                                                              "  where VucutBolgeID=@VucutBolgeID ;";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@VucutBolgeID", vucutBolge.vucutBolgeID);
            command.Parameters.AddWithValue("@VucutBolge", vucutBolge.vucutBolge);
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region SeansVucutbolgeMap
        public int InsertDB(SeansVucutbolgeMap map)
        {
            int ID;
            string sqlQuery = "Insert into dbo.SeansVucutbolgeMap (SeansID,VucutBolgeID,SeansNo)" +
                                                    " Values(@SeansID,@VucutBolgeID,@SeansNo );";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@VucutBolgeID", map.vucutBolgeID);
            command.Parameters.AddWithValue("@SeansID", map.seansID);
            command.Parameters.AddWithValue("@SeansNo", map.seansNo);
            sqlConnection.Open();
            try
            {

                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {
                ID = 0;
            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        public SeansVucutbolgeMap GetSeansVucutbolgeMapByID(int ID)
        {
            SeansVucutbolgeMap Result = new SeansVucutbolgeMap();

            string sqlQuery = String.Format("select * from dbo.SeansVucutbolgeMap" +
                                                " where dbo.SeansVucutbolgeMap.SeansVucutbolgeMapID = {0} and IsDeleted = 0  ;", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Result.seansVucutbolgeMapID = Convert.ToInt32(dataReader["SeansVucutbolgeMapID"]);
                        Result.vucutBolgeID = Convert.ToInt16(dataReader["VucutBolgeID"]);
                        Result.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        Result.seansNo = Convert.ToByte(dataReader["SeansNo"]);
                        Result.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        Result.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        Result.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<SeansVucutbolgeMap> GetSeansVucutbolgeMaps()
        {
            List<SeansVucutbolgeMap> Result = new List<SeansVucutbolgeMap>();
            SeansVucutbolgeMap vucutBolge;

            string sqlQuery = "select * from dbo.SeansVucutbolgeMap where IsDeleted = 0   ";

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        vucutBolge = new SeansVucutbolgeMap();
                        vucutBolge.seansVucutbolgeMapID = Convert.ToInt32(dataReader["SeansVucutbolgeMapID"]);
                        vucutBolge.vucutBolgeID = Convert.ToInt16(dataReader["VucutBolgeID"]);
                        vucutBolge.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        vucutBolge.seansNo = Convert.ToByte(dataReader["SeansNo"]);
                        vucutBolge.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        vucutBolge.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        vucutBolge.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(vucutBolge);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public List<SeansVucutbolgeMap> GetSeansVucutbolgeMapsBySeansID(int ID)
        {
            List<SeansVucutbolgeMap> Result = new List<SeansVucutbolgeMap>();
            SeansVucutbolgeMap vucutBolge;

            string sqlQuery = String.Format("select * from dbo.SeansVucutbolgeMap" +
                                                " where dbo.SeansVucutbolgeMap.SeansID = {0} and IsDeleted = 0  ", ID);

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        vucutBolge = new SeansVucutbolgeMap();
                        vucutBolge.seansVucutbolgeMapID = Convert.ToInt32(dataReader["SeansVucutbolgeMapID"]);
                        vucutBolge.vucutBolgeID = Convert.ToInt16(dataReader["VucutBolgeID"]);
                        vucutBolge.seansID = Convert.ToInt32(dataReader["SeansID"]);
                        vucutBolge.seansNo = Convert.ToByte(dataReader["SeansNo"]);
                        vucutBolge.isDeleted = Convert.ToBoolean(dataReader["IsDeleted"]);
                        vucutBolge.createdDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        vucutBolge.modifyTime = Convert.ToDateTime(dataReader["ModifyTime"]);
                        Result.Add(vucutBolge);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }

        public int UpdateDB(SeansVucutbolgeMap map)
        {
            string sqlQuery = "update SeansVucutbolgeMap set " +
                            "SeansID=@SeansID," +
                            "VucutBolgeID=@VucutBolgeID," +
                            "SeansNo=@SeansNo," +
                            "IsDeleted=@IsDeleted," +
                            "CreatedDate=@CreatedDate," +
                            "ModifyTime=@ModifyTime" +
                                                              "  where SeansVucutbolgeMapID=@SeansVucutbolgeMapID ;";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@SeansVucutbolgeMapID", map.seansVucutbolgeMapID);
            command.Parameters.AddWithValue("@VucutBolgeID", map.vucutBolgeID);
            command.Parameters.AddWithValue("@SeansID", map.seansID);
            command.Parameters.AddWithValue("@SeansNo", map.seansNo);
            command.Parameters.AddWithValue("@IsDeleted", map.isDeleted);
            command.Parameters.AddWithValue("@CreatedDate", map.createdDate);
            command.Parameters.AddWithValue("@ModifyTime", map.modifyTime);
            int ID = 0;
            try
            {
                command.ExecuteNonQuery();
                ID = 1;
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return ID;
        }

        #endregion

        #region son3Ay
        public List<Son3Ay> GetSon3Ay(DateTime date)
        {
            List<Son3Ay> Result = new List<Son3Ay>();
            Son3Ay son;

            string sqlQuery = string.Format("select Musteri.MusteriID,yedek from dbo.Musteri  inner join " +
                                " (select  Seans.MusteriID," +
                                    " LAST_VALUE(SeansBaslangicTarihi)over(order by  Seans.MusteriID) as yedek " +

                                        " from Seans where Seans.IsDeleted = 0  " +

                                " ) as tek on tek.MusteriID = dbo.Musteri.MusteriID " +

                                " where tek.yedek < '{0}' " +

                                " group by Musteri.MusteriID,tek.yedek; ", date.ToString("yyyy-MM-dd"));

            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            try
            {

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        son = new Son3Ay();
                        son.musteriID = Convert.ToInt32(dataReader["MusteriID"]);
                        son.sonTarih = Convert.ToDateTime(dataReader["yedek"]);
                        Result.Add(son);
                    }
                }
            }
            catch
            {

            }
            command.Dispose();
            sqlConnection.Close();
            return Result;
        }
        #endregion
    }
}

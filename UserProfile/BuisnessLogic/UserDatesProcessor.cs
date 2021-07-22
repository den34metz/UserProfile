using System;
using Common.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UserProfile.Models;
using System.Configuration;

namespace UserProfile.BuinessLogic
{
    public class UserDatesProcessor
    {
        //public static int CreateDate(DateTime datetaken, DateTime datenextdue, int edipi, int trainid, byte[] cert, string certname, int datetakenid)
        public static int AddDate(DateTime datetaken, DateTime datenextdue, int edipi, int trainid, string traintitle, byte[] cert, string certname, string type)

        {
            var good = 1;
            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            SqlCommand cmd = new SqlCommand("SpInsertUserDates", cnn) { CommandType = System.Data.CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@DATETAKEN", datetaken);
            cmd.Parameters.AddWithValue("@DATENEXTDUE", datenextdue);
            cmd.Parameters.AddWithValue("@EDIPI", edipi);
            cmd.Parameters.AddWithValue("@TRAIN_ID", trainid);
            cmd.Parameters.AddWithValue("@TRAINTITLE", traintitle);
            cmd.Parameters.AddWithValue("@CERT", cert);
            cmd.Parameters.AddWithValue("@CERTNAME", certname);
            cmd.Parameters.AddWithValue("@TYPE", type);
            cmd.ExecuteNonQuery();

            cnn.Close();

            return good;
        }

        public static List<UserDatesModel> GetDates(int curEdipi)
        {

            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@edipi", curEdipi),
                };

                List<UserDatesModel> datelist = new List<UserDatesModel>();

                SqlCommand cmd = new SqlCommand("SpGetSingleUserDate", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

               // cnn.Open();
                sd.Fill(dt);
                cnn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    datelist.Add(new UserDatesModel
                    {
                        DATETAKENID = Convert.ToInt32(dr["DATETAKENID"]),
                        DATETAKEN = Convert.ToDateTime(dr["DATETAKEN"]),
                        DATENEXTDUE = Convert.ToDateTime(dr["DATENEXTDUE"]),
                        EDIPI = Convert.ToInt32(dr["EDIPI"]),
                        VERIFIED = Convert.ToString(dr["VERIFIED"]),
                        TRAINTITLE = Convert.ToString(dr["TRAINTITLE"]),
                        TRAIN_ID = Convert.ToInt32(dr["TRAIN_ID"])
                    });
                }
                return datelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }


        }
        public static int AddSchedDate( DateTime datesched,int edipi, int trainid, string traintitle, int datetimeid, DateTime curdate)
        {
            var good = 1;
            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            SqlCommand cmd = new SqlCommand("SpAddSchedDate", cnn) { CommandType = System.Data.CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("DATESCHED", datesched);
            cmd.Parameters.AddWithValue("@EDIPI", edipi);
            cmd.Parameters.AddWithValue("@TRAINID", trainid);
            cmd.Parameters.AddWithValue("@TRAINTITLE", traintitle);
            cmd.Parameters.AddWithValue("@DATETIMEID", datetimeid);
            cmd.Parameters.AddWithValue("@DATEMODIFIED", curdate);
            cmd.ExecuteNonQuery();

            cnn.Close();

            return good;
        }
        public static List<UserDatesModel> GetSchedDate(int curEdipi)
        {

            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@edipi", curEdipi),
                };

                List<UserDatesModel> scheddatelist = new List<UserDatesModel>();

                SqlCommand cmd = new SqlCommand("SpGetSingleSchedDate", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                // cnn.Open();
                sd.Fill(dt);
                cnn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    scheddatelist.Add(new UserDatesModel
                    {
                        SCHEDDATEID = Convert.ToInt32(dr["SCHEDDATEID"]),
                        DATETIMEID = Convert.ToInt32(dr["DATETIMEID"]),
                        EDIPI = Convert.ToInt32(dr["EDIPI"]),
                        LOC = Convert.ToString(dr["LOC"]),
                      /*  VERIFIED = Convert.ToString(dr["VERIFIED"]),*/
                        TRAINTITLE = Convert.ToString(dr["TRAINTITLE"]),
                        TRAIN_ID = Convert.ToInt32(dr["TRAINID"]),
                        DATESCHED = Convert.ToDateTime(dr["DATESCHED"])
                    });
                }
                return scheddatelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static List<SchedDatesModel> GetAllSchedDate()
        {
            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            try
            {
                List<SchedDatesModel> scheddates = new List<SchedDatesModel>();

                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ViewGetAllSchedDates", cnn);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                // cnn.Open();
                sd.Fill(dt);
                cnn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    scheddates.Add(new SchedDatesModel
                    {
                        TRAIN_DATE_TIME = Convert.ToDateTime(dr["TRAIN_DATE_TIME"]),
                        LOC = Convert.ToString(dr["LOC"]),
                        DATE_TIME_ID = Convert.ToInt32(dr["DATE_TIME_ID"]),
                        //TYPE = Convert.ToString(dr["TYPE"]),
                        TRAIN_TITLE = Convert.ToString(dr["TRAIN_TITLE"]),
                        TRAIN_ID = Convert.ToInt32(dr["TRAIN_ID"]),
                        //EDIPI = Convert.ToInt32(dr["EDIPI"])

                    });
                }
                return scheddates;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public static List<UserDatesModel> GetCert(int datetakenid)
        {
            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            try
            {
                SqlParameter[] param =
               {
                    new SqlParameter("@DATETAKENID", datetakenid)
                };
                List<UserDatesModel> cert = new List<UserDatesModel>();

                SqlCommand cmd = new SqlCommand("SpGetCert", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                // cnn.Open();
                sd.Fill(dt);
                cnn.Close();

               /* var FileById = (from FC in cert
                               *//* where FC.Id.Equals(id)*//*
                                select new { FC.CERT, FC.CERTNAME}).ToList().FirstOrDefault();
*/
                foreach (DataRow dr in dt.Rows)
                   
                {
                    cert.Add(new UserDatesModel
                    {
                       
                        CERT = (byte[])dr["CERT"],
                        CERTNAME = Convert.ToString(dr["CERTNAME"]),
                        DATETAKENID = Convert.ToInt32(dr["DATETAKENID"])
                        /*DATE_TIME_ID = Convert.ToInt32(dr["DATE_TIME_ID"]),
                        TYPE = Convert.ToString(dr["TYPE"]),
                        TRAIN_TITLE = Convert.ToString(dr["TRAIN_TITLE"]),
                        TRAIN_ID = Convert.ToInt32(dr["TRAIN_ID"])*/

                    });
                }
                return cert;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
    

        
        /* 


         string sql = @"SpInsertUserDates(usermodel.DATETAKEN, usermodel.DATENEXTDUE, usermodel.EDIPI, usermodel.TRAIN_ID, usermodel.CERT, usermodel.CERTNAME, usermodel.DATETAKENID);";

         return SqlDataAccess.SaveTrainingData(sql, usermodel);*/
    }

}

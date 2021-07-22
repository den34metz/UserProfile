using Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UserProfile.Models;

namespace UserProfile.BuisnessLogic
{
    public class TrainingProcessor
    {
        public static List<TrainingModel> GetAllTrainings()
        {

            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            try
            {
                List<TrainingModel> trainlist = new List<TrainingModel>();

                SqlCommand cmd = new SqlCommand("SpGetAllTrainings", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                cnn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    trainlist.Add(new TrainingModel
                    {
                        TRAINID = Convert.ToInt32(dr["TRAINID"]),
                        TRAIN_TITLE = Convert.ToString(dr["TRAIN_TITLE"]),
                        TYPE = Convert.ToString(dr["TYPE"]),
                        FREQUENCY = Convert.ToString(dr["FREQUENCY"]),
                        REQ_NAME = Convert.ToString(dr["REQ_NAME"]),
                        RPT_ELE_NAME = Convert.ToString(dr["RPT_ELE_NAME"]),
                        TRAIN_DETAILS = Convert.ToString(dr["TRAIN_DETAILS"]),
                        ATCTS = Convert.ToString(dr["ATCTS"]),
                        MANDATED_BY = Convert.ToString(dr["MANDATED_BY"]),
                        URL = Convert.ToString(dr["URL"]),
                        REQ_ID = Convert.ToInt32(dr["REQ_ID"]),
                        RPT_ELE_ID = Convert.ToInt32(dr["RPT_ELE_ID"]),
                        CLASS_DUR = Convert.ToInt32(dr["CLASS_DUR"])
                        
                    });
                }
                return trainlist;
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

        public static List<GetSingleTrainingModel> GetSingleTraining()
        {

            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("TrainingTracker");

            try
            {
                List<GetSingleTrainingModel> trainlist = new List<GetSingleTrainingModel>();

                SqlCommand cmd = new SqlCommand("Select * from dbo.ViewSingleTraining", cnn);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                cnn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    trainlist.Add(new GetSingleTrainingModel
                    {
                        TRAIN_ID = Convert.ToInt32(dr["TRAIN_ID"]),
                        EDIPI = Convert.ToInt32(dr["EDIPI"])

                    });
                }
                return trainlist;
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



    }
}
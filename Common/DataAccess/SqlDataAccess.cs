using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Common.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetCommonCon(string conName = "PA_COMMON")
        {
            return ConfigurationManager.ConnectionStrings[conName].ConnectionString;
        }

        public static string GetTrainingTrackerCon(string conName = "TrainingTracker")
        {
            return ConfigurationManager.ConnectionStrings[conName].ConnectionString;
        }

        public static string GetTrainTrackerCon(string conName = "TrainTracker")
        {
            return ConfigurationManager.ConnectionStrings[conName].ConnectionString;
        }

        public static SqlConnection GetDBCon(string connStr)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connStr].ConnectionString);
            conn.Open();
            return conn;

        }
        //used for getting data from common db
        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection conn = new SqlConnection(GetCommonCon()))
            {
                return conn.Query<T>(sql).ToList();
            }
        }

        //used for saving data to common db
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection conn = new SqlConnection(GetCommonCon()))
            {
                return conn.Execute(sql, data);
            }
        }
        //used for getting data from training tracker db
      /*  public static List<T> LoadTraingData<T>(string sql)
        {
            using (IDbConnection conn = new SqlConnection(GetTrainTrackerCon()))
            {
                return conn.Query<T>(sql).ToList();
            }
        }
        //used for saving data to training tracker db
        public static int SaveTrainingData<T>(string sql, T data)
        {
            using (IDbConnection conn = new SqlConnection(GetTrainTrackerCon()))
            {
                return conn.Execute(sql, data);
            }
        }*/
    }
}


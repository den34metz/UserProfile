using Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using UserProfile.Models;
using UserProfile.ViewModels;

namespace UserProfile.BuisnessLogic
{
    public class PocsProcessor
    {
        public static List<PocsModel> GetPocs(int curEdipi)
        {

            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("PACOMMON");

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@edipi", curEdipi),
                };

                List<PocsModel> pocslist = new List<PocsModel>();

                SqlCommand cmd = new SqlCommand("profile.SpGetSinglePoc", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                cnn.Close();

                foreach(DataRow dr in dt.Rows)
                {
                    pocslist.Add(new PocsModel
                    {
                        POC_ID = Convert.ToInt32(dr["POC_ID"]),
                        POC_FIRST = Convert.ToString(dr["POC_FIRST"]),
                        POC_MI = Convert.ToString(dr["POC_MI"]),
                        POC_LAST = Convert.ToString(dr["POC_LAST"]),
                        RT_DESCRIPTION = Convert.ToString(dr["RT_DESCRIPTION"]),
                        EMAIL = Convert.ToString(dr["EMAIL"]),
                        SEMAIL = Convert.ToString(dr["SEMAIL"]),
                        ALT_EMAIL = Convert.ToString(dr["ALT_EMAIL"]),
                        CAC_EDIPI = Convert.ToInt32(dr["CAC_EDIPI"]),
                        PHONE_COMM = Convert.ToString(dr["PHONE_COMM"]),
                        PHONE_DSN = Convert.ToString(dr["PHONE_DSN"]),
                        PHONE_EMERG = Convert.ToString(dr["PHONE_EMERG"]),
                        POC_AVATAR = Convert.ToString(dr["POC_AVATAR"]),
                        ORG = Convert.ToString(dr["ORG"]),
                        ORG_ID = Convert.ToInt32(dr["ORG_ID"]),
                        ORG_LOC_ID = Convert.ToInt32(dr["ORG_LOC_ID"]),
                        AKO_LOGIN = Convert.ToString(dr["AKO_LOGIN"])
                    });
                }
                return pocslist;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            

        }

        public static List<TeamsModel> GetTeam(int curEdipi)
        {
            SqlConnection cnn = null;
            cnn = SqlDataAccess.GetDBCon("PACOMMON");

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@edipi", curEdipi),
                };

                List<TeamsModel> teamslist = new List<TeamsModel>();

                SqlCommand cmd = new SqlCommand("profile.SpGetSinglePocTeam", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(param);

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                cnn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    teamslist.Add(new TeamsModel
                    {
                        CAC_EDIPI = Convert.ToInt32(dr["CAC_EDIPI"]),
                        TEAM = Convert.ToString(dr["TEAM"]),
                        T_PARENT = Convert.ToInt32(dr["T_PARENT"]),

                    });
                }
                return teamslist;
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

        public static List<TeamPocsViewModel> GetPocTeam(int curEdipi)
        {
            TeamPocsViewModel mymodel = new TeamPocsViewModel();
            mymodel.Pocs = GetPocs(curEdipi);
            mymodel.Teams = GetTeam(curEdipi);
            List<TeamPocsViewModel> teampoclist = new List<TeamPocsViewModel>();
            teampoclist.Add(mymodel);
            return teampoclist;
        }

    }
}
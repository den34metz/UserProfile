using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UserProfile.Models
{
    public class PocsModel
    {
        public int POC_ID { get; set; }

        public string POC_LAST { get; set; }

        public string POC_FIRST { get; set; }

        public string POC_MI { get; set; }
        [DisplayName("TYPE:")]
        public string RT_DESCRIPTION { get; set; }

        public int ORG_LOC_ID { get; set; }

        [DisplayName("AKO:")]
        public string AKO_LOGIN { get; set; }

        [DisplayName("NIPR EMAIL:")]
        public string EMAIL { get; set; }
        [DisplayName("SIPR EMAIL:")]
        public string SEMAIL { get; set; }

        public string ALT_EMAIL { get; set; }
        [DisplayName("EDIPI:")]
        public int CAC_EDIPI { get; set; }

        [DisplayName("PHONE_DSN:")]
        public string PHONE_DSN { get; set; }

        [DisplayName("PHONE_COMM:")]
        public string PHONE_COMM { get; set; }

        [DisplayName("CELL:")]
        public string PHONE_EMERG { get; set; }

        [DisplayName("AVATAR")]
        public string POC_AVATAR { get; set; }

        public string LOCATION { get; set; }

        public string ORG { get; set; }

        public int ORG_ID { get; set; }
        [DisplayName("USER NAME:")]
        public string FullName { get { return POC_FIRST + " " + POC_MI + " " + POC_LAST; } } 
                
    }

    public class ViewModelTeams
    {
        public List<TeamsModel> teams { get; set; }
    }
}
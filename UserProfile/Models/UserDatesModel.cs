using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserProfile.Models
{

    public class UserDatesModel
    {
        public string TYPE { get; set; }

        public  byte[] CERT { get; set; }

        public string CERTNAME { get; set; }

        public string TRAINTITLE { get; set; }

        public int TRAIN_ID { get; set; }
        [Key]
        public int DATETAKENID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime DATETAKEN { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime DATENEXTDUE { get; set; }

        public string VERIFIED { get; set; }

        public int EDIPI { get; set; }

        public string LOC { get; set; }

        public int SCHEDDATEID { get; set; }

        public DateTime DATESCHED { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DATETIMEID { get; set; }


    }
}
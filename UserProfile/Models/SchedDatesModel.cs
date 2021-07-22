using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserProfile.Models
{
    public class SchedDatesModel
    {
        public DateTime TRAIN_DATE_TIME { get; set; }

        public String LOC { get; set; }

        public int DATE_TIME_ID { get; set; }

        public int TRAIN_ID { get; set; }

        public string TRAIN_TITLE { get; set; }

        public string TYPE { get; set; }

        public int EDIPI { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DATEMODIFIED { get; set; }

    }
}
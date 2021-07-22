using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfile.Models
{
    public class TrainingModel
    {
        public int TRAINID { get; set; }

        public string TRAIN_TITLE { get; set; }

        public string TYPE { get; set; }

        public string FREQUENCY { get; set; }

        public string REQ_NAME { get; set; }

        public string RPT_ELE_NAME { get; set; }

        public string TRAIN_DETAILS { get; set; }

        public string ATCTS { get; set; }

        public string MANDATED_BY { get; set; }

        public string URL { get; set; }

        public int REQ_ID { get; set; }

        public int RPT_ELE_ID { get; set; }

        public int CLASS_DUR { get; set; }
    }
}
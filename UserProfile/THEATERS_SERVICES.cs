//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserProfile
{
    using System;
    using System.Collections.Generic;
    
    public partial class THEATERS_SERVICES
    {
        public int TS_ID { get; set; }
        public int TS_T_ID { get; set; }
        public int TS_S_ID { get; set; }
        public Nullable<int> TS_S_PARENT { get; set; }
        public Nullable<int> TS_S_P_POC_ID { get; set; }
        public Nullable<int> TS_S_T_TYPE { get; set; }
        public Nullable<decimal> TS_S_HASTPOC { get; set; }
        public Nullable<decimal> TS_S_SIGACT_ID { get; set; }
        public Nullable<decimal> TS_S_KPI_ID { get; set; }
        public Nullable<decimal> TS_S_KPI_LABEL_ID { get; set; }
        public int TS_S_ACTIVE { get; set; }
        public string TS_S_EMAIL { get; set; }
        public Nullable<int> TS_S_OWNER { get; set; }
    }
}
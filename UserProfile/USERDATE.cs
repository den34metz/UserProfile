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
    
    public partial class USERDATE
    {
        public int DATETAKENID { get; set; }
        public Nullable<System.DateTime> DATETAKEN { get; set; }
        public Nullable<System.DateTime> DATENEXTDUE { get; set; }
        public Nullable<int> TRAIN_ID { get; set; }
        public Nullable<int> EDIPI { get; set; }
        public byte[] CERT { get; set; }
        public string CERTNAME { get; set; }
        public string VERIFIED { get; set; }
        public string VERIFIED_BY { get; set; }
        public string verifiedBy { get; set; }
        public string TRAINTITLE { get; set; }
        public Nullable<System.DateTime> DATESCHED { get; set; }
    
        public virtual TRAINING TRAINING { get; set; }
    }
}

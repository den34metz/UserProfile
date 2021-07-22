﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TrainingTracker : DbContext
    {
        public TrainingTracker()
            : base("name=TrainingTracker")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
      /*  public virtual DbSet<REQUIRED> REQUIREDs { get; set; }
        public virtual DbSet<RPT_ELE> RPT_ELE { get; set; }
        public virtual DbSet<SCHEDDATE> SCHEDDATES { get; set; }
        public virtual DbSet<TRAIN_DATE_TIME> TRAIN_DATE_TIME { get; set; }
        public virtual DbSet<TRAINING> TRAININGS { get; set; }*/
        public virtual DbSet<USERDATE> USERDATES { get; set; }
      /*  public virtual DbSet<POC_TRAININGS> POC_TRAININGS { get; set; }
        public virtual DbSet<RPT_ELE_TRAINING> RPT_ELE_TRAINING { get; set; }
        public virtual DbSet<TRAININGS_TRAIN_DATE_TIME> TRAININGS_TRAIN_DATE_TIME { get; set; }
        public virtual DbSet<CONST> CONSTs { get; set; }*/
       /* public virtual DbSet<ViewTrainingData> ViewTrainingDatas { get; set; }*/
    
        public virtual int SpAddUserDates(Nullable<System.DateTime> dATETAKEN, Nullable<System.DateTime> nEXTDUE, Nullable<int> eDIPI, Nullable<int> tRAINID, byte[] cERT, string cERTNAME)
        {
            var dATETAKENParameter = dATETAKEN.HasValue ?
                new ObjectParameter("DATETAKEN", dATETAKEN) :
                new ObjectParameter("DATETAKEN", typeof(System.DateTime));
    
            var nEXTDUEParameter = nEXTDUE.HasValue ?
                new ObjectParameter("NEXTDUE", nEXTDUE) :
                new ObjectParameter("NEXTDUE", typeof(System.DateTime));
    
            var eDIPIParameter = eDIPI.HasValue ?
                new ObjectParameter("EDIPI", eDIPI) :
                new ObjectParameter("EDIPI", typeof(int));
    
            var tRAINIDParameter = tRAINID.HasValue ?
                new ObjectParameter("TRAINID", tRAINID) :
                new ObjectParameter("TRAINID", typeof(int));
    
            var cERTParameter = cERT != null ?
                new ObjectParameter("CERT", cERT) :
                new ObjectParameter("CERT", typeof(byte[]));
    
            var cERTNAMEParameter = cERTNAME != null ?
                new ObjectParameter("CERTNAME", cERTNAME) :
                new ObjectParameter("CERTNAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpAddUserDates", dATETAKENParameter, nEXTDUEParameter, eDIPIParameter, tRAINIDParameter, cERTParameter, cERTNAMEParameter);
        }
    
        public virtual ObjectResult<USERDATE> FNSpAddUserDates(Nullable<System.DateTime> dATETAKEN, Nullable<System.DateTime> nEXTDUE, Nullable<int> eDIPI, Nullable<int> tRAINID, byte[] cERT, string cERTNAME)
        {
            var dATETAKENParameter = dATETAKEN.HasValue ?
                new ObjectParameter("DATETAKEN", dATETAKEN) :
                new ObjectParameter("DATETAKEN", typeof(System.DateTime));
    
            var nEXTDUEParameter = nEXTDUE.HasValue ?
                new ObjectParameter("NEXTDUE", nEXTDUE) :
                new ObjectParameter("NEXTDUE", typeof(System.DateTime));
    
            var eDIPIParameter = eDIPI.HasValue ?
                new ObjectParameter("EDIPI", eDIPI) :
                new ObjectParameter("EDIPI", typeof(int));
    
            var tRAINIDParameter = tRAINID.HasValue ?
                new ObjectParameter("TRAINID", tRAINID) :
                new ObjectParameter("TRAINID", typeof(int));
    
            var cERTParameter = cERT != null ?
                new ObjectParameter("CERT", cERT) :
                new ObjectParameter("CERT", typeof(byte[]));
    
            var cERTNAMEParameter = cERTNAME != null ?
                new ObjectParameter("CERTNAME", cERTNAME) :
                new ObjectParameter("CERTNAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USERDATE>("FNSpAddUserDates", dATETAKENParameter, nEXTDUEParameter, eDIPIParameter, tRAINIDParameter, cERTParameter, cERTNAMEParameter);
        }
    
        public virtual ObjectResult<USERDATE> FNSpAddUserDates(Nullable<System.DateTime> dATETAKEN, Nullable<System.DateTime> nEXTDUE, Nullable<int> eDIPI, Nullable<int> tRAINID, byte[] cERT, string cERTNAME, MergeOption mergeOption)
        {
            var dATETAKENParameter = dATETAKEN.HasValue ?
                new ObjectParameter("DATETAKEN", dATETAKEN) :
                new ObjectParameter("DATETAKEN", typeof(System.DateTime));
    
            var nEXTDUEParameter = nEXTDUE.HasValue ?
                new ObjectParameter("NEXTDUE", nEXTDUE) :
                new ObjectParameter("NEXTDUE", typeof(System.DateTime));
    
            var eDIPIParameter = eDIPI.HasValue ?
                new ObjectParameter("EDIPI", eDIPI) :
                new ObjectParameter("EDIPI", typeof(int));
    
            var tRAINIDParameter = tRAINID.HasValue ?
                new ObjectParameter("TRAINID", tRAINID) :
                new ObjectParameter("TRAINID", typeof(int));
    
            var cERTParameter = cERT != null ?
                new ObjectParameter("CERT", cERT) :
                new ObjectParameter("CERT", typeof(byte[]));
    
            var cERTNAMEParameter = cERTNAME != null ?
                new ObjectParameter("CERTNAME", cERTNAME) :
                new ObjectParameter("CERTNAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USERDATE>("FNSpAddUserDates", mergeOption, dATETAKENParameter, nEXTDUEParameter, eDIPIParameter, tRAINIDParameter, cERTParameter, cERTNAMEParameter);
        }
    
        public virtual int SpInsertUserDates(Nullable<System.DateTime> dATETAKEN, Nullable<System.DateTime> dATENEXTDUE, Nullable<int> eDIPI, Nullable<int> tRAIN_ID, byte[] cERT, string cERTNAME, Nullable<int> dATETAKENID)
        {
            var dATETAKENParameter = dATETAKEN.HasValue ?
                new ObjectParameter("DATETAKEN", dATETAKEN) :
                new ObjectParameter("DATETAKEN", typeof(System.DateTime));
    
            var dATENEXTDUEParameter = dATENEXTDUE.HasValue ?
                new ObjectParameter("DATENEXTDUE", dATENEXTDUE) :
                new ObjectParameter("DATENEXTDUE", typeof(System.DateTime));
    
            var eDIPIParameter = eDIPI.HasValue ?
                new ObjectParameter("EDIPI", eDIPI) :
                new ObjectParameter("EDIPI", typeof(int));
    
            var tRAIN_IDParameter = tRAIN_ID.HasValue ?
                new ObjectParameter("TRAIN_ID", tRAIN_ID) :
                new ObjectParameter("TRAIN_ID", typeof(int));
    
            var cERTParameter = cERT != null ?
                new ObjectParameter("CERT", cERT) :
                new ObjectParameter("CERT", typeof(byte[]));
    
            var cERTNAMEParameter = cERTNAME != null ?
                new ObjectParameter("CERTNAME", cERTNAME) :
                new ObjectParameter("CERTNAME", typeof(string));
    
            var dATETAKENIDParameter = dATETAKENID.HasValue ?
                new ObjectParameter("DATETAKENID", dATETAKENID) :
                new ObjectParameter("DATETAKENID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpInsertUserDates", dATETAKENParameter, dATENEXTDUEParameter, eDIPIParameter, tRAIN_IDParameter, cERTParameter, cERTNAMEParameter, dATETAKENIDParameter);
        }
    }
}
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
    
    public partial class PREFERENCE_THEATER_VALUES
    {
        public int P_ID { get; set; }
        public int T_ID { get; set; }
        public int PTV_VALUE { get; set; }
    
        public virtual PREFERENCE PREFERENCE { get; set; }
        public virtual THEATER THEATER { get; set; }
    }
}

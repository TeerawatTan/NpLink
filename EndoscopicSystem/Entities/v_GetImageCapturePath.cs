//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EndoscopicSystem.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_GetImageCapturePath
    {
        public int EndoscopicImageID { get; set; }
        public string ImagePath { get; set; }
        public int AppointmentID { get; set; }
        public Nullable<int> ProcedureID { get; set; }
        public string HN { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}

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
    
    public partial class Intervention
    {
        public int InterventionID { get; set; }
        public Nullable<bool> Spincterotomy { get; set; }
        public Nullable<bool> StoneExtraction { get; set; }
        public Nullable<bool> StentInsertion { get; set; }
        public Nullable<bool> IsPlastic { get; set; }
        public string PlasticFoot { get; set; }
        public string PlasticCentimeter { get; set; }
        public Nullable<bool> IsMetal { get; set; }
        public string MetalFoot { get; set; }
        public string MetalCentimeter { get; set; }
        public Nullable<bool> Hemonstasis { get; set; }
        public Nullable<bool> Adrenaline { get; set; }
        public Nullable<bool> Coagulation { get; set; }
        public Nullable<bool> Specimens { get; set; }
        public Nullable<bool> BiopsyforPathological { get; set; }
        public string OtherDetail1 { get; set; }
        public string OtherDetail2 { get; set; }
        public string OtherDetail3 { get; set; }
        public string OtherDetail4 { get; set; }
        public string OtherDetail5 { get; set; }
    }
}

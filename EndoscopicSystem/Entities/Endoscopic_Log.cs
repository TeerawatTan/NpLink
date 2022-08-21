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
    
    public partial class Endoscopic_Log
    {
        public int ID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int EndoscopicID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<int> ProcedureID { get; set; }
        public Nullable<bool> NewCase { get; set; }
        public Nullable<bool> FollowUpCase { get; set; }
        public Nullable<int> InCase { get; set; }
        public string Diagnosis { get; set; }
        public string Complication { get; set; }
        public string Comment { get; set; }
        public string ReferringPhysicain { get; set; }
        public Nullable<int> EndoscopistID { get; set; }
        public Nullable<System.DateTime> Arrive { get; set; }
        public string Instrument { get; set; }
        public Nullable<int> MedicationID { get; set; }
        public string FluDose { get; set; }
        public string Intervention { get; set; }
        public string History { get; set; }
        public Nullable<int> NurseFirstID { get; set; }
        public Nullable<int> NurseSecondID { get; set; }
        public Nullable<int> NurseThirthID { get; set; }
        public string Assistant1 { get; set; }
        public string Assistant2 { get; set; }
        public string Anesthesia { get; set; }
        public Nullable<int> Indication { get; set; }
        public string AnesNurse { get; set; }
        public Nullable<int> FindingID { get; set; }
        public Nullable<int> IndicationID { get; set; }
        public Nullable<int> SpecimenID { get; set; }
        public Nullable<int> InterventionID { get; set; }
        public Nullable<System.DateTime> StartRecordDate { get; set; }
        public Nullable<System.DateTime> EndRecordDate { get; set; }
        public string MedicationOther { get; set; }
        public string IndicationOther { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}

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
    
    public partial class Patient
    {
        public int PatientID { get; set; }
        public string HN { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string CardID { get; set; }
        public Nullable<bool> Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public string MobileNumber { get; set; }
        public string FullAddress { get; set; }
        public Nullable<int> RoomID { get; set; }
        public string StaffName { get; set; }
        public Nullable<int> ProcedureID { get; set; }
        public Nullable<int> NurseFirstID { get; set; }
        public Nullable<int> NurseSecondID { get; set; }
        public Nullable<int> NurseThirthID { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public Nullable<System.DateTime> AppointmentDate { get; set; }
        public string PicturePath { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<int> WardID { get; set; }
        public Nullable<bool> ReferCheck { get; set; }
        public string ReferDetail { get; set; }
        public Nullable<int> AnesthesistID { get; set; }
        public Nullable<int> AnesthesistMethodFirstID { get; set; }
        public Nullable<int> AnesthesistMethodSecondID { get; set; }
        public Nullable<int> IndicationID { get; set; }
        public string PreDiagnosisFirstID { get; set; }
        public string PreDiagnosisSecondID { get; set; }
        public Nullable<int> FinancialID { get; set; }
        public Nullable<int> OpdID { get; set; }
        public Nullable<int> AnesthesiaID { get; set; }
    }
}

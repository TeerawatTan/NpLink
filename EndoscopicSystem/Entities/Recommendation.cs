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
    
    public partial class Recommendation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ProcedureId { get; set; }
        public bool IsDisplay { get; set; }
    }
}

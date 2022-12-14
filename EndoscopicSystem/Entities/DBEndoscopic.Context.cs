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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EndoscopicEntities : DbContext
    {
        public EndoscopicEntities()
            : base("name=EndoscopicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<EndoscopicImage> EndoscopicImages { get; set; }
        public virtual DbSet<EndoscopicVideo> EndoscopicVideos { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Nurse> Nurses { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<AmpullaOfVater> AmpullaOfVaters { get; set; }
        public virtual DbSet<AnalCanal> AnalCanals { get; set; }
        public virtual DbSet<Anesthesia> Anesthesias { get; set; }
        public virtual DbSet<Antrum> Antrums { get; set; }
        public virtual DbSet<AscendingColon> AscendingColons { get; set; }
        public virtual DbSet<Body> Bodies { get; set; }
        public virtual DbSet<Cardia> Cardias { get; set; }
        public virtual DbSet<Cecum> Cecums { get; set; }
        public virtual DbSet<Cholangiogram> Cholangiograms { get; set; }
        public virtual DbSet<DescendingColon> DescendingColons { get; set; }
        public virtual DbSet<DuodenalBulb> DuodenalBulbs { get; set; }
        public virtual DbSet<Duodenum> Duodenums { get; set; }
        public virtual DbSet<EGJunction> EGJunctions { get; set; }
        public virtual DbSet<ERCPEsophagu> ERCPEsophagus { get; set; }
        public virtual DbSet<Esophagu> Esophagus { get; set; }
        public virtual DbSet<Fundu> Fundus { get; set; }
        public virtual DbSet<HepaticFlexure> HepaticFlexures { get; set; }
        public virtual DbSet<IleocecalValve> IleocecalValves { get; set; }
        public virtual DbSet<IndicationDropdown> IndicationDropdowns { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<Oropharynx> Oropharynges { get; set; }
        public virtual DbSet<Pancreatogram> Pancreatograms { get; set; }
        public virtual DbSet<Pyloru> Pylorus { get; set; }
        public virtual DbSet<Rectum> Rectums { get; set; }
        public virtual DbSet<SecondPart> SecondParts { get; set; }
        public virtual DbSet<SigmoidColon> SigmoidColons { get; set; }
        public virtual DbSet<SplenicFlexure> SplenicFlexures { get; set; }
        public virtual DbSet<Stomach> Stomaches { get; set; }
        public virtual DbSet<TerminalIleum> TerminalIleums { get; set; }
        public virtual DbSet<TransverseColon> TransverseColons { get; set; }
        public virtual DbSet<ProcedureList> ProcedureLists { get; set; }
        public virtual DbSet<Indication> Indications { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Speciman> Specimen { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<EndoscopicAllImage> EndoscopicAllImages { get; set; }
        public virtual DbSet<Carina> Carinas { get; set; }
        public virtual DbSet<LeftMain> LeftMains { get; set; }
        public virtual DbSet<Lingular> Lingulars { get; set; }
        public virtual DbSet<LLL> LLLs { get; set; }
        public virtual DbSet<LUL> LULs { get; set; }
        public virtual DbSet<RightIntermideate> RightIntermideates { get; set; }
        public virtual DbSet<RightMain> RightMains { get; set; }
        public virtual DbSet<RLL> RLLs { get; set; }
        public virtual DbSet<RML> RMLs { get; set; }
        public virtual DbSet<RUL> RULs { get; set; }
        public virtual DbSet<Trachea> Tracheas { get; set; }
        public virtual DbSet<VocalCord> VocalCords { get; set; }
        public virtual DbSet<Endoscopic> Endoscopics { get; set; }
        public virtual DbSet<v_HistoryEndoscopic> v_HistoryEndoscopic { get; set; }
        public virtual DbSet<v_PatientList> v_PatientList { get; set; }
        public virtual DbSet<Endoscopic_Log> Endoscopic_Log { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<v_AppointmentDetails> v_AppointmentDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BaseOfTongue> BaseOfTongues { get; set; }
        public virtual DbSet<EustachianTubeOrifice> EustachianTubeOrifices { get; set; }
        public virtual DbSet<Glottic> Glottics { get; set; }
        public virtual DbSet<LE> LES { get; set; }
        public virtual DbSet<NasalCavity> NasalCavities { get; set; }
        public virtual DbSet<Nasopharynx> Nasopharynges { get; set; }
        public virtual DbSet<Postcricoid> Postcricoids { get; set; }
        public virtual DbSet<PosteriorPharyngealWall> PosteriorPharyngealWalls { get; set; }
        public virtual DbSet<PosteriorWall> PosteriorWalls { get; set; }
        public virtual DbSet<PyriformSinu> PyriformSinus { get; set; }
        public virtual DbSet<Roof> Roofs { get; set; }
        public virtual DbSet<RosenmullerFossa> RosenmullerFossas { get; set; }
        public virtual DbSet<Septum> Septums { get; set; }
        public virtual DbSet<SoftPalate> SoftPalates { get; set; }
        public virtual DbSet<Subglottic> Subglottics { get; set; }
        public virtual DbSet<Supraglottic> Supraglottics { get; set; }
        public virtual DbSet<Tonsil> Tonsils { get; set; }
        public virtual DbSet<UE> UES { get; set; }
        public virtual DbSet<Uvula> Uvulas { get; set; }
        public virtual DbSet<Vallecula> Valleculas { get; set; }
        public virtual DbSet<Finding> Findings { get; set; }
    
        public virtual ObjectResult<BrochoscopyReport_Result> BrochoscopyReport(string hn, Nullable<int> procedure, Nullable<int> endoscopicId)
        {
            var hnParameter = hn != null ?
                new ObjectParameter("hn", hn) :
                new ObjectParameter("hn", typeof(string));
    
            var procedureParameter = procedure.HasValue ?
                new ObjectParameter("procedure", procedure) :
                new ObjectParameter("procedure", typeof(int));
    
            var endoscopicIdParameter = endoscopicId.HasValue ?
                new ObjectParameter("endoscopicId", endoscopicId) :
                new ObjectParameter("endoscopicId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BrochoscopyReport_Result>("BrochoscopyReport", hnParameter, procedureParameter, endoscopicIdParameter);
        }
    
        public virtual ObjectResult<ColonoscopyReport_Result> ColonoscopyReport(string hn, Nullable<int> procedure, Nullable<int> endoscopicId)
        {
            var hnParameter = hn != null ?
                new ObjectParameter("hn", hn) :
                new ObjectParameter("hn", typeof(string));
    
            var procedureParameter = procedure.HasValue ?
                new ObjectParameter("procedure", procedure) :
                new ObjectParameter("procedure", typeof(int));
    
            var endoscopicIdParameter = endoscopicId.HasValue ?
                new ObjectParameter("endoscopicId", endoscopicId) :
                new ObjectParameter("endoscopicId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ColonoscopyReport_Result>("ColonoscopyReport", hnParameter, procedureParameter, endoscopicIdParameter);
        }
    
        public virtual ObjectResult<EndoscopicReport_Result> EndoscopicReport(string hn, Nullable<int> procedure, Nullable<int> endoscopicId)
        {
            var hnParameter = hn != null ?
                new ObjectParameter("hn", hn) :
                new ObjectParameter("hn", typeof(string));
    
            var procedureParameter = procedure.HasValue ?
                new ObjectParameter("procedure", procedure) :
                new ObjectParameter("procedure", typeof(int));
    
            var endoscopicIdParameter = endoscopicId.HasValue ?
                new ObjectParameter("endoscopicId", endoscopicId) :
                new ObjectParameter("endoscopicId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EndoscopicReport_Result>("EndoscopicReport", hnParameter, procedureParameter, endoscopicIdParameter);
        }
    
        public virtual ObjectResult<GastroscopyReport_Result> GastroscopyReport(string hn, Nullable<int> procedure, Nullable<int> endoscopicId)
        {
            var hnParameter = hn != null ?
                new ObjectParameter("hn", hn) :
                new ObjectParameter("hn", typeof(string));
    
            var procedureParameter = procedure.HasValue ?
                new ObjectParameter("procedure", procedure) :
                new ObjectParameter("procedure", typeof(int));
    
            var endoscopicIdParameter = endoscopicId.HasValue ?
                new ObjectParameter("endoscopicId", endoscopicId) :
                new ObjectParameter("endoscopicId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GastroscopyReport_Result>("GastroscopyReport", hnParameter, procedureParameter, endoscopicIdParameter);
        }
    
        public virtual ObjectResult<Chart_Result> Chart(Nullable<int> monthVal, Nullable<int> yearVal)
        {
            var monthValParameter = monthVal.HasValue ?
                new ObjectParameter("monthVal", monthVal) :
                new ObjectParameter("monthVal", typeof(int));
    
            var yearValParameter = yearVal.HasValue ?
                new ObjectParameter("yearVal", yearVal) :
                new ObjectParameter("yearVal", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Chart_Result>("Chart", monthValParameter, yearValParameter);
        }
    
        public virtual int CleanData(Nullable<int> isSuperAdmin)
        {
            var isSuperAdminParameter = isSuperAdmin.HasValue ?
                new ObjectParameter("IsSuperAdmin", isSuperAdmin) :
                new ObjectParameter("IsSuperAdmin", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CleanData", isSuperAdminParameter);
        }
    }
}

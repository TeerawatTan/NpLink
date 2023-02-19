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
    
    public partial class Finding
    {
        public int FindingID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string VocalCord { get; set; }
        public string Trachea { get; set; }
        public string Carina { get; set; }
        public string RightMain { get; set; }
        public string RightIntermideate { get; set; }
        public string RUL { get; set; }
        public string RML { get; set; }
        public string RLL { get; set; }
        public string LeftMain { get; set; }
        public string LUL { get; set; }
        public string Lingular { get; set; }
        public string LLL { get; set; }
        public Nullable<int> AnalCanalID { get; set; }
        public Nullable<int> RectumID { get; set; }
        public Nullable<int> SigmoidColonID { get; set; }
        public Nullable<int> DescendingColonID { get; set; }
        public Nullable<int> SplenicFlexureID { get; set; }
        public Nullable<int> TransverseColonID { get; set; }
        public Nullable<int> HepaticFlexureID { get; set; }
        public Nullable<int> AscendingColonID { get; set; }
        public Nullable<int> IleocecalVolveID { get; set; }
        public Nullable<int> CecumID { get; set; }
        public Nullable<int> TerminalIleumID { get; set; }
        public Nullable<int> EsophagusID { get; set; }
        public Nullable<int> StomachID { get; set; }
        public Nullable<int> DuodenumID { get; set; }
        public Nullable<int> AmpullaOfVaterID { get; set; }
        public Nullable<int> CholangiogramID { get; set; }
        public Nullable<int> PancreatogramID { get; set; }
        public Nullable<int> OropharynxID { get; set; }
        public Nullable<int> EGJunctionID { get; set; }
        public Nullable<int> CardiaID { get; set; }
        public Nullable<int> FundusID { get; set; }
        public Nullable<int> BodyID { get; set; }
        public Nullable<int> AntrumID { get; set; }
        public Nullable<int> PylorusID { get; set; }
        public Nullable<int> DuodenalBulbID { get; set; }
        public Nullable<int> SecondPartID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<int> VocalCordID { get; set; }
        public Nullable<int> TracheaID { get; set; }
        public Nullable<int> CarinaID { get; set; }
        public Nullable<int> RightMainID { get; set; }
        public Nullable<int> RightIntermideateID { get; set; }
        public Nullable<int> RULID { get; set; }
        public Nullable<int> RMLID { get; set; }
        public Nullable<int> RLLID { get; set; }
        public Nullable<int> LeftMainID { get; set; }
        public Nullable<int> LULID { get; set; }
        public Nullable<int> LingularID { get; set; }
        public Nullable<int> LLLID { get; set; }
        public string AnalCanal { get; set; }
        public string Rectum { get; set; }
        public string SigmoidColon { get; set; }
        public string DescendingColon { get; set; }
        public string SplenicFlexure { get; set; }
        public string TransverseColon { get; set; }
        public string HepaticFlexure { get; set; }
        public string AscendingColon { get; set; }
        public string IleocecalVolve { get; set; }
        public string Cecum { get; set; }
        public string TerminalIleum { get; set; }
        public string Esophagus { get; set; }
        public string Stomach { get; set; }
        public string Duodenum { get; set; }
        public string AmpullaOfVater { get; set; }
        public string Cholangiogram { get; set; }
        public string Pancreatogram { get; set; }
        public string Oropharynx { get; set; }
        public string EGJunction { get; set; }
        public string Cardia { get; set; }
        public string Fundus { get; set; }
        public string Body { get; set; }
        public string Antrum { get; set; }
        public string Pylorus { get; set; }
        public string DuodenalBulb { get; set; }
        public string SecondPart { get; set; }
        public Nullable<int> NasalCavityLeftID { get; set; }
        public string NasalCavityLeft { get; set; }
        public Nullable<int> NasalCavityRightID { get; set; }
        public string NasalCavityRight { get; set; }
        public Nullable<int> SeptumID { get; set; }
        public string Septum { get; set; }
        public Nullable<int> NasopharynxID { get; set; }
        public string Nasopharynx { get; set; }
        public Nullable<int> RoofID { get; set; }
        public string Roof { get; set; }
        public Nullable<int> PosteriorWallID { get; set; }
        public string PosteriorWall { get; set; }
        public Nullable<int> RosenmullerFossaID { get; set; }
        public string RosenmullerFossa { get; set; }
        public Nullable<int> EustachianTubeOrificeLeftID { get; set; }
        public string EustachianTubeOrificeLeft { get; set; }
        public Nullable<int> EustachianTubeOrificeRightID { get; set; }
        public string EustachianTubeOrificeRight { get; set; }
        public Nullable<int> SoftPalateID { get; set; }
        public string SoftPalate { get; set; }
        public Nullable<int> UvulaID { get; set; }
        public string Uvula { get; set; }
        public Nullable<int> TonsilID { get; set; }
        public string Tonsil { get; set; }
        public Nullable<int> BaseOfTongueID { get; set; }
        public string BaseOfTongue { get; set; }
        public Nullable<int> ValleculaID { get; set; }
        public string Vallecula { get; set; }
        public Nullable<int> PyriformSinusLeftID { get; set; }
        public string PyriformSinusLeft { get; set; }
        public Nullable<int> PyriformSinusRightID { get; set; }
        public string PyriformSinusRight { get; set; }
        public Nullable<int> PostcricoidID { get; set; }
        public string Postcricoid { get; set; }
        public Nullable<int> PosteriorPharyngealWallID { get; set; }
        public string PosteriorPharyngealWall { get; set; }
        public Nullable<int> SupraglotticID { get; set; }
        public string Supraglottic { get; set; }
        public Nullable<int> GlotticID { get; set; }
        public string Glottic { get; set; }
        public Nullable<int> SubglotticID { get; set; }
        public string Subglottic { get; set; }
        public Nullable<int> UESID { get; set; }
        public string UES { get; set; }
        public Nullable<int> LESID { get; set; }
        public string LES { get; set; }
        public Nullable<int> PrincipalProcedureID { get; set; }
        public string PrincipalProcedureDetail { get; set; }
        public Nullable<int> SupplementalProcedure1ID { get; set; }
        public string SupplementalProcedure1Detail { get; set; }
        public Nullable<int> SupplementalProcedure2ID { get; set; }
        public string SupplementalProcedure2Detail { get; set; }
        public string Procedure { get; set; }
        public Nullable<int> DxID1 { get; set; }
        public string DxID1Detail { get; set; }
        public Nullable<int> DxID2 { get; set; }
        public string DxID2Detail { get; set; }
        public Nullable<int> DxID3 { get; set; }
        public string DxID3Detail { get; set; }
        public Nullable<int> DxID4 { get; set; }
        public string DxID4Detail { get; set; }
        public string Complication { get; set; }
        public string Histopathology { get; set; }
        public string Recommendation { get; set; }
        public string Comment { get; set; }
        public string RapidUreaseTest { get; set; }
        public Nullable<int> PapillaMajorID { get; set; }
        public string PapillaMajor { get; set; }
        public Nullable<int> PapillaMinorID { get; set; }
        public string PapillaMinor { get; set; }
        public Nullable<int> PancreasID { get; set; }
        public string Pancreas { get; set; }
        public Nullable<int> BiliarySystemID { get; set; }
        public string BiliarySystem { get; set; }
        public Nullable<int> OtherID { get; set; }
        public string OtherDetail { get; set; }
    }
}

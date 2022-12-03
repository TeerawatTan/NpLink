using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EndoscopicSystem.Repository
{
    public class GetDropdownList
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        public List<ProcedureList> GetProcedureList()
        {
            var list = db.ProcedureLists.ToList();
            list.Insert(0, new ProcedureList() { ProcedureID = 0, ProcedureName = "" });
            return list;
        }
        public List<Doctor> GetEndoscopistList()
        {
            var list = db.Doctors.Where(x => x.IsActive.Value).ToList();
            list.Insert(0, new Doctor() { DoctorID = 0, NameTH = "" });
            return list;
        }
        public List<Medication> GetMedicationList()
        {
            var list = db.Medications.ToList();
            list.Insert(0, new Medication() { MedicationID = 0, MedicationName = "" });
            return list;
        }
        public List<IndicationDropdown> GetIndicationList()
        {
            var list = db.IndicationDropdowns.ToList();
            list.Insert(0, new IndicationDropdown() { IndicationID = 0, IndicationName = "" });
            return list;
        }
        public List<Room> GetRoomList()
        {
            var list = db.Rooms.Where(x => x.IsActive.Value).ToList();
            list.Insert(0, new Room() { RoomID = 0, NameTH = "", NameEN = "" });
            return list;
        }
        public List<Nurse> GetNurseList()
        {
            var list = db.Nurses.Where(x => x.IsActive.Value).ToList();
            list.Insert(0, new Nurse() { NurseID = 0, NameTH = "", NameEN = "" });
            return list;
        }
        public List<OpdList> GetOpdLists()
        {
            var list = db.OpdLists.ToList();
            list.Insert(0, new OpdList() { OpdID = 0, OpdName = "" });
            return list;
        }
        public List<WardList> GetWardLists()
        {
            var list = db.WardLists.ToList();
            list.Insert(0, new WardList() { WardID = 0, WardName = "" });
            return list;
        }
        public List<AnesthesistMethod> GetAnesthesistMethods()
        {
            var list = db.AnesthesistMethods.ToList();
            list.Insert(0, new AnesthesistMethod() { ID = 0, Name = "" });
            return list;
        }
        public List<ICD9> GetICD9s()
        {
            var list = db.ICD9.ToList();
            list.Insert(0, new ICD9() { ID = 0, Code = "0", Name = "" });
            return list;
        }
        public List<ICD10> GetICD10s()
        {
            var list = db.ICD10.ToList();
            list.Insert(0, new ICD10() { ID = 0, Code = "0", Name = "" });
            return list;
        }
        public List<Anesthesist> GetAnesthesists()
        {
            var list = db.Anesthesists.ToList();
            list.Insert(0, new Anesthesist() { AnesthesistID = 0, NameTH = "" });
            return list;
        }
        public List<Anesthesia> GetAnesthesias()
        {
            var list = db.Anesthesias.ToList();
            list.Insert(0, new Anesthesia() { AnesthesiaID = 0, AnesthesiaName = "" });
            return list;
        }
        public List<Financial> GetFinancial()
        {
            var list = db.Financials.ToList();
            list.Insert(0, new Financial() { ID = 0, Name = "" });
            return list;
        }
        public List<AnalCanal> GetAnalCanalList() => db.AnalCanals.ToList();
        public List<Rectum> GetRectumList() => db.Rectums.ToList();
        public List<SigmoidColon> GetSigmoidColonList() => db.SigmoidColons.ToList();
        public List<DescendingColon> GetDescendingColonList() => db.DescendingColons.ToList();
        public List<SplenicFlexure> GetSplenicFlexureList() => db.SplenicFlexures.ToList();
        public List<TransverseColon> GetTransverseColonList() => db.TransverseColons.ToList();
        public List<HepaticFlexure> GetHepaticFlexureList() => db.HepaticFlexures.ToList();
        public List<AscendingColon> GetAscendingColonList() => db.AscendingColons.ToList();
        public List<IleocecalValve> GetIleocecalValveList() => db.IleocecalValves.ToList();
        public List<Cecum> GetCecumList() => db.Cecums.ToList();
        public List<TerminalIleum> GetTerminalIleumList() => db.TerminalIleums.ToList();
        public List<Esophagu> GetEsophagusList() => db.Esophagus.ToList();
        public List<Stomach> GetStomachList() => db.Stomaches.ToList();
        public List<Duodenum> GetDuodenumList() => db.Duodenums.ToList();
        public List<AmpullaOfVater> GetAmpullaList() => db.AmpullaOfVaters.ToList();
        public List<Cholangiogram> GetCholangiogramList() => db.Cholangiograms.ToList();
        public List<Pancreatogram> GetPancreatogramList() => db.Pancreatograms.ToList();
        public List<Oropharynx> GetOropharynxList() => db.Oropharynges.ToList();
        public List<EGJunction> GetEGJunctionList() => db.EGJunctions.ToList();
        public List<Cardia> GetCardiaList() => db.Cardias.ToList();
        public List<Fundu> GetFundusList() => db.Fundus.ToList();
        public List<Body> GetBodyList() => db.Bodies.ToList();
        public List<Antrum> GetAntrumList() => db.Antrums.ToList();
        public List<Pyloru> GetPylorusList() => db.Pylorus.ToList();
        public List<DuodenalBulb> GetDuodenalBulbList() => db.DuodenalBulbs.ToList();
        public List<SecondPart> GetSecondPartList() => db.SecondParts.ToList();
        public List<VocalCord> GetVocalCordList() => db.VocalCords.ToList();
        public List<Trachea> GetTracheaList() => db.Tracheas.ToList();
        public List<Carina> GetCarinaList() => db.Carinas.ToList();
        public List<RightMain> GetRightMainList() => db.RightMains.ToList();
        public List<RightIntermideate> GetRightIntermideateList() => db.RightIntermideates.ToList();
        public List<RUL> GetRULList() => db.RULs.ToList();
        public List<RML> GetRMLList() => db.RMLs.ToList();
        public List<RLL> GetRLLList() => db.RLLs.ToList();
        public List<LeftMain> GetLeftMainList() => db.LeftMains.ToList();
        public List<LUL> GetLULList() => db.LULs.ToList();
        public List<Lingular> GetLingularList() => db.Lingulars.ToList();
        public List<LLL> GetLLLList() => db.LLLs.ToList();
        public List<NasalCavity> GetNasalCavities() => db.NasalCavities.ToList();
        public List<Septum> GetSeptums() => db.Septums.ToList();
        public List<Nasopharynx> GetNasopharynxes() => db.Nasopharynges.ToList();
        public List<Roof> GetRoofs() => db.Roofs.ToList();
        public List<PosteriorWall> GetPosteriorWalls() => db.PosteriorWalls.ToList();
        public List<RosenmullerFossa> GetRosenmullerFossas() => db.RosenmullerFossas.ToList();
        public List<EustachianTubeOrifice> GetEustachianTubeOrifices() => db.EustachianTubeOrifices.ToList();
        public List<SoftPalate> GetSoftPalates() => db.SoftPalates.ToList();
        public List<Uvula> GetUvulas() => db.Uvulas.ToList();
        public List<Tonsil> GetTonsils() => db.Tonsils.ToList();
        public List<BaseOfTongue> GetBaseOfTongues() => db.BaseOfTongues.ToList();
        public List<Vallecula> GetValleculas() => db.Valleculas.ToList();
        public List<PyriformSinu> GetPyriformSinus() => db.PyriformSinus.ToList();
        public List<Postcricoid> GetPostcricoids() => db.Postcricoids.ToList();
        public List<PosteriorPharyngealWall> GetPosteriorPharyngealWalls() => db.PosteriorPharyngealWalls.ToList();
        public List<Supraglottic> GetSupraglottics() => db.Supraglottics.ToList();
        public List<Glottic> GetGlottics() => db.Glottics.ToList();
        public List<Subglottic> GetSubglottics() => db.Subglottics.ToList();
        public List<UE> GetUEs() => db.UES.ToList();
        public List<LE> GetLEs() => db.LES.ToList();
        public List<FindingLabel> GetFindingLabels(int procedureId) => db.FindingLabels.Where(w => w.ProcedureID == procedureId).ToList();
    }
}

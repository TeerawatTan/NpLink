using EndoscopicSystem.Entities;
using EndoscopicSystem.V2.Forms;
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
            var list = db.ProcedureLists.Where(w => w.IsActive).ToList();
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
        public List<Financial> GetFinancials()
        {
            var list = db.Financials.ToList();
            list.Insert(0, new Financial() { ID = 0, Name = "" });
            return list;
        }
        public List<InstrumentConfirmModel> GetInstruments(int startIdx = 0)
        {
            var list = db.Instruments.Where(w => w.IsActive).ToList();
            if (startIdx == 0)
                list.Insert(0, new Instrument() { ID = 0, Code = "", SerialNumber = "" });
            List<InstrumentConfirmModel> res = new List<InstrumentConfirmModel>();

            foreach (var item in list)
            {
                InstrumentConfirmModel i = new InstrumentConfirmModel()
                {
                    No = item.ID,
                    Name = item.Code + " - " + item.SerialNumber
                };
                res.Add(i);
            }
            return res;
        }
        public List<Instrument> GetInstrumentsIdAndCode(int startIdx = 0)
        {
            var list = db.Instruments.Where(w => w.IsActive).ToList();
            if (startIdx == 0)
                list.Insert(0, new Instrument() { ID = 0, Code = "", SerialNumber = "" });

            return list;
        }
        public IQueryable<AnalCanal> GetAnalCanalList() => db.AnalCanals;
        public IQueryable<Rectum> GetRectumList() => db.Rectums;
        public IQueryable<SigmoidColon> GetSigmoidColonList() => db.SigmoidColons;
        public IQueryable<DescendingColon> GetDescendingColonList() => db.DescendingColons;
        public IQueryable<SplenicFlexure> GetSplenicFlexureList() => db.SplenicFlexures;
        public IQueryable<TransverseColon> GetTransverseColonList() => db.TransverseColons;
        public IQueryable<HepaticFlexure> GetHepaticFlexureList() => db.HepaticFlexures;
        public IQueryable<AscendingColon> GetAscendingColonList() => db.AscendingColons;
        public IQueryable<IleocecalValve> GetIleocecalValveList() => db.IleocecalValves;
        public IQueryable<Cecum> GetCecumList() => db.Cecums;
        public IQueryable<TerminalIleum> GetTerminalIleumList() => db.TerminalIleums;
        public IQueryable<Esophagu> GetEsophagusList() => db.Esophagus;
        public IQueryable<Stomach> GetStomachList() => db.Stomaches;
        public IQueryable<Duodenum> GetDuodenumList() => db.Duodenums;
        public IQueryable<AmpullaOfVater> GetAmpullaList() => db.AmpullaOfVaters;
        public IQueryable<Cholangiogram> GetCholangiogramList() => db.Cholangiograms;
        public IQueryable<Pancreatogram> GetPancreatogramList() => db.Pancreatograms;
        public IQueryable<Oropharynx> GetOropharynxList() => db.Oropharynges;
        public IQueryable<EGJunction> GetEGJunctionList() => db.EGJunctions;
        public IQueryable<Cardia> GetCardiaList() => db.Cardias;
        public IQueryable<Fundu> GetFundusList() => db.Fundus;
        public IQueryable<Body> GetBodyList() => db.Bodies;
        public IQueryable<Antrum> GetAntrumList() => db.Antrums;
        public IQueryable<Pyloru> GetPylorusList() => db.Pylorus;
        public IQueryable<DuodenalBulb> GetDuodenalBulbList() => db.DuodenalBulbs;
        public IQueryable<SecondPart> GetSecondPartList() => db.SecondParts;
        public IQueryable<VocalCord> GetVocalCordList() => db.VocalCords;
        public IQueryable<Trachea> GetTracheaList() => db.Tracheas;
        public IQueryable<Carina> GetCarinaList() => db.Carinas;
        public IQueryable<RightMain> GetRightMainList() => db.RightMains;
        public IQueryable<RightIntermideate> GetRightIntermideateList() => db.RightIntermideates;
        public IQueryable<RUL> GetRULList() => db.RULs;
        public IQueryable<RML> GetRMLList() => db.RMLs;
        public IQueryable<RLL> GetRLLList() => db.RLLs;
        public IQueryable<LeftMain> GetLeftMainList() => db.LeftMains;
        public IQueryable<LUL> GetLULList() => db.LULs;
        public IQueryable<Lingular> GetLingularList() => db.Lingulars;
        public IQueryable<LLL> GetLLLList() => db.LLLs;
        public IQueryable<NasalCavity> GetNasalCavities() => db.NasalCavities;
        public IQueryable<Septum> GetSeptums() => db.Septums;
        public IQueryable<Nasopharynx> GetNasopharynxes() => db.Nasopharynges;
        public IQueryable<Roof> GetRoofs() => db.Roofs;
        public IQueryable<PosteriorWall> GetPosteriorWalls() => db.PosteriorWalls;
        public IQueryable<RosenmullerFossa> GetRosenmullerFossas() => db.RosenmullerFossas;
        public IQueryable<EustachianTubeOrifice> GetEustachianTubeOrifices() => db.EustachianTubeOrifices;
        public IQueryable<SoftPalate> GetSoftPalates() => db.SoftPalates;
        public IQueryable<Uvula> GetUvulas() => db.Uvulas;
        public IQueryable<Tonsil> GetTonsils() => db.Tonsils;
        public IQueryable<BaseOfTongue> GetBaseOfTongues() => db.BaseOfTongues;
        public IQueryable<Vallecula> GetValleculas() => db.Valleculas;
        public IQueryable<PyriformSinu> GetPyriformSinus() => db.PyriformSinus;
        public IQueryable<Postcricoid> GetPostcricoids() => db.Postcricoids;
        public IQueryable<PosteriorPharyngealWall> GetPosteriorPharyngealWalls() => db.PosteriorPharyngealWalls;
        public IQueryable<Supraglottic> GetSupraglottics() => db.Supraglottics;
        public IQueryable<Glottic> GetGlottics() => db.Glottics;
        public IQueryable<Subglottic> GetSubglottics() => db.Subglottics;
        public IQueryable<UE> GetUEs() => db.UES;
        public IQueryable<LE> GetLEs() => db.LES;
        public IQueryable<FindingLabel> GetFindingLabels(int procedureId) => db.FindingLabels.Where(w => w.ProcedureID == procedureId);
        public IQueryable<PapillaMajor> GetPapillaMajors() => db.PapillaMajors;
        public IQueryable<PapillaMinor> GetPapillaMinors() => db.PapillaMinors;
        public IQueryable<Pancrea> GetPancreas() => db.Pancreas;
        public IQueryable<BiliarySystem> GetBiliarySystems() => db.BiliarySystems;
        public IQueryable<Other> GetOthers() => db.Other;
    }
}

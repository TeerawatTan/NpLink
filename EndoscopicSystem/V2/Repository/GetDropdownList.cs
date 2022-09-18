using EndoscopicSystem.V2.Database;
using System.Collections.Generic;
using System.Linq;

namespace EndoscopicSystem.V2.Repository
{
    public class GetDropdownList
    {
        readonly Database1Entities _db = new Database1Entities();
        public List<ProcedureList> GetProcedureList()
        {
            var list = _db.ProcedureLists.ToList();
            list.Insert(0, new ProcedureList() { ProcedureID = 0, ProcedureName = "" });
            return list;
        }
        public List<Doctor> GetEndoscopistList()
        {
            var list = _db.Doctors.Where(x => x.IsActive.Value).ToList();
            list.Insert(0, new Doctor() { DoctorID = 0, NameTH = "" });
            return list;
        }
        public List<Medication> GetMedicationList()
        {
            var list = _db.Medications.ToList();
            list.Insert(0, new Medication() { MedicationID = 0, MedicationName = "" });
            return list;
        }
        public List<IndicationDropdown> GetIndicationList()
        {
            var list = _db.IndicationDropdowns.ToList();
            list.Insert(0, new IndicationDropdown() { IndicationID = 0, IndicationName = "" });
            return list;
        }
        public List<AnalCanal> GetAnalCanalList()
        {
            var list = _db.AnalCanals.ToList();
            //list.Insert(0, new AnalCanal() { AnalCanalID = 0, AnalCanalName = "N/A" });
            return list;
        }
        public List<Rectum> GetRectumList()
        {
            var list = _db.Rectums.ToList();
            //list.Insert(0, new Rectum() { RectumID = 0, RectumName = "N/A" });
            return list;
        }
        public List<SigmoidColon> GetSigmoidColonList()
        {
            var list = _db.SigmoidColons.ToList();
            //list.Insert(0, new SigmoidColon() { SigmoidColonID = 0, SigmoidColonName = "N/A" });
            return list;
        }
        public List<DescendingColon> GetDescendingColonList()
        {
            var list = _db.DescendingColons.ToList();
            //list.Insert(0, new DescendingColon() { DescendingColonID = 0, DescendingColonName = "N/A" });
            return list;
        }
        public List<SplenicFlexure> GetSplenicFlexureList()
        {
            var list = _db.SplenicFlexures.ToList();
            //list.Insert(0, new SplenicFlexure() { SplenicFlexureID = 0, SplenicFlexureName = "N/A" });
            return list;
        }

        public List<TransverseColon> GetTransverseColonList()
        {
            var list = _db.TransverseColons.ToList();
            //list.Insert(0, new TransverseColon() { TransverseColonID = 0, TransverseColonName = "N/A" });
            return list;
        }

        public List<HepaticFlexure> GetHepaticFlexureList()
        {
            var list = _db.HepaticFlexures.ToList();
            //list.Insert(0, new HepaticFlexure() { HepaticFlexureID = 0, HepaticFlexureName = "N/A" });
            return list;
        }

        public List<AscendingColon> GetAscendingColonList()
        {
            var list = _db.AscendingColons.ToList();
            //list.Insert(0, new AscendingColon() { AscendingColonID = 0, AscendingColonName = "N/A" });
            return list;
        }

        public List<IleocecalValve> GetIleocecalValveList()
        {
            var list = _db.IleocecalValves.ToList();
            //list.Insert(0, new IleocecalValve() { IleocecalValveID = 0, IleocecalValveName = "N/A" });
            return list;
        }
        public List<Cecum> GetCecumList()
        {
            var list = _db.Cecums.ToList();
            //list.Insert(0, new Cecum() { CecumID = 0, CecumName = "N/A" });
            return list;
        }

        public List<TerminalIleum> GetTerminalIleumList()
        {
            var list = _db.TerminalIleums.ToList();
            //list.Insert(0, new TerminalIleum() { TerminalIleumID = 0, TerminalIleumName = "N/A" });
            return list;
        }

        public List<Esophagu> GetEsophagusList()
        {
            var list = _db.Esophagus.ToList();
            //list.Insert(0, new Esophagu() { EsophagusID = 0, EsophagusName = "N/A" });
            return list;
        }

        public List<Stomach> GetStomachList()
        {
            var list = _db.Stomaches.ToList();
            //list.Insert(0, new Stomach() { StomachID = 0, StomachName = "N/A" });
            return list;
        }

        public List<Duodenum> GetDuodenumList()
        {
            var list = _db.Duodenums.ToList();
            //list.Insert(0, new Duodenum() { DuodenumID = 0, DuodenumName = "N/A" });
            return list;
        }

        public List<AmpullaOfVater> GetAmpullaList()
        {
            var list = _db.AmpullaOfVaters.ToList();
            //list.Insert(0, new AmpullaOfVater() { AmpullaOfVaterID = 0, AmpullaOfVaterName = "N/A" });
            return list;
        }

        public List<Cholangiogram> GetCholangiogramList()
        {
            var list = _db.Cholangiograms.ToList();
            //list.Insert(0, new Cholangiogram() { CholangiogramID = 0, CholangiogramName = "N/A" });
            return list;
        }

        public List<Pancreatogram> GetPancreatogramList()
        {
            var list = _db.Pancreatograms.ToList();
            //list.Insert(0, new Pancreatogram() { PancreatogramID = 0, PancreatogramName = "N/A" });
            return list;
        }

        public List<Room> GetRoomList()
        {
            var list = _db.Rooms.Where(x => x.IsActive.Value).ToList();
            list.Insert(0, new Room() { RoomID = 0, NameTH = "", NameEN = "" });
            return list;
        }
        public List<Nurse> GetNurseList()
        {
            var list = _db.Nurses.Where(x => x.IsActive.Value).ToList();
            list.Insert(0, new Nurse() { NurseID = 0, NameTH = "", NameEN = "" });
            return list;
        }

        public List<Oropharynx> GetOropharynxList()
        {
            var list = _db.Oropharynges.ToList();
            //list.Insert(0, new Oropharynx() { OropharynxID = 0, OropharynxName = "N/A" });
            return list;
        }

        public List<EGJunction> GetEGJunctionList()
        {
            var list = _db.EGJunctions.ToList();
            //list.Insert(0, new EGJunction() { EGJunctionID = 0, EGJunctionName = "N/A" });
            return list;
        }

        public List<Cardia> GetCardiaList()
        {
            var list = _db.Cardias.ToList();
            //list.Insert(0, new Cardia() { CardiaID = 0, CardiaName = "N/A" });
            return list;
        }

        public List<Fundu> GetFundusList()
        {
            var list = _db.Fundus.ToList();
            //list.Insert(0, new Fundu() { FundusID = 0, FundusName = "N/A" });
            return list;
        }

        public List<Body> GetBodyList()
        {
            var list = _db.Bodies.ToList();
            //list.Insert(0, new Body() { BodyID = 0, BodyName = "N/A" });
            return list;
        }

        public List<Antrum> GetAntrumList()
        {
            var list = _db.Antrums.ToList();
            //list.Insert(0, new Antrum() { AntrumID = 0, AntrumName = "N/A" });
            return list;
        }

        public List<Pyloru> GetPylorusList()
        {
            var list = _db.Pylorus.ToList();
            //list.Insert(0, new Pyloru() { PylorusID = 0, PylorusName = "N/A" });
            return list;
        }

        public List<DuodenalBulb> GetDuodenalBulbList()
        {
            var list = _db.DuodenalBulbs.ToList();
            //list.Insert(0, new DuodenalBulb() { DuodenalBulbID = 0, DuodenalBulbName = "N/A" });
            return list;
        }

        public List<SecondPart> GetSecondPartList()
        {
            var list = _db.SecondParts.ToList();
            //list.Insert(0, new SecondPart() { SecondPartID = 0, SecondPartName = "N/A" });
            return list;
        }

        public List<VocalCord> GetVocalCordList()
        {
            var list = _db.VocalCords.ToList();
            //list.Insert(0, new VocalCord() { VocalCordID = 0, VocalCordName = "N/A" });
            return list;
        }
        public List<Trachea> GetTracheaList()
        {
            var list = _db.Tracheas.ToList();
            //list.Insert(0, new Trachea() { TracheaID = 0, TracheaName = "N/A" });
            return list;
        }
        public List<Carina> GetCarinaList()
        {
            var list = _db.Carinas.ToList();
            //list.Insert(0, new Carina() { CarinaID = 0, CarinaName = "N/A" });
            return list;
        }
        public List<RightMain> GetRightMainList()
        {
            var list = _db.RightMains.ToList();
            //list.Insert(0, new RightMain() { RightMainID = 0, RightMainName = "N/A" });
            return list;
        }
        public List<RightIntermideate> GetRightIntermideateList()
        {
            var list = _db.RightIntermideates.ToList();
            //list.Insert(0, new RightIntermideate() { RightIntermideateID = 0, RightIntermideateName = "N/A" });
            return list;
        }
        public List<RUL> GetRULList()
        {
            var list = _db.RULs.ToList();
            //list.Insert(0, new RUL() { RULID = 0, RULName = "N/A" });
            return list;
        }
        public List<RML> GetRMLList()
        {
            var list = _db.RMLs.ToList();
            //list.Insert(0, new RML() { RMLID = 0, RMLName = "N/A" });
            return list;
        }
        public List<RLL> GetRLLList()
        {
            var list = _db.RLLs.ToList();
            //list.Insert(0, new RLL() { RLLID = 0, RLLName = "N/A" });
            return list;
        }
        public List<LeftMain> GetLeftMainList()
        {
            var list = _db.LeftMains.ToList();
            //list.Insert(0, new LeftMain() { LeftMainID = 0, LeftMainName = "N/A" });
            return list;
        }
        public List<LUL> GetLULList()
        {
            var list = _db.LULs.ToList();
            //list.Insert(0, new LUL() { LULID = 0, LULName = "N/A" });
            return list;
        }
        public List<Lingular> GetLingularList()
        {
            var list = _db.Lingulars.ToList();
            //list.Insert(0, new Lingular() { LingularID = 0, LingularName = "N/A" });
            return list;
        }
        public List<LLL> GetLLLList()
        {
            var list = _db.LLLs.ToList();
            //list.Insert(0, new LLL() { LLLID = 0, LLLName = "N/A" });
            return list;
        }

        public List<NasalCavity> GetNasalCavities() => _db.NasalCavities.ToList();
        public List<Septum> GetSeptums() => _db.Septums.ToList();
        public List<Nasopharynx> GetNasopharynxes() => _db.Nasopharynges.ToList();
        public List<Roof> GetRoofs() => _db.Roofs.ToList();
        public List<PosteriorWall> GetPosteriorWalls() => _db.PosteriorWalls.ToList();
        public List<RosenmullerFossa> GetRosenmullerFossas() => _db.RosenmullerFossas.ToList();
        public List<EustachianTubeOrifice> GetEustachianTubeOrifices() => _db.EustachianTubeOrifices.ToList();
        public List<SoftPalate> GetSoftPalates() => _db.SoftPalates.ToList();
        public List<Uvula> GetUvulas() => _db.Uvulas.ToList();
        public List<Tonsil> GetTonsils() => _db.Tonsils.ToList();
        public List<BaseOfTongue> GetBaseOfTongues() => _db.BaseOfTongues.ToList();
        public List<Vallecula> GetValleculas() => _db.Valleculas.ToList();
        public List<PyriformSinu> GetPyriformSinus() => _db.PyriformSinus.ToList();
        public List<Postcricoid> GetPostcricoids() => _db.Postcricoids.ToList();
        public List<PosteriorPharyngealWall> GetPosteriorPharyngealWalls() => _db.PosteriorPharyngealWalls.ToList();
        public List<Supraglottic> GetSupraglottics() => _db.Supraglottics.ToList();
        public List<Glottic> GetGlottics() => _db.Glottics.ToList();
        public List<Subglottic> GetSubglottics() => _db.Subglottics.ToList();
        public List<UE> GetUEs() => _db.UES.ToList();
        public List<LE> GetLEs() => _db.LES.ToList();
    }
}

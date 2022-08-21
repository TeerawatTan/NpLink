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
        public List<AnalCanal> GetAnalCanalList()
        {
            var list = db.AnalCanals.ToList();
            //list.Insert(0, new AnalCanal() { AnalCanalID = 0, AnalCanalName = "N/A" });
            return list;
        }
        public List<Rectum> GetRectumList()
        {
            var list = db.Rectums.ToList();
            //list.Insert(0, new Rectum() { RectumID = 0, RectumName = "N/A" });
            return list;
        }
        public List<SigmoidColon> GetSigmoidColonList()
        {
            var list = db.SigmoidColons.ToList();
            //list.Insert(0, new SigmoidColon() { SigmoidColonID = 0, SigmoidColonName = "N/A" });
            return list;
        }
        public List<DescendingColon> GetDescendingColonList()
        {
            var list = db.DescendingColons.ToList();
            //list.Insert(0, new DescendingColon() { DescendingColonID = 0, DescendingColonName = "N/A" });
            return list;
        }
        public List<SplenicFlexure> GetSplenicFlexureList()
        {
            var list = db.SplenicFlexures.ToList();
            //list.Insert(0, new SplenicFlexure() { SplenicFlexureID = 0, SplenicFlexureName = "N/A" });
            return list;
        }

        public List<TransverseColon> GetTransverseColonList()
        {
            var list = db.TransverseColons.ToList();
            //list.Insert(0, new TransverseColon() { TransverseColonID = 0, TransverseColonName = "N/A" });
            return list;
        }

        public List<HepaticFlexure> GetHepaticFlexureList()
        {
            var list = db.HepaticFlexures.ToList();
            //list.Insert(0, new HepaticFlexure() { HepaticFlexureID = 0, HepaticFlexureName = "N/A" });
            return list;
        }

        public List<AscendingColon> GetAscendingColonList()
        {
            var list = db.AscendingColons.ToList();
            //list.Insert(0, new AscendingColon() { AscendingColonID = 0, AscendingColonName = "N/A" });
            return list;
        }

        public List<IleocecalValve> GetIleocecalValveList()
        {
            var list = db.IleocecalValves.ToList();
            //list.Insert(0, new IleocecalValve() { IleocecalValveID = 0, IleocecalValveName = "N/A" });
            return list;
        }
        public List<Cecum> GetCecumList()
        {
            var list = db.Cecums.ToList();
            //list.Insert(0, new Cecum() { CecumID = 0, CecumName = "N/A" });
            return list;
        }

        public List<TerminalIleum> GetTerminalIleumList()
        {
            var list = db.TerminalIleums.ToList();
            //list.Insert(0, new TerminalIleum() { TerminalIleumID = 0, TerminalIleumName = "N/A" });
            return list;
        }

        public List<Esophagu> GetEsophagusList()
        {
            var list = db.Esophagus.ToList();
            //list.Insert(0, new Esophagu() { EsophagusID = 0, EsophagusName = "N/A" });
            return list;
        }

        public List<Stomach> GetStomachList()
        {
            var list = db.Stomaches.ToList();
            //list.Insert(0, new Stomach() { StomachID = 0, StomachName = "N/A" });
            return list;
        }

        public List<Duodenum> GetDuodenumList()
        {
            var list = db.Duodenums.ToList();
            //list.Insert(0, new Duodenum() { DuodenumID = 0, DuodenumName = "N/A" });
            return list;
        }

        public List<AmpullaOfVater> GetAmpullaList()
        {
            var list = db.AmpullaOfVaters.ToList();
            //list.Insert(0, new AmpullaOfVater() { AmpullaOfVaterID = 0, AmpullaOfVaterName = "N/A" });
            return list;
        }

        public List<Cholangiogram> GetCholangiogramList()
        {
            var list = db.Cholangiograms.ToList();
            //list.Insert(0, new Cholangiogram() { CholangiogramID = 0, CholangiogramName = "N/A" });
            return list;
        }

        public List<Pancreatogram> GetPancreatogramList()
        {
            var list = db.Pancreatograms.ToList();
            //list.Insert(0, new Pancreatogram() { PancreatogramID = 0, PancreatogramName = "N/A" });
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

        public List<Oropharynx> GetOropharynxList()
        {
            var list = db.Oropharynges.ToList();
            //list.Insert(0, new Oropharynx() { OropharynxID = 0, OropharynxName = "N/A" });
            return list;
        }

        public List<EGJunction> GetEGJunctionList()
        {
            var list = db.EGJunctions.ToList();
            //list.Insert(0, new EGJunction() { EGJunctionID = 0, EGJunctionName = "N/A" });
            return list;
        }

        public List<Cardia> GetCardiaList()
        {
            var list = db.Cardias.ToList();
            //list.Insert(0, new Cardia() { CardiaID = 0, CardiaName = "N/A" });
            return list;
        }

        public List<Fundu> GetFundusList()
        {
            var list = db.Fundus.ToList();
            //list.Insert(0, new Fundu() { FundusID = 0, FundusName = "N/A" });
            return list;
        }

        public List<Body> GetBodyList()
        {
            var list = db.Bodies.ToList();
            //list.Insert(0, new Body() { BodyID = 0, BodyName = "N/A" });
            return list;
        }

        public List<Antrum> GetAntrumList()
        {
            var list = db.Antrums.ToList();
            //list.Insert(0, new Antrum() { AntrumID = 0, AntrumName = "N/A" });
            return list;
        }

        public List<Pyloru> GetPylorusList()
        {
            var list = db.Pylorus.ToList();
            //list.Insert(0, new Pyloru() { PylorusID = 0, PylorusName = "N/A" });
            return list;
        }

        public List<DuodenalBulb> GetDuodenalBulbList()
        {
            var list = db.DuodenalBulbs.ToList();
            //list.Insert(0, new DuodenalBulb() { DuodenalBulbID = 0, DuodenalBulbName = "N/A" });
            return list;
        }

        public List<SecondPart> GetSecondPartList()
        {
            var list = db.SecondParts.ToList();
            //list.Insert(0, new SecondPart() { SecondPartID = 0, SecondPartName = "N/A" });
            return list;
        }

        public List<VocalCord> GetVocalCordList()
        {
            var list = db.VocalCords.ToList();
            //list.Insert(0, new VocalCord() { VocalCordID = 0, VocalCordName = "N/A" });
            return list;
        }
        public List<Trachea> GetTracheaList()
        {
            var list = db.Tracheas.ToList();
            //list.Insert(0, new Trachea() { TracheaID = 0, TracheaName = "N/A" });
            return list;
        }
        public List<Carina> GetCarinaList()
        {
            var list = db.Carinas.ToList();
            //list.Insert(0, new Carina() { CarinaID = 0, CarinaName = "N/A" });
            return list;
        }
        public List<RightMain> GetRightMainList()
        {
            var list = db.RightMains.ToList();
            //list.Insert(0, new RightMain() { RightMainID = 0, RightMainName = "N/A" });
            return list;
        }
        public List<RightIntermideate> GetRightIntermideateList()
        {
            var list = db.RightIntermideates.ToList();
            //list.Insert(0, new RightIntermideate() { RightIntermideateID = 0, RightIntermideateName = "N/A" });
            return list;
        }
        public List<RUL> GetRULList()
        {
            var list = db.RULs.ToList();
            //list.Insert(0, new RUL() { RULID = 0, RULName = "N/A" });
            return list;
        }
        public List<RML> GetRMLList()
        {
            var list = db.RMLs.ToList();
            //list.Insert(0, new RML() { RMLID = 0, RMLName = "N/A" });
            return list;
        }
        public List<RLL> GetRLLList()
        {
            var list = db.RLLs.ToList();
            //list.Insert(0, new RLL() { RLLID = 0, RLLName = "N/A" });
            return list;
        }
        public List<LeftMain> GetLeftMainList()
        {
            var list = db.LeftMains.ToList();
            //list.Insert(0, new LeftMain() { LeftMainID = 0, LeftMainName = "N/A" });
            return list;
        }
        public List<LUL> GetLULList()
        {
            var list = db.LULs.ToList();
            //list.Insert(0, new LUL() { LULID = 0, LULName = "N/A" });
            return list;
        }
        public List<Lingular> GetLingularList()
        {
            var list = db.Lingulars.ToList();
            //list.Insert(0, new Lingular() { LingularID = 0, LingularName = "N/A" });
            return list;
        }
        public List<LLL> GetLLLList()
        {
            var list = db.LLLs.ToList();
            //list.Insert(0, new LLL() { LLLID = 0, LLLName = "N/A" });
            return list;
        }

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
    }
}

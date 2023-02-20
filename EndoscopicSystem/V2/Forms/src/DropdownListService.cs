using EndoscopicSystem.Repository;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms.src
{
    public class DropdownListService
    {
        private readonly GetDropdownList _db = new GetDropdownList();

        public void DropdownDoctor(ComboBox comboBox)
        {
            comboBox.ValueMember = "DoctorID";
            comboBox.DisplayMember = "NameTH";
            comboBox.DataSource = _db.GetEndoscopistList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownNurse(ComboBox comboBox)
        {
            comboBox.ValueMember = "NurseID";
            comboBox.DisplayMember = "NameTH";
            comboBox.DataSource = _db.GetNurseList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownMedication(ComboBox comboBox)
        {
            comboBox.ValueMember = "MedicationID";
            comboBox.DisplayMember = "MedicationName";
            comboBox.DataSource = _db.GetMedicationList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownIndication(ComboBox comboBox)
        {
            comboBox.ValueMember = "IndicationID";
            comboBox.DisplayMember = "IndicationName";
            comboBox.DataSource = _db.GetIndicationList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownAnalCanal(ComboBox comboBox)
        {
            comboBox.ValueMember = "AnalCanalID";
            comboBox.DisplayMember = "AnalCanalName";
            comboBox.DataSource = _db.GetAnalCanalList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownRectum(ComboBox comboBox)
        {
            comboBox.ValueMember = "RectumID";
            comboBox.DisplayMember = "RectumName";
            comboBox.DataSource = _db.GetRectumList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSigmoidColon(ComboBox comboBox)
        {
            comboBox.ValueMember = "SigmoidColonID";
            comboBox.DisplayMember = "SigmoidColonName";
            comboBox.DataSource = _db.GetSigmoidColonList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownDescendingColon(ComboBox comboBox)
        {
            comboBox.ValueMember = "DescendingColonID";
            comboBox.DisplayMember = "DescendingColonName";
            comboBox.DataSource = _db.GetDescendingColonList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSplenicFlexure(ComboBox comboBox)
        {
            comboBox.ValueMember = "SplenicFlexureID";
            comboBox.DisplayMember = "SplenicFlexureName";
            comboBox.DataSource = _db.GetSplenicFlexureList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownTransverseColon(ComboBox comboBox)
        {
            comboBox.ValueMember = "TransverseColonID";
            comboBox.DisplayMember = "TransverseColonName";
            comboBox.DataSource = _db.GetTransverseColonList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownHepaticFlexure(ComboBox comboBox)
        {
            comboBox.ValueMember = "HepaticFlexureID";
            comboBox.DisplayMember = "HepaticFlexureName";
            comboBox.DataSource = _db.GetHepaticFlexureList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownAscendingColon(ComboBox comboBox)
        {
            comboBox.ValueMember = "AscendingColonID";
            comboBox.DisplayMember = "AscendingColonName";
            comboBox.DataSource = _db.GetAscendingColonList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownIleocecalValve(ComboBox comboBox)
        {
            comboBox.ValueMember = "IleocecalValveID";
            comboBox.DisplayMember = "IleocecalValveName";
            comboBox.DataSource = _db.GetIleocecalValveList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownCecum(ComboBox comboBox)
        {
            comboBox.ValueMember = "CecumID";
            comboBox.DisplayMember = "CecumName";
            comboBox.DataSource = _db.GetCecumList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownTerminalIleum(ComboBox comboBox)
        {
            comboBox.ValueMember = "TerminalIleumID";
            comboBox.DisplayMember = "TerminalIleumName";
            comboBox.DataSource = _db.GetTerminalIleumList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownEsophagus(ComboBox comboBox)
        {
            comboBox.ValueMember = "EsophagusID";
            comboBox.DisplayMember = "EsophagusName";
            comboBox.DataSource = _db.GetEsophagusList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownStomach(ComboBox comboBox)
        {
            comboBox.ValueMember = "StomachID";
            comboBox.DisplayMember = "StomachName";
            comboBox.DataSource = _db.GetStomachList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownDuodenum(ComboBox comboBox)
        {
            comboBox.ValueMember = "DuodenumID";
            comboBox.DisplayMember = "DuodenumName";
            comboBox.DataSource = _db.GetDuodenumList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownAmpulla(ComboBox comboBox)
        {
            comboBox.ValueMember = "AmpullaOfVaterID";
            comboBox.DisplayMember = "AmpullaOfVaterName";
            comboBox.DataSource = _db.GetAmpullaList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownCholangiogram(ComboBox comboBox)
        {
            comboBox.ValueMember = "CholangiogramID";
            comboBox.DisplayMember = "CholangiogramName";
            comboBox.DataSource = _db.GetCholangiogramList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPancreatogram(ComboBox comboBox)
        {
            comboBox.ValueMember = "PancreatogramID";
            comboBox.DisplayMember = "PancreatogramName";
            comboBox.DataSource = _db.GetPancreatogramList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownOropharynx(ComboBox comboBox)
        {
            comboBox.ValueMember = "OropharynxID";
            comboBox.DisplayMember = "OropharynxName";
            comboBox.DataSource = _db.GetOropharynxList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownEGJunction(ComboBox comboBox)
        {
            comboBox.ValueMember = "EGJunctionID";
            comboBox.DisplayMember = "EGJunctionName";
            comboBox.DataSource = _db.GetEGJunctionList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownCardia(ComboBox comboBox)
        {
            comboBox.ValueMember = "CardiaID";
            comboBox.DisplayMember = "CardiaName";
            comboBox.DataSource = _db.GetCardiaList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownFundus(ComboBox comboBox)
        {
            comboBox.ValueMember = "FundusID";
            comboBox.DisplayMember = "FundusName";
            comboBox.DataSource = _db.GetFundusList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownBody(ComboBox comboBox)
        {
            comboBox.ValueMember = "BodyID";
            comboBox.DisplayMember = "BodyName";
            comboBox.DataSource = _db.GetBodyList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownAntrum(ComboBox comboBox)
        {
            comboBox.ValueMember = "AntrumID";
            comboBox.DisplayMember = "AntrumName";
            comboBox.DataSource = _db.GetAntrumList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPylorus(ComboBox comboBox)
        {
            comboBox.ValueMember = "PylorusID";
            comboBox.DisplayMember = "PylorusName";
            comboBox.DataSource = _db.GetPylorusList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownDuodenalBulb(ComboBox comboBox)
        {
            comboBox.ValueMember = "DuodenalBulbID";
            comboBox.DisplayMember = "DuodenalBulbName";
            comboBox.DataSource = _db.GetDuodenalBulbList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSecondPart(ComboBox comboBox)
        {
            comboBox.ValueMember = "SecondPartID";
            comboBox.DisplayMember = "SecondPartName";
            comboBox.DataSource = _db.GetSecondPartList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownVocalCord(ComboBox comboBox)
        {
            comboBox.ValueMember = "VocalCordID";
            comboBox.DisplayMember = "VocalCordName";
            comboBox.DataSource = _db.GetVocalCordList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownTrachea(ComboBox comboBox)
        {
            comboBox.ValueMember = "TracheaID";
            comboBox.DisplayMember = "TracheaName";
            comboBox.DataSource = _db.GetTracheaList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownCarina(ComboBox comboBox)
        {
            comboBox.ValueMember = "CarinaID";
            comboBox.DisplayMember = "CarinaName";
            comboBox.DataSource = _db.GetCarinaList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownRightMain(ComboBox comboBox)
        {
            comboBox.ValueMember = "RightMainID";
            comboBox.DisplayMember = "RightMainName";
            comboBox.DataSource = _db.GetRightMainList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownRightIntermideate(ComboBox comboBox)
        {
            comboBox.ValueMember = "RightIntermideateID";
            comboBox.DisplayMember = "RightIntermideateName";
            comboBox.DataSource = _db.GetRightIntermideateList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownRUL(ComboBox comboBox)
        {
            comboBox.ValueMember = "RULID";
            comboBox.DisplayMember = "RULName";
            comboBox.DataSource = _db.GetRULList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownRML(ComboBox comboBox)
        {
            comboBox.ValueMember = "RMLID";
            comboBox.DisplayMember = "RMLName";
            comboBox.DataSource = _db.GetRMLList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownRLL(ComboBox comboBox)
        {
            comboBox.ValueMember = "RLLID";
            comboBox.DisplayMember = "RLLName";
            comboBox.DataSource = _db.GetRLLList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownLeftMain(ComboBox comboBox)
        {
            comboBox.ValueMember = "LeftMainID";
            comboBox.DisplayMember = "LeftMainName";
            comboBox.DataSource = _db.GetLeftMainList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownLUL(ComboBox comboBox)
        {
            comboBox.ValueMember = "LULID";
            comboBox.DisplayMember = "LULName";
            comboBox.DataSource = _db.GetLULList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownLingular(ComboBox comboBox)
        {
            comboBox.ValueMember = "LingularID";
            comboBox.DisplayMember = "LingularName";
            comboBox.DataSource = _db.GetLingularList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownLLL(ComboBox comboBox)
        {
            comboBox.ValueMember = "LLLID";
            comboBox.DisplayMember = "LLLName";
            comboBox.DataSource = _db.GetLLLList().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownNasalCavityLeft(ComboBox comboBox)
        {
            comboBox.ValueMember = "NasalCavityID";
            comboBox.DisplayMember = "NasalCavityName";
            comboBox.DataSource = _db.GetNasalCavities().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownNasalCavityRight(ComboBox comboBox)
        {
            comboBox.ValueMember = "NasalCavityID";
            comboBox.DisplayMember = "NasalCavityName";
            comboBox.DataSource = _db.GetNasalCavities().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSeptum(ComboBox comboBox)
        {
            comboBox.ValueMember = "SeptumID";
            comboBox.DisplayMember = "SeptumName";
            comboBox.DataSource = _db.GetSeptums().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownNasopharynx(ComboBox comboBox)
        {
            comboBox.ValueMember = "NasopharynxID";
            comboBox.DisplayMember = "NasopharynxName";
            comboBox.DataSource = _db.GetNasopharynxes().ToList();
            comboBox.SelectedIndex = 0;
        }

        public void DropdownRoof(ComboBox comboBox)
        {
            comboBox.ValueMember = "RoofID";
            comboBox.DisplayMember = "RoofName";
            comboBox.DataSource = _db.GetRoofs().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPosteriorWall(ComboBox comboBox)
        {
            comboBox.ValueMember = "PosteriorWallID";
            comboBox.DisplayMember = "PosteriorWallName";
            comboBox.DataSource = _db.GetPosteriorWalls().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownRosenmullerFossa(ComboBox comboBox)
        {
            comboBox.ValueMember = "RosenmullerFossaID";
            comboBox.DisplayMember = "RosenmullerFossaName";
            comboBox.DataSource = _db.GetRosenmullerFossas().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownEustachianTubeOrifice(ComboBox comboBox)
        {
            comboBox.ValueMember = "EustachianTubeOrificeID";
            comboBox.DisplayMember = "EustachianTubeOrificeName";
            comboBox.DataSource = _db.GetEustachianTubeOrifices().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSoftPalate(ComboBox comboBox)
        {
            comboBox.ValueMember = "SoftPalateID";
            comboBox.DisplayMember = "SoftPalateName";
            comboBox.DataSource = _db.GetSoftPalates().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownUvula(ComboBox comboBox)
        {
            comboBox.ValueMember = "UvulaID";
            comboBox.DisplayMember = "UvulaName";
            comboBox.DataSource = _db.GetUvulas().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownTonsil(ComboBox comboBox)
        {
            comboBox.ValueMember = "TonsilID";
            comboBox.DisplayMember = "TonsilName";
            comboBox.DataSource = _db.GetTonsils().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownBaseOfTongue(ComboBox comboBox)
        {
            comboBox.ValueMember = "BaseOfTongueID";
            comboBox.DisplayMember = "BaseOfTongueName";
            comboBox.DataSource = _db.GetBaseOfTongues().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownVallecula(ComboBox comboBox)
        {
            comboBox.ValueMember = "ValleculaID";
            comboBox.DisplayMember = "ValleculaName";
            comboBox.DataSource = _db.GetValleculas().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPyriformSinusLeft(ComboBox comboBox)
        {
            comboBox.ValueMember = "PyriformSinusID";
            comboBox.DisplayMember = "PyriformSinusName";
            comboBox.DataSource = _db.GetPyriformSinus().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPyriformSinusRight(ComboBox comboBox)
        {
            comboBox.ValueMember = "PyriformSinusID";
            comboBox.DisplayMember = "PyriformSinusName";
            comboBox.DataSource = _db.GetPyriformSinus().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPostcricoid(ComboBox comboBox)
        {
            comboBox.ValueMember = "PostcricoidID";
            comboBox.DisplayMember = "PostcricoidName";
            comboBox.DataSource = _db.GetPostcricoids().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPosteriorPharyngealWall(ComboBox comboBox)
        {
            comboBox.ValueMember = "PosteriorPharyngealWallID";
            comboBox.DisplayMember = "PosteriorPharyngealWallName";
            comboBox.DataSource = _db.GetPosteriorPharyngealWalls().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSupraglottic(ComboBox comboBox)
        {
            comboBox.ValueMember = "SupraglotticID";
            comboBox.DisplayMember = "SupraglotticName";
            comboBox.DataSource = _db.GetSupraglottics().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownGlottic(ComboBox comboBox)
        {
            comboBox.ValueMember = "GlotticID";
            comboBox.DisplayMember = "GlotticName";
            comboBox.DataSource = _db.GetGlottics().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSubglottic(ComboBox comboBox)
        {
            comboBox.ValueMember = "SubglotticID";
            comboBox.DisplayMember = "SubglotticName";
            comboBox.DataSource = _db.GetSubglottics().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownUES(ComboBox comboBox)
        {
            comboBox.ValueMember = "UESID";
            comboBox.DisplayMember = "UESName";
            comboBox.DataSource = _db.GetUEs().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownLES(ComboBox comboBox)
        {
            comboBox.ValueMember = "LESID";
            comboBox.DisplayMember = "LESName";
            comboBox.DataSource = _db.GetLEs().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownOPD(ComboBox comboBox)
        {
            comboBox.ValueMember = "OpdID";
            comboBox.DisplayMember = "OpdName";
            comboBox.DataSource = _db.GetOpdLists().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownWard(ComboBox comboBox)
        {
            comboBox.ValueMember = "WardID";
            comboBox.DisplayMember = "WardName";
            comboBox.DataSource = _db.GetWardLists().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownAnesthesia(ComboBox comboBox)
        {
            comboBox.ValueMember = "AnesthesiaID";
            comboBox.DisplayMember = "AnesthesiaName";
            comboBox.DataSource = _db.GetAnesthesias().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownAnesthesist(ComboBox comboBox)
        {
            comboBox.ValueMember = "AnesthesistID";
            comboBox.DisplayMember = "NameTH";
            comboBox.DataSource = _db.GetAnesthesists().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownFinancial(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = _db.GetFinancials().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownInstrument(ComboBox comboBox, int startIdx = 0)
        {
            comboBox.ValueMember = "No";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = _db.GetInstruments(startIdx).ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownInstrumentIdAndCode(ComboBox comboBox, int startIdx = 0)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Code";
            comboBox.DataSource = _db.GetInstrumentsIdAndCode(startIdx).ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPapillaMajor(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = _db.GetPapillaMajors().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPapillaMinor(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = _db.GetPapillaMinors().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownPancrea(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = _db.GetPancreas().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownBiliarySystem(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = _db.GetBiliarySystems().ToList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownOther(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = _db.GetOthers().ToList();
            comboBox.SelectedIndex = 0;
        }
    }
}

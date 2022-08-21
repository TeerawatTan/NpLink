
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/19/2020 20:40:11
-- Generated from EDMX file: C:\Users\sent_\Documents\endoscopic-system\EndoscopicSystem\Entities\DBEndoscopic.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [godsv_EndoscopicDb_9ef9846ffbb64efc9bf741ae19d693b5];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Appointment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appointment];
GO
IF OBJECT_ID(N'[dbo].[Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctor];
GO
IF OBJECT_ID(N'[dbo].[Endoscopic]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Endoscopic];
GO
IF OBJECT_ID(N'[dbo].[EndoscopicAllImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EndoscopicAllImage];
GO
IF OBJECT_ID(N'[dbo].[EndoscopicImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EndoscopicImage];
GO
IF OBJECT_ID(N'[dbo].[EndoscopicVideo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EndoscopicVideo];
GO
IF OBJECT_ID(N'[dbo].[Finding]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Finding];
GO
IF OBJECT_ID(N'[dbo].[History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[History];
GO
IF OBJECT_ID(N'[dbo].[Hospital]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hospital];
GO
IF OBJECT_ID(N'[dbo].[Indication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Indication];
GO
IF OBJECT_ID(N'[dbo].[Intervention]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Intervention];
GO
IF OBJECT_ID(N'[dbo].[Nurse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Nurse];
GO
IF OBJECT_ID(N'[dbo].[Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patient];
GO
IF OBJECT_ID(N'[dbo].[Room]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Room];
GO
IF OBJECT_ID(N'[dbo].[Specimen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specimen];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[master].[AmpullaOfVater]', 'U') IS NOT NULL
    DROP TABLE [master].[AmpullaOfVater];
GO
IF OBJECT_ID(N'[master].[AnalCanal]', 'U') IS NOT NULL
    DROP TABLE [master].[AnalCanal];
GO
IF OBJECT_ID(N'[master].[Anesthesia]', 'U') IS NOT NULL
    DROP TABLE [master].[Anesthesia];
GO
IF OBJECT_ID(N'[master].[Antrum]', 'U') IS NOT NULL
    DROP TABLE [master].[Antrum];
GO
IF OBJECT_ID(N'[master].[AscendingColon]', 'U') IS NOT NULL
    DROP TABLE [master].[AscendingColon];
GO
IF OBJECT_ID(N'[master].[Body]', 'U') IS NOT NULL
    DROP TABLE [master].[Body];
GO
IF OBJECT_ID(N'[master].[Cardia]', 'U') IS NOT NULL
    DROP TABLE [master].[Cardia];
GO
IF OBJECT_ID(N'[master].[Cecum]', 'U') IS NOT NULL
    DROP TABLE [master].[Cecum];
GO
IF OBJECT_ID(N'[master].[Cholangiogram]', 'U') IS NOT NULL
    DROP TABLE [master].[Cholangiogram];
GO
IF OBJECT_ID(N'[master].[DescendingColon]', 'U') IS NOT NULL
    DROP TABLE [master].[DescendingColon];
GO
IF OBJECT_ID(N'[master].[DuodenalBulb]', 'U') IS NOT NULL
    DROP TABLE [master].[DuodenalBulb];
GO
IF OBJECT_ID(N'[master].[Duodenum]', 'U') IS NOT NULL
    DROP TABLE [master].[Duodenum];
GO
IF OBJECT_ID(N'[master].[EGJunction]', 'U') IS NOT NULL
    DROP TABLE [master].[EGJunction];
GO
IF OBJECT_ID(N'[master].[ERCPEsophagus]', 'U') IS NOT NULL
    DROP TABLE [master].[ERCPEsophagus];
GO
IF OBJECT_ID(N'[master].[Esophagus]', 'U') IS NOT NULL
    DROP TABLE [master].[Esophagus];
GO
IF OBJECT_ID(N'[master].[Fundus]', 'U') IS NOT NULL
    DROP TABLE [master].[Fundus];
GO
IF OBJECT_ID(N'[master].[HepaticFlexure]', 'U') IS NOT NULL
    DROP TABLE [master].[HepaticFlexure];
GO
IF OBJECT_ID(N'[master].[IleocecalValve]', 'U') IS NOT NULL
    DROP TABLE [master].[IleocecalValve];
GO
IF OBJECT_ID(N'[master].[IndicationDropdown]', 'U') IS NOT NULL
    DROP TABLE [master].[IndicationDropdown];
GO
IF OBJECT_ID(N'[master].[Medication]', 'U') IS NOT NULL
    DROP TABLE [master].[Medication];
GO
IF OBJECT_ID(N'[master].[Oropharynx]', 'U') IS NOT NULL
    DROP TABLE [master].[Oropharynx];
GO
IF OBJECT_ID(N'[master].[Pancreatogram]', 'U') IS NOT NULL
    DROP TABLE [master].[Pancreatogram];
GO
IF OBJECT_ID(N'[master].[Pylorus]', 'U') IS NOT NULL
    DROP TABLE [master].[Pylorus];
GO
IF OBJECT_ID(N'[master].[Rectum]', 'U') IS NOT NULL
    DROP TABLE [master].[Rectum];
GO
IF OBJECT_ID(N'[master].[SecondPart]', 'U') IS NOT NULL
    DROP TABLE [master].[SecondPart];
GO
IF OBJECT_ID(N'[master].[SigmoidColon]', 'U') IS NOT NULL
    DROP TABLE [master].[SigmoidColon];
GO
IF OBJECT_ID(N'[master].[SplenicFlexure]', 'U') IS NOT NULL
    DROP TABLE [master].[SplenicFlexure];
GO
IF OBJECT_ID(N'[master].[Stomach]', 'U') IS NOT NULL
    DROP TABLE [master].[Stomach];
GO
IF OBJECT_ID(N'[master].[TerminalIleum]', 'U') IS NOT NULL
    DROP TABLE [master].[TerminalIleum];
GO
IF OBJECT_ID(N'[master].[TransverseColon]', 'U') IS NOT NULL
    DROP TABLE [master].[TransverseColon];
GO
IF OBJECT_ID(N'[EndoscopicModelStoreContainer].[ProcedureList]', 'U') IS NOT NULL
    DROP TABLE [EndoscopicModelStoreContainer].[ProcedureList];
GO
IF OBJECT_ID(N'[EndoscopicModelStoreContainer].[v_AppointmentDetails]', 'U') IS NOT NULL
    DROP TABLE [EndoscopicModelStoreContainer].[v_AppointmentDetails];
GO
IF OBJECT_ID(N'[EndoscopicModelStoreContainer].[v_HistoryEndoscopic]', 'U') IS NOT NULL
    DROP TABLE [EndoscopicModelStoreContainer].[v_HistoryEndoscopic];
GO
IF OBJECT_ID(N'[EndoscopicModelStoreContainer].[v_PatientList]', 'U') IS NOT NULL
    DROP TABLE [EndoscopicModelStoreContainer].[v_PatientList];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [DoctorID] int IDENTITY(1,1) NOT NULL,
    [NameEN] nvarchar(200)  NULL,
    [NameTH] nvarchar(200)  NULL,
    [IsActive] bit  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'EndoscopicImages'
CREATE TABLE [dbo].[EndoscopicImages] (
    [EndoscopicImageID] int IDENTITY(1,1) NOT NULL,
    [EndoscopicID] int  NULL,
    [ProcedureID] int  NULL,
    [ImagePath] nvarchar(500)  NULL,
    [ImageComment] nvarchar(100)  NULL,
    [Seq] int  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'EndoscopicVideos'
CREATE TABLE [dbo].[EndoscopicVideos] (
    [EndoscopicVideoID] int IDENTITY(1,1) NOT NULL,
    [EndoscopicID] int  NULL,
    [VideoPath] nvarchar(500)  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL,
    [ProcedureID] int  NULL
);
GO

-- Creating table 'Hospitals'
CREATE TABLE [dbo].[Hospitals] (
    [HospitalID] int IDENTITY(1,1) NOT NULL,
    [HospitalLogoPath] nvarchar(500)  NULL,
    [HospitalNameTH] nvarchar(500)  NULL,
    [HospitalNameEN] nvarchar(500)  NULL,
    [Address1] nvarchar(500)  NULL,
    [Address2] nvarchar(500)  NULL,
    [Address3] nvarchar(500)  NULL,
    [Tel] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'Nurses'
CREATE TABLE [dbo].[Nurses] (
    [NurseID] int IDENTITY(1,1) NOT NULL,
    [NameEN] nvarchar(200)  NULL,
    [NameTH] nvarchar(200)  NULL,
    [IsActive] bit  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [RoomID] int IDENTITY(1,1) NOT NULL,
    [NameEN] nvarchar(200)  NULL,
    [NameTH] nvarchar(200)  NULL,
    [IsActive] bit  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NULL,
    [TwoFactorEnabled] bit  NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NULL,
    [AccessFailedCount] int  NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [Fullname] nvarchar(200)  NULL,
    [IsAdmin] bit  NULL,
    [AspectRatioID] int  NULL,
    [RatioX] int  NULL,
    [RatioY] int  NULL
);
GO

-- Creating table 'AmpullaOfVaters'
CREATE TABLE [dbo].[AmpullaOfVaters] (
    [AmpullaOfVaterID] int IDENTITY(1,1) NOT NULL,
    [AmpullaOfVaterName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'AnalCanals'
CREATE TABLE [dbo].[AnalCanals] (
    [AnalCanalID] int IDENTITY(1,1) NOT NULL,
    [AnalCanalName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Anesthesias'
CREATE TABLE [dbo].[Anesthesias] (
    [AnesthesiaID] int IDENTITY(1,1) NOT NULL,
    [AnesthesiaName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Antrums'
CREATE TABLE [dbo].[Antrums] (
    [AntrumID] int IDENTITY(1,1) NOT NULL,
    [AntrumName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'AscendingColons'
CREATE TABLE [dbo].[AscendingColons] (
    [AscendingColonID] int IDENTITY(1,1) NOT NULL,
    [AscendingColonName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Bodies'
CREATE TABLE [dbo].[Bodies] (
    [BodyID] int IDENTITY(1,1) NOT NULL,
    [BodyName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Cardias'
CREATE TABLE [dbo].[Cardias] (
    [CardiaID] int IDENTITY(1,1) NOT NULL,
    [CardiaName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Cecums'
CREATE TABLE [dbo].[Cecums] (
    [CecumID] int IDENTITY(1,1) NOT NULL,
    [CecumName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Cholangiograms'
CREATE TABLE [dbo].[Cholangiograms] (
    [CholangiogramID] int IDENTITY(1,1) NOT NULL,
    [CholangiogramName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'DescendingColons'
CREATE TABLE [dbo].[DescendingColons] (
    [DescendingColonID] int IDENTITY(1,1) NOT NULL,
    [DescendingColonName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'DuodenalBulbs'
CREATE TABLE [dbo].[DuodenalBulbs] (
    [DuodenalBulbID] int IDENTITY(1,1) NOT NULL,
    [DuodenalBulbName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Duodenums'
CREATE TABLE [dbo].[Duodenums] (
    [DuodenumID] int IDENTITY(1,1) NOT NULL,
    [DuodenumName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'EGJunctions'
CREATE TABLE [dbo].[EGJunctions] (
    [EGJunctionID] int IDENTITY(1,1) NOT NULL,
    [EGJunctionName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'ERCPEsophagus'
CREATE TABLE [dbo].[ERCPEsophagus] (
    [ERCPEsophagusID] int IDENTITY(1,1) NOT NULL,
    [ERCPEsophagusName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Esophagus'
CREATE TABLE [dbo].[Esophagus] (
    [EsophagusID] int IDENTITY(1,1) NOT NULL,
    [EsophagusName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Fundus'
CREATE TABLE [dbo].[Fundus] (
    [FundusID] int IDENTITY(1,1) NOT NULL,
    [FundusName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'HepaticFlexures'
CREATE TABLE [dbo].[HepaticFlexures] (
    [HepaticFlexureID] int IDENTITY(1,1) NOT NULL,
    [HepaticFlexureName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'IleocecalValves'
CREATE TABLE [dbo].[IleocecalValves] (
    [IleocecalValveID] int IDENTITY(1,1) NOT NULL,
    [IleocecalValveName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'IndicationDropdowns'
CREATE TABLE [dbo].[IndicationDropdowns] (
    [IndicationID] int IDENTITY(1,1) NOT NULL,
    [IndicationName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Medications'
CREATE TABLE [dbo].[Medications] (
    [MedicationID] int IDENTITY(1,1) NOT NULL,
    [MedicationName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Oropharynges'
CREATE TABLE [dbo].[Oropharynges] (
    [OropharynxID] int IDENTITY(1,1) NOT NULL,
    [OropharynxName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Pancreatograms'
CREATE TABLE [dbo].[Pancreatograms] (
    [PancreatogramID] int IDENTITY(1,1) NOT NULL,
    [PancreatogramName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Pylorus'
CREATE TABLE [dbo].[Pylorus] (
    [PylorusID] int IDENTITY(1,1) NOT NULL,
    [PylorusName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Rectums'
CREATE TABLE [dbo].[Rectums] (
    [RectumID] int IDENTITY(1,1) NOT NULL,
    [RectumName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'SecondParts'
CREATE TABLE [dbo].[SecondParts] (
    [SecondPartID] int IDENTITY(1,1) NOT NULL,
    [SecondPartName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'SigmoidColons'
CREATE TABLE [dbo].[SigmoidColons] (
    [SigmoidColonID] int IDENTITY(1,1) NOT NULL,
    [SigmoidColonName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'SplenicFlexures'
CREATE TABLE [dbo].[SplenicFlexures] (
    [SplenicFlexureID] int IDENTITY(1,1) NOT NULL,
    [SplenicFlexureName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Stomaches'
CREATE TABLE [dbo].[Stomaches] (
    [StomachID] int IDENTITY(1,1) NOT NULL,
    [StomachName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'TerminalIleums'
CREATE TABLE [dbo].[TerminalIleums] (
    [TerminalIleumID] int IDENTITY(1,1) NOT NULL,
    [TerminalIleumName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'TransverseColons'
CREATE TABLE [dbo].[TransverseColons] (
    [TransverseColonID] int IDENTITY(1,1) NOT NULL,
    [TransverseColonName] nvarchar(200)  NULL,
    [CreateDate] datetime  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'ProcedureLists'
CREATE TABLE [dbo].[ProcedureLists] (
    [ProcedureID] int IDENTITY(1,1) NOT NULL,
    [ProcedureName] nvarchar(250)  NULL,
    [CreateBy] int  NULL,
    [CreateDate] datetime  NULL,
    [UpdateBy] int  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Appointments'
CREATE TABLE [dbo].[Appointments] (
    [AppointmentID] int IDENTITY(1,1) NOT NULL,
    [PatientID] int  NULL,
    [ProcedureID] int  NULL,
    [HN] nvarchar(50)  NULL,
    [Firstname] nvarchar(100)  NULL,
    [Lastname] nvarchar(100)  NULL,
    [Fullname] nvarchar(200)  NULL,
    [CardID] nvarchar(50)  NULL,
    [Sex] bit  NULL,
    [Age] int  NULL,
    [MobileNumber] nvarchar(50)  NULL,
    [FullAddress] nvarchar(500)  NULL,
    [Symptom] nvarchar(255)  NULL,
    [RoomID] int  NULL,
    [StaffName] nvarchar(255)  NULL,
    [ProcedureID1] int  NULL,
    [NurseFirstID] int  NULL,
    [NurseSecondID] int  NULL,
    [NurseThirthID] int  NULL,
    [DoctorID] int  NULL,
    [OperationDate] datetime  NULL,
    [AppointmentDate] datetime  NULL,
    [IsNewCase] bit  NULL,
    [IsFollowCase] bit  NULL,
    [OPD] bit  NULL,
    [IPD] bit  NULL,
    [EndoscopicCheck] bit  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'Endoscopics'
CREATE TABLE [dbo].[Endoscopics] (
    [EndoscopicID] int IDENTITY(1,1) NOT NULL,
    [IsSaved] bit  NOT NULL,
    [PatientID] int  NULL,
    [ProcedureID] int  NULL,
    [NewCase] bit  NULL,
    [FollowUpCase] bit  NULL,
    [InCase] int  NULL,
    [Diagnosis] nvarchar(1000)  NULL,
    [Complication] nvarchar(1000)  NULL,
    [Comment] nvarchar(1000)  NULL,
    [ReferringPhysicain] nvarchar(50)  NULL,
    [EndoscopistID] int  NULL,
    [Arrive] datetime  NULL,
    [Instrument] nvarchar(200)  NULL,
    [MedicationID] int  NULL,
    [FluDose] nvarchar(100)  NULL,
    [Intervention] nvarchar(100)  NULL,
    [History] nvarchar(100)  NULL,
    [NurseFirstID] int  NULL,
    [NurseSecondID] int  NULL,
    [NurseThirthID] int  NULL,
    [Assistant1] nvarchar(500)  NULL,
    [Assistant2] nvarchar(500)  NULL,
    [Anesthesia] nvarchar(500)  NULL,
    [Indication] int  NULL,
    [AnesNurse] nvarchar(500)  NULL,
    [FindingID] int  NULL,
    [IndicationID] int  NULL,
    [SpecimenID] int  NULL,
    [InterventionID] int  NULL,
    [StartRecordDate] datetime  NULL,
    [EndRecordDate] datetime  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'Findings'
CREATE TABLE [dbo].[Findings] (
    [FindingID] int IDENTITY(1,1) NOT NULL,
    [PatientID] int  NULL,
    [VocalCord] nvarchar(100)  NULL,
    [Trachea] nvarchar(100)  NULL,
    [Carina] nvarchar(100)  NULL,
    [RightMain] nvarchar(100)  NULL,
    [RightIntermideate] nvarchar(100)  NULL,
    [RUL] nvarchar(100)  NULL,
    [RML] nvarchar(100)  NULL,
    [RLL] nvarchar(100)  NULL,
    [LeftMain] nvarchar(100)  NULL,
    [LUL] nvarchar(100)  NULL,
    [Lingular] nvarchar(100)  NULL,
    [LLL] nvarchar(100)  NULL,
    [AnalCanalID] int  NULL,
    [RectumID] int  NULL,
    [SigmoidColonID] int  NULL,
    [DescendingColonID] int  NULL,
    [SplenicFlexureID] int  NULL,
    [TransverseColonID] int  NULL,
    [HepaticFlexureID] int  NULL,
    [AscendingColonID] int  NULL,
    [IleocecalVolveID] int  NULL,
    [CecumID] int  NULL,
    [TerminalIleumID] int  NULL,
    [EsophagusID] int  NULL,
    [StomachID] int  NULL,
    [DuodenumID] int  NULL,
    [AmpullaOfVaterID] int  NULL,
    [CholangiogramID] int  NULL,
    [PancreatogramID] int  NULL,
    [OropharynxID] int  NULL,
    [EGJunctionID] int  NULL,
    [CardiaID] int  NULL,
    [FundusID] int  NULL,
    [BodyID] int  NULL,
    [AntrumID] int  NULL,
    [PylorusID] int  NULL,
    [DuodenalBulbID] int  NULL,
    [SecondPartID] int  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'Indications'
CREATE TABLE [dbo].[Indications] (
    [IndicationID] int IDENTITY(1,1) NOT NULL,
    [EvaluateLesion_Infiltration] bit  NULL,
    [AsscessAirwayPatency] bit  NULL,
    [Hemoptysis] bit  NULL,
    [Therapeutic] bit  NULL,
    [OtherDetail1] nvarchar(500)  NULL,
    [OtherDetail2] nvarchar(500)  NULL,
    [OtherDetail3] nvarchar(500)  NULL,
    [OtherDetail4] nvarchar(500)  NULL,
    [OtherDetail5] nvarchar(500)  NULL,
    [CreateBy] int  NULL,
    [CreateDate] datetime  NULL,
    [UpdateBy] int  NULL,
    [UpdateDate] datetime  NULL
);
GO

-- Creating table 'Interventions'
CREATE TABLE [dbo].[Interventions] (
    [InterventionID] int IDENTITY(1,1) NOT NULL,
    [Spincterotomy] bit  NULL,
    [StoneExtraction] bit  NULL,
    [StentInsertion] bit  NULL,
    [IsPlastic] bit  NULL,
    [PlasticFoot] nvarchar(10)  NULL,
    [PlasticCentimeter] nvarchar(10)  NULL,
    [IsMetal] bit  NULL,
    [MetalFoot] nvarchar(10)  NULL,
    [MetalCentimeter] nvarchar(10)  NULL,
    [Hemonstasis] bit  NULL,
    [Adrenaline] bit  NULL,
    [Coagulation] bit  NULL,
    [Specimens] bit  NULL,
    [BiopsyforPathological] bit  NULL,
    [OtherDetail1] nvarchar(500)  NULL,
    [OtherDetail2] nvarchar(500)  NULL,
    [OtherDetail3] nvarchar(500)  NULL,
    [OtherDetail4] nvarchar(500)  NULL,
    [OtherDetail5] nvarchar(500)  NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [PatientID] int IDENTITY(1,1) NOT NULL,
    [HN] nvarchar(50)  NULL,
    [Firstname] nvarchar(100)  NULL,
    [Lastname] nvarchar(100)  NULL,
    [Fullname] nvarchar(200)  NULL,
    [CardID] nvarchar(50)  NULL,
    [Sex] bit  NULL,
    [Age] int  NULL,
    [MobileNumber] nvarchar(50)  NULL,
    [FullAddress] nvarchar(500)  NULL,
    [Symptom] nvarchar(255)  NULL,
    [RoomID] int  NULL,
    [StaffName] nvarchar(255)  NULL,
    [ProcedureID] int  NULL,
    [NurseFirstID] int  NULL,
    [NurseSecondID] int  NULL,
    [NurseThirthID] int  NULL,
    [DoctorID] int  NULL,
    [OperationDate] datetime  NULL,
    [AppointmentDate] datetime  NULL,
    [PicturePath] nvarchar(500)  NULL,
    [IsNewCase] bit  NULL,
    [IsFollowCase] bit  NULL,
    [OPD] bit  NULL,
    [IPD] bit  NULL,
    [IsActive] bit  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- Creating table 'Specimen'
CREATE TABLE [dbo].[Specimen] (
    [SpecimenID] int IDENTITY(1,1) NOT NULL,
    [BalAt] bit  NULL,
    [BalAtDetail] nvarchar(500)  NULL,
    [BalAtCytho] bit  NULL,
    [BalAtPatho] bit  NULL,
    [BalAtGram] bit  NULL,
    [BalAtAFB] bit  NULL,
    [BalAtModAFB] bit  NULL,
    [BrushingAt] bit  NULL,
    [BrushingAtDetail] nvarchar(500)  NULL,
    [BrushingAtCytho] bit  NULL,
    [BrushingAtPatho] bit  NULL,
    [BrushingAtGram] bit  NULL,
    [BrushingAtAFB] bit  NULL,
    [BrushingAtModAFB] bit  NULL,
    [EndoproncialBiopsyAt] bit  NULL,
    [EndoproncialBiopsyAtDetail] nvarchar(500)  NULL,
    [EndoproncialBiopsyAtCytho] bit  NULL,
    [EndoproncialBiopsyAtPatho] bit  NULL,
    [EndoproncialBiopsyAtGram] bit  NULL,
    [EndoproncialBiopsyAtAFB] bit  NULL,
    [EndoproncialBiopsyAtModAFB] bit  NULL,
    [TransbroncialBiopsyAt] bit  NULL,
    [TransbroncialBiopsyAtDetail] nvarchar(500)  NULL,
    [TransbroncialBiopsyAtCytho] bit  NULL,
    [TransbroncialBiopsyAtPatho] bit  NULL,
    [TransbroncialBiopsyAtGram] bit  NULL,
    [TransbroncialBiopsyAtAFB] bit  NULL,
    [TransbroncialBiopsyAtModAFB] bit  NULL,
    [Transbroncial] bit  NULL,
    [TransbroncialDetail] nvarchar(500)  NULL,
    [TransbroncialCytho] bit  NULL,
    [TransbroncialPatho] bit  NULL,
    [TransbroncialGram] bit  NULL,
    [TransbroncialAFB] bit  NULL,
    [TransbroncialModAFB] bit  NULL,
    [BiopsyforPathological] bit  NULL,
    [BiopsyforCLOTest] bit  NULL,
    [BiopsyforCLOTestResult] bit  NULL,
    [Positive] bit  NULL,
    [Nagative] bit  NULL,
    [OtherDetail1] nvarchar(500)  NULL,
    [OtherDetail2] nvarchar(500)  NULL,
    [OtherDetail3] nvarchar(500)  NULL,
    [OtherDetail4] nvarchar(500)  NULL,
    [OtherDetail5] nvarchar(500)  NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [dbo].[Histories] (
    [HistorieID] int IDENTITY(1,1) NOT NULL,
    [EndoscopicID] int  NULL,
    [PatientID] int  NULL,
    [ProcedureID] int  NULL,
    [Symptom] nvarchar(250)  NULL,
    [DoctorID] int  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'v_AppointmentDetails'
CREATE TABLE [dbo].[v_AppointmentDetails] (
    [AppointmentID] int  NOT NULL,
    [HN] nvarchar(50)  NULL,
    [Fullname] nvarchar(200)  NULL,
    [Symptom] nvarchar(255)  NULL,
    [ProcedureID] int  NULL,
    [ProcedureName] nvarchar(250)  NULL,
    [RoomID] int  NULL,
    [EndoscopicRoom] nvarchar(200)  NULL,
    [Doctor] nvarchar(200)  NULL,
    [AppointmentDate] datetime  NULL,
    [EndoscopicCheck] bit  NULL
);
GO

-- Creating table 'v_HistoryEndoscopic'
CREATE TABLE [dbo].[v_HistoryEndoscopic] (
    [HistorieID] int  NOT NULL,
    [PatientID] int  NULL,
    [EndoscopicID] int  NULL,
    [CreateDate] datetime  NULL,
    [Symptom] nvarchar(250)  NULL,
    [DoctorID] int  NULL,
    [Doctor] nvarchar(200)  NULL,
    [ProcedureID] int  NULL,
    [ProcedureName] nvarchar(250)  NULL
);
GO

-- Creating table 'v_PatientList'
CREATE TABLE [dbo].[v_PatientList] (
    [PatientID] int  NOT NULL,
    [AppointmentDate] datetime  NULL,
    [HN] nvarchar(50)  NULL,
    [Symptom] nvarchar(255)  NULL,
    [Fullname] nvarchar(200)  NULL,
    [DoctorID] int  NULL,
    [DoctorName] nvarchar(200)  NULL,
    [ProcedureID] int  NULL,
    [ProcedureName] nvarchar(250)  NULL,
    [RoomID] int  NULL,
    [RoomName] nvarchar(200)  NULL
);
GO

-- Creating table 'EndoscopicAllImages'
CREATE TABLE [dbo].[EndoscopicAllImages] (
    [EndoscopicAllImageID] int IDENTITY(1,1) NOT NULL,
    [EndoscopicID] int  NULL,
    [ProcedureID] int  NULL,
    [ImagePath] nvarchar(500)  NULL,
    [ImageComment] nvarchar(100)  NULL,
    [Seq] int  NULL,
    [CreateDate] datetime  NULL,
    [CreateBy] int  NULL,
    [UpdateDate] datetime  NULL,
    [UpdateBy] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DoctorID] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([DoctorID] ASC);
GO

-- Creating primary key on [EndoscopicImageID] in table 'EndoscopicImages'
ALTER TABLE [dbo].[EndoscopicImages]
ADD CONSTRAINT [PK_EndoscopicImages]
    PRIMARY KEY CLUSTERED ([EndoscopicImageID] ASC);
GO

-- Creating primary key on [EndoscopicVideoID] in table 'EndoscopicVideos'
ALTER TABLE [dbo].[EndoscopicVideos]
ADD CONSTRAINT [PK_EndoscopicVideos]
    PRIMARY KEY CLUSTERED ([EndoscopicVideoID] ASC);
GO

-- Creating primary key on [HospitalID] in table 'Hospitals'
ALTER TABLE [dbo].[Hospitals]
ADD CONSTRAINT [PK_Hospitals]
    PRIMARY KEY CLUSTERED ([HospitalID] ASC);
GO

-- Creating primary key on [NurseID] in table 'Nurses'
ALTER TABLE [dbo].[Nurses]
ADD CONSTRAINT [PK_Nurses]
    PRIMARY KEY CLUSTERED ([NurseID] ASC);
GO

-- Creating primary key on [RoomID] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([RoomID] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AmpullaOfVaterID] in table 'AmpullaOfVaters'
ALTER TABLE [dbo].[AmpullaOfVaters]
ADD CONSTRAINT [PK_AmpullaOfVaters]
    PRIMARY KEY CLUSTERED ([AmpullaOfVaterID] ASC);
GO

-- Creating primary key on [AnalCanalID] in table 'AnalCanals'
ALTER TABLE [dbo].[AnalCanals]
ADD CONSTRAINT [PK_AnalCanals]
    PRIMARY KEY CLUSTERED ([AnalCanalID] ASC);
GO

-- Creating primary key on [AnesthesiaID] in table 'Anesthesias'
ALTER TABLE [dbo].[Anesthesias]
ADD CONSTRAINT [PK_Anesthesias]
    PRIMARY KEY CLUSTERED ([AnesthesiaID] ASC);
GO

-- Creating primary key on [AntrumID] in table 'Antrums'
ALTER TABLE [dbo].[Antrums]
ADD CONSTRAINT [PK_Antrums]
    PRIMARY KEY CLUSTERED ([AntrumID] ASC);
GO

-- Creating primary key on [AscendingColonID] in table 'AscendingColons'
ALTER TABLE [dbo].[AscendingColons]
ADD CONSTRAINT [PK_AscendingColons]
    PRIMARY KEY CLUSTERED ([AscendingColonID] ASC);
GO

-- Creating primary key on [BodyID] in table 'Bodies'
ALTER TABLE [dbo].[Bodies]
ADD CONSTRAINT [PK_Bodies]
    PRIMARY KEY CLUSTERED ([BodyID] ASC);
GO

-- Creating primary key on [CardiaID] in table 'Cardias'
ALTER TABLE [dbo].[Cardias]
ADD CONSTRAINT [PK_Cardias]
    PRIMARY KEY CLUSTERED ([CardiaID] ASC);
GO

-- Creating primary key on [CecumID] in table 'Cecums'
ALTER TABLE [dbo].[Cecums]
ADD CONSTRAINT [PK_Cecums]
    PRIMARY KEY CLUSTERED ([CecumID] ASC);
GO

-- Creating primary key on [CholangiogramID] in table 'Cholangiograms'
ALTER TABLE [dbo].[Cholangiograms]
ADD CONSTRAINT [PK_Cholangiograms]
    PRIMARY KEY CLUSTERED ([CholangiogramID] ASC);
GO

-- Creating primary key on [DescendingColonID] in table 'DescendingColons'
ALTER TABLE [dbo].[DescendingColons]
ADD CONSTRAINT [PK_DescendingColons]
    PRIMARY KEY CLUSTERED ([DescendingColonID] ASC);
GO

-- Creating primary key on [DuodenalBulbID] in table 'DuodenalBulbs'
ALTER TABLE [dbo].[DuodenalBulbs]
ADD CONSTRAINT [PK_DuodenalBulbs]
    PRIMARY KEY CLUSTERED ([DuodenalBulbID] ASC);
GO

-- Creating primary key on [DuodenumID] in table 'Duodenums'
ALTER TABLE [dbo].[Duodenums]
ADD CONSTRAINT [PK_Duodenums]
    PRIMARY KEY CLUSTERED ([DuodenumID] ASC);
GO

-- Creating primary key on [EGJunctionID] in table 'EGJunctions'
ALTER TABLE [dbo].[EGJunctions]
ADD CONSTRAINT [PK_EGJunctions]
    PRIMARY KEY CLUSTERED ([EGJunctionID] ASC);
GO

-- Creating primary key on [ERCPEsophagusID] in table 'ERCPEsophagus'
ALTER TABLE [dbo].[ERCPEsophagus]
ADD CONSTRAINT [PK_ERCPEsophagus]
    PRIMARY KEY CLUSTERED ([ERCPEsophagusID] ASC);
GO

-- Creating primary key on [EsophagusID] in table 'Esophagus'
ALTER TABLE [dbo].[Esophagus]
ADD CONSTRAINT [PK_Esophagus]
    PRIMARY KEY CLUSTERED ([EsophagusID] ASC);
GO

-- Creating primary key on [FundusID] in table 'Fundus'
ALTER TABLE [dbo].[Fundus]
ADD CONSTRAINT [PK_Fundus]
    PRIMARY KEY CLUSTERED ([FundusID] ASC);
GO

-- Creating primary key on [HepaticFlexureID] in table 'HepaticFlexures'
ALTER TABLE [dbo].[HepaticFlexures]
ADD CONSTRAINT [PK_HepaticFlexures]
    PRIMARY KEY CLUSTERED ([HepaticFlexureID] ASC);
GO

-- Creating primary key on [IleocecalValveID] in table 'IleocecalValves'
ALTER TABLE [dbo].[IleocecalValves]
ADD CONSTRAINT [PK_IleocecalValves]
    PRIMARY KEY CLUSTERED ([IleocecalValveID] ASC);
GO

-- Creating primary key on [IndicationID] in table 'IndicationDropdowns'
ALTER TABLE [dbo].[IndicationDropdowns]
ADD CONSTRAINT [PK_IndicationDropdowns]
    PRIMARY KEY CLUSTERED ([IndicationID] ASC);
GO

-- Creating primary key on [MedicationID] in table 'Medications'
ALTER TABLE [dbo].[Medications]
ADD CONSTRAINT [PK_Medications]
    PRIMARY KEY CLUSTERED ([MedicationID] ASC);
GO

-- Creating primary key on [OropharynxID] in table 'Oropharynges'
ALTER TABLE [dbo].[Oropharynges]
ADD CONSTRAINT [PK_Oropharynges]
    PRIMARY KEY CLUSTERED ([OropharynxID] ASC);
GO

-- Creating primary key on [PancreatogramID] in table 'Pancreatograms'
ALTER TABLE [dbo].[Pancreatograms]
ADD CONSTRAINT [PK_Pancreatograms]
    PRIMARY KEY CLUSTERED ([PancreatogramID] ASC);
GO

-- Creating primary key on [PylorusID] in table 'Pylorus'
ALTER TABLE [dbo].[Pylorus]
ADD CONSTRAINT [PK_Pylorus]
    PRIMARY KEY CLUSTERED ([PylorusID] ASC);
GO

-- Creating primary key on [RectumID] in table 'Rectums'
ALTER TABLE [dbo].[Rectums]
ADD CONSTRAINT [PK_Rectums]
    PRIMARY KEY CLUSTERED ([RectumID] ASC);
GO

-- Creating primary key on [SecondPartID] in table 'SecondParts'
ALTER TABLE [dbo].[SecondParts]
ADD CONSTRAINT [PK_SecondParts]
    PRIMARY KEY CLUSTERED ([SecondPartID] ASC);
GO

-- Creating primary key on [SigmoidColonID] in table 'SigmoidColons'
ALTER TABLE [dbo].[SigmoidColons]
ADD CONSTRAINT [PK_SigmoidColons]
    PRIMARY KEY CLUSTERED ([SigmoidColonID] ASC);
GO

-- Creating primary key on [SplenicFlexureID] in table 'SplenicFlexures'
ALTER TABLE [dbo].[SplenicFlexures]
ADD CONSTRAINT [PK_SplenicFlexures]
    PRIMARY KEY CLUSTERED ([SplenicFlexureID] ASC);
GO

-- Creating primary key on [StomachID] in table 'Stomaches'
ALTER TABLE [dbo].[Stomaches]
ADD CONSTRAINT [PK_Stomaches]
    PRIMARY KEY CLUSTERED ([StomachID] ASC);
GO

-- Creating primary key on [TerminalIleumID] in table 'TerminalIleums'
ALTER TABLE [dbo].[TerminalIleums]
ADD CONSTRAINT [PK_TerminalIleums]
    PRIMARY KEY CLUSTERED ([TerminalIleumID] ASC);
GO

-- Creating primary key on [TransverseColonID] in table 'TransverseColons'
ALTER TABLE [dbo].[TransverseColons]
ADD CONSTRAINT [PK_TransverseColons]
    PRIMARY KEY CLUSTERED ([TransverseColonID] ASC);
GO

-- Creating primary key on [ProcedureID] in table 'ProcedureLists'
ALTER TABLE [dbo].[ProcedureLists]
ADD CONSTRAINT [PK_ProcedureLists]
    PRIMARY KEY CLUSTERED ([ProcedureID] ASC);
GO

-- Creating primary key on [AppointmentID] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [PK_Appointments]
    PRIMARY KEY CLUSTERED ([AppointmentID] ASC);
GO

-- Creating primary key on [EndoscopicID] in table 'Endoscopics'
ALTER TABLE [dbo].[Endoscopics]
ADD CONSTRAINT [PK_Endoscopics]
    PRIMARY KEY CLUSTERED ([EndoscopicID] ASC);
GO

-- Creating primary key on [FindingID] in table 'Findings'
ALTER TABLE [dbo].[Findings]
ADD CONSTRAINT [PK_Findings]
    PRIMARY KEY CLUSTERED ([FindingID] ASC);
GO

-- Creating primary key on [IndicationID] in table 'Indications'
ALTER TABLE [dbo].[Indications]
ADD CONSTRAINT [PK_Indications]
    PRIMARY KEY CLUSTERED ([IndicationID] ASC);
GO

-- Creating primary key on [InterventionID] in table 'Interventions'
ALTER TABLE [dbo].[Interventions]
ADD CONSTRAINT [PK_Interventions]
    PRIMARY KEY CLUSTERED ([InterventionID] ASC);
GO

-- Creating primary key on [PatientID] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([PatientID] ASC);
GO

-- Creating primary key on [SpecimenID] in table 'Specimen'
ALTER TABLE [dbo].[Specimen]
ADD CONSTRAINT [PK_Specimen]
    PRIMARY KEY CLUSTERED ([SpecimenID] ASC);
GO

-- Creating primary key on [HistorieID] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [PK_Histories]
    PRIMARY KEY CLUSTERED ([HistorieID] ASC);
GO

-- Creating primary key on [AppointmentID] in table 'v_AppointmentDetails'
ALTER TABLE [dbo].[v_AppointmentDetails]
ADD CONSTRAINT [PK_v_AppointmentDetails]
    PRIMARY KEY CLUSTERED ([AppointmentID] ASC);
GO

-- Creating primary key on [HistorieID] in table 'v_HistoryEndoscopic'
ALTER TABLE [dbo].[v_HistoryEndoscopic]
ADD CONSTRAINT [PK_v_HistoryEndoscopic]
    PRIMARY KEY CLUSTERED ([HistorieID] ASC);
GO

-- Creating primary key on [PatientID] in table 'v_PatientList'
ALTER TABLE [dbo].[v_PatientList]
ADD CONSTRAINT [PK_v_PatientList]
    PRIMARY KEY CLUSTERED ([PatientID] ASC);
GO

-- Creating primary key on [EndoscopicAllImageID] in table 'EndoscopicAllImages'
ALTER TABLE [dbo].[EndoscopicAllImages]
ADD CONSTRAINT [PK_EndoscopicAllImages]
    PRIMARY KEY CLUSTERED ([EndoscopicAllImageID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
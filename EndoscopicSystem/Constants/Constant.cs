using System;
using System.Collections.Generic;

namespace EndoscopicSystem.Constants
{
    public class Constant
    {
        public static readonly string LOGIN_SUCCESS = "เข้าสู่ระบบสำเร็จ";
        public static readonly string LOGIN_FAIL_PASSWORD = "รหัสผ่านไม่ถูกต้อง";
        public static readonly string LOGIN_NOT_SUCCESS = "เข้าสู่ระบบไม่สำเร็จ";

        public static readonly string STATUS_SUCCESS = "success";
        public static readonly string STATUS_ERROR = "error";
        public static readonly string STATUS_DATA_NOT_FOUND = "Error not found";

        public static readonly string Male = "Male";
        public static readonly string FeMale = "FeMale";

        public static List<MonthModel> GetMonths()
        {
            List<MonthModel> months = new List<MonthModel>();
            months.Add(new MonthModel() { MonthID = 1, MonthName = "มกราคม" });
            months.Add(new MonthModel() { MonthID = 2, MonthName = "กุมภาพันธ์" });
            months.Add(new MonthModel() { MonthID = 3, MonthName = "มีนาคม" });
            months.Add(new MonthModel() { MonthID = 4, MonthName = "เมษายน" });
            months.Add(new MonthModel() { MonthID = 5, MonthName = "พฤษภาคม" });
            months.Add(new MonthModel() { MonthID = 6, MonthName = "มิถุนายน" });
            months.Add(new MonthModel() { MonthID = 7, MonthName = "กรกฎาคม" });
            months.Add(new MonthModel() { MonthID = 8, MonthName = "สิงหาคม" });
            months.Add(new MonthModel() { MonthID = 9, MonthName = "กันยายน" });
            months.Add(new MonthModel() { MonthID = 10, MonthName = "ตุลาคม" });
            months.Add(new MonthModel() { MonthID = 11, MonthName = "พฤศจิกายน" });
            months.Add(new MonthModel() { MonthID = 12, MonthName = "ธันวาคม" });
            return months;
        }

        public const int Icd9 = 1;
        public const int Icd10 = 2;
        public const int ProcedureDetail = 3;
        public const int Complication = 4;
        public const int Histopathology = 5;
        public const int RapidUreaseTest = 6;
        public const int Recommendation = 7;

        public const string PREDX1 = "Pre-Dx1";
        public const string PREDX2 = "Pre-Dx2";
        public const string POSTDX1 = "Post-Dx1";
        public const string POSTDX2 = "Post-Dx2";
        public const string POSTDX3 = "Post-Dx3";
        public const string POSTDX4 = "Post-Dx4";
        public const string PRINCIPAL_PROCEDURE = "PrinncipalProcedure";
        public const string SUPPLEMENTAL_PROCEDURE_1 = "SupplementalProcedure-1";
        public const string SUPPLEMENTAL_PROCEDURE_2 = "SupplementalProcedure-2";
        public const string PROCEDURE_DETAIL = "ProcedureDetail";
        public const string COMPLICATION = "Complication";
        public const string HISTOPATHOLOGY = "Histopathology";
        public const string RAPIDUREASETEST = "RapidUreaseTest";
        public const string RECOMMENDATION = "Recommendation";

        public class PageName
        {
            public const string SEARCH_PATIENT_PAGE = "Search Patient Page";
            public const string SEND_PACS_PAGE = "Send Pacs Page";
        }
    }

    public class MonthModel
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }
    }
}

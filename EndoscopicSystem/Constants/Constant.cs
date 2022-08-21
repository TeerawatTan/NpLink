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
    }

    public class MonthModel
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }
    }
}

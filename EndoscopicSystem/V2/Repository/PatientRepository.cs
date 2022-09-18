using EndoscopicSystem.V2.Database;
using System.Collections.Generic;
using System.Linq;

namespace EndoscopicSystem.V2.Repository
{
    public class PatientRepository
    {
        readonly Database1Entities _db = new Database1Entities();

        public List<ChartModel> GetsChart(int m, int y)
        {
            var data = _db.Chart(m, y).ToList();
            List<ChartModel> list = new List<ChartModel>();
            foreach (var item in data)
            {
                ChartModel model = new ChartModel();
                model.ProcedureID = item.ProcedureID;
                model.ProcedureName = item.ProcedureName;
                model.CountPatient = item.CountPatient ?? 0;
                model.Month = item.Month ?? 0;
                model.Year = item.Year ?? 0;
                list.Add(model);
            }
            return list;
        }
    }
    public class ChartModel
    {
        public int ProcedureID { get; set; }
        public int CountPatient { get; set; }
        public string ProcedureName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}

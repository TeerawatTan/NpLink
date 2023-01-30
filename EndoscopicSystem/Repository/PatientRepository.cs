using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EndoscopicSystem.Repository
{
    public class PatientRepository
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();

        public List<ChartModel> GetsChart(int m, int y)
        {
            var data = db.Chart(m, y).ToList();
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

    public class ChartInstrumentModel
    {
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public int CountInstrument { get; set; }
    }
}

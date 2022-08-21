using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndoscopicSystem.Repository
{
    public interface IProcedureListRepository
    {
        List<ProcedureList> GetProcedureDropdownList();
    }
    public class ProcedureListRepository : IProcedureListRepository
    {
        private readonly EndoscopicEntities context;
        public ProcedureListRepository()
        {
            context = new EndoscopicEntities();
        }

        public List<ProcedureList> GetProcedureDropdownList()
        {
            var list = from o in context.ProcedureLists select o;
            return list.ToList();
        }
    }
}

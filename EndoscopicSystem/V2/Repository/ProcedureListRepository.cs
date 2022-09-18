using EndoscopicSystem.V2.Database;
using System.Collections.Generic;
using System.Linq;

namespace EndoscopicSystem.V2.Repository
{
    public interface IProcedureListRepository
    {
        List<ProcedureList> GetProcedureDropdownList();
    }
    public class ProcedureListRepository : IProcedureListRepository
    {
        private readonly Database1Entities _context;
        public ProcedureListRepository()
        {
            _context = new Database1Entities();
        }

        public List<ProcedureList> GetProcedureDropdownList()
        {
            var list = from o in _context.ProcedureLists select o;
            return list.ToList();
        }
    }
}

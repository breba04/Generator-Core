using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Core
{
    public class clsProcedureSchema
    {
        public enum enProcedureType
        {
            Select,
            Insert,
            Update,
            Delete,
            Other
        }
        public string ProcedureName { get; set; }
        public List<clsSPParameter> Parameters { get; set; }
        public enProcedureType SPType { get; set; } 
        public clsProcedureSchema(string ProcedureName, List<clsSPParameter> Parameters, enProcedureType SPType)
        {
            this.ProcedureName = ProcedureName;
            this.Parameters = Parameters;
            this.SPType = SPType;
        }
    }
}

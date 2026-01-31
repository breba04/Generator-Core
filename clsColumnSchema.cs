using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Core
{
    public class clsColumnSchema
    {
        public int ColumnID { get; set; }
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool NotNull { get; set; }
    }
}

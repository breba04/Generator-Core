using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Core
{
    public class clsTableSchema
    {
        string TableName { get; set; }
        List<clsColumnSchema> Columns { get; set; }
        public clsTableSchema(string TableName, List<clsColumnSchema> Columns)
        {
            this.TableName = TableName;
            this.Columns = Columns;
        }
    }
}

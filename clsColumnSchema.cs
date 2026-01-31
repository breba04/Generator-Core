using System;
using System.Collections.Generic;
using System.Data.Common;
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
        //public int? TableRefrence { get; set; } = null;
        //public clsColumnSchema ColumnRefrence { get; set; } = null;
        public clsColumnSchema(int ColumnID, string ColumnName, string ColumnType,bool IsPrimaryKey,
            bool NotNull
            //,int? TableRefrence = null, clsColumnSchema ColumnRefrence = null
            )
        {
            this.ColumnID = ColumnID;
            this.ColumnName = ColumnName;
            this.ColumnType = ColumnType;
            this.IsPrimaryKey = IsPrimaryKey;
            this.NotNull = NotNull;
            //this.TableRefrence = TableRefrence;
            //this.ColumnRefrence = ColumnRefrence;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Core
{
    public class clsTableSchema
    {
        public string TableName { get; set; }
        public List<clsColumnSchema> Columns { get; set; }
        public clsTableSchema()
        {
            TableName = "";
            Columns = new List<clsColumnSchema>();
        }
        public bool AddColumn(clsColumnSchema column)
        {
            if (string.IsNullOrWhiteSpace(TableName))
                throw new InvalidOperationException("Set table name before adding columns.");

            if (Columns.Any(c => c.ColumnName == column.ColumnName))
                throw new InvalidOperationException(
                    $"Table '{TableName}' already contains a column named '{column.ColumnName}'."
                );

            if (column.IsPrimaryKey && Columns.Any(c => c.IsPrimaryKey))
                throw new InvalidOperationException(
                    $"Table '{TableName}' already has a primary key column."
                );

            Columns.Add(column);
            return true;
        }
    }
}

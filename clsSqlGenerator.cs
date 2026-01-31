using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Core
{
    static public class clsSqlGenerator
    {
        static public string GenerateCreateDatabaseScript(string databaseName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = ' {databaseName} ')");
            sb.AppendLine($"CREATE DATABASE  {databaseName};");
            return sb.ToString();
        }
        static public string GenerateCreateTableScript(string databaseName , clsTableSchema Table)
        {
            bool isAddedPrimaryKey = false;
            string isNullable = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"USE  {databaseName}");
            sb.AppendLine($"CREATE TABLE [{Table.TableName}] ( ");
            foreach (clsColumnSchema column in Table.Columns)
            {
                isNullable = column.NotNull ? "NOT NULL" : "NULL";
                sb.Append($"  [ {column.ColumnName}] {column.ColumnType} {isNullable} ");
                if (!isAddedPrimaryKey && column.IsPrimaryKey)
                {
                    sb.Append(" PRIMARY KEY");
                    isAddedPrimaryKey = true;
                }
                sb.Append(',');
            }
            sb.Replace(",", ") ",sb.Length - 1,1);
            return sb.ToString();
        }
    }
}

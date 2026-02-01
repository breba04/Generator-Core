using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            sb.AppendLine($"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{databaseName}')");
            sb.AppendLine($"CREATE DATABASE  {databaseName};");
            return sb.ToString();
        }
        static public string GenerateUseDatabaseScript(string databaseName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"USE  {databaseName};");
            return sb.ToString();
        }
        static public string GenerateDropDatabaseScript(string databaseName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IF EXISTS (SELECT name FROM sys.databases WHERE name = '{databaseName}')");
            sb.AppendLine($"DROP DATABASE  {databaseName};");
            return sb.ToString();
        }
        static public string GenerateCreateTableScript(string databaseName, clsTableSchema Table)
        {

            bool isAddedPrimaryKey = false;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GenerateUseDatabaseScript(databaseName));
            sb.AppendLine($"CREATE TABLE [{Table.TableName}] ( ");
            foreach (clsColumnSchema column in Table.Columns)
            {
                sb.Append($"  [{column.ColumnName}] {column.ColumnType} {(column.NotNull ? "NOT NULL" : "NULL")} ");
                if (!isAddedPrimaryKey && column.IsPrimaryKey)
                {
                    sb.Append(" PRIMARY KEY");
                    isAddedPrimaryKey = true;
                }
                sb.Append(" ,");

            }
            sb.Replace(",", ") ", sb.Length - 1, 1);
            //sb.AppendLine(string.Join(",\n", Colums));
            sb.AppendLine(")");
            return sb.ToString();
        }
        static private string _GenerateCreateInsertStoredProcedureScript(clsTableSchema Table)
        {
            string primaryColumn = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"INSERT INTO [{Table.TableName}] (");
            foreach (clsColumnSchema column in Table.Columns)
            {
                if (!column.IsPrimaryKey)
                {
                    sb.Append($"  [ {column.ColumnName}] ,");
                }
            }
            sb.Replace(",", ") ", sb.Length - 1, 1);
            sb.AppendLine(" VALUES (");
            foreach (clsColumnSchema column in Table.Columns)
            {
                if (!column.IsPrimaryKey)
                {
                    sb.Append($"  @{column.ColumnName} ,");
                }
                else
                {
                    primaryColumn = column.ColumnName;
                }
            }
            sb.Replace(",", ") ", sb.Length - 1, 1);
            if (primaryColumn != "")
            {
                sb.AppendLine(@";");
                sb.AppendLine(@"SELECT SCOPE_IDENTITY() AS NewID; ");
            }
            return sb.ToString();
        }
        static private string _GenerateCreateUpdateStoredProcedureScript(clsTableSchema Table)
        {
            StringBuilder sb = new StringBuilder();
            string primaryColumn = "";
            sb.AppendLine($@"Update [{Table.TableName}] SET ");
            foreach (clsColumnSchema column in Table.Columns)
            {
                if (!column.IsPrimaryKey)
                {
                    sb.Append($"  [{column.ColumnName}] = @{column.ColumnName} ,");
                }
                if(column.IsPrimaryKey && string.IsNullOrEmpty(primaryColumn))
                {
                    primaryColumn = column.ColumnName;
                }
            }
            sb.Remove(sb.Length - 1, 1);
            if (primaryColumn != "")
            {
                sb.AppendLine($@" WHERE [{primaryColumn}] = @{primaryColumn} ");
            }
            return sb.ToString();
        }
        static private string _GenerateCreateDeleteStoredProcedureScript(clsTableSchema Table)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"DELETE FROM [{Table.TableName}]  ");
            foreach (clsColumnSchema column in Table.Columns)
            {
                if (column.IsPrimaryKey)
                {
                    sb.Append($" WHERE [{column.ColumnName}] = @{column.ColumnName} ");
                    break;
                }
            }
            
            sb.AppendLine(@"; ");
            sb.AppendLine(@"SELECT  @@ROWCOUNT() AS RowAffected; ");
            return sb.ToString();
        }
        static private string _GenerateCreateSelectStoredProcedureScript(clsTableSchema Table ,clsColumnSchema OdrerBy = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"SELECT * FROM [{Table.TableName}]  ");
            if (OdrerBy != null)
            {
                sb.AppendLine($@" ORDER BY [{OdrerBy.ColumnName}]  ");
            }
            return sb.ToString();
        }
        static public string GenerateCreateStoredProcedureScript(clsProcedureSchema Procedure,clsTableSchema Table)
        {
        
            StringBuilder sb = new StringBuilder();
            sb.Append($"CREATE PROCEDURE SP_{Procedure.ProcedureName} ");
            foreach (clsSPParameter parameter in Procedure.Parameters)
            {
                sb.Append($"  @{parameter.Name} {parameter.Type}");
                if (parameter.isOutput)
                {
                    sb.Append(" OUTPUT");
                }
                sb.Append(',');
            }
            sb.Replace(",", ") ", sb.Length - 1, 1);
            sb.AppendLine(" AS");
            sb.AppendLine("BEGIN");
            switch(Procedure.SPType)
            {
                case clsProcedureSchema.enProcedureType.Insert:
                    if (Table != null)
                    {
                        sb.AppendLine(_GenerateCreateInsertStoredProcedureScript(Table));
                    }
                    break;
                case clsProcedureSchema.enProcedureType.Update:
                    if (Table != null)
                    {
                        sb.AppendLine(_GenerateCreateUpdateStoredProcedureScript(Table));
                    }
                    break;
                case clsProcedureSchema.enProcedureType.Delete:
                    if (Table != null)
                    {
                        sb.AppendLine(_GenerateCreateDeleteStoredProcedureScript(Table));
                    }
                    break;
                case clsProcedureSchema.enProcedureType.Select:
                    {
                        sb.AppendLine(_GenerateCreateSelectStoredProcedureScript(Table));
                    }
                    break;
                default:
                    sb.AppendLine("-- somthing not currect in GenerateCreateStoredProcedureScript");
                    break;
            }
            sb.AppendLine("END");
            return sb.ToString();
        }
    }
}

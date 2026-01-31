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
            sb.AppendLine($"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Breba')");
            sb.AppendLine($"CREATE DATABASE Breba;");
            return sb.ToString();
        }
        static public string GenerateCreateTableScript(string databaseName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Breba')");
            sb.AppendLine($"CREATE DATABASE Breba;");
            return sb.ToString();
        }
    }
}

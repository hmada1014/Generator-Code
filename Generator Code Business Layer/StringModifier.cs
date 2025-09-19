using System;
using System.Collections.Generic;
using System.Text;

namespace Generator_Code_Business_Layer
{
    public class clsStringModifier
    {
        public static readonly Dictionary<string, string> TypeMappings_CSharp = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"int", "int"}, {"bigint", "long"}, {"smallint", "short"},
        {"tinyint", "byte"}, {"bit", "bool"}, {"decimal", "decimal"},
        {"numeric", "decimal"}, {"money", "decimal"}, {"smallmoney", "decimal"},
        {"float", "double"}, {"real", "float"}, {"datetime", "DateTime"},
        {"smalldatetime", "DateTime"}, {"date", "DateTime"}, {"time", "TimeSpan"},
        {"char", "string"}, {"varchar", "string"}, {"text", "string"},
        {"nchar", "string"}, {"nvarchar", "string"}, {"ntext", "string"},
        {"binary", "byte[]"}, {"varbinary", "byte[]"}, {"image", "byte[]"},
        {"uniqueidentifier", "Guid"}, {"timestamp", "byte[]"}, {"xml", "string"}
    };

        public static readonly Dictionary<string, string> TypeMappings_SQL = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"int", "int"}, {"long", "bigint"}, {"short", "smallint"},
        {"byte", "tinyint"}, {"bool", "bit"}, {"decimal", "decimal"},
        {"double", "float"}, {"float", "real"}, {"DateTime", "datetime"},
        {"TimeSpan", "time"}, {"string", "nvarchar(max)"},
        {"byte[]", "varbinary(max)"}, {"Guid", "uniqueidentifier"}
    };

        public static string GetDefaultValue(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "int":
                case "long":
                case "short":
                case "decimal":
                case "double":
                case "float":
                    return "-1";
                case "bool":
                    return "false";
                case "datetime":
                    return "DateTime.MinValue";
                case "string":
                    return "string.Empty";
                case "guid":
                    return "Guid.Empty";
                case "byte":
                    return "1";
                default:
                    return "null";
            }
        }
        public static char ReturnISNull(string IsNull, string DaTypeta)
        {
            return DaTypeta.ToLower() != "string" ? IsNull.ToUpper() == "YES" ? '?' : ' ' : ' ';
        }
        //helper
        public static StringBuilder GetParametersName(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (Parameter.Value.IsPrimaryKey.Contains("PK") && !IncludePrimaryKey == true)
                    continue;

                sb.Append($"{Parameter.Key},");
            }
            if (sb.Length >= 1) sb.Length--;
            return sb;
        }

        public static StringBuilder GetParametersNameWithString(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey , string Word)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (Parameter.Value.IsPrimaryKey.Contains("PK") && !IncludePrimaryKey == true)
                    continue;

                sb.Append($"{Word}{Parameter.Key},");
            }
            if (sb.Length >= 1) sb.Length--;
            return sb;
        }
        public static StringBuilder GetParametersNameWithThis(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (Parameter.Value.IsPrimaryKey.Contains("PK") && IncludePrimaryKey == true)
                    continue;

                sb.Append($"this.{Parameter.Key},");
            }
            if(sb.Length>=1)sb.Length--;
            return sb;
        }
    }
}

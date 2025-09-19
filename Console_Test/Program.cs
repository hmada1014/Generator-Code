using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Test
{
    internal class Program
    {
        private static readonly Dictionary<string, string> TypeMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
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


        private static char ReturnISNull(string IsNull, string DaTypeta)
        {
            return DaTypeta.ToLower() != "string" ? IsNull.ToUpper() == "YES" ? '?' : ' ' : ' ';
        }
        public static StringBuilder ReturnParameters(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool WithDataType = true)
        {
            StringBuilder sb = new StringBuilder();
            var Key = Parameters.Where(kvp => !kvp.Value.IsPrimaryKey.Contains("PK"));
            foreach (var item in Key)
            {
                TypeMappings.TryGetValue(item.Value.DataType, out string DataType);

                if (WithDataType == true)
                {
                    sb.Append($"{DataType}{ReturnISNull(item.Value.IsNull, DataType)} {item.Key}, ");
                }
                else
                {
                    sb.Append($"{item.Key}, ");
                }
            }

            return sb.Remove(sb.Length - 2, 2);
        }


        private static string GetDefaultValue(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "int":
                case "long":
                case "short":
                case "byte":
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
                default:
                    return "null";
            }
        }
        public static StringBuilder InitializeTheVariable(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool WithPK)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> item in Parameters)
            {
                if (!WithPK && item.Value.IsPrimaryKey.Contains("PK"))
                    continue;

                TypeMappings.TryGetValue(item.Value.DataType, out string DataType);
                sb.AppendLine($"{DataType} {item.Key} = {GetDefaultValue(DataType)}");

            }


            return sb;
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        struct stOptions
        {
            public bool isAllSelected;

            bool name;
        }


        static void Main(string[] args)
        {
            Dictionary<string, (string DataType, string IsNull, string PrimaryKey)> p = new Dictionary<string, (string DataType, string IsNull, string PrimaryKey)>
            {
                //{"ID",("int","Yes")},
                //{"Name",("nvarchar","No")},
                {"PersonID", ("int", "int", "PK_People")},
                {"NationalNo", ("nvarchar", "NO", " ")},
                { "FirstName", ("nvarchar", "NO"," " )},
                { "SecondName", ("nvarchar", "NO", " ")},
                { "ThirdName", ("nvarchar", "YES"," " )},
                { "NationalityCountryID", ("int", "NO", "FK_People_Countries1")}
             };

            var c = InitializeTheVariable(p, true);

            Console.WriteLine(c.ToString());

            Console.ReadLine();


            stOptions options = new stOptions();



        }
    }
}

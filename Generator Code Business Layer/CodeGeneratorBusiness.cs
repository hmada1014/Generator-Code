using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Code_Business_Layer
{
    public class clsCodeGeneratorBusiness
    {
        private DataView _Table;
        private Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> _Parameters = new Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)>();
        private string _TableName;
        public clsCodeGeneratorBusiness(DataView table, string tableName)
        {
            _Table = table;            
            _TableName = tableName;
            _FillParameters(_Table);
        }
        private void _FillParameters(DataView Table)
        {
            if (Table == null) return;
            foreach (DataRowView parameter in Table)
            {
                string Key = (string)parameter.Row[0];
                string DataType = (string)parameter.Row[1];
                string IsNull = (string)parameter.Row[3];
                string IsPrimaryKey = string.IsNullOrEmpty(parameter.Row[4].ToString()) ? "" : (string)parameter.Row[4];
                _Parameters[Key] = (DataType, IsNull, IsPrimaryKey);
            }
        }
        public StringBuilder CodeGeneratorBusiness()
        {
            if (_Parameters == null) return null;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public class cls{_TableName}");
            sb.AppendLine("{");
            sb.AppendLine($"public enum en{_TableName}Mode {{ Add = 0, Update = 1 }}");
            sb.AppendLine($"public en{_TableName}Mode Mode = en{_TableName}Mode.Add;");
            sb.AppendLine($"{CreatVariableForClass(_Parameters)}");
            sb.AppendLine($"public cls{_TableName}()");
            sb.AppendLine("{");
            sb.AppendLine($"{InitializeTheVariableForEmptyConstructor(_Parameters)}");
            sb.AppendLine($"this.Mode = en{_TableName}Mode.Add;");
            sb.AppendLine("}");
            sb.AppendLine($"private cls{_TableName}({GetParametersWithDataTypes(_Parameters ,true)})");
            sb.AppendLine("{");
            sb.AppendLine($"{InitializeTheVariableForFullConstructor(_Parameters)}");
            sb.AppendLine($"this.Mode = en{_TableName}Mode.Update;");
            sb.AppendLine("}");
            sb.AppendLine($" public static async Task<cls{_TableName}> Find({ReturnOnlyPkParameters(_Parameters, true)})");
            sb.AppendLine("{");
            sb.AppendLine($"// TODO: Implement Find lookup for {_TableName}");
            sb.AppendLine("var Parameters = await \" call your Find By from dataAccess\"; ");
            sb.AppendLine("//Replace with ISFound from Parameters.IsFound");
            sb.AppendLine("if(true)");
            sb.AppendLine("{");
            sb.AppendLine("//Helper:");
            sb.AppendLine($"// Parameters: {clsStringModifier.GetParametersNameWithString(_Parameters,true , "Parameters.")}");
            sb.AppendLine($"// return new cls{_TableName}({ReturnParameters(_Parameters, false)});");
            sb.AppendLine("}");
            sb.AppendLine("return null;");
            sb.AppendLine("}");
            sb.AppendLine($" public static async Task<bool> Is{_TableName}Exist({ReturnOnlyPkParameters(_Parameters,true)})");
            sb.AppendLine("{");
            sb.AppendLine($"// TODO: Check if {_TableName} exists");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine($" private async Task<bool> _AddNew{_TableName}()");
            sb.AppendLine("{");
            sb.AppendLine($"// TODO: Implement INSERT for {_TableName}");      
            sb.AppendLine("//Helper:");
            sb.AppendLine($"//Parameters: {clsStringModifier.GetParametersNameWithThis(_Parameters,false)}");
            sb.AppendLine();   
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine($" private async Task< bool> _Update{_TableName}()");
            sb.AppendLine("{");
            sb.AppendLine($"// TODO: Implement Update for {_TableName}");
            sb.AppendLine($"//Helper:");
            sb.AppendLine($"//Parameters : {clsStringModifier.GetParametersNameWithThis(_Parameters,true)}");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine($"public static async Task<bool> Delete{_TableName}({ReturnOnlyPkParameters(_Parameters, true)})");
            sb.AppendLine("{");
            sb.AppendLine($"// TODO: Implement Delete for  {_TableName}");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine($" public static async Task<DataTable> GetAll{_TableName}()");
            sb.AppendLine("{");
            sb.AppendLine($"// TODO: Implement GetAll for {_TableName} ");
            sb.AppendLine($"return null;");
            sb.AppendLine("}");
            sb.AppendLine("public async Task<bool> Save()");
            sb.AppendLine("{");
            sb.AppendLine("switch (this.Mode)");
            sb.AppendLine("{");
            sb.AppendLine($"case en{_TableName}Mode.Add:");
            sb.AppendLine($"if (await _AddNew{_TableName}())");
            sb.AppendLine("{");
            sb.AppendLine($"this.Mode = en{_TableName}Mode.Update;");   
            sb.AppendLine("return true;");
            sb.AppendLine("}");
            sb.AppendLine("else");
            sb.AppendLine("{");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine($"case en{_TableName}Mode.Update:");
            sb.AppendLine($" return await _Update{_TableName}();");
            sb.AppendLine("}");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("}");
            return sb;
        }
        //Functions procerss
        private StringBuilder CreatVariableForClass(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                 clsStringModifier.TypeMappings_CSharp.TryGetValue(Parameter.Value.DataType, out string DataType);
                sb.AppendLine($"public {DataType}{clsStringModifier.ReturnISNull(Parameter.Value.IsNull, DataType)} {Parameter.Key} {{ get; set; }} ");
            }

            return sb;

        }
        private StringBuilder InitializeTheVariableForEmptyConstructor(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                clsStringModifier.TypeMappings_CSharp.TryGetValue(Parameter.Value.DataType, out string DataType);
                sb.AppendLine($"this.{Parameter.Key} = {clsStringModifier.GetDefaultValue(DataType)};");

            }
            return sb;
        }
        private  StringBuilder InitializeTheVariableForFullConstructor(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                clsStringModifier.TypeMappings_CSharp.TryGetValue(Parameter.Value.DataType, out string DataType);
                sb.AppendLine($"this.{Parameter.Key} = {Parameter.Key};");

            }
            return sb;
        }
        private  char ReturnISNull(string IsNull, string DaTypeta)
        {
            return DaTypeta.ToLower() != "string" ? IsNull.ToUpper() == "YES" ? '?' : ' ' : ' ';
        }
        private  StringBuilder ReturnOnlyPkParameters(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool WithDataType)
        {
            StringBuilder sb = new StringBuilder();
            var Key = Parameters.Where(kvp => kvp.Value.IsPrimaryKey.Contains("PK"));
            foreach (var item in Key)
            {
                clsStringModifier.TypeMappings_CSharp.TryGetValue(item.Value.DataType, out string DataType);

                if (WithDataType == true)
                {
                    sb.Append($"{DataType}{ReturnISNull(item.Value.IsNull, DataType)} {item.Key}, ");
                }
                else
                {
                    sb.Append($"{item.Key}, ");
                }
            }

            return sb.Length > 2 ? sb.Remove(sb.Length - 2, 2) : sb;
        }
        private  StringBuilder ReturnParameters(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool WithDataType)
        {
            StringBuilder sb = new StringBuilder();
            var Key = Parameters.Where(kvp => !kvp.Value.IsPrimaryKey.Contains("PK"));
            foreach (var item in Key)
            {
                clsStringModifier.TypeMappings_CSharp.TryGetValue(item.Value.DataType, out string DataType);

                if (WithDataType == true)
                {
                    sb.Append($"{DataType}{ReturnISNull(item.Value.IsNull, DataType)} {item.Key}, ");
                }
                else
                {
                    sb.Append($"{item.Key}, ");
                }
            }

            return sb.Length > 2 ? sb.Remove(sb.Length - 2, 2) : sb;
        }
        private  StringBuilder GetParametersWithDataTypes(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder ParametersWithDataType = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (!Parameter.Value.IsPrimaryKey.Contains("PK") || IncludePrimaryKey == true)
                {
                    clsStringModifier.TypeMappings_CSharp.TryGetValue(Parameter.Value.DataType, out string DataType);
                    ParametersWithDataType.Append($"{DataType}{ReturnISNull(Parameter.Value.IsNull, DataType)} {Parameter.Key}, ");
                }
            }
            return ParametersWithDataType.Length > 2? ParametersWithDataType.Remove(ParametersWithDataType.Length - 2, 2) : ParametersWithDataType;
        }
    }
}

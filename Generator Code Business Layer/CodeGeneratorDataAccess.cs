using Generator_Code_DataAccess_Layer;
using MyProject.Shared;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Code_Business_Layer
{
    public class clsCodeGeneratorDataAccess
    {
        private DataView _Table;
        private Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> _Parameters = new Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)>();
        private string _TableName;
        public clsCodeGeneratorDataAccess(DataView Table, string TableName)
        {
            _Table = Table;
            _TableName = TableName;

            _FillParameters(_Table);
        }
        public static async Task<DataView> GetAllDatabeses()
        {
            return await clsCodeGeneratorDataAccess_DataAcessLayer.GetAllDataBase();
        }
        public static async Task<DataView> GetAllTablesFromDataBase(string TableName)
        {
            return await clsCodeGeneratorDataAccess_DataAcessLayer.GetAllTablesFromDataBase(TableName);
        }
        public static async Task<DataView> GetTableDetailsFromDataBase(string DataBaseName, string TableNam)
        {
            return await clsCodeGeneratorDataAccess_DataAcessLayer.GetTableDetailsFromDataBase(DataBaseName, TableNam);
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
        private StringBuilder _AddNew()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public static async Task<int>AddNew{_TableName}({GetParametersWithDataTypes(_Parameters, false)})");
            sb.AppendLine("{");
            sb.AppendLine("int ID = -1;");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine("using(SqlConnection connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine("using(SqlCommand command = new SqlCommand(\"Add Your Store Procedure\", connection))");
            sb.AppendLine("{");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine($"{GetCommendParametersWithValue(_Parameters, false)}");
            sb.AppendLine("SqlParameter OutPutIDParam = new SqlParameter(\"put here your output variable in store procedure \", SqlDbType.Int)");
            sb.AppendLine("{");
            sb.AppendLine("Direction = ParameterDirection.Output");
            sb.AppendLine("};");
            sb.AppendLine("command.Parameters.Add(OutPutIDParam);");
            sb.AppendLine(" await connection.OpenAsync();");
            sb.AppendLine(" await command.ExecuteNonQueryAsync();");
            sb.AppendLine("if (OutPutIDParam.Value != DBNull.Value)");
            sb.AppendLine("{");
            sb.AppendLine(" ID = (int)OutPutIDParam.Value;");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine(" catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("}");
            sb.AppendLine("return ID;");
            sb.AppendLine("}");
            return sb;
        }
        private StringBuilder _Update()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public static async Task<bool> Update{_TableName}({GetParametersWithDataTypes(_Parameters, true)})");
            sb.AppendLine("{");
            sb.AppendLine("int AffectedRows = 0;");
            sb.AppendLine(" using (SqlConnection connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine(" SqlCommand command = new SqlCommand(\"Add Your Store Procedure\", connection);");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine($"{GetCommendParametersWithValue(_Parameters, true)}");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine("await connection.OpenAsync();");
            sb.AppendLine("AffectedRows = await command.ExecuteNonQueryAsync();");
            sb.AppendLine("}");
            sb.AppendLine("catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine(" return AffectedRows > 0;");
            sb.AppendLine("}");
            return sb;
        }
        private StringBuilder _Delete()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" public static async Task<bool> Delete{_TableName}({ReturnOnlyPkParameters(_Parameters, true)})");
            sb.AppendLine("{");
            sb.AppendLine("int AffectedRows = 0;");
            sb.AppendLine(" using (SqlConnection connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine(" SqlCommand command = new SqlCommand(\"Add Your Store Procedure\", connection);");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine($"{ReturnCommandOnlyPkParameters(_Parameters)}");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine("await connection.OpenAsync();");
            sb.AppendLine("AffectedRows = await command.ExecuteNonQueryAsync();");
            sb.AppendLine("}");
            sb.AppendLine(" catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("return AffectedRows > 0;");
            sb.AppendLine("}");
            return sb;
        }
        private StringBuilder _GetAll()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public static async Task<DataTable> GetAll{_TableName}()");
            sb.AppendLine("{");
            sb.AppendLine("DataTable dt = new DataTable();");
            sb.AppendLine("using (SqlConnection connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine("SqlCommand command = new SqlCommand(\"Add Your Store Procedure\", connection);");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine("await connection.OpenAsync();");
            sb.AppendLine(" using (SqlDataReader reader = await command.ExecuteReaderAsync())");
            sb.AppendLine("{");
            sb.AppendLine(" // Add columns");
            sb.AppendLine("for (int i = 0; i < reader.FieldCount; i++)");
            sb.AppendLine("dt.Columns.Add(reader.GetName(i),reader.GetFieldType(i));");
            sb.AppendLine();
            sb.AppendLine("// Add rows asynchronously");
            sb.AppendLine("while (await reader.ReadAsync())");
            sb.AppendLine("{");
            sb.AppendLine("object[] values = new object[reader.FieldCount];");
            sb.AppendLine(" reader.GetValues(values);");
            sb.AppendLine("dt.Rows.Add(values);");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("// Consider logging the exception");
            sb.AppendLine("}");
            sb.AppendLine("return dt;");
            sb.AppendLine("}");
            sb.AppendLine("}");

            return sb;
        }
        private StringBuilder _IsExist()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public static async Task<bool> Is{_TableName}Exist({ReturnOnlyPkParameters(_Parameters, true)})");
            sb.AppendLine("{");
            sb.AppendLine("bool IsFound = false;");
            sb.AppendLine("using (SqlConnection connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine("SqlCommand command = new SqlCommand(\"Add Your Store Procedure\", connection);");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine($"command.Parameters.AddWithValue(\"@{ReturnOnlyPkParameters(_Parameters, false)}\", (object){ReturnOnlyPkParameters(_Parameters, false)} ?? DBNull.Value);");
            sb.AppendLine("SqlParameter returnParameter = new SqlParameter(\"@ReturnVal\", SqlDbType.Int)");
            sb.AppendLine("{");
            sb.AppendLine("Direction = ParameterDirection.ReturnValue");
            sb.AppendLine("};");
            sb.AppendLine("command.Parameters.Add(returnParameter);");
            sb.AppendLine("await connection.OpenAsync();");
            sb.AppendLine("await command.ExecuteNonQueryAsync();");
            sb.AppendLine("IsFound = (int)returnParameter.Value == 1;");
            sb.AppendLine("}");
            sb.AppendLine("catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("// Consider logging the exception");
            sb.AppendLine("// Log.Error(ex, \"Error checking if person exists\");");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine(" return IsFound;");
            sb.AppendLine("}");
            return sb;
        }
        private StringBuilder _SearchBy()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public static async Task<DataView> SearchBy{_TableName}({ReturnOnlyPkParameters(_Parameters, true)})");
            sb.AppendLine("{");
            sb.AppendLine("DataTable dt = new DataTable();");
            sb.AppendLine("using (SqlConnection connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine(" SqlCommand command = new SqlCommand(\"Add Your Store Procedure\", connection);");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine($"command.Parameters.AddWithValue(\"@{ReturnOnlyPkParameters(_Parameters, false)}\", {ReturnOnlyPkParameters(_Parameters, false)});");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine(" await connection.OpenAsync();");
            sb.AppendLine(" using (SqlDataReader reader = await command.ExecuteReaderAsync())");
            sb.AppendLine("{");
            sb.AppendLine("if (reader.HasRows)");
            sb.AppendLine("{");
            sb.AppendLine("dt.Load(reader);");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("return dt.DefaultView;");
            sb.AppendLine("}");
            return sb;

        }
        private StringBuilder _GetBy()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public static async Task<(bool IsFound,{ReturnParameters(_Parameters, true)})> Get{_TableName}ByID({ReturnOnlyPkParameters(_Parameters, true)})");
            sb.AppendLine("{");
            sb.AppendLine($"{InitializeTheVariable(_Parameters, false)}");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine("using (var connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine("using (var command = new SqlCommand(\"Add Your Store Procedure\", connection))");
            sb.AppendLine("{");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine($"command.Parameters.AddWithValue(\"@{ReturnOnlyPkParameters(_Parameters, false)}\", {ReturnOnlyPkParameters(_Parameters, false)});");
            sb.AppendLine("await connection.OpenAsync();");
            sb.AppendLine("using (var reader = await command.ExecuteReaderAsync())");
            sb.AppendLine("{");
            sb.AppendLine("if (await reader.ReadAsync())");
            sb.AppendLine("{");
            sb.AppendLine($"{FillVariablesWithReader(_Parameters, false)}");
            sb.AppendLine($"return (true ,{ReturnParameters(_Parameters, false)});");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("}");
            sb.AppendLine($"return (false,{ReturnParameters(_Parameters, false)});");
            sb.AppendLine("}");
            return sb;
        }
        private StringBuilder _GetTotal()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public static async Task<int> GetTotal{_TableName}()");
            sb.AppendLine("{");
            sb.AppendLine("int total = 0;");
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine(" using (SqlConnection connection = new SqlConnection(\"put your connnection string here\"))");
            sb.AppendLine("{");
            sb.AppendLine("using (SqlCommand command = new SqlCommand(\"Add Your Store Procedure\", connection))");
            sb.AppendLine("{");
            sb.AppendLine("command.CommandType = CommandType.StoredProcedure;");
            sb.AppendLine("await connection.OpenAsync();");
            sb.AppendLine(" object result = await command.ExecuteScalarAsync();");
            sb.AppendLine("if (result != null && int.TryParse(result.ToString(), out int parsedTotal))");
            sb.AppendLine("{");
            sb.AppendLine(" total = parsedTotal;");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("catch (Exception ex)");
            sb.AppendLine("{");
            sb.AppendLine("// TODO log exception");
            sb.AppendLine("}");
            sb.AppendLine("return total;");
            sb.AppendLine("}");

            return sb;
        }
        public StringBuilder GenerateDataAccess()
        {
            if (_Parameters == null) return null;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public class cls{_TableName}");
            sb.AppendLine("{");

            sb.AppendLine("//AddNew");
            sb.AppendLine(_AddNew().ToString());

            sb.AppendLine("//Update");
            sb.AppendLine(_Update().ToString());

            sb.AppendLine("//Delete");
            sb.AppendLine(_Delete().ToString());

            sb.AppendLine("//GetAll");
            sb.AppendLine(_GetAll().ToString());

            sb.AppendLine("//IsExist");
            sb.AppendLine(_IsExist().ToString());

            sb.AppendLine("//SearchBy");
            sb.AppendLine(_SearchBy().ToString());

            sb.AppendLine("//GetBy");
            sb.AppendLine(_GetBy().ToString());

            sb.AppendLine("//GetTotal");
            sb.AppendLine(_GetTotal().ToString());

            sb.AppendLine("}");

            return sb;

        }
        public StringBuilder GenerateDataAccess(stCodeGenerateOptions options)
        {
            if (_Parameters == null) return null;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public class cls{_TableName}");
            sb.AppendLine("{");
            if (options.AddNew)   { sb.AppendLine("//AddNew"); sb.AppendLine(_AddNew().ToString());}
            if (options.Update)   { sb.AppendLine("//Update"); sb.AppendLine(_Update().ToString());}
            if (options.Delete)   { sb.AppendLine("//Delete"); sb.AppendLine(_Delete().ToString());}
            if (options.GetAll)   { sb.AppendLine("//GetAll"); sb.AppendLine(_GetAll().ToString());}
            if (options.IsExists) { sb.AppendLine("//IsExist"); sb.AppendLine(_IsExist().ToString());}
            if (options.SearchBy) { sb.AppendLine("//SearchBy"); sb.AppendLine(_SearchBy().ToString());}
            if (options.GetBy)    { sb.AppendLine("//GetBy"); sb.AppendLine(_GetBy().ToString());}
            if (options.GetTotal) { sb.AppendLine("//GetTotal"); sb.AppendLine(_GetTotal().ToString());}
            sb.AppendLine("}");

            return sb;
        }
        // Functions procerss
        private StringBuilder GetParametersWithDataTypes(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
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
            return ParametersWithDataType.Length > 2 ?ParametersWithDataType.Remove(ParametersWithDataType.Length - 2, 2):ParametersWithDataType;
        }
        private char ReturnISNull(string IsNull, string DaTypeta)
        {
            return DaTypeta.ToLower() != "string" ? IsNull.ToUpper() == "YES" ? '?' : ' ' : ' ';
        }
        private StringBuilder GetCommendParametersWithValue(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (!Parameter.Value.IsPrimaryKey.Contains("PK") || IncludePrimaryKey == true)
                {
                    if (Parameter.Value.IsNull.ToUpper() == "NO")
                        sb.AppendLine($"command.Parameters.AddWithValue(\"@{Parameter.Key.Trim()}\", {Parameter.Key.Trim()});");
                    else
                        sb.AppendLine($"//Nullable\ncommand.Parameters.AddWithValue(\"@{Parameter.Key.Trim()}\", {_Condition(Parameter.Key, Parameter.Value.DataType)} ? DBNull.Value :(object) {Parameter.Key});");
                }
            }

            return sb;
        }
        private string _Condition(string Parameter, string DataType)
        {
            if (DataType.ToLower() == "string") return "string.IsNullOrEmpty(Parameter)";
            else return $"{Parameter} == null";
        }
        private StringBuilder ReturnCommandOnlyPkParameters(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters)
        {
            StringBuilder sb = new StringBuilder();
            var Key = Parameters.Where(kvp => kvp.Value.IsPrimaryKey.Contains("PK"));
            foreach (var item in Key)
            {
                sb.AppendLine($" command.Parameters.AddWithValue(\"@{item.Key}\", {item.Key});");
            }
            return sb;
        }
        private StringBuilder ReturnOnlyPkParameters(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool WithDataType)
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

            return sb.Length>2? sb.Remove(sb.Length - 2, 2):sb;
        }
        private StringBuilder ReturnParameters(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool WithDataType)
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
        private StringBuilder InitializeTheVariable(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> item in Parameters)
            {

                if (!IncludePrimaryKey && item.Value.IsPrimaryKey.Contains("PK"))
                    continue;

                clsStringModifier.TypeMappings_CSharp.TryGetValue(item.Value.DataType, out string DataType);
                sb.AppendLine($"{DataType} {item.Key} = {clsStringModifier.GetDefaultValue(DataType)};");

            }
            return sb;
        }
        private StringBuilder FillVariablesWithReader(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {

                if (!IncludePrimaryKey && Parameter.Value.IsPrimaryKey.Contains("PK"))
                    continue;

                clsStringModifier.TypeMappings_CSharp.TryGetValue(Parameter.Value.DataType, out string DataType);
                if (Parameter.Value.IsNull.ToUpper() == "NO")
                {
                    sb.AppendLine($"{Parameter.Key} = ({DataType})reader[\"{Parameter.Key}\"];");
                }
                else
                {
                    sb.AppendLine($"{Parameter.Key} = reader[\"{Parameter.Key}\"] == DBNull.Value ? {clsStringModifier.GetDefaultValue(Parameter.Value.DataType)} : ({DataType})reader[\"{Parameter.Key}\"];");
                }
            }


            return sb;
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Generator_Code_Business_Layer
{
    public class clsCodeGeneratorStoredProcedure
    {
        private DataView _Table;
        private Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> _Parameters = new Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)>();
        private string _TableName;
        public clsCodeGeneratorStoredProcedure(DataView table, string tableName)
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
        public StringBuilder CodeGeneratorStoredProcedure()
        {
            if (_Parameters == null) return null;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("//Add");
            sb.AppendLine(Add().ToString());

            sb.AppendLine("//Update");
            sb.AppendLine(Update().ToString());

            sb.AppendLine("//Delete");
            sb.AppendLine(Delete().ToString());

            sb.AppendLine("//GetAll");
            sb.AppendLine(GetAll().ToString());

            sb.AppendLine("//IsExist");
            sb.AppendLine(IsExist().ToString());

            sb.AppendLine("//GetBy");
            sb.AppendLine(GetBy().ToString());
            return sb;

        }
        private StringBuilder Add()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Create procedure SP_AddNew{_TableName}");
            sb.AppendLine($"{GetVariableForStoredProcedureWithDataType(_Parameters, false)}");
            sb.AppendLine("@NewID int output");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine($"INSERT INTO {_TableName}({clsStringModifier.GetParametersName(_Parameters, false)})");
            sb.AppendLine($"VALUES({GetVariableForStoredProcedure(_Parameters, false)})");
            sb.AppendLine("set @NewID = SCOPE_IDENTITY()");
            sb.AppendLine("end");
            return sb;
        }
        private StringBuilder Update()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"create procedure SP_Update{_TableName}");
            sb.AppendLine($"{GetVariableForStoredProcedureWithDataType(_Parameters, true)}");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine($"update {_TableName}");
            sb.AppendLine("Set");
            sb.AppendLine($"{InitializeTheVariableStoredProcedure(_Parameters, false)}");
            sb.AppendLine("where 'Enter Condition'= 'Condition'");
            sb.AppendLine("end");



            return sb;
        }
        private StringBuilder Delete()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"create Procedure SP_Delete{_TableName}");
            sb.AppendLine("@ID int");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine($"delete from {_TableName}");
            sb.AppendLine("where 'Enter Condition'= 'Condition'");
            sb.AppendLine("end");


            return sb;
        }
        private StringBuilder GetAll()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"create Procedure SP_GetAll{_TableName}");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine($"select * from {_TableName}");
            sb.AppendLine("end");


            return sb;
        }
        private StringBuilder IsExist()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"create Procedure SP_Is{_TableName}Exists");
            sb.AppendLine("@ID int");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine($"if exists (select * from {_TableName} where 'Enter Condition'= 'Condition')");
            sb.AppendLine("return 1");
            sb.AppendLine("else");
            sb.AppendLine("return 0");
            sb.AppendLine("end");
            return sb;
        }
        private StringBuilder GetBy()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"create Procedure SP_Get{_TableName}By");
            sb.AppendLine("@ID int");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine($"select * from {_TableName}");
            sb.AppendLine("where 'Enter Condition'= 'Condition'");
            sb.AppendLine("end");


            return sb;
        }
        // Functions procerss
        private StringBuilder GetVariableForStoredProcedureWithDataType(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (Parameter.Value.IsPrimaryKey.Contains("PK") && !IncludePrimaryKey == true)
                    continue;

                //clsStringModifier.TypeMappings_SQL.TryGetValue(Parameter.Value.DataType, out string DataType);
                sb.AppendLine($"@{Parameter.Key} {Parameter.Value.DataType} {_ReturnValue(Parameter.Value.DataType)},");
            }

            return sb;
        }
        private StringBuilder GetVariableForStoredProcedure(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (Parameter.Value.IsPrimaryKey.Contains("PK") && !IncludePrimaryKey == true)
                    continue;

                clsStringModifier.TypeMappings_SQL.TryGetValue(Parameter.Value.DataType, out string DataType);
                sb.AppendLine($"@{Parameter.Key} ,");
            }

            if (sb.Length > 1) sb.Length = sb.Length-4;
            return sb;
        }
        private  StringBuilder InitializeTheVariableStoredProcedure(Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameters, bool IncludePrimaryKey)
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, (string DataType, string IsNull, string IsPrimaryKey)> Parameter in Parameters)
            {
                if (!IncludePrimaryKey && Parameter.Value.IsPrimaryKey.Contains("PK"))
                    continue;
                sb.AppendLine($"{Parameter.Key} = @{Parameter.Key}  ,");
            }
            if (sb.Length > 1) sb.Length+=-4;
            return sb;

        }


        private string _ReturnValue(string DataType)
        {
            return DataType == "nvarchar" ? "(20)":"";
        }

    }
}

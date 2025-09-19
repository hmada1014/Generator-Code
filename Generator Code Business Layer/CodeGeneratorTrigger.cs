using MyProject.Shared;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Generator_Code_Business_Layer
{
    public class clsCodeGeneratorTrigger
    {
        private DataView _Table;
        private Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)> _Parameters = new Dictionary<string, (string DataType, string IsNull, string IsPrimaryKey)>();
        private string _TableName;
        public clsCodeGeneratorTrigger(DataView table, string tableName)
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
        public StringBuilder CodeGeneratorTriggers(stCodeGenerateTriggerOptions st)
        {
            StringBuilder sb = new StringBuilder();

            if (st.AfterInsert) { sb.AppendLine("// After Insert"); sb.AppendLine(AfterInsertTriggers().ToString()); }
            if (st.AfterUpdate) { sb.AppendLine("// After Update"); sb.AppendLine(AfterUpdateTriggers().ToString()); }
            if (st.AfterDelete) { sb.AppendLine("// After Delete"); sb.AppendLine(AfterDeleteTriggers().ToString()); }

            if (st.instedOfDelete) { sb.AppendLine("// Insted Of Delete"); sb.AppendLine(InsteadOfDeleteTrigger().ToString()); }
            if (st.instedOfInsert) { sb.AppendLine("// Insted Of Insert"); sb.AppendLine(InstedOfInsertTrigger().ToString()); }
            if (st.instedOfUpdate) { sb.AppendLine("// Insted Of Update"); sb.AppendLine(InstedOfUpdateTrigger().ToString()); }

            return sb;
        }

        private StringBuilder AfterInsertTriggers()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Create trigger trg_AfterInsert{_TableName} on {_TableName}");
            sb.AppendLine("after insert");
            sb.AppendLine("as");
            sb.AppendLine("begin ");
            sb.AppendLine("-- DO your implament ");
            sb.AppendLine("--Example :");
            sb.AppendLine($"--INSERT INTO {_TableName}({clsStringModifier.GetParametersName(_Parameters, true)})");
            sb.AppendLine($"--select {clsStringModifier.GetParametersName(_Parameters, true)} from inserted");
            sb.AppendLine("end");
            return sb;
        }
        private StringBuilder AfterUpdateTriggers()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CREATE TRIGGER trg_AfterUpdate{_TableName} ON {_TableName}");
            sb.AppendLine("After Update");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine("-- Do your implament");
            sb.AppendLine("--Example :");
            sb.AppendLine("-- if UPDATE(Grade) -- in this condtion will not insert into StudentUpdateLog while Grade not changed");
            sb.AppendLine("-- begin");
            sb.AppendLine("--insert into StudentUpdateLog(StudentID,OldGrade,NewGrade)");
            sb.AppendLine("--select i.StudentID,d.Grade as OldGrade,i.Grade as NewGrade");
            sb.AppendLine("--From inserted i");
            sb.AppendLine("--inner join deleted d on d.StudentID  = i.StudentID");
            sb.AppendLine("--End");
            sb.AppendLine("end");
            return sb;
        }
        private StringBuilder AfterDeleteTriggers()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CREATE TRIGGER trg_AfterDelete{_TableName} ON {_TableName}");
            sb.AppendLine("After Delete");
            sb.AppendLine("as");
            sb.AppendLine("begin");
            sb.AppendLine("--insert into StudentDeleteLog(StudentID,Name,Subject,Grade)");
            sb.AppendLine("--select StudentID,Name,Subject,Grade from deleted");
            sb.AppendLine("End");

            return sb;
        }

        private StringBuilder InsteadOfDeleteTrigger()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"CREATE TRIGGER trg_InsteadOfDelete{_TableName} ON {_TableName}");
            sb.AppendLine("INSTEAD OF DELETE");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("-- Marking the record as inactive instead of deleting");
            sb.AppendLine("-- Example :");
            sb.AppendLine("-- Cation After Delete will not work if you use  insted of Delete");
            sb.AppendLine("-- UPDATE Students");
            sb.AppendLine("-- SET IsActive = 0");
            sb.AppendLine("-- FROM Students S");
            sb.AppendLine("-- INNER JOIN deleted D ON S.StudentID = D.StudentID;");
            sb.AppendLine("end");
            return sb;
        }
        private StringBuilder InstedOfUpdateTrigger()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-----------------------How Worke-------------------------");
            sb.AppendLine("-- here we make trigger instead of update on StudentView");
            sb.AppendLine("-- will update two table PersonalInfo and AcademicInfo");
            sb.AppendLine("-- create TRIGGER trg_InsteadOfUpdateStudentView ON StudentView");
            sb.AppendLine("-- instead of UPDATE");
            sb.AppendLine("-- AS");
            sb.AppendLine("-- Begin");
            sb.AppendLine("-- update PersonalInfo");
            sb.AppendLine("-- set Name = i.Name,");
            sb.AppendLine("-- Address = i.Address");
            sb.AppendLine("-- from PersonalInfo p");
            sb.AppendLine("-- inner join inserted i on p.StudentID = i.StudentID\n");
            sb.AppendLine("-- Update AcademicInfo");
            sb.AppendLine("-- set Course = i.Course ,");
            sb.AppendLine("-- Grade = i.Grade");
            sb.AppendLine("-- from AcademicInfo A");
            sb.AppendLine("-- inner join inserted i on a.StudentID = a.StudentID");
            sb.AppendLine("-- end");
            sb.AppendLine("-- when we call update our trigger trg_InsteadOfUpdateStudentView will fire");
            sb.AppendLine("-- because we made trigger on StudentView");
            sb.AppendLine("-- SQl Update will not fire because we overried on it our trigger trg_InsteadOfUpdateStudentView will work");
            sb.AppendLine("-- update will be on PersonalInfo and AcademicInfo");
            sb.AppendLine("-- Update StudentView");
            sb.AppendLine("-- set Name = 'Hamed',");
            sb.AppendLine("-- Course = 'Math',");
            sb.AppendLine("-- Grade = 66");
            sb.AppendLine("-- where StudentID = 2");
            sb.AppendLine("-- here we will see result after update");
            sb.AppendLine("__ select * from StudentView");
            sb.AppendLine();
            sb.AppendLine("-- here our trigger will not fire because our trigger is on StudentView");
            sb.AppendLine("-- update PersonalInfo");
            sb.AppendLine("-- set Name = 'Ahmed'");
            sb.AppendLine("-- where StudentID = 2");

            sb.AppendLine("-----------------------How Worke-------------------------");

            sb.AppendLine("CREATE TRIGGER trigger_name");
            sb.AppendLine("ON view_or_table");
            sb.AppendLine("INSTEAD OF UPDATE");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    -- Your custom update logic here");
            sb.AppendLine("END;");

            return sb;
        }
        private StringBuilder InstedOfInsertTrigger()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-----------------------How Worke-------------------------");
            sb.AppendLine("-- CREATE VIEW StudentView AS");
            sb.AppendLine("-- SELECT P.StudentID, P.Name, P.Address, A.Course, A.Grade");
            sb.AppendLine("-- FROM PersonalInfo P");
            sb.AppendLine("-- JOIN AcademicInfo A ON P.StudentID = A.StudentID;");
            sb.AppendLine("-- ");
            sb.AppendLine("-- CREATE TRIGGER trg_InsteadOFinsertStudentview ON StudentView");
            sb.AppendLine("-- INSTEAD OF INSERT");
            sb.AppendLine("-- AS");
            sb.AppendLine("-- BEGIN");
            sb.AppendLine("-- ");
            sb.AppendLine("--     INSERT INTO PersonalInfo(StudentID, Name, Address)");
            sb.AppendLine("--     SELECT StudentID, Name, Address FROM inserted;");
            sb.AppendLine("-- ");
            sb.AppendLine("--     INSERT INTO AcademicInfo(StudentID, Course, Grade)");
            sb.AppendLine("--     SELECT StudentID, Course, Grade FROM inserted;");
            sb.AppendLine("-- ");
            sb.AppendLine("-- END;");
            sb.AppendLine("-----------------------How Worke-------------------------");
            sb.AppendLine("CREATE TRIGGER trigger_name");
            sb.AppendLine("ON table_or_view");
            sb.AppendLine("INSTEAD OF INSERT");
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    -- Your custom insert logic here");
            sb.AppendLine("END;");
            return sb;
        }
    }
}

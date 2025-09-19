using Generator_Code_Business_Layer;
using MyProject.Shared;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace Generator_Code_Presentation_Layer
{
    public partial class frmMain : Form
    {
        private frmConnection _frmConnection;
        public FormWindowState FormWindowState { get; private set; }
        public frmMain(frmConnection frmConnection)
        {
            InitializeComponent();
            _frmConnection = frmConnection;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            _frmConnection.moCircularProgressBar1.Status = CircularProgressBar.MoCircularProgressBar.ConnectionStatus.Connecting;
            _frmConnection.moCircularProgressBar1.Visible = false;
            _frmConnection.Show();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            cbOptionsCodeGenerator.SelectedIndex = 0;
            _SetupdgvCodeGeneratorCustom();
            _SetupdgvTableDetailsStoredProcedureCustom();

        }
        private void btnConnectionSettings_Click(object sender, EventArgs e)
        {
            frmConnnectionSetting connnectionSetting = new frmConnnectionSetting();
            connnectionSetting.DataBack += _FillTableData;
            connnectionSetting.ShowDialog();
        }
        private async void _FillTableData(object sender, string DatabaseSelected)
        {
            _RestTableDetails();

            DataView data = await clsCodeGeneratorDataAccess.GetAllTablesFromDataBase(DatabaseSelected);
            dgvTablesFromDBCodeGenerator.DataSource = data;
            dgvTablesFromDBCodeGeneratorOptions.DataSource = data;
            dgvTablesFromDBStoredProcedure.DataSource = data;
            dgvTablesFromDBCodeGeneratorTrigger.DataSource = data;

            if (dgvTablesFromDBCodeGenerator.Rows.Count > 0)
            {
                lblDBSelectedCodeGenerator.Text = DatabaseSelected;
                lblDBSelectedCodeGeneratorOptions.Text = DatabaseSelected;
                lblDBSelectedStoredProcedure.Text = DatabaseSelected;
                lblDBSelectedCodeGeneratorTrigger.Text = DatabaseSelected;
            }
            else
            {
                lblDBSelectedCodeGenerator.Text = "N/A";
                lblDBSelectedCodeGeneratorOptions.Text = "N/A";
                lblDBSelectedStoredProcedure.Text = "N/A";
                lblDBSelectedCodeGeneratorTrigger.Text = "N/A";

            }
        }
        private async void dgvTablesFromDatabase_DoubleClick(object sender, EventArgs e)
        {
            if (dgvTablesFromDBCodeGenerator.Rows.Count > 0)
            {
                dgvTableDetailsCodeGenerator.DataSource = await clsCodeGeneratorDataAccess.GetTableDetailsFromDataBase(ConfigurationManager.AppSettings["DataBaseName"], dgvTablesFromDBCodeGenerator.CurrentCell.Value.ToString());
                lblTableDetailsNameCodeGenerator.Text = dgvTablesFromDBCodeGenerator.CurrentCell.Value.ToString();

                gbContainerCodeGenerator.Enabled = true;
            }
        }
        private async void dgvTablesFromDatabaseCodeGenOpts_DoubleClick(object sender, EventArgs e)
        {
            if (dgvTablesFromDBCodeGeneratorOptions.Rows.Count > 0)
            {
                dgvTableDetailsCodeGeneratorOptions.DataSource = await clsCodeGeneratorDataAccess.GetTableDetailsFromDataBase(ConfigurationManager.AppSettings["DataBaseName"], dgvTablesFromDBCodeGeneratorOptions.CurrentCell.Value.ToString());
                lblTableDetailsNameCodeGeneratorOptions.Text = dgvTablesFromDBCodeGeneratorOptions.CurrentCell.Value.ToString();
                gbContainerCodeGeneratorOptions.Enabled = true;
            }
        }
        private void _RestTableDetails()
        {
            dgvTableDetailsCodeGenerator.DataSource = null;
            dgvTableDetailsCodeGeneratorOptions.DataSource = null;
            dgvTableDetailsStoredProcedure.DataSource = null;
            dgvTableDetailsCodeGeneratorTrigger.DataSource = null;

            lblDBSelectedCodeGenerator.Text = "N/A";
            lblDBSelectedCodeGeneratorOptions.Text = "N/A";
            lblDBSelectedStoredProcedure.Text = "N/A";
            lblDBSelectedCodeGeneratorTrigger.Text = "N/A"; 
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
           
            clsCodeGeneratorDataAccess codeGeneratorDataAccess = new clsCodeGeneratorDataAccess((DataView)dgvTableDetailsCodeGenerator.DataSource, lblTableDetailsNameCodeGenerator.Text);
            txtCodeGenerator.Text = codeGeneratorDataAccess.GenerateDataAccess().ToString();

            txtCodeGenerator.Text += "\n\n//Business\n\n";

            clsCodeGeneratorBusiness codeGeneratorBusiness = new clsCodeGeneratorBusiness((DataView)dgvTableDetailsCodeGenerator.DataSource, lblTableDetailsNameCodeGenerator.Text);

            txtCodeGenerator.Text += codeGeneratorBusiness.CodeGeneratorBusiness();
        }
        //---------------------------------------
        // Generat For all tables
        private async void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dgvTablesFromDBCodeGenerator.Rows.Count; i++)
            {
                // Extract table name directly from the row instead of relying on UI state
                string tableName = dgvTablesFromDBCodeGenerator.Rows[i].Cells[0].Value.ToString();
                // Get table details directly (extract this logic from the double-click handler)
                DataView tableDetails = await GetTableDetails(tableName);

                clsCodeGeneratorDataAccess codeGeneratorDataAccess =
                    new clsCodeGeneratorDataAccess(tableDetails, tableName);

                sb.AppendLine(codeGeneratorDataAccess.GenerateDataAccess().ToString());
            }

            txtCodeGenerator.Text = sb.ToString();
        }
        // Generat For all tables
        private async Task<DataView> GetTableDetails(string tableName)
        {
            // Whatever logic you currently have inside dgvTablesFromDatabase_DoubleClick
            // Example:
            lblTableDetailsNameCodeGenerator.Text = tableName;
            return await clsCodeGeneratorDataAccess.GetTableDetailsFromDataBase(ConfigurationManager.AppSettings["DataBaseName"], lblTableDetailsNameCodeGenerator.Text);
        }
        //---------------------------------------
        private void btnMaximized_Click(object sender, EventArgs e)
        {
            if (FormWindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                FormWindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                FormWindowState = FormWindowState.Maximized;
            }

        }
        private void btnGenerateStoredProcedure_Click(object sender, EventArgs e)
        {
            clsCodeGeneratorStoredProcedure codeGeneratorStoredProcedure = new clsCodeGeneratorStoredProcedure((DataView)dgvTableDetailsStoredProcedure.DataSource, lblTableDetailsNameStoredProcedure.Text);
            txtSoredProcedure.Text = codeGeneratorStoredProcedure.CodeGeneratorStoredProcedure().ToString();
        }
        private async void dgvTablesFromDataBaseStoredProcedure_DoubleClick(object sender, EventArgs e)
        {
            //dgvTableDetailsStoredProcedure.DataSource

            if (dgvTablesFromDBStoredProcedure.Rows.Count > 0)
            {
                dgvTableDetailsStoredProcedure.DataSource = await clsCodeGeneratorDataAccess.GetTableDetailsFromDataBase(ConfigurationManager.AppSettings["DataBaseName"], dgvTablesFromDBStoredProcedure.CurrentCell.Value.ToString());
                lblTableDetailsNameStoredProcedure.Text = dgvTablesFromDBStoredProcedure.CurrentCell.Value.ToString();
                gbContainerStoredProcedure.Enabled = true;
            }
        }
        private async void dgvTablesFromDBCodeGeneratorTrigger_DoubleClick(object sender, EventArgs e)
        {
            if (dgvTablesFromDBCodeGeneratorTrigger.Rows.Count > 0)
            {
                dgvTableDetailsCodeGeneratorTrigger.DataSource = await clsCodeGeneratorDataAccess.GetTableDetailsFromDataBase(ConfigurationManager.AppSettings["DataBaseName"], dgvTablesFromDBCodeGeneratorTrigger.CurrentCell.Value.ToString());
                lblTableDetailsNameCodeGeneratorTrigger.Text = dgvTablesFromDBCodeGeneratorTrigger.CurrentCell.Value.ToString();
                gbContainerCodeGeneratorTrigger.Enabled = true;
            }
        }
        private void btnCodeGenerateOptions_Click(object sender, EventArgs e)
        {
            stCodeGenerateOptions st = new stCodeGenerateOptions();
            _GetSelectedCodeGenerate(ref st);


            clsCodeGeneratorDataAccess codeGeneratorDataAccess = new clsCodeGeneratorDataAccess((DataView)dgvTableDetailsCodeGeneratorOptions.DataSource, lblTableDetailsNameCodeGeneratorOptions.Text);
            txtCodeGeneratorOptions.Text = codeGeneratorDataAccess.GenerateDataAccess(st).ToString();

            if (!cbGetBusiness.Checked) return;
            txtCodeGeneratorOptions.Text += "\n\n//Business\n\n";
            clsCodeGeneratorBusiness codeGeneratorBusiness = new clsCodeGeneratorBusiness((DataView)dgvTableDetailsCodeGeneratorOptions.DataSource, lblTableDetailsNameCodeGeneratorOptions.Text);
            txtCodeGeneratorOptions.Text += codeGeneratorBusiness.CodeGeneratorBusiness();

        }
        private void _GetSelectedCodeGenerate(ref stCodeGenerateOptions st)
        {
            if (cbSelectAll.Checked)
            {
                st.AddNew = true; st.Update = true; st.Delete = true;
                st.GetAll = true; st.GetTotal = true; st.IsExists = true;
                st.SearchBy = true; st.GetBy = true;
                return;
            }
            if (cbAddNew.Checked) st.AddNew = true;
            if (cbUpdate.Checked) st.Update = true;
            if (cbDelete.Checked) st.Delete = true;
            if (cbGetALL.Checked) st.GetAll = true;
            if (cbGetTotal.Checked) st.GetTotal = true;
            if (cbIsExists.Checked) st.SearchBy = true;
            if (cbGetBy.Checked) st.GetBy = true;

        }
        private string _GetDirectionOfSave()
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFile.Title = "Select an Image";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    return openFile.FileName;
                }
            }

            return " ";
        }
        private void _SetupdgvCodeGeneratorCustom()
        {

            DataGridViewTextBoxColumn ColumnName = new DataGridViewTextBoxColumn();
            ColumnName.HeaderText = "Column Name";
            ColumnName.Name = "ColumnName";
            dgvCodeGeneratorCustom.Columns.Add(ColumnName);

            DataGridViewComboBoxColumn DataType = new DataGridViewComboBoxColumn();
            DataType.HeaderText = "Data Type";
            DataType.Name = "DataType";
            DataType.Items.AddRange("INT", "BIGINT", "SMALLINT", "TINYINT", "DECIMAL", "NUMERIC", "FLOAT", "REAL", "BIT", "CHAR", "VARCHAR", "TEXT", "NCHAR", "NVARCHAR", "NTEXT", "DATE", "TIME", "DATETIME", "SMALLDATETIME", "TIMESTAMP", "BINARY", "VARBINARY", "IMAGE", "UNIQUEIDENTIFIER");
            dgvCodeGeneratorCustom.Columns.Add(DataType);



            DataGridViewTextBoxColumn ColumnCharacterLeangth = new DataGridViewTextBoxColumn();
            ColumnCharacterLeangth.HeaderText = "Character Leangth";
            ColumnCharacterLeangth.Name = "CharacterLeangth";
            dgvCodeGeneratorCustom.Columns.Add(ColumnCharacterLeangth);



            DataGridViewComboBoxColumn ColumnISNull = new DataGridViewComboBoxColumn();
            ColumnISNull.HeaderText = "Is Allow Null";
            ColumnISNull.Name = "IsNull";
            ColumnISNull.Items.AddRange("YES", "No");
            dgvCodeGeneratorCustom.Columns.Add(ColumnISNull);



            DataGridViewComboBoxColumn ColumnIsPrimaryKey = new DataGridViewComboBoxColumn();
            ColumnIsPrimaryKey.HeaderText = "Is Primary Key";
            ColumnIsPrimaryKey.Name = "IsPrimaryKey";
            ColumnIsPrimaryKey.Items.AddRange("Yes PK", "No");
            dgvCodeGeneratorCustom.Columns.Add(ColumnIsPrimaryKey);

        }
        private void _SetupdgvTableDetailsStoredProcedureCustom()
        {

            DataGridViewTextBoxColumn ColumnName = new DataGridViewTextBoxColumn();
            ColumnName.HeaderText = "Column Name";
            ColumnName.Name = "ColumnName";
            dgvTableDetailsStoredProcedureCustom.Columns.Add(ColumnName);

            DataGridViewComboBoxColumn DataType = new DataGridViewComboBoxColumn();
            DataType.HeaderText = "Data Type";
            DataType.Name = "DataType";
            DataType.Items.AddRange("INT", "BIGINT", "SMALLINT", "TINYINT", "DECIMAL", "NUMERIC", "FLOAT", "REAL", "BIT", "CHAR", "VARCHAR", "TEXT", "NCHAR", "NVARCHAR", "NTEXT", "DATE", "TIME", "DATETIME", "SMALLDATETIME", "TIMESTAMP", "BINARY", "VARBINARY", "IMAGE", "UNIQUEIDENTIFIER");
            dgvTableDetailsStoredProcedureCustom.Columns.Add(DataType);



            DataGridViewTextBoxColumn ColumnCharacterLeangth = new DataGridViewTextBoxColumn();
            ColumnCharacterLeangth.HeaderText = "Character Leangth";
            ColumnCharacterLeangth.Name = "CharacterLeangth";
            dgvTableDetailsStoredProcedureCustom.Columns.Add(ColumnCharacterLeangth);



            DataGridViewComboBoxColumn ColumnISNull = new DataGridViewComboBoxColumn();
            ColumnISNull.HeaderText = "Is Allow Null";
            ColumnISNull.Name = "IsNull";
            ColumnISNull.Items.AddRange("YES", "No");
            dgvTableDetailsStoredProcedureCustom.Columns.Add(ColumnISNull);



            DataGridViewComboBoxColumn ColumnIsPrimaryKey = new DataGridViewComboBoxColumn();
            ColumnIsPrimaryKey.HeaderText = "Is Primary Key";
            ColumnIsPrimaryKey.Name = "IsPrimaryKey";
            ColumnIsPrimaryKey.Items.AddRange("Yes PK", "No");
            dgvTableDetailsStoredProcedureCustom.Columns.Add(ColumnIsPrimaryKey);

        }
        private void btnCodeGenerateCustom_Click(object sender, EventArgs e)
        {

            DataView dvCodeGeneratorCustom = new DataView();
            if (_ValidationTxT(txtTableDetailsNameCodeGeneratorCustom)) return;
            if (_VlidationColumns((DataGridView)dgvCodeGeneratorCustom, ref dvCodeGeneratorCustom)) { MessageBox.Show("Make sure to fill columns correctly", "Error"); return; }

            clsCodeGeneratorDataAccess codeGeneratorDataAccess = new clsCodeGeneratorDataAccess(dvCodeGeneratorCustom, txtTableDetailsNameCodeGeneratorCustom.Text);
            txtCodeGeneratorCustom.Text = codeGeneratorDataAccess.GenerateDataAccess().ToString();

            txtCodeGeneratorCustom.Text += "\n\n//Business\n\n";
            clsCodeGeneratorBusiness codeGeneratorBusiness = new clsCodeGeneratorBusiness(dvCodeGeneratorCustom, txtTableDetailsNameCodeGeneratorCustom.Text);
            txtCodeGeneratorCustom.Text += codeGeneratorBusiness.CodeGeneratorBusiness();
        }
        private bool _VlidationColumns(DataGridView Table, ref DataView dv)
        {
            bool HasError = false;

            // تجهيز DataTable بنفس أعمدة DataGridView
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in Table.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            // المرور على الصفوف
            foreach (DataGridViewRow Row in Table.Rows)
            {
                if (Row.IsNewRow) continue;

                // التحقق من الأعمدة المطلوبة
                bool ColumnName = Row.Cells[0].Value == null || string.IsNullOrWhiteSpace(Row.Cells[0].Value.ToString());
                bool ColumnDataType = Row.Cells[1].Value == null || string.IsNullOrWhiteSpace(Row.Cells[1].Value.ToString());
                bool ColumnIsNull = Row.Cells[3].Value == null || string.IsNullOrWhiteSpace(Row.Cells[3].Value.ToString());
                bool ColumnIsPrimaryKey = Row.Cells[4].Value == null || string.IsNullOrEmpty(Row.Cells[4].Value.ToString());

                if (ColumnName || ColumnDataType || ColumnIsNull || ColumnIsPrimaryKey)
                {
                    HasError = true;
                    break;
                }

                // إضافة الصف إلى DataTable
                DataRow dr = dt.NewRow();
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    dr[i] = Row.Cells[i].Value ?? DBNull.Value;
                }
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count <= 0) { HasError = true; }
            // تحويل DataTable إلى DataView
            dv = new DataView(dt);

            return HasError;
        }
        private void btnGenerateStoredProcedureCustom_Click(object sender, EventArgs e)
        {
            DataView dvGeneratorStoredProcedureCustom = new DataView();
            if (_ValidationTxT(txtTableDetailsNameStoredProcedureCustom)) return;
            if (_VlidationColumns((DataGridView)dgvTableDetailsStoredProcedureCustom, ref dvGeneratorStoredProcedureCustom)) { MessageBox.Show("Make sure to fill columns correctly", "Error"); return; }

            clsCodeGeneratorStoredProcedure codeGeneratorStoredProcedure = new clsCodeGeneratorStoredProcedure((DataView)dgvTableDetailsStoredProcedureCustom.DataSource, txtTableDetailsNameStoredProcedureCustom.Text);
            txtSoredProcedureCustom.Text = codeGeneratorStoredProcedure.CodeGeneratorStoredProcedure().ToString();
        }
        private bool _ValidationTxT(object sender)
        {
            TextBox text = sender as TextBox;
            bool isValid = false;
            if (string.IsNullOrEmpty(text.Text))
            {
                ep.SetError(text, "Enter Table Name");
                isValid = true;
            }
            else
                ep.SetError(text, string.Empty);

            return isValid;
        }
        private void btnCodeGenerateTrigger_Click(object sender, EventArgs e)
        {
            stCodeGenerateTriggerOptions st = new stCodeGenerateTriggerOptions();
            _GetSelectedCodeGenerateTrigger(ref st);

            clsCodeGeneratorTrigger codeGeneratorTrigger = new clsCodeGeneratorTrigger((DataView)dgvTableDetailsCodeGeneratorTrigger.DataSource ,lblTableDetailsNameCodeGeneratorTrigger.Text);

            txtCodeGeneratorTrigger.Text = codeGeneratorTrigger.CodeGeneratorTriggers(st).ToString();
        }

        private void _GetSelectedCodeGenerateTrigger(ref stCodeGenerateTriggerOptions st)
        {
        
            if (cbAfterInsertTrigger.Checked) st.AfterInsert = true;
            if (cbAfterUpdateTrigger.Checked) st.AfterUpdate = true;
            if (cbAfterDeleteTrigger.Checked) st.AfterDelete = true;
            if (cbInsertInstedOfTrigger.Checked) st.instedOfInsert = true;
            if (cbUpdateInstedOfTrigger.Checked) st.instedOfUpdate = true;
            if (cbDeleteInstedOfTrigger.Checked) st.instedOfDelete = true;           
        }

    }
}

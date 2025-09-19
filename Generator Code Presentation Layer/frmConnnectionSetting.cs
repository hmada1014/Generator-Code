using Generator_Code_Business_Layer;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace Generator_Code_Presentation_Layer
{
    public partial class frmConnnectionSetting : Form
    {
        // to send back data to form who call 
        public delegate void DataBackEventHandler(object sender, string TableName);
        public event DataBackEventHandler DataBack;
        public frmConnnectionSetting()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void _LoadAllDataBase()
        {
            DataView data = await clsCodeGeneratorDataAccess.GetAllDatabeses();
            foreach (DataRowView Table in data)
            {
                cbDatabaseName.Items.Add(Table.Row.ItemArray[0]);
            }
            if(cbDatabaseName.Items.Count > 0)cbDatabaseName.SelectedIndex = 0;
        }
        private void frmConnnectionSetting_Load(object sender, EventArgs e)
        {
            _LoadAllDataBase();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _SaveConnectionValuesToConfig();
            DataBack?.Invoke(this, cbDatabaseName.SelectedItem.ToString());
            this.Close() ;
        }
        private void _SaveConnectionValuesToConfig()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["DataBaseName"].Value = cbDatabaseName.SelectedItem.ToString();
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            configuration.AppSettings.Settings["ConnectionString"].Value =
               $"Server={ConfigurationManager.AppSettings["Server"]};Database={ConfigurationManager.AppSettings["DataBaseName"]};User Id={ConfigurationManager.AppSettings["UserID"]};Password={ConfigurationManager.AppSettings["ServerPassword"]};";
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}

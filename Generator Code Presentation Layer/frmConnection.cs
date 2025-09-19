using CircularProgressBar;
using Generator_Code_Business_Layer;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Generator_Code_Presentation_Layer
{
    public partial class frmConnection : Form
    {
        public frmConnection()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private async void btnConnection_Click(object sender, EventArgs e)
        {
            moCircularProgressBar1.Status = MoCircularProgressBar.ConnectionStatus.Connecting;
            moCircularProgressBar1.Visible = true;

            bool ConnectionResult = await Task.Run(() =>
            {
                Task.Delay(500).Wait();
                return _CheckConnection();
            });

            if (ConnectionResult)
            {
                moCircularProgressBar1.Status = MoCircularProgressBar.ConnectionStatus.Success;
                if (cbRememberMe.Checked)
                    _SaveUserIDAndPasswordAndServerToRegistry(txtUserID.Text.Trim(), txtPassword.Text.Trim(), txtServer.Text.Trim());
                else
                    _DeleteUserNameAndPasswordFromRegistry();

                _SaveConnectionValuesToConfig(txtUserID.Text.Trim(), txtPassword.Text.Trim(), txtServer.Text.Trim());
                Task.Delay(1500).Wait();
                this.Hide();
                frmMain main = new frmMain(this);
                main.ShowDialog();
            }
            else
            {
                moCircularProgressBar1.Status = MoCircularProgressBar.ConnectionStatus.Failed;
            }


        }
        private bool _CheckConnection()
        {
            return clsDatabaseConnectionManager.ConnectionStatues(txtServer.Text.Trim(), txtPassword.Text.Trim(), txtUserID.Text.Trim());
        }
        private void _SaveConnectionValuesToConfig(string UserID, string ServerPassword, string Server)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["UserID"].Value = UserID;
            configuration.AppSettings.Settings["ServerPassword"].Value = ServerPassword;
            configuration.AppSettings.Settings["Server"].Value = Server;

            configuration.AppSettings.Settings["ConnectionString"].Value = $"Server={Server};Database=;User Id={UserID};Password={ServerPassword};";
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

        }
        private void _LoadUserIDAndPasswordAndServerFromRegistry()
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\CodeGenerator";
            string UserIDValue = "UserID";
            string ServerPasswordValue = "password";
            string ServerValue = "Server";

            txtUserID.Text = Registry.GetValue(KeyPath, UserIDValue, null) as string;
            txtPassword.Text = Registry.GetValue(KeyPath, ServerPasswordValue, null) as string;
            txtServer.Text = Registry.GetValue(KeyPath, ServerValue, null) as string;

            cbRememberMe.Checked = (!string.IsNullOrEmpty(txtUserID.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtPassword.Text));
        }
        private void _SaveUserIDAndPasswordAndServerToRegistry(string UserID, string ServerPassword, string Server)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\CodeGenerator";
            string UserIDValue = "UserID";
            string ServerPasswordValue = "password";
            string ServerValue = "Server";
            try
            {
                Registry.SetValue(KeyPath, UserIDValue, UserID, RegistryValueKind.String);
                Registry.SetValue(KeyPath, ServerPasswordValue, ServerPassword, RegistryValueKind.String);
                Registry.SetValue(KeyPath, ServerValue, Server, RegistryValueKind.String);
            }
            catch (Exception ex)
            {


            }

        }
        private void _DeleteUserNameAndPasswordFromRegistry()
        {
            string KeyPath = @"SOFTWARE\CodeGenerator";
            string UserIDValue = "UserID";
            string ServerPasswordValue = "password";
            string ServerValue = "Server";
            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(KeyPath, true))
                    {

                        if (key != null)
                        {

                            if (key.GetValue(UserIDValue) != null) key.DeleteValue(UserIDValue);
                            if (key.GetValue(ServerPasswordValue) != null) key.DeleteValue(ServerPasswordValue);
                            if (key.GetValue(ServerValue) != null) key.DeleteValue(ServerValue);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {


            }
            catch (Exception ex)
            {


            }

        }
        private void frmConnction_Load(object sender, EventArgs e)
        {
            _LoadUserIDAndPasswordAndServerFromRegistry();
        }
    }
}

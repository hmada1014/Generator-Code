

namespace Generator_Code_Presentation_Layer
{
    
    partial class frmConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnection));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.moPanal1 = new MoPanal.MoPanal();
            this.txtUserID = new Guna.UI.WinForms.GunaTextBox();
            this.txtPassword = new Guna.UI.WinForms.GunaTextBox();
            this.txtServer = new Guna.UI.WinForms.GunaTextBox();
            this.moCircularProgressBar1 = new CircularProgressBar.MoCircularProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.cbRememberMe = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnConnection = new Guna.UI.WinForms.GunaAdvenceButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cascadia Code", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(72, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 32);
            this.label3.TabIndex = 12;
            this.label3.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Code", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(72, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 32);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cascadia Code", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(72, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "User ID";
            // 
            // moPanal1
            // 
            this.moPanal1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(18)))), ((int)(((byte)(32)))));
            this.moPanal1.Dock = System.Windows.Forms.DockStyle.Top;
            this.moPanal1.Location = new System.Drawing.Point(0, 0);
            this.moPanal1.Name = "moPanal1";
            this.moPanal1.OnMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(18)))), ((int)(((byte)(40)))));
            this.moPanal1.OnMouseLeaveColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(18)))), ((int)(((byte)(32)))));
            this.moPanal1.Size = new System.Drawing.Size(473, 37);
            this.moPanal1.TabIndex = 19;
            this.moPanal1.Text = "moPanal1";
            // 
            // txtUserID
            // 
            this.txtUserID.BackColor = System.Drawing.Color.Transparent;
            this.txtUserID.BaseColor = System.Drawing.Color.White;
            this.txtUserID.BorderColor = System.Drawing.Color.Silver;
            this.txtUserID.BorderSize = 0;
            this.txtUserID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserID.FocusedBaseColor = System.Drawing.Color.White;
            this.txtUserID.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtUserID.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUserID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUserID.Location = new System.Drawing.Point(207, 199);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.PasswordChar = '\0';
            this.txtUserID.Radius = 5;
            this.txtUserID.SelectedText = "";
            this.txtUserID.Size = new System.Drawing.Size(203, 30);
            this.txtUserID.TabIndex = 21;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Transparent;
            this.txtPassword.BaseColor = System.Drawing.Color.White;
            this.txtPassword.BorderColor = System.Drawing.Color.Silver;
            this.txtPassword.BorderSize = 0;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.FocusedBaseColor = System.Drawing.Color.White;
            this.txtPassword.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtPassword.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.Location = new System.Drawing.Point(207, 237);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Radius = 5;
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(203, 30);
            this.txtPassword.TabIndex = 22;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtServer
            // 
            this.txtServer.BackColor = System.Drawing.Color.Transparent;
            this.txtServer.BaseColor = System.Drawing.Color.White;
            this.txtServer.BorderColor = System.Drawing.Color.Silver;
            this.txtServer.BorderSize = 0;
            this.txtServer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtServer.FocusedBaseColor = System.Drawing.Color.White;
            this.txtServer.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtServer.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtServer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtServer.Location = new System.Drawing.Point(207, 275);
            this.txtServer.Name = "txtServer";
            this.txtServer.PasswordChar = '\0';
            this.txtServer.Radius = 5;
            this.txtServer.SelectedText = "";
            this.txtServer.Size = new System.Drawing.Size(203, 30);
            this.txtServer.TabIndex = 23;
            // 
            // moCircularProgressBar1
            // 
            this.moCircularProgressBar1.FailureColor = System.Drawing.Color.Red;
            this.moCircularProgressBar1.Location = new System.Drawing.Point(223, 358);
            this.moCircularProgressBar1.Name = "moCircularProgressBar1";
            this.moCircularProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(18)))), ((int)(((byte)(100)))));
            this.moCircularProgressBar1.Size = new System.Drawing.Size(49, 50);
            this.moCircularProgressBar1.Status = CircularProgressBar.MoCircularProgressBar.ConnectionStatus.Connecting;
            this.moCircularProgressBar1.SuccessColor = System.Drawing.Color.Green;
            this.moCircularProgressBar1.SymbolSize = 0.5F;
            this.moCircularProgressBar1.TabIndex = 25;
            this.moCircularProgressBar1.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tattoo No1", 16F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(110)))), ((int)(((byte)(223)))));
            this.btnClose.Location = new System.Drawing.Point(447, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 23);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Tattoo No1", 20F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(110)))), ((int)(((byte)(223)))));
            this.btnMinimize.Location = new System.Drawing.Point(419, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(26, 23);
            this.btnMinimize.TabIndex = 27;
            this.btnMinimize.Text = "-";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // cbRememberMe
            // 
            this.cbRememberMe.AutoSize = true;
            this.cbRememberMe.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.cbRememberMe.ForeColor = System.Drawing.Color.White;
            this.cbRememberMe.Location = new System.Drawing.Point(207, 316);
            this.cbRememberMe.Name = "cbRememberMe";
            this.cbRememberMe.Size = new System.Drawing.Size(109, 17);
            this.cbRememberMe.TabIndex = 28;
            this.cbRememberMe.Text = "Remember Me";
            this.cbRememberMe.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Generator_Code_Presentation_Layer.Properties.Resources.Code_Generato_423_X_198_;
            this.pictureBox1.Location = new System.Drawing.Point(45, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 137);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // btnConnection
            // 
            this.btnConnection.AnimationHoverSpeed = 0.07F;
            this.btnConnection.AnimationSpeed = 0.03F;
            this.btnConnection.BackColor = System.Drawing.Color.Transparent;
            this.btnConnection.BaseColor = System.Drawing.Color.Silver;
            this.btnConnection.BorderColor = System.Drawing.Color.Black;
            this.btnConnection.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnConnection.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnConnection.CheckedForeColor = System.Drawing.Color.White;
            this.btnConnection.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnConnection.CheckedImage")));
            this.btnConnection.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnConnection.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnConnection.FocusedColor = System.Drawing.Color.Empty;
            this.btnConnection.Font = new System.Drawing.Font("Cascadia Code", 15F, System.Drawing.FontStyle.Bold);
            this.btnConnection.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnConnection.Image = null;
            this.btnConnection.ImageSize = new System.Drawing.Size(20, 20);
            this.btnConnection.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnConnection.Location = new System.Drawing.Point(182, 425);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnConnection.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnConnection.OnHoverForeColor = System.Drawing.Color.White;
            this.btnConnection.OnHoverImage = null;
            this.btnConnection.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnConnection.OnPressedColor = System.Drawing.Color.Black;
            this.btnConnection.Radius = 10;
            this.btnConnection.Size = new System.Drawing.Size(130, 42);
            this.btnConnection.TabIndex = 18;
            this.btnConnection.Text = "Connect";
            this.btnConnection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // frmConnction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(18)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(473, 490);
            this.Controls.Add(this.cbRememberMe);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.moCircularProgressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.moPanal1);
            this.Controls.Add(this.btnConnection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConnction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conection to Database";
            this.Load += new System.EventHandler(this.frmConnction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaAdvenceButton btnConnection;
        private MoPanal.MoPanal moPanal1;
        private Guna.UI.WinForms.GunaTextBox txtUserID;
        private Guna.UI.WinForms.GunaTextBox txtPassword;
        private Guna.UI.WinForms.GunaTextBox txtServer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.CheckBox cbRememberMe;
        public CircularProgressBar.MoCircularProgressBar moCircularProgressBar1;
    }
}


namespace Generator_Code_Presentation_Layer
{
    partial class frmConnnectionSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnnectionSetting));
            this.moPanal1 = new MoPanal.MoPanal();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDatabaseName = new Guna.UI.WinForms.GunaComboBox();
            this.btnSave = new Guna.UI.WinForms.GunaAdvenceButton();
            this.SuspendLayout();
            // 
            // moPanal1
            // 
            this.moPanal1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.moPanal1.Dock = System.Windows.Forms.DockStyle.Top;
            this.moPanal1.Location = new System.Drawing.Point(0, 0);
            this.moPanal1.Name = "moPanal1";
            this.moPanal1.OnMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.moPanal1.OnMouseLeaveColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.moPanal1.Size = new System.Drawing.Size(441, 32);
            this.moPanal1.TabIndex = 0;
            this.moPanal1.Text = "moPanal1";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tattoo No1", 16F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(110)))), ((int)(((byte)(223)))));
            this.btnClose.Location = new System.Drawing.Point(415, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 23);
            this.btnClose.TabIndex = 27;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 24);
            this.label1.TabIndex = 28;
            this.label1.Text = "Database Name ";
            // 
            // cbDatabaseName
            // 
            this.cbDatabaseName.BackColor = System.Drawing.Color.Transparent;
            this.cbDatabaseName.BaseColor = System.Drawing.Color.White;
            this.cbDatabaseName.BorderColor = System.Drawing.Color.Silver;
            this.cbDatabaseName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabaseName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDatabaseName.FocusedColor = System.Drawing.Color.Empty;
            this.cbDatabaseName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.cbDatabaseName.ForeColor = System.Drawing.Color.Black;
            this.cbDatabaseName.FormattingEnabled = true;
            this.cbDatabaseName.Location = new System.Drawing.Point(187, 73);
            this.cbDatabaseName.Name = "cbDatabaseName";
            this.cbDatabaseName.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cbDatabaseName.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cbDatabaseName.Radius = 5;
            this.cbDatabaseName.Size = new System.Drawing.Size(242, 30);
            this.cbDatabaseName.TabIndex = 30;
            // 
            // btnSave
            // 
            this.btnSave.AnimationHoverSpeed = 0.07F;
            this.btnSave.AnimationSpeed = 0.03F;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BaseColor = System.Drawing.Color.Silver;
            this.btnSave.BorderColor = System.Drawing.Color.Black;
            this.btnSave.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btnSave.CheckedBorderColor = System.Drawing.Color.Black;
            this.btnSave.CheckedForeColor = System.Drawing.Color.White;
            this.btnSave.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btnSave.CheckedImage")));
            this.btnSave.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.FocusedColor = System.Drawing.Color.Empty;
            this.btnSave.Font = new System.Drawing.Font("Cascadia Code", 15F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnSave.Image = null;
            this.btnSave.ImageSize = new System.Drawing.Size(20, 20);
            this.btnSave.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnSave.Location = new System.Drawing.Point(155, 139);
            this.btnSave.Name = "btnSave";
            this.btnSave.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnSave.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnSave.OnHoverForeColor = System.Drawing.Color.White;
            this.btnSave.OnHoverImage = null;
            this.btnSave.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.btnSave.OnPressedColor = System.Drawing.Color.Black;
            this.btnSave.Radius = 10;
            this.btnSave.Size = new System.Drawing.Size(130, 42);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmConnnectionSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 193);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbDatabaseName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.moPanal1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConnnectionSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmConnnectionSetting";
            this.Load += new System.EventHandler(this.frmConnnectionSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MoPanal.MoPanal moPanal1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaComboBox cbDatabaseName;
        private Guna.UI.WinForms.GunaAdvenceButton btnSave;
    }
}
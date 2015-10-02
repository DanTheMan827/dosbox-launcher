namespace DOSBox_Launcher
{
    partial class frmNew
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtLauncherName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.chkExit = new System.Windows.Forms.CheckBox();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(184, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(176, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(175, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.btnOK, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.btnCancel, 0, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(12, 84);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(363, 30);
            this.TableLayoutPanel1.TabIndex = 8;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(27, 41);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(105, 13);
            this.Label2.TabIndex = 13;
            this.Label2.Text = "Launcher File Name:";
            // 
            // txtLauncherName
            // 
            this.txtLauncherName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLauncherName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLauncherName.Location = new System.Drawing.Point(138, 38);
            this.txtLauncherName.Name = "txtLauncherName";
            this.txtLauncherName.Size = new System.Drawing.Size(237, 20);
            this.txtLauncherName.TabIndex = 12;
            this.txtLauncherName.TextChanged += new System.EventHandler(this.txtLauncherName_TextChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(120, 13);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "File to open in DOSBox:";
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFilename.Location = new System.Drawing.Point(138, 12);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(237, 20);
            this.txtFilename.TabIndex = 10;
            this.txtFilename.Click += new System.EventHandler(this.txtFilename_Click);
            // 
            // chkExit
            // 
            this.chkExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkExit.AutoSize = true;
            this.chkExit.Location = new System.Drawing.Point(12, 64);
            this.chkExit.Name = "chkExit";
            this.chkExit.Size = new System.Drawing.Size(155, 17);
            this.chkExit.TabIndex = 9;
            this.chkExit.Text = "Exit DOSBox when finished";
            this.chkExit.UseVisualStyleBackColor = true;
            // 
            // frmNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 126);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtLauncherName);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.chkExit);
            this.Icon = global::DOSBox_Launcher.Properties.Resources.DOSBoxIcon;
            this.MaximumSize = new System.Drawing.Size(99999, 165);
            this.MinimumSize = new System.Drawing.Size(368, 139);
            this.Name = "frmNew";
            this.Text = "New DOSBox Launcher Configuration File";
            this.Load += new System.EventHandler(this.frmNew_Load);
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtLauncherName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtFilename;
        internal System.Windows.Forms.CheckBox chkExit;
    }
}
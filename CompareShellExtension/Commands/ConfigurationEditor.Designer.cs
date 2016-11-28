namespace CompareShellExtension.Commands
{
    partial class ConfigurationEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationEditor));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.fileComparisonToolGroupBox = new System.Windows.Forms.GroupBox();
            this.fileComparisonToolExecutableBrowseButton = new System.Windows.Forms.Button();
            this.fileComparisonToolArgumentsTextBox = new System.Windows.Forms.TextBox();
            this.fileComparisonToolArgumentsLabel = new System.Windows.Forms.Label();
            this.fileComparisonToolExecutableTextBox = new System.Windows.Forms.TextBox();
            this.fileComparisonToolExecutableLabel = new System.Windows.Forms.Label();
            this.directoryComparisonToolGroupBox = new System.Windows.Forms.GroupBox();
            this.directoryComparisonToolExecutableBrowseButton = new System.Windows.Forms.Button();
            this.directoryComparisonToolArgumentsTextBox = new System.Windows.Forms.TextBox();
            this.directoryComparisonToolArgumentsLabel = new System.Windows.Forms.Label();
            this.directoryComparisonToolExecutableTextBox = new System.Windows.Forms.TextBox();
            this.directoryComparisonToolExecutableLabel = new System.Windows.Forms.Label();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.websiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.configurationOnExtendedMenuCheckBox = new System.Windows.Forms.CheckBox();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fileComparisonToolGroupBox.SuspendLayout();
            this.directoryComparisonToolGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(610, 540);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 32);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(691, 540);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 32);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // fileComparisonToolGroupBox
            // 
            this.fileComparisonToolGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileComparisonToolGroupBox.Controls.Add(this.fileComparisonToolExecutableBrowseButton);
            this.fileComparisonToolGroupBox.Controls.Add(this.fileComparisonToolArgumentsTextBox);
            this.fileComparisonToolGroupBox.Controls.Add(this.fileComparisonToolArgumentsLabel);
            this.fileComparisonToolGroupBox.Controls.Add(this.fileComparisonToolExecutableTextBox);
            this.fileComparisonToolGroupBox.Controls.Add(this.fileComparisonToolExecutableLabel);
            this.fileComparisonToolGroupBox.Location = new System.Drawing.Point(12, 12);
            this.fileComparisonToolGroupBox.Name = "fileComparisonToolGroupBox";
            this.fileComparisonToolGroupBox.Size = new System.Drawing.Size(754, 118);
            this.fileComparisonToolGroupBox.TabIndex = 0;
            this.fileComparisonToolGroupBox.TabStop = false;
            this.fileComparisonToolGroupBox.Text = "File Comparison Tool";
            // 
            // fileComparisonToolExecutableBrowseButton
            // 
            this.fileComparisonToolExecutableBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileComparisonToolExecutableBrowseButton.Location = new System.Drawing.Point(718, 34);
            this.fileComparisonToolExecutableBrowseButton.Name = "fileComparisonToolExecutableBrowseButton";
            this.fileComparisonToolExecutableBrowseButton.Size = new System.Drawing.Size(30, 26);
            this.fileComparisonToolExecutableBrowseButton.TabIndex = 2;
            this.fileComparisonToolExecutableBrowseButton.Text = "...";
            this.fileComparisonToolExecutableBrowseButton.UseVisualStyleBackColor = true;
            this.fileComparisonToolExecutableBrowseButton.Click += new System.EventHandler(this.fileComparisonToolExecutableBrowseButton_Click);
            // 
            // fileComparisonToolArgumentsTextBox
            // 
            this.fileComparisonToolArgumentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileComparisonToolArgumentsTextBox.Location = new System.Drawing.Point(115, 66);
            this.fileComparisonToolArgumentsTextBox.Name = "fileComparisonToolArgumentsTextBox";
            this.fileComparisonToolArgumentsTextBox.Size = new System.Drawing.Size(633, 26);
            this.fileComparisonToolArgumentsTextBox.TabIndex = 4;
            // 
            // fileComparisonToolArgumentsLabel
            // 
            this.fileComparisonToolArgumentsLabel.AutoSize = true;
            this.fileComparisonToolArgumentsLabel.Location = new System.Drawing.Point(6, 69);
            this.fileComparisonToolArgumentsLabel.Name = "fileComparisonToolArgumentsLabel";
            this.fileComparisonToolArgumentsLabel.Size = new System.Drawing.Size(87, 20);
            this.fileComparisonToolArgumentsLabel.TabIndex = 3;
            this.fileComparisonToolArgumentsLabel.Text = "Arguments";
            // 
            // fileComparisonToolExecutableTextBox
            // 
            this.fileComparisonToolExecutableTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileComparisonToolExecutableTextBox.Location = new System.Drawing.Point(115, 34);
            this.fileComparisonToolExecutableTextBox.Name = "fileComparisonToolExecutableTextBox";
            this.fileComparisonToolExecutableTextBox.Size = new System.Drawing.Size(597, 26);
            this.fileComparisonToolExecutableTextBox.TabIndex = 1;
            // 
            // fileComparisonToolExecutableLabel
            // 
            this.fileComparisonToolExecutableLabel.AutoSize = true;
            this.fileComparisonToolExecutableLabel.Location = new System.Drawing.Point(6, 37);
            this.fileComparisonToolExecutableLabel.Name = "fileComparisonToolExecutableLabel";
            this.fileComparisonToolExecutableLabel.Size = new System.Drawing.Size(88, 20);
            this.fileComparisonToolExecutableLabel.TabIndex = 0;
            this.fileComparisonToolExecutableLabel.Text = "Executable";
            // 
            // directoryComparisonToolGroupBox
            // 
            this.directoryComparisonToolGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryComparisonToolGroupBox.Controls.Add(this.directoryComparisonToolExecutableBrowseButton);
            this.directoryComparisonToolGroupBox.Controls.Add(this.directoryComparisonToolArgumentsTextBox);
            this.directoryComparisonToolGroupBox.Controls.Add(this.directoryComparisonToolArgumentsLabel);
            this.directoryComparisonToolGroupBox.Controls.Add(this.directoryComparisonToolExecutableTextBox);
            this.directoryComparisonToolGroupBox.Controls.Add(this.directoryComparisonToolExecutableLabel);
            this.directoryComparisonToolGroupBox.Location = new System.Drawing.Point(12, 136);
            this.directoryComparisonToolGroupBox.Name = "directoryComparisonToolGroupBox";
            this.directoryComparisonToolGroupBox.Size = new System.Drawing.Size(754, 118);
            this.directoryComparisonToolGroupBox.TabIndex = 1;
            this.directoryComparisonToolGroupBox.TabStop = false;
            this.directoryComparisonToolGroupBox.Text = "Directory Comparison Tool";
            // 
            // directoryComparisonToolExecutableBrowseButton
            // 
            this.directoryComparisonToolExecutableBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryComparisonToolExecutableBrowseButton.Location = new System.Drawing.Point(718, 34);
            this.directoryComparisonToolExecutableBrowseButton.Name = "directoryComparisonToolExecutableBrowseButton";
            this.directoryComparisonToolExecutableBrowseButton.Size = new System.Drawing.Size(30, 26);
            this.directoryComparisonToolExecutableBrowseButton.TabIndex = 2;
            this.directoryComparisonToolExecutableBrowseButton.Text = "...";
            this.directoryComparisonToolExecutableBrowseButton.UseVisualStyleBackColor = true;
            this.directoryComparisonToolExecutableBrowseButton.Click += new System.EventHandler(this.directoryComparisonToolExecutableBrowseButton_Click);
            // 
            // directoryComparisonToolArgumentsTextBox
            // 
            this.directoryComparisonToolArgumentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryComparisonToolArgumentsTextBox.Location = new System.Drawing.Point(115, 66);
            this.directoryComparisonToolArgumentsTextBox.Name = "directoryComparisonToolArgumentsTextBox";
            this.directoryComparisonToolArgumentsTextBox.Size = new System.Drawing.Size(633, 26);
            this.directoryComparisonToolArgumentsTextBox.TabIndex = 4;
            // 
            // directoryComparisonToolArgumentsLabel
            // 
            this.directoryComparisonToolArgumentsLabel.AutoSize = true;
            this.directoryComparisonToolArgumentsLabel.Location = new System.Drawing.Point(6, 69);
            this.directoryComparisonToolArgumentsLabel.Name = "directoryComparisonToolArgumentsLabel";
            this.directoryComparisonToolArgumentsLabel.Size = new System.Drawing.Size(87, 20);
            this.directoryComparisonToolArgumentsLabel.TabIndex = 3;
            this.directoryComparisonToolArgumentsLabel.Text = "Arguments";
            // 
            // directoryComparisonToolExecutableTextBox
            // 
            this.directoryComparisonToolExecutableTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryComparisonToolExecutableTextBox.Location = new System.Drawing.Point(115, 34);
            this.directoryComparisonToolExecutableTextBox.Name = "directoryComparisonToolExecutableTextBox";
            this.directoryComparisonToolExecutableTextBox.Size = new System.Drawing.Size(597, 26);
            this.directoryComparisonToolExecutableTextBox.TabIndex = 1;
            // 
            // directoryComparisonToolExecutableLabel
            // 
            this.directoryComparisonToolExecutableLabel.AutoSize = true;
            this.directoryComparisonToolExecutableLabel.Location = new System.Drawing.Point(6, 37);
            this.directoryComparisonToolExecutableLabel.Name = "directoryComparisonToolExecutableLabel";
            this.directoryComparisonToolExecutableLabel.Size = new System.Drawing.Size(88, 20);
            this.directoryComparisonToolExecutableLabel.TabIndex = 0;
            this.directoryComparisonToolExecutableLabel.Text = "Executable";
            // 
            // infoTextBox
            // 
            this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoTextBox.Location = new System.Drawing.Point(12, 260);
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.ReadOnly = true;
            this.infoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.infoTextBox.Size = new System.Drawing.Size(754, 194);
            this.infoTextBox.TabIndex = 2;
            this.infoTextBox.Text = resources.GetString("infoTextBox.Text");
            // 
            // websiteLinkLabel
            // 
            this.websiteLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.websiteLinkLabel.AutoSize = true;
            this.websiteLinkLabel.Location = new System.Drawing.Point(12, 550);
            this.websiteLinkLabel.Name = "websiteLinkLabel";
            this.websiteLinkLabel.Size = new System.Drawing.Size(135, 20);
            this.websiteLinkLabel.TabIndex = 3;
            this.websiteLinkLabel.TabStop = true;
            this.websiteLinkLabel.Text = "http://example.org";
            this.websiteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.websiteLinkLabel_LinkClicked);
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsGroupBox.Controls.Add(this.configurationOnExtendedMenuCheckBox);
            this.optionsGroupBox.Location = new System.Drawing.Point(12, 460);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(754, 74);
            this.optionsGroupBox.TabIndex = 6;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // configurationOnExtendedMenuCheckBox
            // 
            this.configurationOnExtendedMenuCheckBox.AutoSize = true;
            this.configurationOnExtendedMenuCheckBox.Location = new System.Drawing.Point(10, 31);
            this.configurationOnExtendedMenuCheckBox.Name = "configurationOnExtendedMenuCheckBox";
            this.configurationOnExtendedMenuCheckBox.Size = new System.Drawing.Size(599, 24);
            this.configurationOnExtendedMenuCheckBox.TabIndex = 0;
            this.configurationOnExtendedMenuCheckBox.Text = "Place the configuration menu on the \"extended\" (shift + right-click) context menu" +
    "";
            this.mainToolTip.SetToolTip(this.configurationOnExtendedMenuCheckBox, "Makes the menu item to access this configuration screen available only on the \"ex" +
        "tended\" context menu, i.e. when shift + right-clicking on a file or directory in" +
        " Windows Explorer.");
            this.configurationOnExtendedMenuCheckBox.UseVisualStyleBackColor = true;
            // 
            // mainToolTip
            // 
            this.mainToolTip.AutoPopDelay = 10000;
            this.mainToolTip.InitialDelay = 500;
            this.mainToolTip.ReshowDelay = 100;
            // 
            // ConfigurationEditor
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(778, 584);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.websiteLinkLabel);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.directoryComparisonToolGroupBox);
            this.Controls.Add(this.fileComparisonToolGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(620, 500);
            this.Name = "ConfigurationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure Comparison Tools";
            this.fileComparisonToolGroupBox.ResumeLayout(false);
            this.fileComparisonToolGroupBox.PerformLayout();
            this.directoryComparisonToolGroupBox.ResumeLayout(false);
            this.directoryComparisonToolGroupBox.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox fileComparisonToolGroupBox;
        private System.Windows.Forms.TextBox fileComparisonToolArgumentsTextBox;
        private System.Windows.Forms.Label fileComparisonToolArgumentsLabel;
        private System.Windows.Forms.TextBox fileComparisonToolExecutableTextBox;
        private System.Windows.Forms.Label fileComparisonToolExecutableLabel;
        private System.Windows.Forms.GroupBox directoryComparisonToolGroupBox;
        private System.Windows.Forms.TextBox directoryComparisonToolArgumentsTextBox;
        private System.Windows.Forms.Label directoryComparisonToolArgumentsLabel;
        private System.Windows.Forms.TextBox directoryComparisonToolExecutableTextBox;
        private System.Windows.Forms.Label directoryComparisonToolExecutableLabel;
        private System.Windows.Forms.Button fileComparisonToolExecutableBrowseButton;
        private System.Windows.Forms.Button directoryComparisonToolExecutableBrowseButton;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.LinkLabel websiteLinkLabel;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.CheckBox configurationOnExtendedMenuCheckBox;
        private System.Windows.Forms.ToolTip mainToolTip;
    }
}
using CompareShellExtension.Infrastructure;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CompareShellExtension.Commands
{
    // We use Windows Forms and totally vintage "data binding" here to keep the memory and performance
    // footprint as low as possible. Not because we don't like WPF, just to be clear :-)
    public partial class ConfigurationEditor : Form
    {
        public Configuration Configuration { get; private set; }

        public ConfigurationEditor(Configuration configuration)
        {
            this.Configuration = configuration;
            InitializeComponent();
            this.websiteLinkLabel.Text = AppConstants.WebsiteUrl;

            this.fileComparisonToolExecutableTextBox.Text = this.Configuration.FileComparisonExecutable;
            this.fileComparisonToolArgumentsTextBox.Text = this.Configuration.FileComparisonArguments;
            this.directoryComparisonToolExecutableTextBox.Text = this.Configuration.DirectoryComparisonExecutable;
            this.directoryComparisonToolArgumentsTextBox.Text = this.Configuration.DirectoryComparisonArguments;
            this.configurationOnExtendedMenuCheckBox.Checked = this.Configuration.ShowConfigurationOnlyOnExtendedContextMenu;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            this.Configuration.FileComparisonExecutable = this.fileComparisonToolExecutableTextBox.Text;
            this.Configuration.FileComparisonArguments = this.fileComparisonToolArgumentsTextBox.Text;
            this.Configuration.DirectoryComparisonExecutable = this.directoryComparisonToolExecutableTextBox.Text;
            this.Configuration.DirectoryComparisonArguments = this.directoryComparisonToolArgumentsTextBox.Text;
            this.Configuration.ShowConfigurationOnlyOnExtendedContextMenu = this.configurationOnExtendedMenuCheckBox.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void fileComparisonToolExecutableBrowseButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = this.Configuration.FileComparisonExecutable;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.fileComparisonToolExecutableTextBox.Text = dialog.FileName;
            }
        }

        private void directoryComparisonToolExecutableBrowseButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = this.Configuration.FileComparisonExecutable;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.directoryComparisonToolExecutableTextBox.Text = dialog.FileName;
            }
        }

        private void websiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(AppConstants.WebsiteUrl);
        }
    }
}
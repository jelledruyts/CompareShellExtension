namespace CompareShellExtension.Infrastructure
{
    public class Configuration
    {
        #region Properties

        public string SelectedFile { get; set; }
        public string SelectedDirectory { get; set; }
        public string FileComparisonExecutable { get; set; }
        public string FileComparisonArguments { get; set; }
        public string DirectoryComparisonExecutable { get; set; }
        public string DirectoryComparisonArguments { get; set; }
        public bool ShowConfigurationOnlyOnExtendedContextMenu { get; set; }

        #endregion

        #region Methods

        public bool IsValidForFileComparison()
        {
            return !string.IsNullOrWhiteSpace(this.FileComparisonExecutable);
        }

        public bool IsValidForDirectoryComparison()
        {
            return !string.IsNullOrWhiteSpace(this.DirectoryComparisonExecutable);
        }

        #endregion
    }
}
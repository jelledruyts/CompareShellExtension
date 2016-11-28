using Microsoft.Win32;

namespace CompareShellExtension.Infrastructure
{
    public static class ConfigurationFactory
    {
        #region Constants

        private const string PublisherRootKey = @"SOFTWARE\" + AppConstants.Publisher;
        private const string ConfigurationRootKey = PublisherRootKey + @"\" + AppConstants.Product;

        #endregion

        #region Load

        public static Configuration LoadConfiguration()
        {
            var configuration = new Configuration();
            using (var rootKey = Registry.CurrentUser.OpenSubKey(ConfigurationRootKey))
            {
                if (rootKey != null)
                {
                    configuration.SelectedFile = (string)rootKey.GetValue(nameof(configuration.SelectedFile));
                    configuration.SelectedDirectory = (string)rootKey.GetValue(nameof(configuration.SelectedDirectory));
                    configuration.FileComparisonExecutable = (string)rootKey.GetValue(nameof(configuration.FileComparisonExecutable));
                    configuration.FileComparisonArguments = (string)rootKey.GetValue(nameof(configuration.FileComparisonArguments));
                    configuration.DirectoryComparisonExecutable = (string)rootKey.GetValue(nameof(configuration.DirectoryComparisonExecutable));
                    configuration.DirectoryComparisonArguments = (string)rootKey.GetValue(nameof(configuration.DirectoryComparisonArguments));
                    var showConfigurationOnlyOnExtendedContextMenu = false;
                    if (!bool.TryParse((string)rootKey.GetValue(nameof(configuration.ShowConfigurationOnlyOnExtendedContextMenu)), out showConfigurationOnlyOnExtendedContextMenu))
                    {
                        showConfigurationOnlyOnExtendedContextMenu = false;
                    }
                    configuration.ShowConfigurationOnlyOnExtendedContextMenu = showConfigurationOnlyOnExtendedContextMenu;
                }
            }
            return configuration;
        }

        #endregion

        #region Save

        public static void SaveConfiguration(Configuration configuration, bool log)
        {
            if (configuration != null)
            {
                using (var rootKey = Registry.CurrentUser.CreateSubKey(ConfigurationRootKey))
                {
                    rootKey.SetValue(nameof(configuration.SelectedFile), configuration.SelectedFile ?? string.Empty);
                    rootKey.SetValue(nameof(configuration.SelectedDirectory), configuration.SelectedDirectory ?? string.Empty);
                    rootKey.SetValue(nameof(configuration.FileComparisonExecutable), configuration.FileComparisonExecutable ?? string.Empty);
                    rootKey.SetValue(nameof(configuration.FileComparisonArguments), configuration.FileComparisonArguments ?? string.Empty);
                    rootKey.SetValue(nameof(configuration.DirectoryComparisonExecutable), configuration.DirectoryComparisonExecutable ?? string.Empty);
                    rootKey.SetValue(nameof(configuration.DirectoryComparisonArguments), configuration.DirectoryComparisonArguments ?? string.Empty);
                    rootKey.SetValue(nameof(configuration.ShowConfigurationOnlyOnExtendedContextMenu), configuration.ShowConfigurationOnlyOnExtendedContextMenu.ToString());
                    if (log)
                    {
                        Logger.LogInformation($"Saved configuration to registry key \"{rootKey.Name}\"");
                    }
                }
            }
        }

        #endregion

        #region Remove

        public static void RemoveConfiguration()
        {
            // Delete the configuration tree from the registry.
            Registry.CurrentUser.DeleteSubKeyTree(ConfigurationRootKey, false);

            // Also delete the parent publisher key if it's now empty.
            using (var publisherKey = Registry.CurrentUser.OpenSubKey(PublisherRootKey))
            {
                if (publisherKey != null && publisherKey.SubKeyCount == 0)
                {
                    Registry.CurrentUser.DeleteSubKey(PublisherRootKey);
                }
            }
            Logger.LogInformation("Removed configuration from registry.");
        }

        #endregion
    }
}
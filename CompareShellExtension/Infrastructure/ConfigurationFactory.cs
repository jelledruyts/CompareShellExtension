using Microsoft.Win32;

namespace CompareShellExtension.Infrastructure
{
    public static class ConfigurationFactory
    {
        #region Constants

        private const string PublisherRootKey = @"SOFTWARE\" + AppConstants.Publisher;
        private const string ConfigurationRootKey = PublisherRootKey + @"\" + AppConstants.Product;

        #endregion

        #region Singleton Instance

        private static Configuration current;
        public static Configuration Current
        {
            get
            {
                if (current == null)
                {
                    ReloadConfiguration();
                }
                return current;
            }
        }

        #endregion

        #region Reload

        public static void ReloadConfiguration()
        {
            if (current == null)
            {
                current = new Configuration();
            }
            using (var rootKey = Registry.CurrentUser.OpenSubKey(ConfigurationRootKey))
            {
                if (rootKey != null)
                {
                    current.FileComparisonExecutable = (string)rootKey.GetValue(nameof(current.FileComparisonExecutable));
                    current.FileComparisonArguments = (string)rootKey.GetValue(nameof(current.FileComparisonArguments));
                    current.DirectoryComparisonExecutable = (string)rootKey.GetValue(nameof(current.DirectoryComparisonExecutable));
                    current.DirectoryComparisonArguments = (string)rootKey.GetValue(nameof(current.DirectoryComparisonArguments));
                    var showConfigurationOnlyOnExtendedContextMenu = false;
                    if (!bool.TryParse((string)rootKey.GetValue(nameof(current.ShowConfigurationOnlyOnExtendedContextMenu)), out showConfigurationOnlyOnExtendedContextMenu))
                    {
                        showConfigurationOnlyOnExtendedContextMenu = false;
                    }
                    current.ShowConfigurationOnlyOnExtendedContextMenu = showConfigurationOnlyOnExtendedContextMenu;
                }
            }
        }

        #endregion

        #region Save

        public static void SaveConfiguration(Configuration configuration)
        {
            if (configuration != null)
            {
                using (var rootKey = Registry.CurrentUser.CreateSubKey(ConfigurationRootKey))
                {
                    // Note: the selected file and directory are not currently persisted.
                    rootKey.SetValue(nameof(configuration.FileComparisonExecutable), configuration.FileComparisonExecutable);
                    rootKey.SetValue(nameof(configuration.FileComparisonArguments), configuration.FileComparisonArguments);
                    rootKey.SetValue(nameof(configuration.DirectoryComparisonExecutable), configuration.DirectoryComparisonExecutable);
                    rootKey.SetValue(nameof(configuration.DirectoryComparisonArguments), configuration.DirectoryComparisonArguments);
                    rootKey.SetValue(nameof(configuration.ShowConfigurationOnlyOnExtendedContextMenu), configuration.ShowConfigurationOnlyOnExtendedContextMenu.ToString());
                    Logger.LogInformation($"Saved configuration to registry key \"{rootKey.Name}\"");
                }
            }
            current = configuration;
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
using Microsoft.Win32;
using System;

namespace CompareShellExtension.Infrastructure
{
    internal static class Installer
    {
        // See https://msdn.microsoft.com/en-us/library/windows/desktop/cc144067(v=vs.85).aspx#predefined_shell_objects
        private const string FileType = "AllFileSystemObjects";

        public static void Register(Type shellExtensionType)
        {
            Logger.Register();
            RegisterShellContextMenuHandler(shellExtensionType.GUID, FileType, AppConstants.FriendlyName);
            Logger.LogInformation($"Registered shell extension \"{shellExtensionType.GUID}\" for file type \"{FileType}\"");
        }

        public static void Unregister(Type shellExtensionType)
        {
            UnregisterShellContextMenuHandler(shellExtensionType.GUID, FileType);
            ConfigurationFactory.RemoveConfiguration();
            Logger.LogInformation($"Unregistered shell extension \"{shellExtensionType.GUID}\" for file type \"{FileType}\"");
            Logger.Unregister();
        }

        #region Helper Methods

        /// <summary>
        /// Register the context menu handler.
        /// </summary>
        /// <param name="clsid">The CLSID of the component.</param>
        /// <param name="fileType">
        /// The file type that the context menu handler is associated with. For 
        /// example, '*' means all file types; '.txt' means all .txt files. The 
        /// parameter must not be NULL or an empty string. 
        /// </param>
        /// <param name="friendlyName">The friendly name of the component.</param>
        private static void RegisterShellContextMenuHandler(Guid clsid, string fileType, string friendlyName)
        {
            // See https://msdn.microsoft.com/en-us/library/windows/desktop/cc144067(v=vs.85).aspx#_shell_reg_shell_ext_handlers
            if (clsid == Guid.Empty)
            {
                throw new ArgumentException("clsid must not be empty");
            }
            if (string.IsNullOrEmpty(fileType))
            {
                throw new ArgumentException("fileType must not be null or empty");
            }

            // If fileType starts with '.', try to read the default value of the 
            // HKCR\<File Type> key which contains the ProgID to which the file type 
            // is linked.
            if (fileType.StartsWith("."))
            {
                using (var key = Registry.ClassesRoot.OpenSubKey(fileType))
                {
                    if (key != null)
                    {
                        // If the key exists and its default value is not empty, use 
                        // the ProgID as the file type.
                        var defaultVal = key.GetValue(null) as string;
                        if (!string.IsNullOrEmpty(defaultVal))
                        {
                            fileType = defaultVal;
                        }
                    }
                }
            }

            // Create the key HKCR\<File Type>\shellex\ContextMenuHandlers\{<CLSID>}.
            var keyName = string.Format(@"{0}\shellex\ContextMenuHandlers\{1}", fileType, clsid.ToString("B"));
            using (var key = Registry.ClassesRoot.CreateSubKey(keyName))
            {
                // Set the default value of the key.
                if (key != null && !string.IsNullOrEmpty(friendlyName))
                {
                    key.SetValue(null, friendlyName);
                }
            }
        }

        /// <summary>
        /// Unregister the context menu handler.
        /// </summary>
        /// <param name="clsid">The CLSID of the component.</param>
        /// <param name="fileType">
        /// The file type that the context menu handler is associated with. For 
        /// example, '*' means all file types; '.txt' means all .txt files. The 
        /// parameter must not be NULL or an empty string. 
        /// </param>
        private static void UnregisterShellContextMenuHandler(Guid clsid, string fileType)
        {
            if (clsid == null)
            {
                throw new ArgumentException("clsid must not be null");
            }
            if (string.IsNullOrEmpty(fileType))
            {
                throw new ArgumentException("fileType must not be null or empty");
            }

            // If fileType starts with '.', try to read the default value of the 
            // HKCR\<File Type> key which contains the ProgID to which the file type 
            // is linked.
            if (fileType.StartsWith("."))
            {
                using (var key = Registry.ClassesRoot.OpenSubKey(fileType))
                {
                    if (key != null)
                    {
                        // If the key exists and its default value is not empty, use 
                        // the ProgID as the file type.
                        var defaultVal = key.GetValue(null) as string;
                        if (!string.IsNullOrEmpty(defaultVal))
                        {
                            fileType = defaultVal;
                        }
                    }
                }
            }

            // Remove the key HKCR\<File Type>\shellex\ContextMenuHandlers\{<CLSID>}.
            var keyName = string.Format(@"{0}\shellex\ContextMenuHandlers\{1}", fileType, clsid.ToString("B"));
            Registry.ClassesRoot.DeleteSubKeyTree(keyName, false);
        }

        #endregion
    }
}
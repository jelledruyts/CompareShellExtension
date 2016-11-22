using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompareShellExtension
{
    public class CommandContext
    {
        #region Fields

        private IList<string> selectedFiles;
        private IList<string> selectedDirectories;

        #endregion

        #region Properties

        public IList<string> SelectedShellItems { get; }
        public bool IsExtendedContextMenu { get; }

        #endregion

        #region Constructors

        public CommandContext(IList<string> selectedShellItems, bool isExtendedContextMenu)
        {
            this.SelectedShellItems = selectedShellItems ?? new string[0];
            this.IsExtendedContextMenu = isExtendedContextMenu;
        }

        #endregion

        #region Methods

        public IList<string> GetSelectedFiles()
        {
            if (this.selectedFiles == null)
            {
                this.selectedFiles = this.SelectedShellItems.Where(s => File.Exists(s)).ToArray();
            }
            return this.selectedFiles;
        }

        public IList<string> GetSelectedDirectories()
        {
            if (this.selectedDirectories == null)
            {
                this.selectedDirectories = this.SelectedShellItems.Where(s => Directory.Exists(s)).ToArray();
            }
            return this.selectedDirectories;
        }

        #endregion
    }
}
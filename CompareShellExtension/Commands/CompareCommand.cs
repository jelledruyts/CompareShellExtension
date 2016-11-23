using CompareShellExtension.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CompareShellExtension.Commands
{
    public class CompareCommand : ICommand
    {
        public string Name => "Compare";
        public string HelpText => "Compares files or directories";

        public CommandState GetState(CommandContext context)
        {
            var configuration = ConfigurationFactory.Current;
            var selectedFiles = context.GetSelectedFiles();
            var selectedDirectories = context.GetSelectedDirectories();

            // Don't allow mixing files and directories.
            if (selectedFiles.Any() && selectedDirectories.Any())
            {
                return null;
            }

            // Check if file comparison is possible.
            if (selectedFiles.Any())
            {
                if (selectedFiles.Count == 2)
                {
                    // Two files are selected in the shell, compare them.
                    return new CommandState("Compare files...", configuration.IsValidForFileComparison());
                }
                if (selectedFiles.Count == 1 && !string.IsNullOrWhiteSpace(configuration.SelectedFile))
                {
                    // A file was selected before and another file is now selected in the shell, compare them.
                    var shortFileName = Path.GetFileName(configuration.SelectedFile);
                    return new CommandState($"Compare to \"{shortFileName}\"...", configuration.IsValidForFileComparison());
                }
            }

            // Check if directory comparison is possible.
            if (selectedDirectories.Any())
            {
                if (selectedDirectories.Count == 2)
                {
                    // Two directories are selected in the shell, compare them.
                    return new CommandState("Compare directories...", configuration.IsValidForDirectoryComparison());
                }
                if (selectedDirectories.Count == 1 && !string.IsNullOrWhiteSpace(configuration.SelectedDirectory))
                {
                    // A directory was selected before and another directory is now selected in the shell, compare them.
                    var shortDirectoryName = Path.GetFileName(configuration.SelectedDirectory.TrimEnd(Path.DirectorySeparatorChar));
                    return new CommandState($"Compare to \"{shortDirectoryName}\"...", configuration.IsValidForDirectoryComparison());
                }
            }

            // No comparison is possible.
            return null;
        }

        public void Execute(CommandContext context)
        {
            var configuration = ConfigurationFactory.Current;
            var selectedFiles = context.GetSelectedFiles();
            var selectedDirectories = context.GetSelectedDirectories();

            // Check if file comparison is possible.
            if (selectedFiles.Any())
            {
                var fileName1 = default(string);
                var fileName2 = default(string);
                if (selectedFiles.Count == 2)
                {
                    fileName1 = selectedFiles.First();
                    fileName2 = selectedFiles.Last();
                }
                if (selectedFiles.Count == 1 && !string.IsNullOrWhiteSpace(configuration.SelectedFile))
                {
                    fileName1 = configuration.SelectedFile;
                    fileName2 = selectedFiles.Single();
                }
                if (!string.IsNullOrWhiteSpace(fileName1) && !string.IsNullOrWhiteSpace(fileName2))
                {
                    // Compare files.
                    RunComparisonTool(configuration.FileComparisonExecutable, configuration.FileComparisonArguments, fileName1, fileName2);
                }
            }

            // Check if directory comparison is possible.
            if (selectedDirectories.Any())
            {
                var directoryName1 = default(string);
                var directoryName2 = default(string);
                if (selectedDirectories.Count == 2)
                {
                    directoryName1 = selectedDirectories.First();
                    directoryName2 = selectedDirectories.Last();
                }
                if (selectedDirectories.Count == 1 && !string.IsNullOrWhiteSpace(configuration.SelectedDirectory))
                {
                    directoryName1 = configuration.SelectedDirectory;
                    directoryName2 = selectedDirectories.Single();
                }
                if (!string.IsNullOrWhiteSpace(directoryName1) && !string.IsNullOrWhiteSpace(directoryName2))
                {
                    // Compare directories.
                    RunComparisonTool(configuration.DirectoryComparisonExecutable, configuration.DirectoryComparisonArguments, directoryName1, directoryName2);
                }
            }
        }

        private static void RunComparisonTool(string executable, string arguments, string item1, string item2)
        {
            if (string.IsNullOrWhiteSpace(executable))
            {
                Logger.LogWarning("A comparison tool was requested but no executable was configured.");
            }
            else
            {
                executable = Environment.ExpandEnvironmentVariables(executable);
                if (arguments != null)
                {
                    arguments = arguments.Replace("%1", $"\"{item1}\"");
                    arguments = arguments.Replace("%2", $"\"{item2}\"");
                    arguments = Environment.ExpandEnvironmentVariables(arguments);
                }
                Logger.LogInformation($"Running comparison tool with command line \"{executable}\" {arguments}");
                try
                {
                    using (var process = Process.Start(executable, arguments))
                    {
                    }
                }
                catch (Exception exc)
                {
                    Logger.LogError($"Error running comparison tool with command line \"{executable}\" {arguments}: {exc.ToString()}");
                    MessageBox.Show($"The comparison tool could not be launched: {exc.Message}. More details can be found in the Application event log." + Environment.NewLine +
                        "Note that if the configuration is invalid, you can change it via the \"extended\" context menu. " +
                        "Please access it via Shift + Right-Click on a file or directory in Windows Explorer.",
                        "Cannot Launch Comparison Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
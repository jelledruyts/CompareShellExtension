using CompareShellExtension.Infrastructure;
using System.Linq;

namespace CompareShellExtension.Commands
{
    public class SelectForComparisonCommand : ICommand
    {
        public string Name => "SelectForComparison";
        public string HelpText => "Selects a file or directory for comparison";

        public CommandState GetState(CommandContext context)
        {
            var isVisible = false;

            // Only show if there is a single selected item.
            if (context.SelectedShellItems.Count == 1)
            {
                // Only show if comparison would actually be possible.
                var configuration = ConfigurationFactory.LoadConfiguration();
                if ((context.GetSelectedFiles().Any() && configuration.IsValidForFileComparison()) || (context.GetSelectedDirectories().Any() && configuration.IsValidForDirectoryComparison()))
                {
                    isVisible = true;
                }
            }
            return new CommandState("Select for comparison", isVisible);
        }

        public void Execute(CommandContext context)
        {
            var configuration = ConfigurationFactory.LoadConfiguration();
            var selectedFiles = context.GetSelectedFiles();
            if (selectedFiles.Count == 1)
            {
                configuration.SelectedFile = selectedFiles.Single();
                ConfigurationFactory.SaveConfiguration(configuration, false);
                Logger.LogInformation($"File selected for comparison: \"{configuration.SelectedFile}\"");
            }
            else
            {
                var selectedDirectories = context.GetSelectedDirectories();
                if (selectedDirectories.Count == 1)
                {
                    configuration.SelectedDirectory = selectedDirectories.Single();
                    ConfigurationFactory.SaveConfiguration(configuration, false);
                    Logger.LogInformation($"Directory selected for comparison: \"{configuration.SelectedDirectory}\"");
                }
            }
        }
    }
}
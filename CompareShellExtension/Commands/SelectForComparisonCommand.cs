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
            return new CommandState("Select for comparison", context.SelectedShellItems.Count == 1);
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
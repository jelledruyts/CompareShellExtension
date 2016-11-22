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
            var selectedFiles = context.GetSelectedFiles();
            if (selectedFiles.Count == 1)
            {
                ConfigurationFactory.Current.SelectedFile = selectedFiles.Single();
                Logger.LogInformation($"File selected for comparison: \"{ConfigurationFactory.Current.SelectedFile}\"");
            }
            else
            {
                var selectedDirectories = context.GetSelectedDirectories();
                if(selectedDirectories.Count == 1)
                {
                    ConfigurationFactory.Current.SelectedDirectory = selectedDirectories.Single();
                    Logger.LogInformation($"Directory selected for comparison: \"{ConfigurationFactory.Current.SelectedDirectory}\"");
                }
            }
        }
    }
}
using CompareShellExtension.Infrastructure;

namespace CompareShellExtension.Commands
{
    public class ClearSelectionCommand : ICommand
    {
        public string Name => "ClearSelection";
        public string HelpText => "Clears the file and directory selected for comparison";

        public CommandState GetState(CommandContext context)
        {
            var isVisible = false;

            // Only show on the extended context menu.
            if (context.IsExtendedContextMenu)
            {
                // Only show when there actually is a selected file or directory.
                var configuration = ConfigurationFactory.LoadConfiguration();
                isVisible = !string.IsNullOrWhiteSpace(configuration.SelectedFile) || !string.IsNullOrWhiteSpace(configuration.SelectedDirectory);
            }
            return new CommandState("Clear selection for comparison", isVisible);
        }

        public void Execute(CommandContext context)
        {
            var configuration = ConfigurationFactory.LoadConfiguration();
            configuration.SelectedFile = null;
            configuration.SelectedDirectory = null;
            ConfigurationFactory.SaveConfiguration(configuration, false);
            Logger.LogInformation("Cleared the file and directory selected for comparison");
        }
    }
}
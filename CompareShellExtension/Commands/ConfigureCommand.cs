using CompareShellExtension.Infrastructure;
using System.Windows.Forms;

namespace CompareShellExtension.Commands
{
    public class ConfigureCommand : ICommand
    {
        public string Name => "Configure";
        public string HelpText => "Opens the configuration screen";

        public CommandState GetState(CommandContext context)
        {
            var configuration = ConfigurationFactory.LoadConfiguration();
            var isVisible = !configuration.ShowConfigurationOnlyOnExtendedContextMenu || context.IsExtendedContextMenu;
            return new CommandState("Configure comparison tools...", isVisible);
        }

        public void Execute(CommandContext context)
        {
            // Ensure to reload the current configuration from the registry (in case it has been modified manually).
            var configuration = ConfigurationFactory.LoadConfiguration();
            var editor = new ConfigurationEditor(configuration);
            var result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConfigurationFactory.SaveConfiguration(editor.Configuration, true);
            }
        }
    }
}
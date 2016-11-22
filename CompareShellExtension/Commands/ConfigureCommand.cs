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
            var configuration = ConfigurationFactory.Current;
            var isVisible = context.IsExtendedContextMenu || !configuration.IsValid();
            return new CommandState("Configure comparison tools...", isVisible);
        }

        public void Execute(CommandContext context)
        {
            // Ensure to reload the current configuration from the registry (in case it has been modified manually).
            ConfigurationFactory.ReloadConfiguration();
            var editor = new ConfigurationEditor(ConfigurationFactory.Current);
            var result = editor.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConfigurationFactory.SaveConfiguration(editor.Configuration);
            }
        }
    }
}
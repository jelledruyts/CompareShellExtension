using CompareShellExtension.Infrastructure;

namespace CompareShellExtension.Commands
{
    public class DummyCommand : ICommand
    {
        private int id;
        public DummyCommand(int id)
        {
            this.id = id;
        }

        public string Name => "Dummy Command " + this.id;
        public string HelpText => "Dummy Help Text";

        public CommandState GetState(CommandContext context)
        {
            return new CommandState(this.Name);
        }

        public void Execute(CommandContext context)
        {
            Logger.LogInformation($"DummyCommand[{this.id}].Execute: " + string.Join(";", context.SelectedShellItems));
        }
    }
}
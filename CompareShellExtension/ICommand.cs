namespace CompareShellExtension
{
    public interface ICommand
    {
        string Name { get; }
        string HelpText { get; }
        CommandState GetState(CommandContext context);
        void Execute(CommandContext context);
    }
}
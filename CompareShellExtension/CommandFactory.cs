using CompareShellExtension.Commands;
using System.Collections.Generic;

namespace CompareShellExtension
{
    internal static class CommandFactory
    {
        private static IList<ICommand> availableCommands;

        static CommandFactory()
        {
            availableCommands = new List<ICommand>();
            availableCommands.Add(new SelectForComparisonCommand());
            availableCommands.Add(new CompareCommand());
            availableCommands.Add(new ClearSelectionCommand());
            availableCommands.Add(new ConfigureCommand());
        }

        public static IList<ICommand> GetAvailableCommands()
        {
            return availableCommands;
        }
    }
}
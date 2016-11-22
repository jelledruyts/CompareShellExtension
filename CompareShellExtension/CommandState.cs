namespace CompareShellExtension
{
    public class CommandState
    {
        public string MenuText { get; }
        public bool IsVisible { get; }
        public bool IsEnabled { get; }
        public bool IsChecked { get; }

        public CommandState(string menuText)
            : this(menuText, true)
        {
        }

        public CommandState(string menuText, bool isVisible)
            : this(menuText, isVisible, true, false)
        {
        }

        public CommandState(string menuText, bool isVisible, bool isEnabled, bool isChecked)
        {
            this.MenuText = menuText;
            this.IsVisible = isVisible;
            this.IsEnabled = isEnabled;
            this.IsChecked = isChecked;
        }
    }
}
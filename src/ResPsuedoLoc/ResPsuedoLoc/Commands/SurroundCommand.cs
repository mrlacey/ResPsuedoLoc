using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class SurroundCommand : BaseCommand
    {
        public const int CommandId = 4128;

        private SurroundCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        public static SurroundCommand Instance
        {
            get;
            private set;
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in SurroundCommand's constructor requires
            // the UI thread.
            ThreadHelper.ThrowIfNotOnUIThread();

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new SurroundCommand(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ForEachStringResourceEntry(SurroundLogic);
        }

        public static string SurroundLogic(string input)
        {
            const string surroundStart = "[! ";
            const string surroundEnd = " !]";

            if (input.StartsWith(surroundStart) && input.EndsWith(surroundEnd))
            {
                return input.TrimPrefix(surroundStart).TrimSuffix(surroundEnd);
            }
            else
            {
                return $"{surroundStart}{input}{surroundEnd}";
            }
        }
    }
}

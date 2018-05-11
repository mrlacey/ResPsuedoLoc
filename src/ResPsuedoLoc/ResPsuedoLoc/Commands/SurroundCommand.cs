using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    internal sealed class SurroundCommand : BaseCommand
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

            //  var fileContents = File.ReadAllText(this.SelectedFileName);

            var xdoc = new XmlDocument();
            xdoc.Load(this.SelectedFileName);

            const string surroundStart = "[! ";
            const string surroundEnd = " !]";

            foreach (XmlElement element in xdoc.GetElementsByTagName("data"))
            {
                System.Diagnostics.Debug.WriteLine(element);

                var valueElement = element.GetElementsByTagName("value").Item(0);

                var currentText = valueElement.InnerText;

                string newText;

                if (currentText.StartsWith(surroundStart) && currentText.EndsWith(surroundEnd))
                {
                    newText = currentText.TrimPrefix(surroundStart).TrimSuffix(surroundEnd);
                }
                else
                {
                    newText = $"{surroundStart}{currentText}{surroundEnd}";
                }

                valueElement.InnerText = newText;
            }

            xdoc.Save(this.SelectedFileName);
        }
    }
}

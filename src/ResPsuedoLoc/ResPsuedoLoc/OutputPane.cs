// <copyright file="OutputPane.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ResPsuedoLoc
{
    public class OutputPane
    {
        private static Guid rplPaneGuid = new Guid("ACC5B167-EB63-465B-8BCE-BFF0CC308009");

        private static OutputPane instance;

        private readonly IVsOutputWindowPane pane;

        private OutputPane()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (ServiceProvider.GlobalProvider.GetService(typeof(SVsOutputWindow)) is IVsOutputWindow outWindow
             && (ErrorHandler.Failed(outWindow.GetPane(ref rplPaneGuid, out this.pane)) || this.pane == null))
            {
                if (ErrorHandler.Failed(outWindow.CreatePane(ref rplPaneGuid, Vsix.Name, 1, 0)))
                {
                    System.Diagnostics.Debug.WriteLine("Failed to create the Output window pane.");
                    return;
                }

                if (ErrorHandler.Failed(outWindow.GetPane(ref rplPaneGuid, out this.pane)) || (this.pane == null))
                {
                    System.Diagnostics.Debug.WriteLine("Failed to get access to the Output window pane.");
                }
            }
        }

        public static OutputPane Instance => instance ?? (instance = new OutputPane());

        public void Activate()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.pane?.Activate();
        }

        public void WriteLine(string message)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.pane?.OutputStringThreadSafe($"{message}{Environment.NewLine}");
        }
    }
}

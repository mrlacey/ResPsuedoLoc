// <copyright file="BaseCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ResPsuedoLoc.Commands
{
    public class BaseCommand
    {
#pragma warning disable SA1401 // Fields must be private
        protected readonly AsyncPackage package;
#pragma warning restore SA1401 // Fields must be private

        public BaseCommand(AsyncPackage package)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
        }

        protected BaseCommand()
        {
            // For testing use only
        }

        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public void ForEachStringResourceEntry(Func<string, string> doThis)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsMonitorSelection monitorSelection =
                    (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));

            monitorSelection.GetCurrentSelection(
                                                 out IntPtr hierarchyPointer,
                                                 out uint itemId,
                                                 out IVsMultiItemSelect multiItemSelect,
                                                 out IntPtr selectionContainerPointer);

            IVsHierarchy selectedHierarchy = Marshal.GetTypedObjectForIUnknown(
                                                 hierarchyPointer,
                                                 typeof(IVsHierarchy)) as IVsHierarchy;

            ((IVsProject)selectedHierarchy).GetMkDocument(itemId, out string itemFullPath);

            var xdoc = new XmlDocument();
            xdoc.Load(itemFullPath);

            foreach (XmlElement element in xdoc.GetElementsByTagName("data"))
            {
                System.Diagnostics.Debug.WriteLine(element);

                var valueElement = element.GetElementsByTagName("value").Item(0);

                valueElement.InnerText = doThis(valueElement.InnerText);
            }

            xdoc.Save(itemFullPath);
        }

        protected void MenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (sender is OleMenuCommand menuCmd)
            {
                menuCmd.Visible = menuCmd.Enabled = false;

                if (!this.IsSingleProjectItemSelection(out var hierarchy, out var itemId))
                {
                    return;
                }

                ((IVsProject)hierarchy).GetMkDocument(itemId, out var itemFullPath);
                var transformFileInfo = new FileInfo(itemFullPath);

                var fileName = transformFileInfo.Name.ToLowerInvariant();

                if (fileName.EndsWith(".resx") || fileName.EndsWith(".resw"))
                {
                    menuCmd.Visible = menuCmd.Enabled = true;
                }
            }
        }

        private bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemId)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            hierarchy = null;
            itemId = VSConstants.VSITEMID_NIL;

            var solution = Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
#pragma warning disable SA1119 // Statement must not use unnecessary parenthesis
            if (!(Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SVsShellMonitorSelection)) is IVsMonitorSelection monitorSelection) || solution == null)
#pragma warning restore SA1119 // Statement must not use unnecessary parenthesis
            {
                return false;
            }

            var hierarchyPtr = IntPtr.Zero;
            var selectionContainerPtr = IntPtr.Zero;

            try
            {
                var hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemId, out var multiItemSelect, out selectionContainerPtr);

                if (ErrorHandler.Failed(hr) || hierarchyPtr == IntPtr.Zero || itemId == VSConstants.VSITEMID_NIL)
                {
                    // there is no selection
                    return false;
                }

                if (multiItemSelect != null)
                {
                    // multiple items are selected
                    return false;
                }

                if (itemId == VSConstants.VSITEMID_ROOT)
                {
                    // there is a hierarchy root node selected, thus it is not a single item inside a project
                    return false;
                }

                hierarchy = Marshal.GetObjectForIUnknown(hierarchyPtr) as IVsHierarchy;
                if (hierarchy == null)
                {
                    return false;
                }

                if (ErrorHandler.Failed(solution.GetGuidOfProject(hierarchy, out var _)))
                {
                    // hierarchy is not a project inside the Solution if it does not have a ProjectID Guid
                    return false;
                }

                // if we got this far then there is a single project item selected
                return true;
            }
            finally
            {
                if (selectionContainerPtr != IntPtr.Zero)
                {
                    Marshal.Release(selectionContainerPtr);
                }

                if (hierarchyPtr != IntPtr.Zero)
                {
                    Marshal.Release(hierarchyPtr);
                }
            }
        }
    }
}

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

        protected string SelectedFileName { get; set; }

        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public void ForEachStringResourceEntry(Func<string, string> doThis)
        {
            var xdoc = new XmlDocument();
            xdoc.Load(this.SelectedFileName);

            foreach (XmlElement element in xdoc.GetElementsByTagName("data"))
            {
                System.Diagnostics.Debug.WriteLine(element);

                var valueElement = element.GetElementsByTagName("value").Item(0);

                valueElement.InnerText = doThis(valueElement.InnerText);
            }

            xdoc.Save(this.SelectedFileName);
        }

        protected void MenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            try
            {
                if (sender is OleMenuCommand menuCmd)
                {
                    menuCmd.Visible = menuCmd.Enabled = false;

                    if (!this.IsSingleProjectItemSelection(out var hierarchy, out var itemid))
                    {
                        this.SelectedFileName = null;
                        return;
                    }

                    ((IVsProject)hierarchy).GetMkDocument(itemid, out var itemFullPath);
                    var transformFileInfo = new FileInfo(itemFullPath);

                    // Save the name of the selected file so we whave it when the command is executed
                    this.SelectedFileName = transformFileInfo.FullName;

                    if (transformFileInfo.Name.EndsWith(".resw")
                     || transformFileInfo.Name.EndsWith(".resx"))
                    {
                        menuCmd.Visible = menuCmd.Enabled = true;
                    }
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc);
                throw;
            }
        }

        protected bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
        {
            hierarchy = null;
            itemid = VSConstants.VSITEMID_NIL;

            var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
            var monitorSelection = Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
            if (monitorSelection == null || solution == null)
            {
                return false;
            }

            var hierarchyPtr = IntPtr.Zero;
            var selectionContainerPtr = IntPtr.Zero;

            try
            {
                var hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemid, out var multiItemSelect, out selectionContainerPtr);

                if (ErrorHandler.Failed(hr) || hierarchyPtr == IntPtr.Zero || itemid == VSConstants.VSITEMID_NIL)
                {
                    // there is no selection
                    return false;
                }

                if (multiItemSelect != null)
                {
                    // multiple items are selected
                    return false;
                }

                if (itemid == VSConstants.VSITEMID_ROOT)
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

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
    }
}

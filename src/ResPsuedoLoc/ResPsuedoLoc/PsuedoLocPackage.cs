// <copyright file="PsuedoLocPackage.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using ResPsuedoLoc.Commands;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc
{
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionHasMultipleProjects, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids.SolutionHasSingleProject, PackageAutoLoadFlags.BackgroundLoad)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "3.4", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(typeof(OptionsGrid), "Resource Pseudo-Localizer", "General", 0, 0, true)]
    [Guid(PsuedoLocPackage.PackageGuidString)]
    [ProvideUIContextRule(UiContextSupportedFiles, name: "Supported Files", expression: "RESX | RESW", termNames: new[] { "resx", "resw" }, termValues: new[] { "HierSingleSelectionName:.resx$", "HierSingleSelectionName:.resw$" })]
    public sealed class PsuedoLocPackage : AsyncPackage
    {
        public const string PackageGuidString = "13097f34-2ebd-4ccc-bb05-bafad28a5c3b";

        public const string UiContextSupportedFiles = "c9323aea-d755-4630-bb85-c1938577ce26";

        public static readonly Guid CommandSet = new Guid("ea9955ee-bcbf-4e98-a1a2-3ff72fe7746a");

        public PsuedoLocPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        public OptionsGrid Options
        {
            get
            {
                return (OptionsGrid)this.GetDialogPage(typeof(OptionsGrid));
            }
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await SurroundCommand.InitializeAsync(this);
            await ReverseCommand.InitializeAsync(this);
            await PaddingCommand.InitializeAsync(this);
            await InvertCaseCommand.InitializeAsync(this);
            await DiacriticsCommand.InitializeAsync(this);
            await DoubleCommand.InitializeAsync(this);
            await UppercaseCommand.InitializeAsync(this);
            await XxxxxCommand.InitializeAsync(this);
            await L337Command.InitializeAsync(this);
            await AlternateCaseCommand.InitializeAsync(this);

            await SponsorRequestHelper.CheckIfNeedToShowAsync();
        }
    }
}

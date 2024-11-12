// <copyright file="OptionsGrid.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace ResPsuedoLoc.Commands
{
    public class OptionsGrid : DialogPage
    {
        [DisplayName("Create Backup")]
        [Description("Create a backup file before first modifying a resource file.")]
        public bool CreateBackup { get; set; } = true;
    }
}

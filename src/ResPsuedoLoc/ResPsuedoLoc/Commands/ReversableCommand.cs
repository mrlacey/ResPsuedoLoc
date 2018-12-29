// <copyright file="ReversableCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Microsoft.VisualStudio.Shell;

namespace ResPsuedoLoc.Commands
{
    /// <summary>
    /// Commands where we can identify how modified and so get all into the same state
    /// </summary>
    public class ReversableCommand : BaseCommand
    {
        protected ReversableCommand(AsyncPackage package)
            : base(package)
        {
        }

        protected ReversableCommand()
            : base()
        {
            // For testing use only
        }

        public ToggleMode Mode { get; protected set; } = ToggleMode.NotSet;

        public virtual List<string> TestActingOnMultipleStrings(List<string> inputs)
        {
            return inputs;
        }

        protected ToggleMode GetToggleMode(string input)
        {
            return ToggleMode.NotSet;
        }
    }
}

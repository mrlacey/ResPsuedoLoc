// <copyright file="DoubleCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class DoubleCommand : BaseCommand
    {
        public const int CommandId = 4129;

        private DoubleCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        public static DoubleCommand Instance
        {
            get;
            private set;
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new DoubleCommand(package, commandService);
        }

        public static string DoubleLogic(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (IsDoubled(input))
            {
                return RemoveDoubling(input);
            }
            else
            {
                return Double(input);
            }
        }

        internal static bool IsDoubled(string input)
        {
            var inputJustLetters = new StringBuilder();

            foreach (var character in input)
            {
                if (char.IsLetter(character))
                {
                    inputJustLetters.Append(character);
                }
            }

            var justLetters = inputJustLetters.ToString();

            for (int i = 1; i < justLetters.Length; i += 2)
            {
                if (justLetters[i] != justLetters[i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        internal static string RemoveDoubling(string input)
        {
            var result = new StringBuilder();

            int i = 0;

            while (i < input.Length)
            {
                result.Append(input[i]);

                if (char.IsLetter(input[i]))
                {
                    i += 2;
                }
                else
                {
                    ++i;
                }
            }

            return result.ToString();
        }

        internal static string Double(string input)
        {
            var result = new StringBuilder();

            foreach (var character in input)
            {
                if (char.IsLetter(character))
                {
                    result.Append(character);
                }

                result.Append(character);
            }

            return result.ToString();
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ForEachStringResourceEntry(DoubleLogic);
        }
    }
}

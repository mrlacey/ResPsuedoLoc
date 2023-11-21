﻿// <copyright file="SponsorRequestHelper.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace ResPsuedoLoc
{
    public class SponsorRequestHelper
    {
        public static async Task CheckIfNeedToShowAsync()
        {
            if (await SponsorDetector.IsSponsorAsync())
            {
                if (new Random().Next(1, 10) == 2)
                {
                    await ShowThanksForSponsorshipMessageAsync();
                }
            }
            else
            {
                await ShowPromptForSponsorshipAsync();
            }
        }

        private static async Task ShowThanksForSponsorshipMessageAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(CancellationToken.None);

            OutputPane.Instance.WriteLine("Thank you for your sponsorship. It really helps.");
            OutputPane.Instance.WriteLine("If you have ideas for new features or suggestions for new features");
            OutputPane.Instance.WriteLine("please raise an issue at https://github.com/mrlacey/ResPsuedoLoc/issues");
            OutputPane.Instance.WriteLine(string.Empty);
        }

        private static async Task ShowPromptForSponsorshipAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(CancellationToken.None);

            OutputPane.Instance.WriteLine("Sorry to interrupt. I know your time is busy, presumably that's why you installed this extension (Resource Pseudo Localizer).");
            OutputPane.Instance.WriteLine("I'm happy that the extensions I've created have been able to help you and many others");
            OutputPane.Instance.WriteLine("but I also need to make a living, and limited paid work over the last few years has been a challenge. :(");
            OutputPane.Instance.WriteLine(string.Empty);
            OutputPane.Instance.WriteLine("Show your support by making a one-off or recurring donation at https://github.com/sponsors/mrlacey");
            OutputPane.Instance.WriteLine(string.Empty);
            OutputPane.Instance.WriteLine("If you become a sponsor, I'll tell you how to hide this message too. ;)");
            OutputPane.Instance.WriteLine(string.Empty);
            OutputPane.Instance.Activate();
        }
    }
}

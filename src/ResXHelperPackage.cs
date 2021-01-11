using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace ResXHelper
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(ResXHelperPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class ResXHelperPackage : AsyncPackage
    {
        public static DTE2 Dte;
        public const string PackageGuidString = "7f93a347-7bf3-4fb7-925c-b6237a56cbc2";

        #region Package Members

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            Dte = await GetServiceAsync(typeof(DTE)) as DTE2;
            Assumes.Present(Dte);

            Logger.Initialize(this, Vsix.Name);
            await AddResourcesCommand.InitializeAsync(this);
        }

        #endregion
    }
}

global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using ResXHelper2022.Model;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using static ResXHelper2022.OptionsProvider;

namespace ResXHelper2022
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.ResXHelper2022String)]
    [ProvideOptionPage(typeof(OptionsProvider.SettingsOptions), "ResX Helper", "Default languages", 0, 0, true, SupportsProfiles = true)]
    public sealed class ResXHelper2022Package : ToolkitPackage
    {
        public List<ResourceLanguage> DefaultLanguages
        {
            get
            {
                var page = (SettingsOptions)GetDialogPage(typeof(SettingsOptions));
                return page.DefaultLanguages;
            }
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
            //Logger.Initialize(this, Vsix.Name);
        }
    }
}
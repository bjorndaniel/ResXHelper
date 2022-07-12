using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using ResXHelper.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ResXHelper.Shared
{
    [Guid("4CE7129B-A40D-4B73-A5F9-044C2A10D12E")]
    [ComVisible(true)]
    public class CustomOptionsPage : DialogPage
    {
        private const string _collectionName = "ResXHelperSettings";

        public List<Language> DefaultLanguages { get; set; } = new List<Language>();

        protected override IWin32Window Window
        {
            get
            {
                var page = new CustomOptionsPageForm(DefaultLanguages);
                page.customOptionsPage = this;
                page.Initialize();
                return page;
            }
        }
        //From https://stackoverflow.com/questions/32751040/store-array-in-options-using-dialogpage
        public override void SaveSettingsToStorage()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            base.SaveSettingsToStorage();
            var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            if (!userSettingsStore.CollectionExists(_collectionName))
            {
                userSettingsStore.CreateCollection(_collectionName);
            }

            var json = System.Text.Json.JsonSerializer.Serialize(DefaultLanguages);
            userSettingsStore.SetString(_collectionName, nameof(DefaultLanguages), json);
        }

        public override void LoadSettingsFromStorage()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            base.LoadSettingsFromStorage();
            var settingsManager = new ShellSettingsManager(ServiceProvider.GlobalProvider);
            var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            if (!userSettingsStore.PropertyExists(_collectionName, nameof(DefaultLanguages)))
            {
                return;
            }
            DefaultLanguages = System.Text.Json.JsonSerializer.Deserialize<List<Language>>(userSettingsStore.GetString(_collectionName, nameof(DefaultLanguages)));
        }
    }
}

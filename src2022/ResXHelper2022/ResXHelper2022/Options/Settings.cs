using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ResXHelper2022
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.SettingsOptions), "ResXHelper2022", "Settings", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class SettingsOptions : DialogPage
        {

        }
    }

    public class Settings : BaseOptionModel<Settings>
    {
        [Category("ResX Helper 2022")]
        [DisplayName("Default languages")]
        [Description("An informative description.")]
        [DefaultValue(true)]
        public bool MyOption { get; set; } = true;
    }
}

namespace ResXHelper2022.Options
{
    public partial class SettingsForm : UserControl
    {
        public SettingsForm(IEnumerable<ResourceLanguage> defaultLanguages)
        {
            InitializeComponent();
            SelectedLanguages = new BindingList<ResourceLanguage>(defaultLanguages.ToList());
        }

        public void Initialize()
        {
            LoadLanguages();
            LBAllLanguages.DataSource = AllLanguages;
            LBAllLanguages.DisplayMember = nameof(ResourceLanguage.Name);
            LBSelectedLanguages.DataSource = SelectedLanguages;
            LBSelectedLanguages.DisplayMember = nameof(ResourceLanguage.Name);
        }

        public BindingList<ResourceLanguage> AllLanguages { get; set; } = new BindingList<ResourceLanguage>();

        public BindingList<ResourceLanguage> SelectedLanguages { get; set; } = new BindingList<ResourceLanguage>();

        internal SettingsOptions CustomOptionsPage { get; set; }

        private void LoadLanguages()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "ResXHelper2022.Resources.languages.json";
            var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            var list = reader.ReadToEnd();
            var result = JsonSerializer.Deserialize<List<ResourceLanguage>>(list);
            AllLanguages = new BindingList<ResourceLanguage>(result.Where(_ => !SelectedLanguages.Any(sl => sl.Code == _.Code)).ToList());
        }

        private void LBAllLanguages_DoubleClick(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem is ResourceLanguage item)
            {
                AddSelectedItem(item);
            }
        }

        private void AddSelectedItem(ResourceLanguage item)
        {
            if (item != null)
            {
                AllLanguages.Remove(item);
                SelectedLanguages.Add(item);
                LBSelectedLanguages.Refresh();
                LBAllLanguages.Refresh();
                CustomOptionsPage.DefaultLanguages = SelectedLanguages.ToList();
            }
        }

        private void RemoveSelectedItem(ResourceLanguage item)
        {
            if (item != null)
            {
                SelectedLanguages.Remove(item);
                AllLanguages.Add(item);
                LBSelectedLanguages.Refresh();
                LBAllLanguages.Refresh();
                CustomOptionsPage.DefaultLanguages = SelectedLanguages.ToList();
            }
        }

        private void LBSelectedLanguages_DoubleClick(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem is ResourceLanguage item)
            {
                RemoveSelectedItem(item);
            }
        }

        private void BtnAddOnClick(object sender, EventArgs e)
        {
            if (LBAllLanguages.SelectedItem is ResourceLanguage item)
            {
                AddSelectedItem(item);
            }
        }

        private void BtnRemoveOnClick(object sender, EventArgs e)
        {
            if (LBSelectedLanguages.SelectedItem is ResourceLanguage item)
            {
                RemoveSelectedItem(item);
            }
        }

    }
}

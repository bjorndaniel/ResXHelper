using ResXHelper.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ResXHelper
{
    public partial class CustomOptionsPageForm : UserControl
    {
        public CustomOptionsPageForm(List<Language> defaultLanguages)
        {
            InitializeComponent();
            SelectedLanguages = new BindingList<Language>(defaultLanguages);
        }

        public BindingList<Language> AllLanguages { get; set; } = new BindingList<Language>();

        public BindingList<Language> SelectedLanguages { get; set; } = new BindingList<Language>();

        internal CustomOptionsPage customOptionsPage;

        public void Initialize()
        {
            LoadLanguages();
            LBAllLanguages.DataSource = AllLanguages;
            LBAllLanguages.DisplayMember = nameof(Language.Name);
            LBSelectedLanguages.DataSource = SelectedLanguages;
            LBSelectedLanguages.DisplayMember = nameof(Language.Name);
        }

        private void LoadLanguages()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "ResXHelper.Resources.languages.json";
            var stream = assembly.GetManifestResourceStream(resourceName);
            using (var reader = new StreamReader(stream))
            {
                var list = reader.ReadToEnd();

                var result = System.Text.Json.JsonSerializer.Deserialize<List<Language>>(list);
                if (SelectedLanguages.Any())
                {
                    var bindingList = new BindingList<Language>();
                    foreach(var r in result)
                    {
                        if(!SelectedLanguages.Any(_ => _.Code == r.Code))
                        {
                            bindingList.Add(r);
                        }
                        AllLanguages = bindingList;
                    }
                }
                else
                {
                    AllLanguages = new BindingList<Language>(result);
                }
            }
        }

        private void LBAllLanguages_DoubleClick(object sender, System.EventArgs e)
        {
            var item = ((ListBox)sender).SelectedItem as Language;
            if(item != null)
            {
                AddSelectedItem(item);
            }
        }

        private void AddSelectedItem(Language item)
        {
            if (item != null)
            {
                AllLanguages.Remove(item);
                SelectedLanguages.Add(item);
                LBSelectedLanguages.Refresh();
                LBAllLanguages.Refresh();
                customOptionsPage.DefaultLanguages = SelectedLanguages.ToList();
            }
        }

        private void RemoveSelectedItem(Language item)
        {
            if (item != null)
            {
                SelectedLanguages.Remove(item);
                AllLanguages.Add(item);
                LBSelectedLanguages.Refresh();
                LBAllLanguages.Refresh();
                customOptionsPage.DefaultLanguages = SelectedLanguages.ToList();
            }
        }

        private void LBSelectedLanguages_DoubleClick(object sender, System.EventArgs e)
        {
            var item = ((ListBox)sender).SelectedItem as Language;
            if (item != null)
            {
                RemoveSelectedItem(item);
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var item = LBAllLanguages.SelectedItem as Language;
            if (item != null)
            {
                AddSelectedItem(item);
            }
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            var item = LBSelectedLanguages.SelectedItem as Language;
            if (item != null)
            {
                RemoveSelectedItem(item);
            }
        }
    }
}

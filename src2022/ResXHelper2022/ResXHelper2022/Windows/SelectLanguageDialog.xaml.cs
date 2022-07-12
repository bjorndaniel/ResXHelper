using ResXHelper2022.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ResXHelper2022
{
    /// <summary>
    /// Interaction logic for SelectLanguageDialog.xaml
    /// </summary>
    public partial class SelectLanguageDialog : Window, INotifyPropertyChanged
    {
        public SelectLanguageDialog(List<ResourceLanguage> defaultLanguages)
        {
            InitializeComponent();
            DataContext = this;
            LoadLanguages(defaultLanguages);
            LBLanguages.ItemsSource = AllLanguages;
            LBSelectedLanguages.ItemsSource = SelectedLanguages;
            LBLanguages.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Descending));
            LBSelectedLanguages.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Descending));
            ((CollectionView)CollectionViewSource.GetDefaultView(LBLanguages.ItemsSource)).Filter = LanguageFilter;

            RaisePropertyChanged(nameof(AllLanguages));
            RaisePropertyChanged(nameof(SelectedLanguages));
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

        }

        public ObservableCollection<ResourceLanguage> AllLanguages { get; set; } = new ObservableCollection<ResourceLanguage>();

        public ObservableCollection<ResourceLanguage> SelectedLanguages { get; set; } = new ObservableCollection<ResourceLanguage>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public List<string> FileNames { get; set; } = new List<string>();

        public bool CanAdd => !string.IsNullOrEmpty(TxtName.Text);

        private void LoadLanguages(List<ResourceLanguage> defaultLanguages)
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "ResXHelper2022.Resources.languages.json";
            var stream = assembly.GetManifestResourceStream(resourceName);
            using (var reader = new StreamReader(stream))
            {
                var list = reader.ReadToEnd();

                var result = System.Text.Json.JsonSerializer.Deserialize<List<ResourceLanguage>>(list);
                if (defaultLanguages.Any())
                {
                    var collection = new ObservableCollection<ResourceLanguage>();
                    foreach (var r in result)
                    {
                        if (!defaultLanguages.Any(_ => _.Code == r.Code))
                        {
                            collection.Add(r);
                        }
                    }
                    AllLanguages = collection;
                    SelectedLanguages = new ObservableCollection<ResourceLanguage>(defaultLanguages);
                }
                else
                {
                    AllLanguages = new ObservableCollection<ResourceLanguage>(result);
                }

            }

        }

        private bool LanguageFilter(object item)
        {
            if (string.IsNullOrEmpty(TxtSearch.Text))
            {
                return true;
            }
            else
            {
                return ((item as ResourceLanguage).Name.IndexOf(TxtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void BtnAddLanguage_Click(object sender, RoutedEventArgs e)
        {
            var item = LBLanguages.SelectedItem as ResourceLanguage;
            AddSelectedItem(item);
        }

        private void BtnRemoveLanguage_Click(object sender, RoutedEventArgs e)
        {
            var item = LBSelectedLanguages.SelectedItem as ResourceLanguage;
            RemoveSelectedItem(item);
        }

        private void AddSelectedItem(ResourceLanguage item)
        {
            if (item != null)
            {
                AllLanguages.Remove(item);
                SelectedLanguages.Add(item);
                RaisePropertyChanged(nameof(AllLanguages));
                RaisePropertyChanged(nameof(SelectedLanguages));
            }
        }

        private void RemoveSelectedItem(ResourceLanguage item)
        {
            if (item != null)
            {
                SelectedLanguages.Remove(item);
                AllLanguages.Add(item);
                RaisePropertyChanged(nameof(AllLanguages));
                RaisePropertyChanged(nameof(SelectedLanguages));
            }
        }

        private void BtnAddFiles_Click(object sender, RoutedEventArgs e)
        {
            var fileName = TxtName.Text;
            FileNames.Clear();
            FileNames.Add($"{fileName}.resx");
            foreach (var lang in SelectedLanguages)
            {
                FileNames.Add($"{fileName}.{lang.Code}.resx");
            }
            DialogResult = true;
            Close();
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e) => RaisePropertyChanged(nameof(CanAdd));

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(LBLanguages.ItemsSource).Refresh();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtSearch.Text = string.Empty;
        }

        private void LBLanguages_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ((ListBox)sender)?.SelectedItem as ResourceLanguage;
            AddSelectedItem(item);
        }

        private void LBSelectedLanguages_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ((ListBox)sender)?.SelectedItem as ResourceLanguage;
            RemoveSelectedItem(item);
        }
    }
}

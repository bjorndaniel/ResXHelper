using ResXHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace ResXHelper
{
    /// <summary>
    /// Interaction logic for SelectLanguageDialog.xaml
    /// </summary>
    public partial class SelectLanguageDialog : Window, INotifyPropertyChanged
    {

        public SelectLanguageDialog(string folder)
        {
            InitializeComponent();
            DataContext = this;
            LoadLanguages();
            LBLanguages.ItemsSource = AllLanguages;
            LBSelectedLanguages.ItemsSource = SelectedLanguages;
            LBLanguages.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Descending));
            LBSelectedLanguages.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Descending));
            ((CollectionView)CollectionViewSource.GetDefaultView(LBLanguages.ItemsSource)).Filter = LanguageFilter ;

            RaisePropertyChanged(nameof(AllLanguages));
            RaisePropertyChanged(nameof(SelectedLanguages));
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

        }

        public ObservableCollection<Language> AllLanguages { get; set; } = new ObservableCollection<Language>();

        public ObservableCollection<Language> SelectedLanguages { get; set; } = new ObservableCollection<Language>();

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

        private void LoadLanguages()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "ResXHelper.Resources.languages.json";
            var stream = assembly.GetManifestResourceStream(resourceName);
            using (var reader = new StreamReader(stream))
            {
                var list = reader.ReadToEnd();

                var result = System.Text.Json.JsonSerializer.Deserialize<List<Language>>(list);
                AllLanguages = new ObservableCollection<Language>(result);
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
                return ((item as Language).Name.IndexOf(TxtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0); 
            }
        }

        private void BtnAddLanguage_Click(object sender, RoutedEventArgs e)
        {
            var item = LBLanguages.SelectedItem as Language;
            if(item != null)
            {
                AllLanguages.Remove(item);
                SelectedLanguages.Add(item);
                RaisePropertyChanged(nameof(AllLanguages));
                RaisePropertyChanged(nameof(SelectedLanguages));
            }
        }

        private void BtnRemoveLanguage_Click(object sender, RoutedEventArgs e)
        {
            var item = LBSelectedLanguages.SelectedItem as Language;
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
            foreach(var lang in SelectedLanguages)  
            {
                FileNames.Add($"{fileName}.{lang.Code}.resx");
            }
            DialogResult = true;
            Close();
        }

        private void TxtName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => RaisePropertyChanged(nameof(CanAdd));

        private void TxtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(LBLanguages.ItemsSource).Refresh();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtSearch.Text = string.Empty;
        }
    }
}

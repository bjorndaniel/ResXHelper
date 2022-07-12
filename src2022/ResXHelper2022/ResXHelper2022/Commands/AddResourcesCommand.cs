namespace ResXHelper2022
{
    [Command(PackageIds.AddResourcesCommand)]
    internal sealed class AddResourcesCommand : BaseCommand<AddResourcesCommand>
    {

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var options = await Settings.GetLiveInstanceAsync();
            var languages = (Package as ResXHelper2022Package)?.DefaultLanguages ?? new List<ResourceLanguage>();
            var window = new SelectLanguageDialog(languages);
            var result = await VS.Windows.ShowDialogAsync(window);
            if (result ?? false)
            {
                var project = await VS.Solutions.GetActiveProjectAsync();
                var location = new FileInfo(project.FullPath);
                var template = ReadTemplate();
                FileInfo file = null;
                foreach (var f in window.FileNames)
                {
                    try
                    {
                        file = new FileInfo(Path.Combine(location.Directory.FullName, f));
                    }
                    catch (PathTooLongException ex)
                    {
                        System.Windows.MessageBox.Show("The file name is too long 😢", Vsix.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                        Logger.Log(ex);
                        continue;
                    }
                    var dir = file.DirectoryName;
                    PackageUtilities.EnsureOutputPath(dir);
                    if (!file.Exists)
                    {
                        try
                        {
                            using (var streamWriter = new StreamWriter(file.OpenWrite()))
                            {
                                await streamWriter.WriteAsync(template);
                            }
                            await project.AddExistingFilesAsync(file.FullName);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("The file '" + file + "' already exist.");
                    }
                }
            }
        }

        private string ReadTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "ResXHelper2022.Resources.ResxTemplate.txt";
            var stream = assembly.GetManifestResourceStream(resourceName);
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

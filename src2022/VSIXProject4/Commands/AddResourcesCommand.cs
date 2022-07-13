using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using VSIXProject4.Model;

namespace VSIXProject4
{
    [Command(PackageIds.AddResourcesCommand)]
    internal sealed class AddResourcesCommand : BaseCommand<AddResourcesCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var languages = (Package as VSIXProject4Package)?.DefaultLanguages ?? new List<ResourceLanguage>();
            var window = new SelectLanguageDialog(languages);
            var result = await VS.Windows.ShowDialogAsync(window);
            if (result ?? false)
            {
                var project = await VS.Solutions.GetActiveProjectAsync();
                var selectedFolder = await VS.Solutions.GetActiveItemAsync();
                var location = new FileInfo(project.FullPath);
                var saveDir = location.Directory.FullName;
                if (selectedFolder.Type == SolutionItemType.PhysicalFolder)
                {
                    saveDir = selectedFolder.FullPath;
                }

                var template = ReadTemplate();
                FileInfo file;
                foreach (var f in window.FileNames)
                {
                    try
                    {
                        file = new FileInfo(Path.Combine(saveDir, f));
                    }
                    catch (PathTooLongException)
                    {
                        System.Windows.MessageBox.Show("The file name is too long 😢", Vsix.Name, MessageBoxButton.OK, MessageBoxImage.Error);
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
                        catch (Exception) { }
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
            const string resourceName = "VSIXProject4.Resources.ResxTemplate.txt";
            var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}

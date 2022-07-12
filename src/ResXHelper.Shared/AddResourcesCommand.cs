//File writer code from https://github.com/madskristensen/AddAnyFile
using MadsKristensen.AddAnyFile;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
using MessageBox = System.Windows.MessageBox;
using EnvDTE;
#if VS2019
using Task = System.Threading.Tasks.Task;
#else
using Project = EnvDTE.Project;
using ResXHelper_2022;
using ResXHelperPackage = ResXHelper_2022.ResXHelper_2022Package;
#endif


namespace ResXHelper.Shared
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AddResourcesCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("d77d4996-fc82-48a4-8c0f-ce49bf2e7966");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddResourcesCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private AddResourcesCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.ExecuteAsync, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddResourcesCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in AddResourcesCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AddResourcesCommand(package, commandService);
        }



        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void ExecuteAsync(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var item = ProjectHelpers.GetSelectedItem();
            var folder = ProjectHelpers.FindFolder(item);

            if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
            {
                return;
            }

            var selectedItem = item as ProjectItem;
            var selectedProject = item as Project;
            var project = selectedItem?.ContainingProject ?? selectedProject ?? ProjectHelpers.GetActiveProject();

            if (project == null)
            {
                return;
            }

            var lang = (package as ResXHelperPackage)?.DefaultLanguages ?? new List<Model.Language>();
            var dialog = new SelectLanguageDialog(lang);
#if VS2019
            var hwnd = new IntPtr(ResXHelperPackage.Dte.MainWindow.HWnd);
#else
            var hwnd = ResXHelperPackage.Dte.MainWindow.HWnd;
#endif
            System.Windows.Window window = (System.Windows.Window)HwndSource.FromHwnd(hwnd).RootVisual;
            dialog.Owner = window;

            bool? result = dialog.ShowDialog();

            if (result ?? false)
            {
                FileInfo file = null;

                foreach (var f in dialog.FileNames)
                {
                    try
                    {
                        file = new FileInfo(Path.Combine(folder, f));
                    }
                    catch (PathTooLongException ex)
                    {
                        MessageBox.Show("The file name is too long 😢", Vsix.Name, MessageBoxButton.OK, MessageBoxImage.Error);
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
                                await streamWriter.WriteAsync(ReadTemplate());
                            }
                            ProjectItem projectItem = null;

                            if (item is ProjectItem projItem)
                            {
                                if ("{6BB5F8F0-4483-11D3-8BCF-00C04F8EC28C}" == projItem.Kind) // Constants.vsProjectItemKindVirtualFolder
                                {
                                    projectItem = projItem.ProjectItems.AddFromFile(file.FullName);
                                }
                            }
                            if (projectItem == null)
                            {
                                projectItem = project.AddFileToProject(file);
                            }
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
            const string resourceName = "ResXHelper.Resources.ResxTemplate.txt";
            var stream = assembly.GetManifestResourceStream(resourceName);
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

    }
}

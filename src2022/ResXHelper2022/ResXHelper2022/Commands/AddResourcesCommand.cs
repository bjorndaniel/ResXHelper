namespace ResXHelper2022
{
    [Command(PackageIds.AddResourcesCommand)]
    internal sealed class AddResourcesCommand : BaseCommand<AddResourcesCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var options = await Settings.GetLiveInstanceAsync();
            await VS.MessageBox.ShowWarningAsync("ResXHelper2022", "Button clicked");
        }
    }
}

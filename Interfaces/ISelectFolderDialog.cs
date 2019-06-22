namespace VSNewProjectDialogExample.Interfaces
{
    public interface ISelectFolderDialog
    {
        string SelectedFolder { get; }
        bool ShowDialog();
    }
}
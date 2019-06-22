using Microsoft.WindowsAPICodePack.Dialogs;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Dialogs
{
    public class SelectFolderDialogWinApi : ISelectFolderDialog
    {
        private readonly string _defaultDirectory;
        private readonly string _title;

        public SelectFolderDialogWinApi(string title, string defaultDirectory)
        {
            _title = title;
            _defaultDirectory = defaultDirectory;
            SelectedFolder = _defaultDirectory;
        }

        public string SelectedFolder { get; private set; }

        public bool ShowDialog()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = _defaultDirectory;
            dialog.DefaultDirectory = _defaultDirectory;
            dialog.Title = _title;

            dialog.IsFolderPicker = true;


            dialog.AddToMostRecentlyUsedList = true;
            dialog.AllowNonFileSystemItems = false;

            dialog.EnsureFileExists = true;
            dialog.EnsurePathExists = true;
            dialog.EnsureReadOnly = false;
            dialog.EnsureValidNames = true;
            dialog.Multiselect = false;
            dialog.ShowPlacesList = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedFolder = dialog.FileName;
                return true;
            }

            return false;
        }
    }
}
using System.Windows.Forms;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Dialogs
{
    public class SelectFolderDialogWinForms : ISelectFolderDialog
    {
        public SelectFolderDialogWinForms()
        {
            SelectedFolder = string.Empty;
        }

        public string SelectedFolder { get; private set; }

        public bool ShowDialog()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFolder = dialog.SelectedPath;
                return true;
            }

            return false;
        }
    }
}
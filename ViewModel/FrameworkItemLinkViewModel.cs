using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace VSNewProjectDialogExample.ViewModel
{
    public class FrameworkItemLinkViewModel : ViewModelBase
    {
        private readonly string _url;

        public FrameworkItemLinkViewModel(string title, string url)
        {
            _url = url;
            Title = title;
        }

        public string Title { get; }

        public ICommand OpenLinkCommand { get; }
    }
}
using System;
using GalaSoft.MvvmLight;

namespace VSNewProjectDialogExample.ViewModel
{
    public class FrameworkItemVersionViewModel : ViewModelBase
    {
        public FrameworkItemVersionViewModel(string frameworkName, Version frameworkVersion)
        {
            FrameworkVersion = frameworkVersion;
            FrameworkName = frameworkName;
        }

        public string FrameworkName { get; }
        public Version FrameworkVersion { get; }
    }
}
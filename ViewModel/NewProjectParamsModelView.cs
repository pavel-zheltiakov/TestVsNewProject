using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VSNewProjectDialogExample.Extensions;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class NewProjectParamsModelView : ViewModelBase
    {
        private readonly IObservableContext _observableContext;
        private readonly ISelectFolderDialog _selectFolderDialog;
       
        private bool _addToSourceControl;
        private bool _createDirectoryForSolution;
        private string _projectName;
        private string _projectPath;
        private string _solutionName;
        private ICreateProjectProperties _createProjectProperties;

        private bool _loading;
        public NewProjectParamsModelView(IObservableContext observableContext, ISelectFolderDialog selectFolderDialog)
        {
            _observableContext = observableContext;
            _selectFolderDialog = selectFolderDialog;
            MessengerInstance.Register<ICreateProjectProperties>(this, CreateProjectPropertiesChanged);
            ProjectPath = _selectFolderDialog.SelectedFolder;
            _observableContext.ItemsLoading.ObserveOnCurrentSyncronizationContext()
                .Subscribe(loading =>
                {
                    _loading = loading;
                    RaisePropertyChanged(() => IsEditEnabled);
                }
                );

            _createProjectProperties = null;

            BrowseProjectPathCommand = new RelayCommand(() =>
            {
                if (_selectFolderDialog.ShowDialog())
                {
                    ProjectPath = _selectFolderDialog.SelectedFolder;
                }
            });
        }


        public bool IsEditEnabled
        {
            get { return !_loading && _createProjectProperties != null; }
        }

        public string ProjectName
        {
            get { return _projectName; }
            set { Set(() => ProjectName, ref _projectName, value); }
        }

        public string ProjectPath
        {
            get { return _projectPath; }
            set { Set(() => ProjectPath, ref _projectPath, value); }
        }

        public string SolutionName
        {
            get { return _solutionName; }
            set { Set(() => SolutionName, ref _solutionName, value); }
        }

        public ICommand BrowseProjectPathCommand { get; }

        public bool AddToSourceControl
        {
            get { return _addToSourceControl; }
            set { Set(() => AddToSourceControl, ref _addToSourceControl, value); }
        }

        public bool CreateDirectoryForSolution
        {
            get { return _createDirectoryForSolution; }
            set
            {
                if (Set(() => CreateDirectoryForSolution, ref _createDirectoryForSolution, value))
                    RaisePropertyChanged(() => CreateDirectoryForSolution);
            }
        }

        public bool CanEditSolutionName
        {
            get { return CreateDirectoryForSolution; }
        }

        private void CreateProjectPropertiesChanged(ICreateProjectProperties obj)
        {
            _createProjectProperties = obj;
            if (obj != null)
            {
                SolutionName = obj.DefaultSolutionName;
                ProjectName = obj.DefaultProjectName;
            }
            AddToSourceControl = false;
            CreateDirectoryForSolution = false;
            RaisePropertyChanged(()=>IsEditEnabled);
        }
    }
}
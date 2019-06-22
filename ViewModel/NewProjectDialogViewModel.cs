using GalaSoft.MvvmLight;
using VSNewProjectDialogExample.Implementation.Repositories;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class NewProjectDialogViewModel : ViewModelBase
    {
        private readonly IObservableContext _observableContext;

        public NewProjectDialogViewModel(IObservableContext observableContext)
        {
            _observableContext = observableContext;
            ProjectTemplateSources = new[]
            {
                new ProjectTemplateGroupViewModel(new RecentProjectTemplateSource(), _observableContext),
                new ProjectTemplateGroupViewModel(new LoadedProjectTemplateSource(), _observableContext),
                new ProjectTemplateGroupViewModel(new OnlineProjectTemplateSource(), _observableContext)
            };
        }

        public ProjectTemplateGroupViewModel[] ProjectTemplateSources { get; }
    }
}
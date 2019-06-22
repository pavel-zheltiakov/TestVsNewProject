using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using VSNewProjectDialogExample.Extensions;
using VSNewProjectDialogExample.Implementation.Filtering;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class ProjectTemplateGroupViewModel : ViewModelBase
    {
        private readonly ISubject<IProjectElementDataProvider> _groupDataProviderSubject;
        private readonly IProjectGroupElement _groupElement;
        private readonly IObservableContext _observableContext;
        private ProjectTemplateGroupViewModel[] _children;

        private bool _isOpened;
        private bool _isSelected;


        public ProjectTemplateGroupViewModel(
            IProjectGroupElement groupElement,
            IObservableContext observableContext
            )
        {
            _groupDataProviderSubject =
                SimpleIoc.Default.GetInstanceWithoutCaching<ISubject<IProjectElementDataProvider>>();
            _groupElement = groupElement;
            _observableContext = observableContext;


            if (_groupElement.HasChildGroups)
            {
                // Emulate items exists to display expand button
                _children = new ProjectTemplateGroupViewModel[] {null};
                // Subscribe on event when template group expanded to  start loading
                // Subscribe on event when template group expanded to load 
                _groupDataProviderSubject
                    .LoadSubscribe(
                        ProjectGroupChildBeginLoading,
                        ProjectGroupLoadObservable,
                        ProjectGroupChildLoaded,
                        ProjectGroupChildLoadedError
                    );
            }
        }

        public string Name
        {
            get { return _groupElement.Name; }
        }

        public string GroupId
        {
            get { return _groupElement.Id; }
        }

        public bool IsOpened
        {
            get { return _isOpened; }
            set
            {
                if (Set(() => IsOpened, ref _isOpened, value) && value)
                {
                    _groupDataProviderSubject.OnNext(_groupElement.ChildGroupsProvider);

                    if (_groupElement.GroupId == "Sources")
                        _observableContext.TemplatesProvider.OnNext(_groupElement.TemplatesProvider);
                    
                   
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (Set(() => IsSelected, ref _isSelected, value) && value)
                {
                    _observableContext.TemplatesProvider.OnNext(_groupElement.TemplatesProvider);
                }
            }
        }

        public IEnumerable<ProjectTemplateGroupViewModel> Children
        {
            get { return _children; }
        }


        public bool IsLoading { get; set; }

        private IObservable<IProjectElement[]> ProjectGroupLoadObservable(IProjectElementDataProvider c)
        {
            if (c is IFilterableSource)
            {
                var fiterProvider = c as IFilterableSource;
                fiterProvider.Apply(new[] {new ProjectElementGroupIdFilter(_groupElement.Id)});
            }

            return Observable.Defer(
                () => Task<IObservable<IProjectElement[]>>
                    .Factory.StartNew(
                        () => Observable.Return(
                            c.Load().ToArray()
                            ))
                );
        }

        private void ProjectGroupChildBeginLoading(IProjectElementDataProvider groupDataProvider)
        {
            _observableContext.GroupsLoading.OnNext(true);
            Debug.WriteLine("Group Expanded {0}", groupDataProvider);
        }


        private void ProjectGroupChildLoadedError(Exception e)
        {
            Debug.WriteLine(e);
            _observableContext.GroupsLoading.OnNext(false);
        }

        private void ProjectGroupChildLoaded(IEnumerable<IProjectElement> projectGroups)
        {
            _observableContext.GroupsLoading.OnNext(false);

            _children = projectGroups
                .OfType<IProjectGroupElement>()
                .Select(group =>
                    new ProjectTemplateGroupViewModel(
                        group,
                        _observableContext
                        )).ToArray();


            RaisePropertyChanged(() => Children);

        }
    }
}
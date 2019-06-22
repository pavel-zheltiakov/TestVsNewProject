using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VSNewProjectDialogExample.Extensions;
using VSNewProjectDialogExample.Implementation.Filtering;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class ProjectTemplateListViewModel : ViewModelBase
    {
        private readonly IObservableContext _observableContext;
        private string _projectListView;


        private IDisposable _subscription;
        private bool _isItemsLoading;
        private bool _isGroupsLoading;

        private bool IsItemsLoading
        {
            get { return _isItemsLoading; }
            set
            {
                Set(()=>IsItemsLoading, ref _isItemsLoading,value);
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private bool IsGroupsLoading
        {
            get { return _isGroupsLoading; }
            set
            {
                Set(() => IsGroupsLoading, ref _isGroupsLoading, value); 
                RaisePropertyChanged(()=>IsLoading);
            }
        }

        public bool IsLoading
        {
            get { return IsGroupsLoading || IsItemsLoading; }
        }

        public ProjectTemplateListViewModel(
            IObservableContext observableContext
            )
        {
            _observableContext = observableContext;


            _observableContext.ItemsLoading
                .ObserveOnCurrentSyncronizationContext()
                .Subscribe(loading =>
                {
                    Debug.WriteLine("IsItemsLoading : {0}", loading);
                    IsItemsLoading = loading;
                });

            _observableContext.GroupsLoading
                .ObserveOnCurrentSyncronizationContext()
                .Subscribe(loading =>
                {
                    Debug.WriteLine("IsGroupsLoading : {0}", loading);
                    IsGroupsLoading = loading;
                });


            _subscription = 
            Observable.CombineLatest(                
                _observableContext.TemplatesProvider,
                _observableContext.NameContains,
                _observableContext.FrameworkFilter,
                _observableContext.Paging,                
                _observableContext.Sorting,
                (provider, filter, framework,paging, sort) =>
                    new Func<IEnumerable<IProjectElement>>(
                        () =>
                        {

                            Debug.WriteLine("--- Begin Loading ---");
                            try
                            {
                                var filterableProvider = provider as IFilterableSource;
                                if (filterableProvider != null)
                                {
                                    filterableProvider.Apply(new[] {filter, paging, framework});
                                }

                                var sortableProvider = provider as ISortableSource;
                                if (sortableProvider != null && sort != null)
                                {
                                    sortableProvider.Apply(new[] {sort});
                                }


                                var load = provider.Load().ToArray();
                                _observableContext.TemplateProviderLoaded.OnNext(provider);
                                return load;
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e);
                            }
                            return new IProjectElement[] {};
                        }
                        ))
                .LoadSubscribe(
                    ProjectTemplatesBeginLoad,
                    ProjectTemplatedLoadObservable,
                    ProjectTemplatesLoaded,
                    ProjectTemplatesLoadError
                );

            _observableContext.FrameworkFilter.OnNext(new ProjectElementFrameworkVersionFilter(new Version(4,5,0)));

            SwitchViewCommand = new RelayCommand<string>(p => ProjectListView = p);
            SwitchViewCommand.Execute("LargeIcons");
        }

        public ICollectionView Templates { get; private set; }


        public string ParentGroupId { get; set; }

        public string ProjectListView
        {
            get { return _projectListView; }
            set { Set(() => ProjectListView, ref _projectListView, value); }
        }

        public ICommand SwitchViewCommand { get; }

        private void Templates_CurrentChanged(object sender, EventArgs e)
        {
            MessengerInstance.Send(Templates.CurrentItem as ICreateProjectProperties);
        }

        private void ProjectTemplatesBeginLoad(Func<IEnumerable<IProjectElement>> o)
        {
            _observableContext.ItemsLoading.OnNext(true);
            Debug.WriteLine("Provider Changed {0}", o);
        }

        private void ProjectTemplatesLoadError(Exception e)
        {
            Debug.WriteLine(e);
            _observableContext.ItemsLoading.OnNext(false);
        }

        private void ProjectTemplatesLoaded(IProjectElement[] templates)
        {
            _observableContext.ItemsLoading.OnNext(false);

            Templates = CollectionViewSource.GetDefaultView(templates
                .Select(CreateProjectTemplateViewModels).ToArray());

            Templates.CurrentChanged += Templates_CurrentChanged;

            RaisePropertyChanged(() => Templates);
            

            Debug.WriteLine("Items Loaded {0},{1},{2}", templates, templates.Count(),
                string.Join(",", templates.Select(c => c.Name)));
        }


        private static IObservable<IProjectElement[]> ProjectTemplatedLoadObservable(
            Func<IEnumerable<IProjectElement>> c)
        {
            return Observable.Defer(
                () => Task.Factory.StartNew(() => Observable.Return(c().ToArray()))
                );
        }

        private object CreateProjectTemplateViewModels(IProjectElement template)
        {
            if (template is GalleryProjectSample)
                return new SampleTemplateViewModel(template as GalleryProjectSample);

            return new ProjectTemplateViewModel(template as ProjectTemplate);
        }

        public override void Cleanup()
        {
            _subscription.Dispose();
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VSNewProjectDialogExample.Extensions;
using VSNewProjectDialogExample.Implementation.Filtering;
using VSNewProjectDialogExample.Implementation.Sorting;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class NewProjectGlobalFilterViewModel : ViewModelBase
    {
        private readonly IObservableContext _observableContext;
        private string _filterValue;
        private FrameworkItemVersionViewModel _frameworkVersion;
        private bool _isSearching;
        private string _searchWatermarkText;

        private ISortDecorator _sortDecorator;
        private ISortDecorator[] _sortDecorators;
        private bool _isFilterFocused;


        public NewProjectGlobalFilterViewModel(IObservableContext observableContext
            )
        {
            _observableContext = observableContext;

            _filterHistory = new ObservableCollection<string>();
            FilterHistory = CollectionViewSource.GetDefaultView(_filterHistory);
            FilterHistory.Filter += item =>
            {
                var i = item as string;
                return !string.IsNullOrEmpty(i) && i.Contains(FilterValue);
            };


            _observableContext.TemplatesProvider
                .ObserveOnCurrentSyncronizationContext()
                .Subscribe(provider =>
            {
                var sortableProvider = provider as ISortableSource;
                if (sortableProvider != null)
                {
                    SortDecorators = sortableProvider.AvailableSorts.ToArray();
                    if (SortDecorators.Any())
                        SortDecorator = SortDecorators[0];
                    else
                    {
                        SortDecorator = EmptySortDecorator.Value;
                    }
                }
            });

            FrameworkVersions = new
                object[]
            {
                new FrameworkItemVersionViewModel(".NET Framework 2.0", new Version(2, 0, 0, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 3.0", new Version(3, 0, 0, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 3.5", new Version(3, 5, 0, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 4", new Version(4, 0, 0, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 4.5", new Version(4, 5, 0, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 4.5.1", new Version(4, 5, 1, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 4.5.2", new Version(4, 5, 2, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 4.6", new Version(4, 6, 0, 0)),
                new FrameworkItemVersionViewModel(".NET Framework 4.6.1", new Version(4, 6, 1, 0)),
                new FrameworkItemLinkViewModel("Get more frameworks", "http://getdotnet.azurewebsites.net/target-dotnet-platforms.html")
            };

            FrameworkVersion =
                FrameworkVersions.OfType<FrameworkItemVersionViewModel>().FirstOrDefault(c=>c.FrameworkVersion > new Version(4,5,0));

            FilterValue = "";


            SearchCommand = new RelayCommand(Search, () => !IsSearching);
            ResetSearchCommand = new RelayCommand(ResetSearch, () => IsSearching);
            SetFocusSearchCommand = new RelayCommand(
                () =>
                {
                    IsFilterFocused = true;
                    IsFilterFocused = false;
                }
            );
        }


        public object[] FrameworkVersions { get; }

        private ObservableCollection<string> _filterHistory { get; }
        public ICollectionView FilterHistory { get; }

        public FrameworkItemVersionViewModel FrameworkVersion
        {
            get { return _frameworkVersion; }
            set
            {
                if(Set(() => FrameworkVersion, ref _frameworkVersion, value))
                _observableContext.FrameworkFilter.OnNext(
                        new ProjectElementFrameworkVersionFilter(value.FrameworkVersion)
                        );
            }
        }

        public ISortDecorator[] SortDecorators
        {
            get { return _sortDecorators; }
            private set { Set(() => SortDecorators, ref _sortDecorators, value); }
        }

        public ISortDecorator SortDecorator
        {
            get { return _sortDecorator; }
            set
            {
                if (Set(() => SortDecorator, ref _sortDecorator, value))
                    _observableContext.Sorting.OnNext(value);
            }
        }

        public string FilterValue
        {
            get { return _filterValue; }
            set
            {
                if (Set(() => FilterValue, ref _filterValue, value))
                {
                    FilterHistory.Refresh();
                    _observableContext.NameContains.OnNext(new ProjectElementNameFilter(value));
                }
            }
        }


        public ICommand SearchCommand { get; }
        public ICommand ResetSearchCommand { get; }

        public ICommand SetFocusSearchCommand { get; }

        public bool IsFilterFocused
        {
            get { return _isFilterFocused; }
            set { _isFilterFocused = value; RaisePropertyChanged(()=>IsFilterFocused);}
        }

        public bool IsSearching
        {
            get { return _isSearching; }
            set { Set(() => IsSearching, ref _isSearching, value); }
        }

        public string SearchWatermarkText
        {
            get { return _searchWatermarkText; }
            set { Set(() => SearchWatermarkText, ref _searchWatermarkText, value); }
        }

        private void Search()
        {            
            if (!string.IsNullOrEmpty(FilterValue) && !_filterHistory.Contains(FilterValue))
                _filterHistory.Add(FilterValue);
            IsSearching = true;
            _observableContext.NameContains.OnNext(new ProjectElementNameFilter(FilterValue));
        }

        private void ResetSearch()
        {
            FilterValue = "";
            _observableContext.NameContains.OnNext(new ProjectElementNameFilter(FilterValue));
            IsSearching = false;
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VSNewProjectDialogExample.Extensions;
using VSNewProjectDialogExample.Implementation.Filtering;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class PageViewModel : ViewModelBase
    {
        public PageViewModel(int pageNumber)
        {
            DisplayPageText = pageNumber.ToString();
            PageNumber = pageNumber;
        }

        public string DisplayPageText { get; }
        public int PageNumber { get; }
    }

    public class ProjectTemplateListPageNavigationViewModel : ViewModelBase
    {
        private readonly IObservableContext _observableContext;

        private readonly int _pagesOnScreen;

        private int _currentPage;
        private bool _isPagingVisible;


        private int _pageCount;
        private ICollectionView _pages;
        private readonly int _pageSize;
        private ObservableCollection<PageViewModel> m_Pages;

        public ProjectTemplateListPageNavigationViewModel(
            IObservableContext observableContext)
        {
            _observableContext = observableContext;
            _pageSize = 20;
            _pagesOnScreen = 5;
            _currentPage = 0;
            _observableContext.Paging.OnNext(new ProjectElementPageFilter(1, _pageSize));

            NextPageCommand = new RelayCommand(
                () => SelectNextPage(), () => Pages != null && _currentPage < _pageCount - _pagesOnScreen);

            PrevPageCommand = new RelayCommand(
                () => SelectPreviousPage(), () => Pages != null && _currentPage > 1);

            observableContext.TemplatesProvider
                .ObserveOnCurrentSyncronizationContext()
                .Subscribe(TemplateProviderChanged);

            observableContext.TemplateProviderLoaded
                .OfType<IPagingProjectElementDataProvider>()
                .ObserveOnCurrentSyncronizationContext()
                .Subscribe(
                    TemplateProviderDataLoaded
                );
        }


        public ICollectionView Pages
        {
            get { return _pages; }
            set { Set(() => Pages, ref _pages, value); }
        }

        public bool IsPagingVisible
        {
            get { return _isPagingVisible; }
            set { Set(() => IsPagingVisible, ref _isPagingVisible, value); }
        }

        public RelayCommand NextPageCommand { get; }
        public RelayCommand PrevPageCommand { get; }

        private void TemplateProviderChanged(IProjectElementDataProvider provider)
        {
            _currentPage = 0;
            _pageCount = 0;
            InitPages(0);
        }

        private void SelectPreviousPage()
        {
            if (Pages == null)
                return;

            //Pages.MoveCurrentToPrevious();
            _currentPage--;
            SelectPageNotification();
        }

        private void SelectNextPage()
        {
            if (Pages == null)
                return;
            _currentPage++;
            //Pages.MoveCurrentToNext();
            SelectPageNotification();
        }

        private void TemplateProviderDataLoaded(IPagingProjectElementDataProvider provider)
        {
            if (_pageCount == 0)
                InitPages(provider.ItemsCount);
        }

        private void InitPages(int totalItems)
        {
            m_Pages = new ObservableCollection<PageViewModel>();

            if (totalItems > 0)
            {
                var pageCount = totalItems/_pageSize;
                if (totalItems%_pageSize != 0)
                    pageCount++;

                _pageCount = pageCount;
                _currentPage = 1;

                for (var i = 0; i < pageCount; i++)
                {
                    m_Pages.Add(new PageViewModel(i + 1));
                }

                Pages = CollectionViewSource.GetDefaultView(m_Pages);
                Pages.Filter += f =>
                {
                    var page = f as PageViewModel;
                    if (page == null)
                        return false;


                    return (page.PageNumber >= _currentPage ||
                            (page.PageNumber >= _pageCount - _pagesOnScreen &&
                             _currentPage > _pageCount - _pagesOnScreen)) &&
                           page.PageNumber < _currentPage + _pagesOnScreen
                        ;
                };

                Pages.CurrentChanged += Pages_CurrentChanged;
            }

            IsPagingVisible = _pageCount > 0 && totalItems > 0;
        }

        private void Pages_CurrentChanged(object sender, EventArgs e)
        {
            SelectPageNotification();
        }

        private void SelectPageNotification()
        {
            if (Pages == null)
                return;

            Pages.Refresh();
            Pages.MoveCurrentTo(m_Pages.Where(c => c.PageNumber == _currentPage));

            _observableContext.Paging.OnNext(new ProjectElementPageFilter(_currentPage,
                _pageSize));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using VSNewProjectDialogExample.Implementation.Filtering;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Repositories
{
    public class FakePagedProjectElementProvider : FakeProjectElementProvider, IPagingProjectElementDataProvider

    {
        private readonly Func<IEnumerable<IProjectElement>> _getList;

        public FakePagedProjectElementProvider(Func<IEnumerable<IProjectElement>> getList,
            ISortDecorator[] awailableSort)
            : base(getList, awailableSort)
        {
            _getList = getList;
        }

        public override IEnumerable<IProjectElement> Load()
        {
            var query = _getList();
            query = ApplyFiltersExcludePaging(query);
            ItemsCount = query.Count();
            query = ApplySort(query);

            query = query.Select((c, index) =>
            {
                if (c is IProjectElementIndexed)
                    ((IProjectElementIndexed) c).Index = index;

                return c;
            });
            query = ApplyFilters(query);
            return query;
        }

        public int ItemsCount { get; private set; }

        private IEnumerable<IProjectElement> ApplyFiltersExcludePaging(IEnumerable<IProjectElement> elements)
        {
            return
                elements.Where(
                    item =>
                        _applyedFilters.Where(filter => !(filter is ProjectElementPageFilter))
                            .All(filter => filter.Check(item)));
        }
    }

    public class SortDecoratorComparer : IComparer<object>
    {
        private readonly ISortDecorator _sortDecorator;

        public SortDecoratorComparer(ISortDecorator sortDecorator)
        {
            _sortDecorator = sortDecorator;
        }

        public int Compare(object x, object y)
        {
            return _sortDecorator.Compare(x, y);
        }
    }

    public class FakeProjectElementProvider : IProjectElementDataProvider, IFilterableSource, ISortableSource
    {
        protected readonly IList<IProjectElementFilter> _applyedFilters;
        protected readonly IList<ISortDecorator> _applyedSources;
        private readonly Func<IEnumerable<IProjectElement>> _getList;


        public FakeProjectElementProvider(Func<IEnumerable<IProjectElement>> getList, ISortDecorator[] awailableSort)
        {
            _getList = getList;
            _applyedFilters = new List<IProjectElementFilter>();
            _applyedSources = new List<ISortDecorator>();
            AvailableSorts = awailableSort;
        }

        public void Apply(IEnumerable<IProjectElementFilter> filters)
        {
            _applyedFilters.Clear();
            foreach (var filter in filters)
            {
                _applyedFilters.Add(filter);
            }
        }

        public virtual IEnumerable<IProjectElement> Load()
        {
            var query = _getList();
            query = ApplyFilters(query);
            query = ApplySort(query);

            return query;
        }

        public IEnumerable<ISortDecorator> AvailableSorts { get; }

        public void Apply(IEnumerable<ISortDecorator> decorators)
        {
            _applyedSources.Clear();
            foreach (var sort in decorators)
            {
                _applyedSources.Add(sort);
            }
        }


        protected IEnumerable<IProjectElement> ApplyFilters(IEnumerable<IProjectElement> elements)
        {
            return elements.Where(item => _applyedFilters.All(filter => filter.Check(item)));
        }

        protected IEnumerable<IProjectElement> ApplySort(IEnumerable<IProjectElement> elements)
        {
            var firstSort = _applyedSources.FirstOrDefault();
            if (firstSort != null)
            {
                var orderedQuery = elements.OrderBy(x => x, new SortDecoratorComparer(firstSort));
                foreach (var nextSort in _applyedSources.Skip(1))
                {
                    orderedQuery = orderedQuery.ThenBy(x => x, new SortDecoratorComparer(nextSort));
                }

                elements = orderedQuery;
            }

            return elements;
        }
    }
}
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation
{
    public class NewProjectDialogObservableContext : IObservableContext
    {
        public NewProjectDialogObservableContext(ISubject<IProjectElementFilter> frameworkFilter,
            ISubject<IProjectElementFilter> nameContains,
            ISubject<IProjectElementDataProvider> templatesProvider,
            ISubject<IProjectElementFilter> paging,
            ISubject<ISortDecorator> sorting,
            ISubject<bool> itemsLoading,
            ISubject<bool> groupsLoading,
            ISubject<IProjectElementDataProvider> templateProviderLoaded,
            IObservable<IProjectElement> templateSelected
            )
        {
            FrameworkFilter = frameworkFilter;
            NameContains = nameContains;
            TemplatesProvider = templatesProvider;
            Paging = paging;
            Sorting = sorting;
            ItemsLoading = itemsLoading;
            GroupsLoading = groupsLoading;
            TemplateProviderLoaded = templateProviderLoaded;
            TemplateSelected = templateSelected;
        }

        public ISubject<IProjectElementFilter> FrameworkFilter { get; }
        public ISubject<IProjectElementFilter> NameContains { get; }
        public ISubject<IProjectElementDataProvider> TemplatesProvider { get; }
        public ISubject<IProjectElementFilter> Paging { get; }
        public ISubject<ISortDecorator> Sorting { get; }
        public ISubject<bool> ItemsLoading { get; }
        public ISubject<bool> GroupsLoading { get; }
        public ISubject<IProjectElementDataProvider> TemplateProviderLoaded { get; }
        public IObservable<IProjectElement> TemplateSelected { get; }
    }
}
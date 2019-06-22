using System;
using System.Reactive.Subjects;

namespace VSNewProjectDialogExample.Interfaces
{
    public interface IObservableContext
    {
        /// <summary>
        ///     Stream of events of framework version changed
        /// </summary>
        ISubject<IProjectElementFilter> FrameworkFilter { get; }

        /// <summary>
        ///     Stream of events of name search input
        /// </summary>
        ISubject<IProjectElementFilter> NameContains { get; }

        /// <summary>
        ///     Stream of events of templates provider selected
        /// </summary>
        ISubject<IProjectElementDataProvider> TemplatesProvider { get; }

        /// <summary>
        ///     Stream of events of page switching
        /// </summary>
        ISubject<IProjectElementFilter> Paging { get; }

        /// <summary>
        ///     Stream of events of sort order selected
        /// </summary>
        ISubject<ISortDecorator> Sorting { get; }

        /// <summary>
        ///     Stream of events when loading started
        /// </summary>
        ISubject<bool> ItemsLoading { get; }

        /// <summary>
        ///     Stream of events when group loading started
        /// </summary>
        ISubject<bool> GroupsLoading { get; }

        /// <summary>
        ///     Stream of received data from template provider
        /// </summary>
        ISubject<IProjectElementDataProvider> TemplateProviderLoaded { get; }

        /// <summary>
        ///     Stream of events of selecting templates from list
        /// </summary>
        IObservable<IProjectElement> TemplateSelected { get; }
    }
}
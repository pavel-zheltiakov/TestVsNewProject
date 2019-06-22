using System.Reactive.Subjects;

namespace VSNewProjectDialogExample.Interfaces
{
    public interface ITemplateSortObservable : ISubject<ISortDecorator[]>
    {
    }
}
using System.Collections.Generic;

namespace VSNewProjectDialogExample.Interfaces
{
    internal interface ISortableSource
    {
        IEnumerable<ISortDecorator> AvailableSorts { get; }
        void Apply(IEnumerable<ISortDecorator> decorators);
    }
}
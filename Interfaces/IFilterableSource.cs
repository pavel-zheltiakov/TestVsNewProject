using System.Collections.Generic;

namespace VSNewProjectDialogExample.Interfaces
{
    internal interface IFilterableSource
    {
        void Apply(IEnumerable<IProjectElementFilter> filters);
    }
}
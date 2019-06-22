using System.Collections.Generic;

namespace VSNewProjectDialogExample.Interfaces
{
    public interface IProjectElementDataProvider
    {
        IEnumerable<IProjectElement> Load();
    }
}
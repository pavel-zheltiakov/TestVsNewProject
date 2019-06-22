using VSNewProjectDialogExample.Implementation.Templates;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class SortProjectElementByName : BasePropertySortDecorator<ProjectTemplate, string>
    {
        public SortProjectElementByName(string name, bool ascending)
            : base(name, ascending, p => p.Name)
        {
        }
    }
}
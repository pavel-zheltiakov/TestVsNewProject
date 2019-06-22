using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class SortProjectElementByAuthor : BasePropertySortDecorator<IAutorProject, string>
    {
        public SortProjectElementByAuthor(string name, bool ascending)
            : base(name, ascending, p => p.AuthorName)
        {
        }
    }
}
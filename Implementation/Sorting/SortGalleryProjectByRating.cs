using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class SortGalleryProjectByRating : BasePropertySortDecorator<IAutorProject, int>
    {
        public SortGalleryProjectByRating(string name, bool ascending)
            : base(name, ascending, p => p.Rating)
        {
        }
    }
}
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class SortGalleryProjectByVotes : BasePropertySortDecorator<IAutorProject, int>
    {
        public SortGalleryProjectByVotes(string name, bool ascending)
            : base(name, ascending, p => p.RatingVotes)
        {
        }
    }
}
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class SortGalleryProjectByDownloads : BasePropertySortDecorator<IAutorProject, int>
    {
        public SortGalleryProjectByDownloads(string name, bool ascending)
            : base(name, ascending, p => p.Downloads)
        {
        }
    }
}
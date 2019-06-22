using System;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class SortGalleryProjectByRecent : BasePropertySortDecorator<IAutorProject, DateTime>
    {
        public SortGalleryProjectByRecent(string name, bool ascending)
            : base(name, ascending, p => p.PublishDate)
        {
        }
    }
}
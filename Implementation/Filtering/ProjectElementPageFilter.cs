using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Filtering
{
    public class ProjectElementPageFilter : IProjectElementFilter
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;

        public ProjectElementPageFilter(int pageNumber, int pageSize)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
        }

        public bool Check(IProjectElement template)
        {
            var indexed = template as IProjectElementIndexed;
            if (indexed == null)
                return true;

            return indexed.Index >= (_pageNumber - 1)*_pageSize && indexed.Index < _pageNumber*_pageSize;
        }
    }
}
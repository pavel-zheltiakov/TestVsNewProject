using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Filtering
{
    public class ProjectElementNameFilter : IProjectElementFilter
    {
        private readonly string _filterName;

        public ProjectElementNameFilter(string filterName)
        {
            _filterName = filterName;
        }

        public bool Check(IProjectElement template)
        {
            if (string.IsNullOrEmpty(_filterName))
                return true;

            return template != null && template.Name.Contains(_filterName);
        }
    }
}
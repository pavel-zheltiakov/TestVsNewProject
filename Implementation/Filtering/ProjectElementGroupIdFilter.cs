using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Filtering
{
    public class ProjectElementGroupIdFilter : IProjectElementFilter
    {
        private readonly string _groupId;

        public ProjectElementGroupIdFilter(string groupId)
        {
            _groupId = groupId;
        }

        public bool Check(IProjectElement template)
        {
            return template != null && template.GroupId == _groupId;
        }
    }
}
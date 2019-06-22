using System.Data;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation
{
    public class ProjectGroupDescription : IProjectGroupElement
    {
        public ProjectGroupDescription(
            string id,
            string displayName,
            string parentGroupId,
            bool hasItems,
            IProjectElementDataProvider childGroupsProvider,
            IProjectElementDataProvider templatesProvider)
        {
            if (string.IsNullOrEmpty(id))
                throw new NoNullAllowedException("Id");
            if (string.IsNullOrEmpty(displayName))
                throw new NoNullAllowedException("displayName");

            Id = id;
            Name = displayName;
            GroupId = parentGroupId;
            HasChildGroups = hasItems;
            ChildGroupsProvider = childGroupsProvider;
            TemplatesProvider = templatesProvider;
        }

        public string Name { get; }

        public string Id { get; }

        public string GroupId { get; }

        public bool HasChildGroups { get; }

        public IProjectElementDataProvider ChildGroupsProvider { get; }
        public IProjectElementDataProvider TemplatesProvider { get; }
    }
}
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Repositories
{
    public abstract class FakeProjectElementSource : IProjectGroupElement
    {
        protected FakeProjectElementSource(string id, string name)
        {
            Id = id;
            Name = name;
            GroupId = "Sources";
            HasChildGroups = true;
            TemplatesProvider = NoItemsTemplateDataProvider.Instance;
            ChildGroupsProvider = NoItemsTemplateDataProvider.Instance;
        }


        public string Id { get; }
        public string Name { get; }
        public string GroupId { get; }
        public bool HasChildGroups { get; }
        public IProjectElementDataProvider ChildGroupsProvider { get; protected set; }
        public IProjectElementDataProvider TemplatesProvider { get; protected set; }
    }
}
namespace VSNewProjectDialogExample.Interfaces
{
    public interface IProjectGroupElement : IProjectElement
    {
        bool HasChildGroups { get; }

        IProjectElementDataProvider ChildGroupsProvider { get; }
        IProjectElementDataProvider TemplatesProvider { get; }
    }
}
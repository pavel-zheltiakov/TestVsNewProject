namespace VSNewProjectDialogExample.Interfaces
{
    public interface ISortDecorator
    {
        string Name { get; }
        int Compare(object a, object b);
    }
}
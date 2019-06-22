namespace VSNewProjectDialogExample.Interfaces
{
    public interface IProjectElementFilter
    {
        bool Check(IProjectElement template);
    }
}
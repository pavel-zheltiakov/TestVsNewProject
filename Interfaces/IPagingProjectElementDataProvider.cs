namespace VSNewProjectDialogExample.Interfaces
{
    public interface IPagingProjectElementDataProvider :
        IProjectElementDataProvider
    {
        int ItemsCount { get; }
    }
}
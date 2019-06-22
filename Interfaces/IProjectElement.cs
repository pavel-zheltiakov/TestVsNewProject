namespace VSNewProjectDialogExample.Interfaces
{
    /// <summary>
    ///     Element of project templates catalog
    /// </summary>
    public interface IProjectElement
    {
        /// <summary>
        ///     Unique id in catalog
        /// </summary>
        string Id { get; }

        /// <summary>
        ///     Name in catalog
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Id of parent IProjectElement
        /// </summary>
        string GroupId { get; }
    }
}
using System;

namespace VSNewProjectDialogExample.Interfaces
{
    public interface IAutorProject
    {
        int Rating { get; }
        int RatingVotes { get; }
        string AuthorName { get; }
        int Downloads { get; }
        DateTime PublishDate { get; }
    }
}
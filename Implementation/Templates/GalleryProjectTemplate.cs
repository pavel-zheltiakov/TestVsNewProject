using System;

namespace VSNewProjectDialogExample.Implementation.Templates
{
    public class GalleryProjectTemplate : ProjectTemplate
    {
        public GalleryProjectTemplate(string id, string name, string description, string defaultProjectName,
            string[] projectAttributes, string projectIcon, string projectType, Version requiredFrameworkVersion,
            string author, int rating, int downloads, int ratingVotes, string version, string parentId) :
                base(
                id, name, description, defaultProjectName, projectAttributes, projectIcon, projectType,
                requiredFrameworkVersion, parentId)
        {
            Author = author;
            Rating = rating;
            Downloads = downloads;
            RatingVotes = ratingVotes;
            Version = version;
        }

        public string Author { get; }
        public int Rating { get; }
        public int Downloads { get; }
        public int RatingVotes { get; }
        public string Version { get; }
    }
}
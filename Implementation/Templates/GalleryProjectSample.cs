using System;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Templates
{
    public class GalleryProjectSample : ProjectTemplate, IAutorProject
    {
        public GalleryProjectSample(string id, string name, string description, string defaultProjectName,
            string[] projectAttributes, string projectIcon, string projectType, Version requiredFrameworkVersion,
            string previewImage, string version, int ratingVotes, int downloads, int rating, string authorName,
            string parentGroupId, DateTime publishDate)
            : base(
                id, name, description, defaultProjectName, projectAttributes, projectIcon, projectType,
                requiredFrameworkVersion, parentGroupId)

        {
            PreviewImage = previewImage;
            Version = version;
            RatingVotes = ratingVotes;
            Downloads = downloads;
            Rating = rating;
            AuthorName = authorName;
            PublishDate = publishDate;
            ProjectVersion = version;
        }

        public string Version { get; }
        public string PreviewImage { get; }
        public string ProjectVersion { get; }

        public string AuthorName { get; }
        public int Rating { get; }
        public int Downloads { get; }
        public int RatingVotes { get; }
        public DateTime PublishDate { get; }
    }
}
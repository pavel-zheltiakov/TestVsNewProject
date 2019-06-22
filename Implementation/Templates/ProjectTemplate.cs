using System;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Templates
{
    public class ProjectTemplate : IProjectElement, IProjectElementIndexed
    {
        public ProjectTemplate(string id, string name, string description, string defaultProjectName,
            string[] projectAttributes, string projectIcon, string projectType, Version requiredFrameworkVersion,
            string projectGroupId)
        {
            Id = id;
            ProjectAttributes = projectAttributes;
            ProjectIcon = projectIcon;
            ProjectType = projectType;
            RequiredFrameworkVersion = requiredFrameworkVersion;
            GroupId = projectGroupId;
            Name = name;
            Description = description;
            DefaultProjectName = defaultProjectName;
        }

        public string ProjectType { get; }
        public string DefaultProjectName { get; }
        public string Description { get; }
        public string[] ProjectAttributes { get; }
        public string ProjectIcon { get; }
        public Version RequiredFrameworkVersion { get; }
        public string Name { get; }
        public string GroupId { get; }
        public string Id { get; }

        public int Index { get; set; }
    }
}
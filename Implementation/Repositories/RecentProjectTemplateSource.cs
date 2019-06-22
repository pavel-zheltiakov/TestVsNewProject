using System;
using System.Collections.Generic;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Repositories
{
    public class RecentProjectTemplateSource : IProjectGroupElement
    {
        public RecentProjectTemplateSource()
        {
            Id = "Recent";
            Name = "Recent";
            HasChildGroups = true;
            GroupId = "Sources";
            ChildGroupsProvider = new FakeProjectElementProvider(Groups, new ISortDecorator[] {});
            TemplatesProvider = new FakeProjectElementProvider(GetSource, new ISortDecorator[]
            {
            });
        }


        public string Id { get; }
        public string Name { get; }
        public string GroupId { get; }
        public bool HasChildGroups { get; }
        public IProjectElementDataProvider ChildGroupsProvider { get; }
        public IProjectElementDataProvider TemplatesProvider { get; }

        public IEnumerable<ProjectGroupDescription> Groups()
        {
            yield return
                new ProjectGroupDescription("Recent.All", "All", Id, false, NoItemsTemplateDataProvider.Instance,
                    TemplatesProvider
                    );
        }

        private IEnumerable<ProjectTemplate> GetSource()
        {
            yield return
                new ProjectTemplate("WPF", "WPF Application", "WPF Application Description", "WpfApplication",
                    new[] {"Windows"}, "", "", Version.Parse("3.5"), "Recent.All");
            yield return
                new ProjectTemplate("", "Console Application", "Console Application Description", "ConsoleApplication",
                    new[] {"Windows"}, "", "", Version.Parse("2.0"), "Recent.All");
        }
    }
}
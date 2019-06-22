using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VSNewProjectDialogExample.Implementation.Sorting;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Repositories
{
    public class OnlineProjectTemplateSource : FakeProjectElementSource, IProjectGroupElement
    {
        private readonly IProjectElementDataProvider _samplesDataProvider;

        private readonly ISortDecorator[] _sortDecorators =
        {
            new SortGalleryProjectByVotes("Most Popular", false),
            new SortGalleryProjectByRecent("Most Recent", false),
            new SortGalleryProjectByDownloads("Most Downloads", false),
            new SortGalleryProjectByRating("Highest Rating", false),
            new SortProjectElementByName("Name: Ascending", true),
            new SortProjectElementByName("Name: Descending", false),
            new SortProjectElementByAuthor("Author: Ascending", true),
            new SortProjectElementByAuthor("Author: Descending", false)
        };

        private readonly IProjectElementDataProvider _templatesDataProvider;


        public OnlineProjectTemplateSource()
            : base("Online", "Online")
        {
            ChildGroupsProvider = new FakeProjectElementProvider(Groups, new ISortDecorator[] {});
            _templatesDataProvider = new FakeProjectElementProvider(Templates, _sortDecorators);
            _samplesDataProvider = new FakePagedProjectElementProvider(Samples, _sortDecorators);
        }

        /*
        Visual C#
Other
Visual F#
Visual C++
Visual Basic



JavaScript
Visual F#
Visual C++
Visual Basic
Visual C#*/


        private IEnumerable<ProjectGroupDescription> Groups()
        {
            Thread.Sleep(3000);

            var templatesGroup = new ProjectGroupDescription("Online.Templates", "Templates", Id, true,
                ChildGroupsProvider, _templatesDataProvider);
            var samplesGroup = new ProjectGroupDescription("Online.Samples", "Samples", Id, true, ChildGroupsProvider,
                _samplesDataProvider);

            yield return templatesGroup;
            yield return samplesGroup;

            yield return
                new ProjectGroupDescription("Online.Templates.VisualCS", "Visual C#", templatesGroup.Id, true,
                    ChildGroupsProvider, _templatesDataProvider);
            yield return
                new ProjectGroupDescription("Online.Templates.Other", "Other", templatesGroup.Id, true,
                    ChildGroupsProvider, _templatesDataProvider);
            yield return
                new ProjectGroupDescription("Online.Templates.VB", "Visual Basic", templatesGroup.Id, true,
                    ChildGroupsProvider, _templatesDataProvider);
            yield return
                new ProjectGroupDescription("Online.Templates.CPP", "Visual C++", templatesGroup.Id, true,
                    ChildGroupsProvider, _templatesDataProvider);

            yield return
                new ProjectGroupDescription("Online.Samples.VisualCS", "Visual C#", samplesGroup.Id, true,
                    ChildGroupsProvider, _samplesDataProvider);
            yield return
                new ProjectGroupDescription("Online.Samples.Other", "Other", samplesGroup.Id, true, ChildGroupsProvider,
                    _samplesDataProvider);
            yield return
                new ProjectGroupDescription("Online.Samples.VB", "Visual Basic", samplesGroup.Id, true,
                    ChildGroupsProvider, _samplesDataProvider);
            yield return
                new ProjectGroupDescription("Online.Samples.CPP", "Visual C++", samplesGroup.Id, true,
                    ChildGroupsProvider, _samplesDataProvider);
        }

        private IEnumerable<ProjectTemplate> Templates()
        {
            Thread.Sleep(5000);
            yield return
                new ProjectTemplate("Online.Templates.VisualCS.WPF", "WPF Application", "WPF Application Description",
                    "WPF Application", new string[] {}, "", "Visual C#", new Version(3, 5, 0, 0),
                    "Online.Templates.VisualCS");
            yield return
                new ProjectTemplate("Online.Templates.VisualCS.Web", "Web Application", "Web Application Description",
                    "Web Application", new string[] {}, "", "Visual C#", new Version(3, 5, 0, 0),
                    "Online.Templates.VisualCS");
            yield return
                new ProjectTemplate("Online.Templates.VisualCS.Universal", "Universal Application",
                    "Universal Application Description", "Universal Application", new string[] {}, "", "Visual C#",
                    new Version(3, 5, 0, 0), "Online.Templates.VisualCS");
            yield return
                new ProjectTemplate("Online.Templates.VisualCS.WCF", "WCF Library", "WCF Library Description",
                    "WCF Library", new string[] {}, "", "Visual C#", new Version(3, 5, 0, 0),
                    "Online.Templates.VisualCS");

            yield return
                new ProjectTemplate("Online.Templates.Other.WPF", "WPF Application Sample",
                    "WPF Application Description", "WPF Application", new string[] {}, "", "Visual C#",
                    new Version(3, 5, 0, 0), "Online.Templates.Other");
            yield return
                new ProjectTemplate("Online.Templates.VisualCS.Web", "Web Application Sample",
                    "Web Application Description", "Web Application", new string[] {}, "", "Visual C#",
                    new Version(3, 5, 0, 0), "Online.Templates.VB");
            yield return
                new ProjectTemplate("Online.Templates.VisualCS.Universal", "Universal Application",
                    "Universal Application Description", "Universal Application", new string[] {}, "", "Visual C#",
                    new Version(3, 5, 0, 0), "Online.Templates.CPP");
        }

        private IEnumerable<GalleryProjectSample> Samples()
        {
            Thread.Sleep(5000);
            var rnd = new Random(3737);
            foreach (var g in Groups().Where(c => c.GroupId == "Online.Samples"))
            {
                for (var i = 0; i < 200; i++)
                {
                    yield return new GalleryProjectSample(string.Format("Online.Samples.{0}.{1}", g.Id, i),
                        string.Format("{0} Project Sample {1}", g.Name, i),
                        string.Format("{0} Project Sample {1} details description", g.Name, i),
                        string.Format("{0}Sample{1}", g.Name, i),
                        new string[] {},
                        "/Resources/Images/classlibrary_icon.png",
                        g.Name,
                        new Version(2 + i/20%3, i/40%3),
                        "/Resources/Images/classlibrary_icon.png",
                        "1.1.1",
                        rnd.Next(10, 1000),
                        rnd.Next(100, 10000),
                        rnd.Next(0, 5),
                        "Test Author",
                        g.Id,
                        new DateTime(rnd.Next(2008, 2016), rnd.Next(1, 12), rnd.Next(1, 28))
                        ) {Index = i};
                }
            }
        }
    }
}
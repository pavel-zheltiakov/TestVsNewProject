using System;
using System.Collections.Generic;
using System.Linq;
using VSNewProjectDialogExample.Implementation.Sorting;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Repositories
{
    public class LoadedProjectTemplateSource : FakeProjectElementSource, IProjectGroupElement
    {
        private readonly IProjectElementDataProvider
            _loadedSamplesProvider;

        private readonly IProjectElementDataProvider
            _loadedTemplatesProvider;

        private readonly ISortDecorator[] _sortDecorators =
        {
            new SortProjectElementByName("Default", true),
            new SortProjectElementByName("Name Ascending", true),
            new SortProjectElementByName("Name Descending", false)
        };

        public LoadedProjectTemplateSource()
            : base("Loaded", "Loaded")
        {
            ChildGroupsProvider = new FakeProjectElementProvider(Groups, new ISortDecorator[] {});
            _loadedTemplatesProvider = new FakeProjectElementProvider(Templates, _sortDecorators);

            _loadedSamplesProvider = new FakeProjectElementProvider(Samples, _sortDecorators);
        }

        private IEnumerable<ProjectGroupDescription> Groups()
        {
            yield return
                new ProjectGroupDescription("Loaded.Templates", "Templates", Id, true, ChildGroupsProvider,
                    _loadedTemplatesProvider);
            yield return
                new ProjectGroupDescription("Loaded.Samples", "Samples", Id, true, ChildGroupsProvider,
                    _loadedSamplesProvider);


            yield return
                new ProjectGroupDescription("Templates.VisualCS", "Visual C#", "Loaded.Templates", true,
                    ChildGroupsProvider, _loadedTemplatesProvider);
            yield return
                new ProjectGroupDescription("Templates.VisualBasic", "Visual Basic", "Loaded.Templates", true,
                    ChildGroupsProvider, _loadedTemplatesProvider);
        }

        private IEnumerable<ProjectTemplate> Templates()
        {
            yield return
                new ProjectTemplate("Templates.VisualCS.WPF", "WPF Application", "WPF Application Description",
                    "Templates.VisualCS", new string[] {}, "/Resources/Images/classlibrary_icon.png", "Visual C#",
                    new Version(3, 5, 0, 0), "Templates.VisualCS");
        }

        private IEnumerable<GalleryProjectSample> Samples()
        {
            return Enumerable.Empty<GalleryProjectSample>();
            //yield return new GalleryProjectSample("", "WPF Sample", "WPF Application Description", "WPFApplication", new string[] { }, "/Resources/Images/classlibrary_icon.png", "Visual C#", new Version(3, 5, 0, 0), "{937153C9-BF59-4A60-B513-F4F9E2CF7698}");
        }
    }
}
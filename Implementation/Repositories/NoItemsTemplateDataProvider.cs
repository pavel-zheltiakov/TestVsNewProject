using System.Collections.Generic;
using System.Linq;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Repositories
{
    public class NoItemsTemplateDataProvider : IProjectElementDataProvider
    {
        public static readonly NoItemsTemplateDataProvider Instance = new NoItemsTemplateDataProvider();

        private NoItemsTemplateDataProvider()
        {
        }

        public IEnumerable<IProjectElement> Load()
        {
            return Enumerable.Empty<ProjectTemplate>();
        }
    }
}
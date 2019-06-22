using System;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Filtering
{
    public class ProjectElementFrameworkVersionFilter : IProjectElementFilter
    {
        private readonly Version _minVersion;

        public ProjectElementFrameworkVersionFilter(Version minVersion)
        {
            _minVersion = minVersion;
        }

        public bool Check(IProjectElement template)
        {
            return true;

            var projectTemplate = template as ProjectTemplate;
            if (projectTemplate == null)
                return true;
            return projectTemplate.RequiredFrameworkVersion <= _minVersion;
        }
    }
}
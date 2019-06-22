using System.Data;
using GalaSoft.MvvmLight;
using VSNewProjectDialogExample.Implementation;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class ProjectTemplateViewModel : ViewModelBase, ICreateProjectProperties
    {
        public ProjectTemplateViewModel(ProjectTemplate projectTemplate)
        {
            if (projectTemplate == null)
                throw new NoNullAllowedException("projectTemplate");

            Template = projectTemplate;
        }

        
        public object DynamicIcon
        {
            get { return DynamicIconSourceGenerator.Instance.CreateDrawingImageFromProjectTemplate(Template); }
        }

        public string IconSource
        {
            get { return "/Resources/Images/classlibrary_icon.png"; }
        }

        public string Name
        {
            get { return Template.Name; }
        }

        public string ProjectType
        {
            get { return Template.ProjectType; }
        }

        public string Description
        {
            get { return Template.Description; }
        }

        public ProjectTemplate Template { get; }

        public string DefaultProjectName
        {
            get { return Template.DefaultProjectName; }
        }

        public string DefaultSolutionName
        {
            get { return Template.DefaultProjectName; }
        }
    }
}
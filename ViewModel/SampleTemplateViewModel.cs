using GalaSoft.MvvmLight;
using VSNewProjectDialogExample.Implementation;
using VSNewProjectDialogExample.Implementation.Templates;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    public class SampleTemplateViewModel : ViewModelBase, ICreateProjectProperties
    {
        public SampleTemplateViewModel(GalleryProjectSample sample)
        {
            ProjectType = sample.ProjectType;
            AuthorName = sample.AuthorName;
            ProjectVersion = sample.ProjectVersion;
            Downloads = sample.Downloads;
            Rating = sample.Rating;
            RatingVotes = sample.RatingVotes;
            Template = sample;
            Stars = new MultiStarViewModel(sample.Rating, 5);
        }

        public string ProjectType { get; }
        public string AuthorName { get; }
        public string ProjectVersion { get; }
        public int Downloads { get; }
        public int Rating { get; }
        public int RatingVotes { get; }

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

        public GalleryProjectSample Template { get; }
        public MultiStarViewModel Stars { get; }

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
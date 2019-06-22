/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TestVSNewProject"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Reactive.Subjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using VSNewProjectDialogExample.Implementation;
using VSNewProjectDialogExample.Implementation.Dialogs;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static IObservableContext _observableContext;

        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }

            if (!SimpleIoc.Default.IsRegistered<IMessenger>())
                SimpleIoc.Default.Register(() => Messenger.Default);


            if (!SimpleIoc.Default.IsRegistered<ISubject<IProjectElementDataProvider>>())
                SimpleIoc.Default.Register<ISubject<IProjectElementDataProvider>>(
                    () => new Subject<IProjectElementDataProvider>()
                    );

            _observableContext =
                new NewProjectDialogObservableContext(
                    new Subject<IProjectElementFilter>(),
                    new Subject<IProjectElementFilter>(),
                    new Subject<IProjectElementDataProvider>(),
                    new Subject<IProjectElementFilter>(),
                    new Subject<ISortDecorator>(),
                    new Subject<bool>(),
                    new Subject<bool>(), 
                    new Subject<IProjectElementDataProvider>(),
                    new Subject<IProjectElement>()
                    );


            _observableContext.TemplatesProvider.Subscribe((a) =>
            {
                Debug.WriteLine("=== Templates Provider Event ===");
            });


            _observableContext.FrameworkFilter.Subscribe((a) =>
            {
                Debug.WriteLine("=== FrameworkFilter Event ===");
            });

            _observableContext.ItemsLoading.Subscribe((a) =>
            {
                Debug.WriteLine("=== ItemsLoading Event ===");
            });

            _observableContext.NameContains.Subscribe((a) =>
            {
                Debug.WriteLine("=== NameContrains Event ===");
            });

            _observableContext.Paging.Subscribe((a) =>
            {
                Debug.WriteLine("=== Paging Event ===");
            });


            _observableContext.Sorting.Subscribe((a) =>
            {
                Debug.WriteLine("=== Sorting Event ===");
            });

            _observableContext.TemplateProviderLoaded.Subscribe((a) =>
            {
                Debug.WriteLine("=== TemplateProviderLoaded Event ===");
            });

            _observableContext.TemplateSelected.Subscribe((a) =>
            {
                Debug.WriteLine("=== TemplateSelected Event ===");
            });

            if (!SimpleIoc.Default.IsRegistered<IObservableContext>())
                SimpleIoc.Default.Register(() => _observableContext);

            if (!SimpleIoc.Default.IsRegistered<ISelectFolderDialog>())
                SimpleIoc.Default.Register<ISelectFolderDialog>(
                    () => new SelectFolderDialogWinApi("Select project folder",
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Projects")));

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NewProjectDialogViewModel>();
            SimpleIoc.Default.Register<ProjectTemplateListViewModel>();
            SimpleIoc.Default.Register<NewProjectGlobalFilterViewModel>();
            SimpleIoc.Default.Register<ProjectTemplateListPageNavigationViewModel>();
            SimpleIoc.Default.Register<NewProjectParamsModelView>();
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public NewProjectDialogViewModel DialogViewModel
        {
            get { return ServiceLocator.Current.GetInstance<NewProjectDialogViewModel>(); }
        }

        public ProjectTemplateListViewModel TemplateListViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ProjectTemplateListViewModel>(); }
        }

        public NewProjectGlobalFilterViewModel GlobalFilterViewModel
        {
            get { return ServiceLocator.Current.GetInstance<NewProjectGlobalFilterViewModel>(); }
        }

        public NewProjectParamsModelView CreateParamsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<NewProjectParamsModelView>(); }
        }

        public ProjectTemplateListPageNavigationViewModel PagingView
        {
            get { return ServiceLocator.Current.GetInstance<ProjectTemplateListPageNavigationViewModel>(); }
        }

        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<IMessenger>();
        }
    }
}
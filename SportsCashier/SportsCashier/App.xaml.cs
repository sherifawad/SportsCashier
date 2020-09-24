using SportsCashier.DataBase;
using SportsCashier.Models;
using SportsCashier.Services.DialogService;
using SportsCashier.Services.NavigationService;
using SportsCashier.ViewModels;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("fa-regular-400.ttf", Alias ="RegularAwesome")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "SolidAwesome")]
[assembly: ExportFont("fa-brands-400.ttf", Alias = "BrandsAwesome")]
namespace SportsCashier
{
    public partial class App : Application
    {
        //public static IContainer Container;

        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] {
                "SwipeView_Experimental"
             });

            DependencyService.Register<IDialogService, ShellDialogService>();
            DependencyService.Register<INavigationService, ShellRoutingService>();
            DependencyService.Register<IGenericDbRepository<MemberModel>, GenericDbRepository<MemberModel>>();
            DependencyService.Register<IGenericDbRepository<PlayerSport>, GenericDbRepository<PlayerSport>>();
            DependencyService.Register<IGenericDbRepository<PlayerModel>, GenericDbRepository<PlayerModel>>();
            DependencyService.Register<IGenericDbRepository<Sport>, GenericDbRepository<Sport>>();
            MainPage = new AppShell();


            ////class used for registration
            //var builder = new ContainerBuilder();
            ////scan and register all classes in the assembly
            //var dataAccess = Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(dataAccess)
            //       .AsImplementedInterfaces()
            //       .AsSelf();
            //builder.RegisterType<EmptyViewModel>();
            //builder.RegisterType<ShellRoutingService>().As<INavigationService>().SingleInstance();
            //builder.RegisterType<ShellDialogService>().As<IDialogService>().SingleInstance();
            //builder.RegisterType<GenericDbRepository<Sport>>().As<IGenericDbRepository<Sport>>().SingleInstance();
            //builder.RegisterType<GenericDbRepository<PlayerModel>>().As<IGenericDbRepository<PlayerModel>>().SingleInstance();
            //builder.RegisterType<GenericDbRepository<PlayerSport>>().As<IGenericDbRepository<PlayerSport>>().SingleInstance();
            //builder.RegisterType<GenericDbRepository<MemberModel>>().As<IGenericDbRepository<MemberModel>>().SingleInstance();

            ////get container
            //Container = builder.Build();
            //MainPage = Container.Resolve<AppShell>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

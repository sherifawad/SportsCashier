using SportsCashier.DataBase;
using SportsCashier.Extensions;
using SportsCashier.Models;
using SportsCashier.Services.DialogService;
using SportsCashier.Services.MessagingService;
using SportsCashier.Services.NavigationService;
using SportsCashier.ViewModels;
using SportsCashier.Views;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("fa-regular-400.ttf", Alias = "RegularAwesome")]
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
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            AddSports().SafeFireAndForget(false);

            DependencyService.Register<IDialogService, ShellDialogService>();
            DependencyService.Register<INavigationService, ShellRoutingService>();
            DependencyService.Register<IMessagingService, MessagingService>();
            DependencyService.Register<IGenericDbRepository<Invoice>, GenericDbRepository<Invoice>>();
            DependencyService.Register<IGenericDbRepository<MemberModel>, GenericDbRepository<MemberModel>>();
            DependencyService.Register<IGenericDbRepository<PlayerSport>, GenericDbRepository<PlayerSport>>();
            DependencyService.Register<IGenericDbRepository<PlayerModel>, GenericDbRepository<PlayerModel>>();
            DependencyService.Register<IGenericDbRepository<Sport>, GenericDbRepository<Sport>>();
            MainPage = new MembershipPlayersDetailView();
            //MainPage = new AppShell();
        }
        private async Task AddSports()
        {
            var _sportRepo = new GenericDbRepository<Sport>();
            var anySport = await _sportRepo.GetFirstOrDefault(x => x.Id != 0);
            if (anySport == null)
            {
                Sports.ForEach(async (x) => await _sportRepo.Insert(x));
            }
        }

        public List<Sport> Sports
        {
            private set { }
            get => new List<Sport>
                {
                    new Sport()
                    {
                        SportName = "قدم",
                        SportType = "عادية",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "قدم",
                        SportType = "مميزة",
                        SportPrice = 250
                    },
                    new Sport()
                    {
                        SportName = "يد",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "سله",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "طائره",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "تنس طاوله",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "تايكندو",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "جودو",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "كاراتيه",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "كمال اجسام",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "رفع اثقال",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "ملاكمه",
                        SportType = "عادي",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "العاب قوي",
                        SportType = "عادي",
                        SportPrice = 200
                    },
                    new Sport()
                    {
                        SportName = "سلاح شيش",
                        SportType = "عادي",
                        SportPrice = 200
                    },
                    new Sport()
                    {
                        SportName = "خماسي حديث",
                        SportType = "ثنائي",
                        SportPrice = 200
                    },
                    new Sport()
                    {
                        SportName = "خماسي حديث",
                        SportType = "ثلاثي",
                        SportPrice = 250
                    },
                    new Sport()
                    {
                        SportName = "تنس أرضي",
                        SportType = "مدارس",
                        SportPrice = 250
                    },
                    new Sport()
                    {
                        SportName = "تنس أرضي",
                        SportType = "تجهيزي",
                        SportPrice = 300
                    },
                    new Sport()
                    {
                        SportName = "تنس أرضي",
                        SportType = "صباحي",
                        SportPrice = 200
                    },
                    new Sport()
                    {
                        SportName = "تنس أرضي",
                        SportType = "فريق",
                        SportPrice = 350
                    },
                    new Sport()
                    {
                        SportName = "اسكواش",
                        SportType = "مدارس",
                        SportPrice = 350
                    },
                    new Sport()
                    {
                        SportName = "اسكواش",
                        SportType = "اكاديمي",
                        SportPrice = 350
                    },
                    new Sport()
                    {
                        SportName = "اسكواش",
                        SportType = "فريق",
                        SportPrice = 400
                    },
                    new Sport()
                    {
                        SportName = "جيم",
                        SportType = "عادي",
                        SportPrice = 200
                    },
                    new Sport()
                    {
                        SportName = "جيم",
                        SportType = "حديد",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "جمباز",
                        SportType = "عادي",
                        SportPrice = 200
                    },
                    new Sport()
                    {
                        SportName = "جمباز",
                        SportType = "ايروبكس",
                        SportPrice = 300
                    },
                    new Sport()
                    {
                        SportName = "سباحة",
                        SportType = "تعليم كبار",
                        SportPrice = 500
                    },
                    new Sport()
                    {
                        SportName = "سباحة",
                        SportType = "قطاع البطولة",
                        SportPrice = 150
                    },
                    new Sport()
                    {
                        SportName = "سباحة",
                        SportType = "مدارس",
                        SportPrice = 250
                    },
                    new Sport()
                    {
                        SportName = "سباحة",
                        SportType = "تجهيزي",
                        SportPrice = 250
                    },
                    new Sport()
                    {
                        SportName = "سباحة",
                        SportType = "ممارسة",
                        SportPrice = 250
                    }

                };

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

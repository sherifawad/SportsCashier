using SportsCashier.Models;
using SportsCashier.Services.MessagingService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SportsCashier.ViewModels.PlayersPayment
{
    public class SportPickerViewModel : BaseViewModel
    {

        #region Private Property
        private SportModel selectedSport;
        private SportCaegory selectedCaegory;
        #endregion

        #region Public Property


        public Sport GetSport { get; set; }
        public ICollection<SportModel> SportsList { get; set; }

        public ICollection<SportCaegory> SportCaegoriesList { get; set; }




        public SportCaegory SelectedCaegory
        {
            get => selectedCaegory;
            set
            {

                selectedCaegory = value;

                if (value != null)
                {

                    GetSport = new Sport
                    {
                        SportName = selectedSport.SportName,
                        SportCaegory = value

                    };

                }


            }
        }

        public SportModel SelectedSport
        {
            get => selectedSport;
            set
            {
                selectedSport = value;
                SportCaegoriesList = SportsList.FirstOrDefault(s => s.SportName == value.SportName).Caegories;
            }
        }


        #endregion


        #region Constructor

        public SportPickerViewModel()
        {

            SportsList = GetPickerItems();

        }

        #endregion




        private ICollection<SportModel> GetPickerItems()
        {
            return new List<SportModel>
            {
                new SportModel
                {
                    SportName = "قدم",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادية"
                        },
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "مميزة"
                        },
                    }

                },
                new SportModel
                {
                    SportName = "يد",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "سله",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "طائره",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "تنس طاوله",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "تايكندو",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "جودو",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "كاراتيه",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "كمال اجسام",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "رفع اثقال",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "ملاكمه",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "العاب قوي ( تراك )",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 200,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "سلاح شيش",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 200,
                        SportType = "عادي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "خماسي حديث",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 200,
                        SportType = "ثنائي"
                        },
                        new SportCaegory
                        {
                        SportPrice = 250,
                        SportType = "ثلاثي"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "تنس أرضي",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 250,
                        SportType = "مدارس"
                        },
                        new SportCaegory
                        {
                        SportPrice = 300,
                        SportType = "تجهيزي"
                        },
                        new SportCaegory
                        {
                        SportPrice = 200,
                        SportType = "صباحي"
                        },
                        new SportCaegory
                        {
                        SportPrice = 350,
                        SportType = "فريق"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "اسكواش",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 350,
                        SportType = "مدارس"
                        },
                        new SportCaegory
                        {
                        SportPrice = 350,
                        SportType = "اكاديمي"
                        },
                        new SportCaegory
                        {
                        SportPrice = 400,
                        SportType = "فريق"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "جيم غي الصالة",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 200,
                        SportType = "عادي"
                        },
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "حديد"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "جمباز",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 200,
                        SportType = "عادي"
                        },
                        new SportCaegory
                        {
                        SportPrice = 300,
                        SportType = "ايروبكس"
                        }
                    }

                },
                new SportModel
                {
                    SportName = "سباحة",
                    Caegories = new List<SportCaegory>
                    {
                        new SportCaegory
                        {
                        SportPrice = 250,
                        SportType = "ممارسة"
                        },
                        new SportCaegory
                        {
                        SportPrice = 250,
                        SportType = "تجهيزي"
                        },
                        new SportCaegory
                        {
                        SportPrice = 250,
                        SportType = "مدارس"
                        },
                        new SportCaegory
                        {
                        SportPrice = 150,
                        SportType = "قطاع البطولة"
                        },
                        new SportCaegory
                        {
                        SportPrice = 500,
                        SportType = "تعليم كبار"
                        }
                    }

                },


            };
        }
    }
}

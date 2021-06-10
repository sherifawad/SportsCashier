using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsCashier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MembershipPlayersDetailView : ContentPage
    {
        public ICommand BookmarkAlertCommand => new Command<MockSportModel>((sport) => 
        {
            if(sport != null)
            {
                DisplayAlert("Sport", sport.Name, "Ok");
            }
        });
        public MembershipPlayersDetailView()
        {
            InitializeComponent();

            BindingContext = this;
        }
        public ObservableCollection<MockPlayerData> Players => new ObservableCollection<MockPlayerData>
        {
            new MockPlayerData{
                Name = "Sherif",
                Sports = new List<MockSportModel>
                {
                    new MockSportModel {
                        Name = "FootBall", Price = 150
                    } ,
                    new MockSportModel {
                        Name = "HandBall", Price = 150
                    } ,
                    new MockSportModel {
                        Name = "Swimming", Price = 250
                    } ,
                    new MockSportModel {
                        Name = "BasketBall", Price = 150
                    }
                },
                Histories = new List<History>
                {
                    new History
                    {
                        Date = DateTime.Now.AddMonths(1),
                        Sports = new List<MockSportModel>
                        {
                            new MockSportModel {
                                Name = "FootBall", Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=2500
                            } ,
                            new MockSportModel {
                                Name = "HandBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=100
                            } ,
                            new MockSportModel {
                                Name = "Swimming", Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=50
                            } ,
                            new MockSportModel {
                                Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=1
                            }
                        }
                    },
                    new History
                    {
                        Date = DateTime.Now.AddMonths(5),
                        Sports = new List<MockSportModel>
                        {
                            new MockSportModel {
                                Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=10
                            } ,
                            new MockSportModel {
                                Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=11
                            }
                        }
                    },
                }
            },
            new MockPlayerData{
                Name = "Ahmed",
                Sports = new List<MockSportModel>
                {
                    new MockSportModel {
                        Name = "Jodo", Price = 150
                    } ,
                    new MockSportModel {
                        Name = "Swimming", Price = 250
                    }
                },
                Histories = new List<History>
                {
                    new History
                    {
                        Date = DateTime.Now.AddMonths(9),
                        Sports = new List<MockSportModel>
                        {
                            new MockSportModel {
                                Name = "Jodo", Price = 150, ReceiteDate = DateTime.Now.AddMonths(9).AddDays(2), ReceiteNumber=5000
                            } ,
                            new MockSportModel {
                                Name = "Swimming", Price = 250, ReceiteDate = DateTime.Now.AddMonths(9).AddDays(9), ReceiteNumber=5100
                            }
                        }
                    },
                    new History
                    {
                        Date = DateTime.Now.AddMonths(5),
                        Sports = new List<MockSportModel>
                        {
                            new MockSportModel {
                                Name = "Jodo", Price = 150, ReceiteDate = DateTime.Now.AddMonths(5), ReceiteNumber=5023
                            } ,
                            new MockSportModel {
                                Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5), ReceiteNumber=0443
                            }
                        }
                    },
                }
            },
            new MockPlayerData{
                Name = "Aya",
                Sports = new List<MockSportModel>
                {
                    new MockSportModel {
                        Name = "Jumanastic", Price = 180
                    } ,
                    new MockSportModel {
                        Name = "Swimming", Price = 200
                    } ,
                    new MockSportModel {
                        Name = "VollyBall", Price = 150
                    }
                },
                Histories = new List<History>
                {
                    new History
                    {
                        Date = DateTime.Now.AddMonths(3),
                        Sports = new List<MockSportModel>
                        {
                            new MockSportModel {
                                Name = "Jumanastic", Price = 200
                            } ,
                            new MockSportModel {
                                Name = "Swimming", Price = 250
                            } ,
                            new MockSportModel {
                                Name = "VollyBall", Price = 150
                            }
                        }
                    },
                    new History
                    {
                        Date = DateTime.Now.AddMonths(5),
                        Sports = new List<MockSportModel>
                        {
                            new MockSportModel {
                                Name = "Jumanastic", Price = 200
                            } ,
                            new MockSportModel {
                                Name = "Swimming", Price = 225
                            }
                        }
                    }
                }
            }
        };

        public MockPlayerData MockPlayer => new MockPlayerData
        {
            Name = "Sherif",
            Sports = new List<MockSportModel>
            {
                new MockSportModel {
                    Name = "FootBall", Price = 150
                } ,
                new MockSportModel {
                    Name = "HandBall", Price = 150
                } ,
                new MockSportModel {
                    Name = "Swimming", Price = 250
                } ,
                new MockSportModel {
                    Name = "BasketBall", Price = 150
                }
            },
            Histories = new List<History>
            {
                new History
                {
                    Date = DateTime.Now.AddMonths(1),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "FootBall", Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1003
                        } ,
                        new MockSportModel {
                            Name = "HandBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=1004
                        } ,
                        new MockSportModel {
                            Name = "Swimming", Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=1500
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(1), ReceiteNumber=0023
                        }
                    }
                },
                new History
                {
                    Date = DateTime.Now.AddMonths(5),
                    Sports = new List<MockSportModel>
                    {
                        new MockSportModel {
                            Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=1523
                        } ,
                        new MockSportModel {
                            Name = "BasketBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(9), ReceiteNumber=0003
                        }
                    }
                },
            }

        };

        public ObservableCollection<MockSportModel> Items => new ObservableCollection<MockSportModel> {
            new MockSportModel {
                Name = "FootBall", Price = 150
            } ,
            new MockSportModel {
                Name = "HandBall", Price = 150
            } ,
            new MockSportModel {
                Name = "Swimming", Price = 250
            } ,
            new MockSportModel {
                Name = "BasketBall", Price = 150
            }

        };
    }


    public class MockPlayerData
    {
        public string Name { get; set; }
        public List<MockSportModel> Sports { get; set; } = new List<MockSportModel>();
        public List<History> Histories { get; set; } = new List<History>();
        public MockSportModel? AlertSport
        {
            get
            {
                MockSportModel? result = null;
                // loop through all sport alert
                foreach (var alertSport in Histories.Select(x => x.AlertSport))
                {
                    if (result != null)
                    {
                        // if the alert time for sport alert is sooner than the rsult and within 45 days
                        // set the result value to the current sport alert
                        if (alertSport != null && result.Alert > alertSport.Alert.AddDays(45))
                        {
                            result = alertSport;
                        }
                    }
                    else
                    {
                        result = alertSport;
                    }
                }
                return result;
            }
        }
    }

    public class MockSportModel
    {
        private DateTime receiteDate;

        public string Name { get; set; }
        public double Price { get; set; }
        public int ReceiteNumber { get; set; }
        public DateTime ReceiteDate
        {
            get => receiteDate;
            set
            {
                receiteDate = value;
                Alert = receiteDate.AddMonths(1);
            }
        }
        public DateTime Alert { get; set; }
    }

    public class History
    {
        public DateTime Date { get; set; }
        public List<MockSportModel> Sports { get; set; } = new List<MockSportModel>();

        public MockSportModel? AlertSport
        {
            get
            {
                MockSportModel? result = null;
                // loop through all sport alert
                foreach (var sport in Sports)
                {
                    if (result != null)
                    {
                        // if the alert time for sport alert is sooner than the rsult
                        // set the result value to the current sport alert
                        if (result.Alert > sport.Alert)
                        {
                            result = sport;
                        }
                    }
                    else
                    {
                        result = sport;
                    }
                }
                return result;

            }
        }

    }
}
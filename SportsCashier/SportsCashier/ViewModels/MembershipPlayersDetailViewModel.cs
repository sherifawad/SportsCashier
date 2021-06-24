using SportsCashier.Common;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace SportsCashier.ViewModels
{
    public class MembershipPlayersDetailViewModel : ViewModelBase
    {
        #region Public properties

        private ObservableCollection<MockPlayerData> _Players;
        public ObservableCollection<MockPlayerData> Players
        {
            get => _Players; 
            set => SetProperty(ref _Players, value);
        }


        #endregion


        #region Public Command
        public IAsyncValueCommand<MockPlayerData> EditPalyerCommand { get; set; }
        public IAsyncValueCommand<List<MockSportModel>> BookmarkAlertCommand { get; set; }

        #endregion

        public MembershipPlayersDetailViewModel()
        {
            Players = new ObservableCollection<MockPlayerData>();
            BookmarkAlertCommand = new AsyncValueCommand<List<MockSportModel>>(BookmarkAlertAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
            EditPalyerCommand = new AsyncValueCommand<MockPlayerData>(EditPalyerAsync, onException: ex => Debug.WriteLine(ex), allowsMultipleExecutions: false);
        }

        public override Task InitializeAsync()
        {
            Players = new ObservableCollection<MockPlayerData> {
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
                                Name = "Swimming", Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=50
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
                                Name = "Jumanastic", Price = 200, ReceiteDate = DateTime.Now.AddMonths(3).AddDays(2), ReceiteNumber=2500
                            } ,
                            new MockSportModel {
                                Name = "Swimming", Price = 250, ReceiteDate = DateTime.Now.AddMonths(3).AddDays(2), ReceiteNumber=2500
                            } ,
                            new MockSportModel {
                                Name = "VollyBall", Price = 150, ReceiteDate = DateTime.Now.AddMonths(3).AddDays(2), ReceiteNumber=2500
                            }
                        }
                    },
                    new History
                    {
                        Date = DateTime.Now.AddMonths(5),
                        Sports = new List<MockSportModel>
                        {
                            new MockSportModel {
                                Name = "Jumanastic", Price = 200, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=2500
                            } ,
                            new MockSportModel {
                                Name = "Swimming", Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=2500
                            }
                        }
                    }
                }
            }
            };
            return base.InitializeAsync();
        }

        #region Commands Methods
        private async ValueTask EditPalyerAsync(MockPlayerData arg)
        {
            await _navigationService.PushAsync<EditPlayerDetailsViewModel>($"{nameof(EditPlayerDetailsViewModel.PlayerId)}={arg.Id}");
        }

        private async ValueTask BookmarkAlertAsync(List<MockSportModel> arg)
        {
            string message = string.Empty;
            if (arg != null && arg.Count > 0)
            {
                foreach (var sport in arg)
                {
                    message += $"{sport.Name}\n";
                }
                await _dialogService.DisplayAlert("Sports", message, "Ok");
            }

        }

        #endregion
    }
}

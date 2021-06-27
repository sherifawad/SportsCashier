using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsCashier.Common.Services
{
    public class MockDataStore : IDataStore<MockPlayerData>
    {
        readonly List<MockPlayerData> items;

        public MockDataStore()
        {
            items = new List<MockPlayerData>()
            {
                new MockPlayerData{
                Id = Guid.NewGuid().ToString(),
                Name = "Sherif",
                Hide = true,
                Sports = new List<MockSportModel>
                {
                    new MockSportModel {
                        Name = "FootBall", Price = 150
                    } ,
                    new MockSportModel {
                        Name = "HandBall", Price = 150
                    } ,
                    new MockSportModel {
                        Name = "Swimming - Private Group", Price = 600
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
                            },
                            new MockSportModel {
                                Name = "Tennis - Morning Private Single", Price = 60, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=11
                            },
                            new MockSportModel {
                                Name = "BasketBall", Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=10
                            },
                            new MockSportModel {
                                Name = "Tennis - Morning Private Single", Price = 70, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=121
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
                Id = Guid.NewGuid().ToString(),
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
                Id = Guid.NewGuid().ToString(),
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
        }

        public async Task<bool> AddItemAsync(MockPlayerData item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(MockPlayerData item)
        {
            var oldItem = items.Where((MockPlayerData arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((MockPlayerData arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MockPlayerData> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MockPlayerData>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}

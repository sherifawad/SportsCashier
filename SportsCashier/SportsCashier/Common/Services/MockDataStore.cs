using DataBase.Models;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsCashier.Common.Services
{
    public class MockDataStore : IDataStore<Player>
    {
        readonly List<Player> items;

        public MockDataStore()
        {
            items = new List<Player>()
            {
                new Player{
                Id = 1,
                Name = "Sherif",
                Hide = true,
                Sports = new List<int>{ 5000300, 5000900, 5002108,5000500},

                Histories = new List<History>
                {
                    new History
                    {
                        Date = DateTime.Now.AddMonths(1),
                        Sports = new List<SportHistory>
                        {
                            new SportHistory {
                                Code = 5000300, Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=2500
                            } ,
                            new SportHistory {
                                Code = 5000900, Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(2), ReceiteNumber=100
                            } ,
                            new SportHistory {
                                Code = 5002101, Price = 200, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=50
                            } ,
                            new SportHistory {
                                Code = 5000500, Price = 150, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=1
                            },
                            new SportHistory {
                                Code = 5001504, Price = 60, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=11
                            },
                            new SportHistory {
                                Code = 5000500, Price = 135, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=10
                            },
                            new SportHistory {
                                Code = 5001504, Price = 70, ReceiteDate = DateTime.Now.AddMonths(1).AddDays(1), ReceiteNumber=121
                            }
                        }
                    },
                    new History
                    {
                        Date = DateTime.Now.AddMonths(5),
                        Sports = new List<SportHistory>
                        {
                            new SportHistory {
                                Code = 5002101, Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=10
                            } ,
                            new SportHistory {
                                Code = 5000500, Price = 150, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=11
                            }
                        }
                    },
                }
            },
            new Player{
                Id = 2,
                Name = "Ahmed",
                Sports = new List<int>{ 5000300, 5002101},
                Histories = new List<History>
                {
                    new History
                    {
                        Date = DateTime.Now.AddMonths(9),
                        Sports = new List<SportHistory>
                        {
                            new SportHistory {
                                Code = 5001300, Price = 150, ReceiteDate = DateTime.Now.AddMonths(9).AddDays(2), ReceiteNumber=5000
                            } ,
                            new SportHistory {
                                Code = 5002101, Price = 250, ReceiteDate = DateTime.Now.AddMonths(9).AddDays(9), ReceiteNumber=5100
                            }
                        }
                    },
                    new History
                    {
                        Date = DateTime.Now.AddMonths(5),
                        Sports = new List<SportHistory>
                        {
                            new SportHistory {
                                Code = 5001300, Price = 150, ReceiteDate = DateTime.Now.AddMonths(5), ReceiteNumber=5023
                            } ,
                            new SportHistory {
                               Code = 5002101, Price = 225, ReceiteDate = DateTime.Now.AddMonths(5), ReceiteNumber=0443
                            }
                        }
                    },
                }
            },
            new Player{
                Id = 3,
                Name = "Aya",
                Sports = new List<int>{ 5002001, 5002101, 5000700},
                Histories = new List<History>
                {
                    new History
                    {
                        Date = DateTime.Now.AddMonths(3),
                        Sports = new List<SportHistory>
                        {
                            new SportHistory {
                                Code = 5002001, Price = 200, ReceiteDate = DateTime.Now.AddMonths(3).AddDays(2), ReceiteNumber=2500
                            } ,
                            new SportHistory {
                                Code = 5002101, Price = 250, ReceiteDate = DateTime.Now.AddMonths(3).AddDays(2), ReceiteNumber=2500
                            } ,
                            new SportHistory {
                                Code = 5000700, Price = 150, ReceiteDate = DateTime.Now.AddMonths(3).AddDays(2), ReceiteNumber=2500
                            }
                        }
                    },
                    new History
                    {
                        Date = DateTime.Now.AddMonths(5),
                        Sports = new List<SportHistory>
                        {
                            new SportHistory {
                                Code = 5002001, Price = 200, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=2500
                            } ,
                            new SportHistory {
                                Code = 5002101, Price = 225, ReceiteDate = DateTime.Now.AddMonths(5).AddDays(2), ReceiteNumber=2500
                            }
                        }
                    }
                }
            }
            };
        }

        public async Task<bool> AddItemAsync(Player item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Player item)
        {
            var oldItem = items.Where((Player arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Player arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Player> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Player>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}

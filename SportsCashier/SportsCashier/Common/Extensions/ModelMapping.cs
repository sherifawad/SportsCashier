using DataBase.Models;
using SportsCashier.Common.Helpers;
using SportsCashier.Common.Models;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCashier.Common.Extensions
{
    public static class ModelMapping
    {
        public static SportHistoryDto ToSportHistoryDto(this SportHistory source)
        {
            if (source == null)
                return null;

            var sportData = SportsData.GetSpoertsData.FirstOrDefault(x => x.Code == source.Code);

            return new SportHistoryDto
            {
                Id = source.Id,
                Code = source.Code,
                Name = sportData?.Name,
                Alert = source.Alert,
                Discount = source.Discount,
                Icon = sportData.Icon,
                Price = source.Price,
                ReceiteDate = source.ReceiteDate,
                ReceiteNumber = source.ReceiteNumber

            };
        }
        public static SportHistory ToSportHistory(this SportHistoryDto source)
        {
            if (source == null)
                return null;

            return new SportHistory
            {
                Id = source.Id,
                Code = source.Code,
                Alert = source.Alert,
                Discount = source.Discount,
                Price = source.Price,
                ReceiteDate = source.ReceiteDate,
                ReceiteNumber = source.ReceiteNumber

            };
        }
        public static List<SportHistoryDto> ToSportHistoryDtoList(this IEnumerable<SportHistory> source)
        {
            if (source == null)
                return null;
            List<SportHistoryDto> list = new List<SportHistoryDto>();

            foreach (var item in source)
            {
                list.Add(item.ToSportHistoryDto());
            }

            return list;
        }

        public static List<SportHistory> ToSportHistoryList(this IEnumerable<SportHistoryDto> source)
        {
            if (source == null)
                return null;

            List<SportHistory> list = new List<SportHistory>();

            foreach (var item in source)
            {
                list.Add(item.ToSportHistory());
            }

            return list;
        }

        public static HistoryDto ToHistoryDto(this History source)
        {
            if (source == null)
                return null;

            return new HistoryDto
            {
                Id = source.Id,
                Date = source.Date,
                Sports = source.Sports.ToSportHistoryDtoList()
            };
        }
        public static History ToHistory(this HistoryDto source)
        {
            if (source == null)
                return null;

            return new History
            {
                Id = source.Id,
                Date = source.Date,
                Sports = source.Sports.ToSportHistoryList()
            };
        }
        public static List<History> ToHistoryList(this List<HistoryDto> source)
        {
            if (source == null)
                return null;

            List<History> list = new List<History>();

            foreach (var item in source)
            {
                list.Add(item.ToHistory());
            }

            return list;
        }
        public static List<HistoryDto> ToHistoryDtoList(this List<History> source)
        {
            if (source == null)
                return null;

            List<HistoryDto> list = new List<HistoryDto>();

            foreach (var item in source)
            {
                list.Add(item.ToHistoryDto());
            }

            return list;
        }

        public static PlayerDto ToPlayerDto(this Player source)
        {
            return new PlayerDto
            {
                Id = source.Id,
                Hide = source.Hide,
                Image = source.Image,
                Name = source.Name,
                Histories = source.Histories.ToHistoryDtoList(),
                Sports = CodeToSportDataHelpers.ConvertToSportList(source.Sports)

            };
        }
        public static List<PlayerDto> ToPlayerDtoList(this List<Player> source)
        {
            List<PlayerDto> list = new List<PlayerDto>();

            foreach (var item in source)
            {
                list.Add(item.ToPlayerDto());
            }

            return list;
        }
    }
}

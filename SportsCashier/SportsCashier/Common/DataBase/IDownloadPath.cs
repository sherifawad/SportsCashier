using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsCashier.Common.DataBase
{
    public interface IDownloadPath
    {
        Task<string> Get();
    }
}

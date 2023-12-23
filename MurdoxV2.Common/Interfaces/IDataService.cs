using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MurdoxV2.Common.Interfaces
{
    public interface IDataService
    {
        string GetBotToken();
        string GetConnectionString();
        Task<T> LoadJsonAsync<T>(string path);
    }
}

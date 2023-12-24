using MurdoxV2.Common.Interfaces;
using MurdoxV2.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MurdoxV2.Common.Services
{
    public class DataService : IDataService
    {
        public string[] GetApplicationPrefix()
        {
            var jsonFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "config.json"));
            var json = JsonSerializer.Deserialize<ConfigJson?>(jsonFile);
            return json!.Prefix;
        }

        public string GetBotToken()
        {
            var jsonFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "secrets.json"));
            var json = JsonSerializer.Deserialize<ConfigJson?>(jsonFile);
            return json!.Token;
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public async Task<T> LoadJsonAsync<T>(string path)
        {
            var json = await File.ReadAllTextAsync(path);
            var genericJson = JsonSerializer.Deserialize<T>(json);
            return genericJson!;
        }
    }
}

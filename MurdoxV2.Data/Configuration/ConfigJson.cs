using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MurdoxV2.Data.Configuration
{
    public class ConfigJson
    {
        public string? Token { get; set; }
        public string? ConnectionString { get; set; }
    }
}

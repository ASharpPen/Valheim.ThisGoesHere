using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valheim.ThisGoesHere
{
    public class Config
    {
        public List<ConfigEntry> Entries { get; set; }
    }

    public class ConfigEntry
    {
        public string FromFile { get; set; }

        public string ToFile { get; set; }
    }
}

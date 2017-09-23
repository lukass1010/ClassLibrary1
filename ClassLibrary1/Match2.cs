using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Match2
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("date")]
        public Date date { get; set; }
        [JsonProperty("time")]
        public Time time { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("goals")]
        public List<object> goals { get; set; }
        [JsonProperty("hostTeam")]
        public HostTeam hostTeam { get; set; }
        [JsonProperty("guestTeam")]
        public GuestTeam guestTeam { get; set; }

    }
}

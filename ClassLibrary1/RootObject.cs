using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Chronology
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("calendarType")]
        public string calendarType { get; set; }
    }

    public class Footballer1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public int number { get; set; }
        public List<object> goals { get; set; }
    }

    public class HostTeam
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string league { get; set; }
        public List<Footballer1> footballers { get; set; }
        public List<object> matches { get; set; }
        public override string ToString()
        {
            return name.ToString();
        }
    }

    public class Footballer2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public int number { get; set; }
        public List<object> goals { get; set; }
    }

    public class GuestTeam
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string league { get; set; }
        public List<Footballer2> footballers { get; set; }
        public List<object> matches { get; set; }
        public override string ToString()
        {
            return name.ToString(); 
        }
    }


    public class GoalsResponse
    {
        public List<Goal> goals;
    }
   

    public class RootObject
    {
        [JsonProperty("matches")]
        public List<Match2> matches { get; set; }
    }
}

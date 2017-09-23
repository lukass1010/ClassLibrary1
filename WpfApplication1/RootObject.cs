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

    public class Date
    {
        public int year { get; set; }
        public string month { get; set; }
        public string era { get; set; }
        public int dayOfMonth { get; set; }
        public string dayOfWeek { get; set; }
        public int dayOfYear { get; set; }
        public bool leapYear { get; set; }
        public int monthValue { get; set; }
        public Chronology chronology { get; set; }
        public override string ToString()
        {
            return year.ToString() + "-" + monthValue.ToString() + "-" + dayOfMonth.ToString() ;
        }
    }

    public class Time
    {
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public int nano { get; set; }
        public override string ToString()
        {
            return hour.ToString() + ":" + minute.ToString() ;
        }
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

    public class RootObject
    {
        [JsonProperty("matches")]
        public List<Match2> matches { get; set; }
    }
}

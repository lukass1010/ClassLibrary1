using Newtonsoft.Json;
using System;

namespace ClassLibrary1
{
    public class Goal
    {
        public int id;
        public DateTime time;
        public string teamName;
        public Footballer footballer;
        public Match2 match;

        [JsonIgnore]
        public string timeString;
    //    public Team team;

        public override string ToString()
        {
            return footballer.name + " " + footballer.surname + "---> " + timeString;
        }
    }
}

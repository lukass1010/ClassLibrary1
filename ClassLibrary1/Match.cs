using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public class Match
    {
        public int id;
        public string date;
        public string time;
        public string city;
        public List<Goal> goals = new List<Goal>();
        public Team hostTeam;
        public Team guestTeam;

        public override string ToString()
        {
            return date.ToString();
        }
    }


}

using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public class Team
    {
        public int id;
        public string name;
        public string city;
        public string league;
        public List<Footballer> footballers = new List<Footballer>();
        public List<Match> matches = new List<Match>();

        public override string ToString()
        {
            return name ;
        }
    }
}

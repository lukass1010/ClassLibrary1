using System.Collections.Generic;

namespace ClassLibrary1
{
    public class Footballer
    {
        public int id;
        public string name;
        public string surname;
        public int age;
        public int number;
        public List<Goal> goals = new List<Goal>();
        public Team team;
        public override string ToString()
        {
            return name + " " + surname;
        }
    }
}

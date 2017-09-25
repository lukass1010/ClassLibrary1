using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for AddGoal.xaml
    /// </summary>
    public partial class AddGoal : Page
    {
        public ClassLibrary1.Match2 currentMatch;
        public ClassLibrary1.Goal newGoal = new ClassLibrary1.Goal();
        public ClassLibrary1.Team selectedTeam;

        public AddGoal()
        {
            InitializeComponent();
        }

        public AddGoal(ClassLibrary1.Match2 selectedMatch, ClassLibrary1.Team selectedTeam)
        {
            InitializeComponent();
            this.currentMatch = selectedMatch;
            this.selectedTeam = selectedTeam;

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new System.Uri(@"http://localhost:8080/");

            textBoxName.Text = selectedTeam.name.ToString();
            comboBox1.ItemsSource = selectedTeam.footballers;
            /* //load teams
             HttpResponseMessage response = client.GetAsync($"Liga/teams/").Result;
             if (response.IsSuccessStatusCode)
             {
                 comboBox.ItemsSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassLibrary1.Team>>(response.Content.ReadAsStringAsync().Result);

             }
             else
                 MessageBox.Show("Error - couldn't load any Teams");
             //load footballers
        */
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /* // current match
             HttpClient client = new HttpClient();
             client.BaseAddress = new System.Uri(@"http://localhost:8080/");
             string getPoint = "Liga/matches/" + currentMatch.id.ToString();
             HttpResponseMessage response = client.GetAsync(getPoint).Result;

             if (response.IsSuccessStatusCode)
             {

                 ClassLibrary1.Match2 result = Newtonsoft.Json.JsonConvert.DeserializeObject<ClassLibrary1.Match2>(response.Content.ReadAsStringAsync().Result);
                 if (result != null)
                 {
                 //    newGoal.match = new ClassLibrary1.Match();
                 //    newGoal.match.id = result.id;

                 }
             }
             else
                 MessageBox.Show("Error - match");
             // current footballer
             newGoal.teamName = comboBox.SelectedItem.ToString();
             newGoal.footballer = (ClassLibrary1.Footballer)comboBox1.SelectedItem;*/
            //posting new goal
            var timeSpan = TimeSpan.Parse(textBoxTime.Text);
            var emptyDate = DateTime.Now;
            var time = emptyDate.Date + timeSpan;
            newGoal.time = time;
            //var time = new Time();
            //var timeString = textBoxTime.Text.Split(':');
            //time.hour = int.Parse(timeString.ElementAt(0));
            //time.minute = int.Parse(timeString.ElementAt(1));
            newGoal.teamName = selectedTeam.name;
            newGoal.match = currentMatch;
            newGoal.footballer =(ClassLibrary1.Footballer) comboBox1.SelectedItem;

            newGoal.match.goals = new List<object>();
            newGoal.footballer.goals = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(@"http://localhost:8080/");
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(newGoal), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"Liga/goals", content).Result;


            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Succefully added new goal");
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Error - couldn't add new goal");
                this.NavigationService.Navigate(new MainPage());

            }


        }

   

      
    }
}

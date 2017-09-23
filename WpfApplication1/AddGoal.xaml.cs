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

        public AddGoal(ClassLibrary1.Match2 selectedMatch)
        {
            InitializeComponent();
            this.currentMatch = selectedMatch;

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(@"http://localhost:8080/");
            //load teams
            HttpResponseMessage response = client.GetAsync($"Liga/teams/").Result;
            if (response.IsSuccessStatusCode)
            {
                comboBox.ItemsSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassLibrary1.Team>>(response.Content.ReadAsStringAsync().Result);

            }
            else
                MessageBox.Show("Error - couldn't load any Teams");
            //load footballers
       
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            newGoal.time = textBoxTime.Text;
            // current match
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
            newGoal.footballer = (ClassLibrary1.Footballer)comboBox1.SelectedItem;
            //posting new goal

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(newGoal), Encoding.UTF8, "application/json");
            response = client.PostAsync($"Liga/goals", content).Result;


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

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem != null)
            {
                this.selectedTeam = (ClassLibrary1.Team)comboBox.SelectedItem;
                comboBox1.ItemsSource = selectedTeam.footballers;

            }

        }
    }
}

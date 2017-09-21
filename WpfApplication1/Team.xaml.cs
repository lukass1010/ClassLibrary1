using System.Windows.Controls;
using ClassLibrary1;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Team.xaml
    /// </summary>
    public partial class Team : Page
    {
        public ClassLibrary1.Team selectedTeam;
      

        public Team()
        {
            InitializeComponent();
           
        }

        public Team(ClassLibrary1.Team selectedTeam)
        {
            InitializeComponent();
            this.selectedTeam = selectedTeam;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            textBoxName.Text = selectedTeam.name;
            textBoxCity.Text = selectedTeam.city;
            textBoxLeague.Text = selectedTeam.league;

        }

        private void buttonPlayers_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Footballers(selectedTeam));
        }

        private void buttonMatches_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Matches(selectedTeam));
        }

        private void buttonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");
          //  client.DefaultRequestHeaders.Accept.Clear();
          //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            selectedTeam.name = textBoxName.Text;
            selectedTeam.city = textBoxCity.Text;
            selectedTeam.league = textBoxLeague.Text;




            var putPoint = "Liga/teams/" + selectedTeam.id.ToString();

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(selectedTeam), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(putPoint, content).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Succefully updated team");
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Error - couldn't update team");
                return;
            }
        }
    }
}

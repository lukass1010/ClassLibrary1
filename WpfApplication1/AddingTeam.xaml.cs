using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for AddingTeam.xaml
    /// </summary>
    public partial class AddingTeam : Page
    {
        public AddingTeam()
        {
            InitializeComponent();
        }

        private void buttonGoToMain_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ClassLibrary1.Team sendTeam = new ClassLibrary1.Team();

            sendTeam.name = textTeamName.Text;
            sendTeam.city = textTeamCity.Text;
            sendTeam.league = textTeamLeague.Text;

           

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(sendTeam), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"Liga/teams", content).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Succefully added new team");
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Error - couldn't add new team");
                return;
            }


        }
    }
}

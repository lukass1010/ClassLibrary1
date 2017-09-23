using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ClassLibrary1.Team selectedTeam = new ClassLibrary1.Team();
        public ClassLibrary1.Footballer selectedFootballer = new ClassLibrary1.Footballer();
        public List<ClassLibrary1.Team> teams = new List<ClassLibrary1.Team>();

        public MainPage()
        {
            InitializeComponent();

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(@"http://localhost:8080/");
            HttpResponseMessage response = client.GetAsync($"Liga/teams/").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content;//.ReadAsStringAsync();  
                     
               listBox.ItemsSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassLibrary1.Team>>(response.Content.ReadAsStringAsync().Result);

            }
            else
                MessageBox.Show("Error - couldn't load any Teams");
                


        }

        private void buttonAddTeam_Click(object sender, RoutedEventArgs e)
        {
           this.NavigationService.Navigate(new AddingTeam());
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

          this.selectedTeam = (ClassLibrary1.Team)listBox.SelectedItem;


        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Team(selectedTeam));
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var putPoint = "Liga/teams/" + selectedTeam.id.ToString();

            HttpResponseMessage response = client.DeleteAsync(putPoint).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Succefully deleted team");
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Error - couldn't delete team");
                return;
            }
        }
    }
}

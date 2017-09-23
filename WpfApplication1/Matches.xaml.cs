using ClassLibrary1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for Matches.xaml
    /// </summary>
    public partial class Matches : Page
    {
       
        public ClassLibrary1.Team selectedTeam;
        public ClassLibrary1.Match2 currentMatch;
        
        public Matches()
        {
            
            InitializeComponent();
        }
        public Matches(ClassLibrary1.Team selectedTeam)
        {
            this.selectedTeam = selectedTeam;
            InitializeComponent();

             HttpClient client = new HttpClient();
             client.BaseAddress = new System.Uri(@"http://localhost:8080/");
             HttpResponseMessage response = client.GetAsync($"Liga/matches/").Result;
             client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (response.IsSuccessStatusCode)
            {

                RootObject result = JsonConvert.DeserializeObject<RootObject>(response.Content.ReadAsStringAsync().Result);
                if (result != null)
                {
                    listBox.ItemsSource = result.matches.FindAll(i => ((i.hostTeam.name == selectedTeam.name) || (i.guestTeam.name == selectedTeam.name)));
                    listBox.DisplayMemberPath = "date";

                }
            }
            else
                MessageBox.Show("Error - couldn't load any Teams");
                


        }

  

        private void buttonAddMatch_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddMatch(selectedTeam));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditMatch(currentMatch));
        }

        private void buttonSeeGoals_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedItem != null)
            this.NavigationService.Navigate(new Goals(currentMatch,selectedTeam));
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.currentMatch = (ClassLibrary1.Match2)listBox.SelectedItem;
            if (currentMatch != null)
            {
                textTime.Text = currentMatch.time.ToString();
                textCity.Text = currentMatch.city.ToString();
                textHome.Text = currentMatch.hostTeam.name.ToString();
                textAway.Text = currentMatch.guestTeam.name.ToString();
            }
        }
  
    }
}

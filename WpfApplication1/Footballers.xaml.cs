using ClassLibrary1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for Footballers.xaml
    /// </summary>
    public partial class Footballers : Page
    {
        public ClassLibrary1.Team selectedTeam;
        public ClassLibrary1.Footballer selectedFootballer;
        public Footballers()
        {
            InitializeComponent();
        }
        public Footballers(ClassLibrary1.Team selectedTeam)
        {
            InitializeComponent();
            this.selectedTeam = selectedTeam;

            listBox.ItemsSource =selectedTeam.footballers;

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Footballer(selectedTeam));
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            
            if((ClassLibrary1.Footballer)listBox.SelectedItem == null)
            {
                MessageBox.Show("Please select player");
                return;
            }
            else
            {

                this.NavigationService.Navigate(new EditPlayer(selectedFootballer,selectedTeam));
            }
                
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            selectedFootballer.team = null;
            var putPoint = "Liga/footballers/" + selectedFootballer.id.ToString();
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(selectedFootballer, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(putPoint, content).Result;



            if (!response.IsSuccessStatusCode)
                MessageBox.Show("Problem with deleting");

            else
            {
                MessageBox.Show("Successfully deleted a player");
                
            }
            this.NavigationService.Navigate(new MainPage());
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedFootballer = (ClassLibrary1.Footballer)listBox.SelectedItem;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddMatch.xaml
    /// </summary>
    public partial class AddMatch : Page
    {
        public ClassLibrary1.Team selectedTeam;
        public ClassLibrary1.Match newMatch; 
        public AddMatch()
        {
            InitializeComponent();
        }

        public AddMatch(ClassLibrary1.Team selectedTeam)
        {
            this.selectedTeam = selectedTeam;
          
            InitializeComponent();

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(@"http://localhost:8080/");

            //  var content = new StringContent(Newtonsoft.Json.JsonConvert.DeserializeObject(selectedTeam), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.GetAsync($"Liga/teams/").Result;
            if (response.IsSuccessStatusCode)
            {
               comboBox.ItemsSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassLibrary1.Team>>(response.Content.ReadAsStringAsync().Result);
               comboBox1.ItemsSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassLibrary1.Team>>(response.Content.ReadAsStringAsync().Result);

            }
            else
                MessageBox.Show("Error - couldn't load any Teams");




        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            newMatch = new ClassLibrary1.Match();
   
            {

                newMatch.date = textBoxDate.Text;// DateTime.Parse(textBoxDate.Text);
                newMatch.time = textBoxTime.Text;//  DateTime.Parse(textBoxTime.Text);
                newMatch.city = textBoxCity.Text;
                
                newMatch.hostTeam =(ClassLibrary1.Team) comboBox.SelectedItem;
                newMatch.guestTeam = (ClassLibrary1.Team)comboBox1.SelectedItem; 

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(@"http://localhost:8080/");
                //client.DefaultRequestHeaders.Accept.Clear();
                // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //posting new match
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(newMatch), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync($"Liga/matches", content).Result;


                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Succefully added new match");
                    this.NavigationService.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("Error - couldn't add new match");
                    this.NavigationService.Navigate(new MainPage());

                }
                //posting updated team

                /*
                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri(@"http://localhost:8080/");
               // selectedTeam.matches.Add(newMatch);

                var putPoint = "Liga/teams/" + selectedTeam.id.ToString();

                var content2 = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(selectedTeam), Encoding.UTF8, "application/json");
                HttpResponseMessage response2 = client.PutAsync(putPoint, content2).Result;
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
                */

                /* 
                 var putPoint = "Liga/teams/" + selectedTeam.id.ToString();

                 var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(selectedTeam, Formatting.Indented, new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 }), Encoding.UTF8, "application/json");
                 HttpResponseMessage response = client.PutAsync(putPoint, content).Result;

                 if (response.IsSuccessStatusCode)
                 {
                     MessageBox.Show("Succefully added new match");
                     this.NavigationService.Navigate(new MainPage());
                 }
                 else
                 {
                     MessageBox.Show("Error - couldn't add new match");
                     this.NavigationService.Navigate(new MainPage());

                 }*/
            }
          
        }

   
    }
}

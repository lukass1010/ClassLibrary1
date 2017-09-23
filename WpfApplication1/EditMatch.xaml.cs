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
    /// Interaction logic for EditMatch.xaml
    /// </summary>
    public partial class EditMatch : Page
    {
        public ClassLibrary1.Match2 currentMatch;
        public ClassLibrary1.Match updateMatch = new ClassLibrary1.Match();

        public EditMatch()
        {
            InitializeComponent();
        }

        public EditMatch(ClassLibrary1.Match2 editMatch)
        {
            this.currentMatch = editMatch;
            InitializeComponent();
            if (currentMatch != null)
            {
                textBoxDate.Text = currentMatch.date.ToString();
                textBoxTime.Text = currentMatch.time.ToString();
                textBoxCity.Text = currentMatch.hostTeam.city.ToString();
                comboBox.SelectedValue = currentMatch.hostTeam.name.ToString();
                comboBox1.SelectedValue = currentMatch.guestTeam.name.ToString();
            }
            else
            {
                MessageBox.Show("Couldn't load this match");
                this.NavigationService.Navigate(new MainPage());

            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(@"http://localhost:8080/");
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
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            updateMatch.city = textBoxCity.Text;
            updateMatch.date = textBoxDate.Text;
            updateMatch.time = textBoxTime.Text;
            updateMatch.hostTeam = (ClassLibrary1.Team)comboBox.SelectedItem; ;
            updateMatch.guestTeam = (ClassLibrary1.Team)comboBox1.SelectedItem;


            //var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(currentMatch), Encoding.UTF8, "application/json");
            var putPoint = $"Liga/matches/" + currentMatch.id.ToString();
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(updateMatch, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(putPoint, content).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Succefully saved changes");
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Error - couldn't save changes");
                return;
            }


        }
    }
}

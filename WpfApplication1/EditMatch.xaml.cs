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
        public ClassLibrary1.Match currentMatch;
        public EditMatch()
        {
            InitializeComponent();
        }

        public EditMatch(ClassLibrary1.Match editMatch)
        {
            this.currentMatch = editMatch;
            InitializeComponent();
            if (currentMatch != null)
            {
                textBoxDate.Text = currentMatch.date.ToString();
                textBoxTime.Text = currentMatch.time.ToString();
                textBoxCity.Text = currentMatch.hostTeam.city.ToString();
                textBoxHost.Text = currentMatch.hostTeam.name.ToString();
                textBoxGuest.Text = currentMatch.guestTeam.name.ToString();

            }
            else
            {
                MessageBox.Show("Couldn't load this match");
                this.NavigationService.Navigate(new MainPage());

            }
         
        }      


        private void button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        /*    if (DateTime.TryParse(textBoxDate.Text, out currentMatch.date)) ;
            else
            {
                MessageBox.Show("Invalid date!");
                return;
            }

            if (DateTime.TryParse(textBoxTime.Text, out currentMatch.time)) ;
            else
            {
                MessageBox.Show("Invalid time!");
                return;
            }
*/
            currentMatch.city = textBoxCity.Text;


            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(currentMatch), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"Liga/matches", content).Result;
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

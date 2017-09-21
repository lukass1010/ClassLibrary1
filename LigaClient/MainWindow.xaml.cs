using LigaClient.ViewModel;
using System.Windows;

namespace LigaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonF_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new FootballerViewModel();
        }

        private void ButtonG_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new GoalViewModel();
        }

        private void ButtonM_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new MatchViewModel();
        }

        private void ButtonT_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TeamViewModel();
        }

        /*
private void button_Click(object sender, RoutedEventArgs e)
{
int id = int.Parse(textBox.Text);
HttpClient client = new HttpClient();
client.BaseAddress = new Uri("localhost:80/");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

Footballer footballer = null;
HttpResponseMessage response = client.GetAsync($"footballers/{id}").Result;
if (response.IsSuccessStatusCode)
{
string jsonString = response.Content.ReadAsStringAsync().Result;
footballer = Newtonsoft.Json.JsonConvert.DeserializeObject<Footballer>(jsonString);
}
nameBlock.Text = footballer.name;
surnameBlock.Text = footballer.surname;

}
*/
    }
}

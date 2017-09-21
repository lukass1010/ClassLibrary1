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
    /// Interaction logic for EditPlayer.xaml
    /// </summary>
    public partial class EditPlayer : Page
    {
        public ClassLibrary1.Footballer editPlayer;
        public ClassLibrary1.Team selectedTeam;

        public EditPlayer()
        {
            InitializeComponent();
        }
        public EditPlayer(ClassLibrary1.Footballer editPlayer, ClassLibrary1.Team selectedTeam)
        {
            InitializeComponent();
           
            this.editPlayer = editPlayer;
            this.selectedTeam = selectedTeam;
            textBoxName.Text = this.editPlayer.name;
            textBoxSurname.Text = this.editPlayer.surname;
            textBoxAge.Text = this.editPlayer.age.ToString();
            textBoxNumber.Text = this.editPlayer.number.ToString();



        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

          
            editPlayer.name = textBoxName.Text;
            editPlayer.surname = textBoxSurname.Text;
            editPlayer.team = selectedTeam;

            if (!int.TryParse(textBoxAge.Text, out editPlayer.age))
            {
                MessageBox.Show("Age must be a number");
                return;
            }
            else if (!int.TryParse(textBoxNumber.Text, out editPlayer.number))
            {
                MessageBox.Show("Number must be a number");
                return;
            }
            else
            {




                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(@"http://localhost:8080/");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                 var putPoint = "Liga/footballers/" + editPlayer.id.ToString();
                 var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(editPlayer, Formatting.Indented, new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 }), Encoding.UTF8, "application/json");
                 HttpResponseMessage response = client.PutAsync(putPoint, content).Result;


            
                if (!response.IsSuccessStatusCode)
                    MessageBox.Show("Problem with updating player");

                else
                {
                    MessageBox.Show("Successfully udpated a player");
                    
                }
                this.NavigationService.Navigate(new MainPage());

            }

        }
    }
}

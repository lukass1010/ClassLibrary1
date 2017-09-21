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
    /// Interaction logic for Footballer.xaml
    /// </summary>
    public partial class Footballer : Page
    {
        public ClassLibrary1.Team selectedTeam;
        public ClassLibrary1.Footballer newFootballer;
        public Footballer()
        {
         
            InitializeComponent();

        }

        public Footballer(ClassLibrary1.Team selectedTeam)
        {
            this.selectedTeam = selectedTeam;
          
            InitializeComponent();
        }


     

        private void button_Click(object sender, RoutedEventArgs e)
        {
            newFootballer = new ClassLibrary1.Footballer();
            newFootballer.name = textBoxName.Text;
            newFootballer.surname = textBoxSurname.Text;
            newFootballer.team = selectedTeam;

            if (int.TryParse(textBoxAge.Text, out newFootballer.age)) ;
            else
            {
                MessageBox.Show("Age must be a number!");
                return;
            }

            if (int.TryParse(textBoxNumber.Text, out newFootballer.number)) ;
            else
            {
                MessageBox.Show("Number must be a number!");
                return;
            }


           // selectedTeam.footballers.Add(newFootballer);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:8080/");

            //posting new player
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(newFootballer), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"Liga/footballers/" ,content).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Succefully added new player");
            }
 
            else
                MessageBox.Show("unSuccesfully added new player");
            this.NavigationService.Navigate(new MainPage());
            


        }
    }
}

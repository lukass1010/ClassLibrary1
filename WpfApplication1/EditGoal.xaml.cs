using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for EditGoal.xaml
    /// </summary>
    public partial class EditGoal : Page
    {
        public ClassLibrary1.Match2 currentMatch;
        public ClassLibrary1.Goal editGoal;
        public ClassLibrary1.Team selectedTeam;
        public EditGoal()
        {
            InitializeComponent();
        }

        public EditGoal(ClassLibrary1.Goal editGoal,ClassLibrary1.Team selectedTeam)
        {
            if (editGoal != null)
            {
                InitializeComponent();
                this.editGoal = editGoal;
                this.selectedTeam = selectedTeam;
                comboBox1.ItemsSource = selectedTeam.footballers;

                textBoxTime.Text = editGoal.time.ToString();
                textBoxCity.Text = editGoal.teamName.ToString();

           
            }
            else
            {
                MessageBox.Show("Couldn't load this match");
                //this.NavigationService.Navigate(new MainPage());
                return;

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            editGoal.time = textBoxTime.Text;
            editGoal.footballer = (ClassLibrary1.Footballer)comboBox1.SelectedItem;

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(@"http://localhost:8080/");
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(editGoal), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"Liga/goals", content).Result;


            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Succefully added new goal");
                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("Error - couldn't add new goal");
                this.NavigationService.Navigate(new MainPage());

            }

        }

      
    }
}

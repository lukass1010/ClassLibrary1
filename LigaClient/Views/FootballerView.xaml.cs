using ClassLibrary1;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace LigaClient.Views
{
    /// <summary>
    /// Interaction logic for FootballerView.xaml
    /// </summary>
    public partial class FootballerView : UserControl
    {
        public FootballerView()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void buttonRead_Click(object sender, RoutedEventArgs e)
        {
            int age  = int.Parse(textBoxAge.Text);
            string name = textBoxName.Text;
            string surname = textBoxSurname.Text;
            int number = int.Parse(textBoxAge.Text);



            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("localhost:80/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Footballer footballer = null;
            HttpResponseMessage response = client.GetAsync($"footballers/{surname}").Result; 
            if (!response.IsSuccessStatusCode)
            {

                response = client.GetAsync($"footballers/{name}").Result;

            }
            else
            {
                response = client.GetAsync($"footballers/{number}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    response = client.GetAsync($"footballers/{age}").Result;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content.ReadAsStringAsync().Result;
                footballer = Newtonsoft.Json.JsonConvert.DeserializeObject<Footballer>(jsonString);
            }

            textBoxAge.Text = Convert.ToString(footballer.age);
            textBoxName.Text = footballer.name;
            textBoxSurname.Text = footballer.surname;
            textBoxNumber.Text = Convert.ToString(footballer.number);

        }

        private void buttonWrite_Click(object sender, RoutedEventArgs e)
        {
           

        }
    }
}

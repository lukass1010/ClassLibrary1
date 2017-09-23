﻿using System;
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
    /// Interaction logic for Goals.xaml
    /// </summary>
    public partial class Goals : Page
    {
        ClassLibrary1.Match2 currentMatch;
        ClassLibrary1.Goal currentGoal;
        public Goals()
        {
            InitializeComponent();
        }

        public Goals(ClassLibrary1.Match2 currentMatch)
        {
            InitializeComponent();
            this.currentMatch = currentMatch;

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(@"http://localhost:8080/");
            HttpResponseMessage response = client.GetAsync($"Liga/goals/{currentMatch.goals}").Result;
            if (response.IsSuccessStatusCode)
            {
                listBox.ItemsSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassLibrary1.Team>>(response.Content.ReadAsStringAsync().Result);

            }
            else
                MessageBox.Show("Error - couldn't load any goals");




        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditGoal(currentGoal));
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.currentGoal = (ClassLibrary1.Goal)listBox.SelectedItem;
        }
    }
}

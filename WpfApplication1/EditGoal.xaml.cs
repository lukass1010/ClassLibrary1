using System;
using System.Collections.Generic;
using System.Linq;
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
        public EditGoal()
        {
            InitializeComponent();
        }

        public EditGoal(ClassLibrary1.Goal editGoal)
        {
            if (editGoal != null)
            {
                InitializeComponent();
               
                textBoxTime.Text = editGoal.time.ToString();
                textBoxCity.Text = editGoal.teamName.ToString();
           
            }
            else
            {
                MessageBox.Show("Couldn't load this match");
                this.NavigationService.Navigate(new MainPage());

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

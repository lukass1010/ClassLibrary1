using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Main.NavigationService.Navigate(new MainPage());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new MainPage());
        }
    }
}

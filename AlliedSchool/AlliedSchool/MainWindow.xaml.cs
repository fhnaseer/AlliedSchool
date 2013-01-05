using AlliedSchool.ViewModels;
using System.Windows;

namespace AlliedSchool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WelcomePageViewModel(this);
        }
    }
}

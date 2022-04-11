using System.Windows;
using Case2.ViewModels;
using Prism.Ioc;
using Prism.Regions;

namespace Case2.Views
{
    /// <summary>
    /// Interaction logic for Login2.xaml
    /// </summary>
    public partial class Login2 : Window
    {
        public Login2()
        {
            InitializeComponent();
            var vm = DataContext as Login2ViewModel;
            if (vm != null)
            {
                vm.LoginComplete += result => DialogResult = result;
            }
        }
    }
}

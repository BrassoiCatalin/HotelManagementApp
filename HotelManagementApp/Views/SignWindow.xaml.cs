using HotelManagementApp.ViewModel;
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
using System.Windows.Shapes;

namespace HotelManagementApp.Views
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignWindow : Window
    {
        public SignWindow()
        {
            InitializeComponent();

            DataContext = new SignViewModel();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            (DataContext as SignViewModel).SignWindow = this;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}

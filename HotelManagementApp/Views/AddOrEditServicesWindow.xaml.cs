using HotelManagementApp.Models;
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
    /// Interaction logic for AddOrEditServicesWindow.xaml
    /// </summary>
    public partial class AddOrEditServicesWindow : Window
    {
        public AddOrEditServicesWindow(ServicesToShow? servicesToShow = null)
        {
            InitializeComponent();

            DataContext = new AddOrEditServicesViewModel(servicesToShow);
            (DataContext as AddOrEditServicesViewModel).AddOrEditServicesWindow = this;
        }
    }
}

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
    /// Interaction logic for AddOrEditRoomWindow.xaml
    /// </summary>
    public partial class AddOrEditRoomWindow : Window
    {
        public AddOrEditRoomWindow(RoomToShow? roomToShow = null)
        {
            InitializeComponent();

            DataContext = new AddOrEditRoomViewModel(roomToShow);
            (DataContext as AddOrEditRoomViewModel).AddOrEditRoomWindow = this;
        }
    }
}

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
    /// Interaction logic for RoomEditorWindow.xaml
    /// </summary>
    public partial class RoomEditorWindow : Window
    {
        public RoomEditorWindow()
        {
            InitializeComponent();

            DataContext = new RoomEditorViewModel();
            (DataContext as RoomEditorViewModel).RoomEditorWindow = this;
            //RoomEditorWindow is a property from VM
        }
    }
}

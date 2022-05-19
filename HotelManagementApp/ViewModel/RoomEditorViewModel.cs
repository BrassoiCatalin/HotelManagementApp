using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
using HotelManagementApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class RoomEditorViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...

        private ObservableCollection<RoomToShow> _roomToEditList;

        private RoomToShow _selectedRoom;

        #endregion


        #region Public Properties...

        public ObservableCollection<RoomToShow> RoomToEditList
        {
            get { return _roomToEditList; }
            set
            {
                _roomToEditList = value;
                NotifyPropertyChanged(nameof(RoomToEditList));
            }
        }

        public bool CanExecuteCommands { get; set; } = false;
        public RoomToShow SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;

                if (_selectedRoom != null)
                {
                    CanExecuteCommands = true;
                }

                NotifyPropertyChanged(nameof(SelectedRoom));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; set; }
        #endregion



        #region Constructors...

        public RoomEditorViewModel()
        {
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit, param => CanExecuteCommands);
            DeleteCommand = new RelayCommand(Delete, param => CanExecuteCommands);
            BackCommand = new RelayCommand(Back);

            string path = "../../../Images/image.jpg";

            RoomToEditList = new ObservableCollection<RoomToShow>()
            {
                new RoomToShow
                {
                    Id = 1,
                    Type = "Type1",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path),
                    Number = 1
                },
                new RoomToShow
                {
                    Id = 2,
                    Type = "Type2",
                    Price = 2.45f,
                    Features = "AC, Watever",
                    Image = File.ReadAllBytes(path),
                    Number = 2
                },
                new RoomToShow
                {
                    Id = 3,
                    Type = "Type3",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path),
                    Number = 3
                },
                new RoomToShow
                {
                    Id = 4,
                    Features = "AC, Watever",
                    Type = "Type4",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path),
                    Number = 4
                },
            };

        }

        #endregion






        #region Private Methods...

        private void Add(object param)
        {
            AddOrEditRoomWindow window = new AddOrEditRoomWindow();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            App.Current.MainWindow.Show();
        }
        //daca trebe cumva schimbat la astea doua ca sa putem da back?
        private void Edit(object param)//enabled doar cand e un element selectat
        {
            AddOrEditRoomWindow window = new AddOrEditRoomWindow();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            App.Current.MainWindow.Show();
        }

        private void Delete(object param)//enabled doar cand e un element selectat
        {
            
        }

        private void Back(object param)
        {

        }
        #endregion


        #region MyRegion

        #endregion


    }
}

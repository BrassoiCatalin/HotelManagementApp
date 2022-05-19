using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
using HotelManagementApp.Views;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class HotelViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...

        private Visibility _isClientConnected;
        private Visibility _isAdminConnected;
        private Visibility _isEmployeeConnected;

        private ObservableCollection<RoomToShow> _roomToShowList;
        #endregion



        #region Public Properties...

        public Visibility IsClientConnected
        {
            get { return _isClientConnected; }
            set
            {
                _isClientConnected = value;
                NotifyPropertyChanged(nameof(IsClientConnected));
            }
        }

        public Visibility IsAdminConnected
        {
            get { return _isAdminConnected; }
            set
            {
                _isAdminConnected = value;
                NotifyPropertyChanged(nameof(IsAdminConnected));
            }
        }


        public Visibility IsEmployeeConnected
        {
            get { return _isEmployeeConnected; }
            set
            {
                _isEmployeeConnected = value;
                NotifyPropertyChanged(nameof(IsEmployeeConnected));
            }
        }


        public ObservableCollection<RoomToShow> RoomToShowList
        {
            get { return _roomToShowList; }
            set
            {
                _roomToShowList = value;
                NotifyPropertyChanged(nameof(RoomToShowList));
            }
        }

        public ICommand OffersCommand { get; }
        public ICommand HistoryCommand { get; }
        public ICommand RoomsCommand { get; }
        public ICommand ServicesCommand { get; }
        public ICommand ReservationsCommand { get; }

        #endregion



        #region Constructors...

        public HotelViewModel()
        {
            OffersCommand = new RelayCommand(Offers);
            HistoryCommand = new RelayCommand(History);
            RoomsCommand = new RelayCommand(Rooms);
            ServicesCommand = new RelayCommand(Services);
            ReservationsCommand = new RelayCommand(Reservations);

            if (ConnectedUser.User == null)
            {
                IsClientConnected = IsAdminConnected = IsEmployeeConnected = Visibility.Hidden;
            }
            else if (ConnectedUser.User.RoleId == 1)
            {
                IsAdminConnected = IsClientConnected = IsEmployeeConnected = Visibility.Visible;
            }
            else if (ConnectedUser.User.RoleId == 2)
            {
                IsClientConnected = Visibility.Visible;
                IsAdminConnected = IsEmployeeConnected = Visibility.Hidden;
            }
            else if (ConnectedUser.User.RoleId == 3)
            {
                IsClientConnected = IsEmployeeConnected = Visibility.Visible;
                IsAdminConnected = Visibility.Hidden;
            }

            string path = "../../../Images/image.jpg";

            RoomToShowList = new ObservableCollection<RoomToShow>()
            {
                new RoomToShow
                {
                    Id = 1,
                    Type = "Type1",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path)
                },
                new RoomToShow
                {
                    Id = 2,
                    Type = "Type2",
                    Price = 2.45f,
                    Features = "AC, Watever",
                    Image = File.ReadAllBytes(path)
                },
                new RoomToShow
                {
                    Id = 3,
                    Type = "Type3",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path)
                },
                new RoomToShow
                {
                    Id = 4,
                    Features = "AC, Watever",
                    Type = "Type4",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path)
                },
            };
        }

        #endregion


        #region Private Methods...
        private void Offers(object param)
        {

        }

        private void History(object param)
        {

        }

        private void Rooms(object param)
        {
            RoomEditorWindow window = new RoomEditorWindow();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            App.Current.MainWindow.Show();
        }

        private void Services(object param)
        {

        }

        private void Reservations(object param)
        {

        }

        #endregion
    }
}

using HotelManagementApp.DataModels;
using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
using HotelManagementApp.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        private readonly HoteldbContext _hoteldbContext;
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
            _hoteldbContext = new HoteldbContext();

            OffersCommand = new RelayCommand(Offers);
            HistoryCommand = new RelayCommand(History);
            RoomsCommand = new RelayCommand(Rooms);
            ServicesCommand = new RelayCommand(Services);
            ReservationsCommand = new RelayCommand(Reservations);

            var allRoles = _hoteldbContext.UserRoles.FromSqlRaw("GetAllRoles").ToList();

            if (ConnectedUser.User == null)
            {
                IsClientConnected = IsAdminConnected = IsEmployeeConnected = Visibility.Hidden;
            }
            else if (ConnectedUser.User.RoleId == allRoles.Single(role => role.Role.Equals("Administrator")).Id)
            {
                IsAdminConnected = IsClientConnected = IsEmployeeConnected = Visibility.Visible;
            }
            else if (ConnectedUser.User.RoleId == allRoles.Single(role => role.Role.Equals("Client")).Id)
            {
                IsClientConnected = Visibility.Visible;
                IsAdminConnected = IsEmployeeConnected = Visibility.Hidden;
            }
            else if (ConnectedUser.User.RoleId == allRoles.Single(role => role.Role.Equals("Employee")).Id)
            {
                IsClientConnected = IsEmployeeConnected = Visibility.Visible;
                IsAdminConnected = Visibility.Hidden;
            }

            string path = "../../../Images/image.jpg";

            RoomToShowList = new ObservableCollection<RoomToShow>()
            {
                new RoomToShow
                {
                    Type = "Type1",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path)
                },
                new RoomToShow
                {
                    Type = "Type2",
                    Price = 2.45f,
                    Features = "AC, Watever",
                    Image = File.ReadAllBytes(path)
                },
                new RoomToShow
                {
                    Type = "Type3",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Image = File.ReadAllBytes(path)
                },
                new RoomToShow
                {
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
            OffersWindow window = new OffersWindow();
            GoToWindow(window);
        }

        private void History(object param)
        {
            MessageBox.Show("Modify this");
            return;

            RoomEditorWindow window = new RoomEditorWindow();
            GoToWindow(window);
        }

        private void Rooms(object param)
        {
            RoomEditorWindow window = new RoomEditorWindow();
            GoToWindow(window);
        }

        private void Services(object param)
        {
            MessageBox.Show("Modify this");
            return;

            RoomEditorWindow window = new RoomEditorWindow();
            GoToWindow(window);
        }

        private void Reservations(object param)
        {
            MessageBox.Show("Modify this");
            return;

            RoomEditorWindow window = new RoomEditorWindow();
            GoToWindow(window);
        }

        private void GoToWindow(Window window)
        {
            App.Current.MainWindow.Hide();
            window.ShowDialog();
            App.Current.MainWindow.Show();
        }

        #endregion
    }
}

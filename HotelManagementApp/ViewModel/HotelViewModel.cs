﻿using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
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

namespace HotelManagementApp.ViewModel
{
    internal class HotelViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...

        private Visibility _isClientConnected;
        private Visibility _isAdminConnected;
        private Visibility _isEmployeeConnected;

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

        #endregion



        #region Constructors...

        public HotelViewModel()
        {
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


        private ObservableCollection<RoomToShow> _roomToShowList;

        public ObservableCollection<RoomToShow> RoomToShowList
        {
            get { return _roomToShowList; }
            set
            {
                _roomToShowList = value;
                NotifyPropertyChanged(nameof(RoomToShowList));
            }
        }


        #region Methods...

        #endregion
    }
}
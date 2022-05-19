using HotelManagementApp.DataModels;
using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
using HotelManagementApp.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class AddOrEditRoomViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...
        private readonly HoteldbContext _hoteldbContext;
        private int _roomNumber;
        private List<string> _roomTypes;
        private string _selectedRoomType;

        private Visibility _isAddRoomWindow = Visibility.Hidden;
        private Visibility _isEditRoomWindow = Visibility.Hidden;

        private int _roomToShowNumber = 0;

        #endregion

        #region Public Properties...
        public AddOrEditRoomWindow AddOrEditRoomWindow { get; set; }
        public List<string> RoomTypes
        {
            get { return _roomTypes; }
            set
            {
                _roomTypes = value;
                NotifyPropertyChanged(nameof(RoomTypes));
            }
        }

        public string SelectedRoomType
        {
            get { return _selectedRoomType; }
            set
            {
                _selectedRoomType = value;
                NotifyPropertyChanged(nameof(SelectedRoomType));
            }
        }

        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                NotifyPropertyChanged(nameof(RoomNumber));
            }
        }


        public Visibility IsAddRoomWindow
        {
            get { return _isAddRoomWindow; }
            set
            {
                _isAddRoomWindow = value;
                NotifyPropertyChanged(nameof(IsAddRoomWindow));
            }
        }


        public Visibility IsEditRoomWindow
        {
            get { return _isEditRoomWindow; }
            set
            {
                _isEditRoomWindow = value;
                NotifyPropertyChanged(nameof(IsEditRoomWindow));
            }
        }

        public ICommand AddNewRoomCommand { get; }
        public ICommand EditRoomCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Constructors...
        public AddOrEditRoomViewModel(RoomToShow? roomToShow = null)
        {
            _hoteldbContext = new HoteldbContext();
            RoomTypes = _hoteldbContext.RoomTypes.Select(roomType => roomType.Description).ToList();

            if (roomToShow != null)
            {
                IsEditRoomWindow = Visibility.Visible;
                _roomToShowNumber = roomToShow.Number;
                RoomNumber = roomToShow.Number;
                SelectedRoomType = RoomTypes.FirstOrDefault(rooms => rooms.Equals(roomToShow.Type));
            }
            else
            {
                IsAddRoomWindow = Visibility.Visible;
                SelectedRoomType = RoomTypes.FirstOrDefault();
            }

            AddNewRoomCommand = new RelayCommand(AddNewRoom);
            EditRoomCommand = new RelayCommand(EditRoom);
            CancelCommand = new RelayCommand(Exit);
            
        }
        #endregion

        #region Private Methods...
        private void AddNewRoom(object param)
        {
            //SelectedRoomType 
            //RoomNumber

            if (RoomNumber < 1)
            {
                MessageBox.Show("Admin must insert a room number bigger than 0.");
                return;
            }

            if (_hoteldbContext.Rooms.Any(number => number.Number == RoomNumber))
            {
                MessageBox.Show("Admin must insert a number which is not yet in the database.");
                return;
            }

            //1.validate admin input 
            //1.0 If input is valid
            //1.1 If admin room is not existing already
            var room = new Room();
            room.Number = RoomNumber;
            room.RoomTypeId = _hoteldbContext.RoomTypes.FromSqlRaw($"GetRoomTypeIdByRoomTypeName '{SelectedRoomType}'").ToList().First().Id;
            //2.Add in database
            _hoteldbContext.Rooms.Add(room);
            ////3.Save changes
            _hoteldbContext.SaveChanges();

            //after this, redirectBack (windows)
            AddOrEditRoomWindow.Close();
        }
        private void EditRoom(object param)
        {
            //SelectedRoomType 
            //RoomNumber

            if (RoomNumber < 1)
            {
                MessageBox.Show("Admin must insert a room number bigger than 0.");
                return;
            }

            if (_hoteldbContext.Rooms.Any(number => number.Number == RoomNumber))
            {
                MessageBox.Show("Admin must insert a number which is not yet in the database.");
                return;
            }

            //get current room by number from db

            var storedRoom = _hoteldbContext.Rooms.Single(room => room.Number == _roomToShowNumber);

            //1.validate admin input 
            //1.0 If input is valid
            //1.1 If admin room is not existing already
            var room = new Room();
            room.Number = RoomNumber;
            room.RoomTypeId = _hoteldbContext.RoomTypes.FromSqlRaw($"GetRoomTypeIdByRoomTypeName '{SelectedRoomType}'").ToList().First().Id;

            //set the room from db to have the new values
            storedRoom.Number = room.Number;
            storedRoom.RoomTypeId = room.RoomTypeId;

            //2.Update in database

            ////3.Save changes
            _hoteldbContext.SaveChanges();

            //after this, redirectBack (windows)
            AddOrEditRoomWindow.Close();
        }

        private void Exit(object param)
        {
            AddOrEditRoomWindow.Close();
        }
        #endregion

    }
}

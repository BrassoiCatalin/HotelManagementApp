using HotelManagementApp.DataModels;
using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
using HotelManagementApp.Views;
using Microsoft.EntityFrameworkCore;
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

        private readonly HoteldbContext _hoteldbContext;

        #endregion


        #region Public Properties...
        public RoomEditorWindow RoomEditorWindow { get; set; }

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
        public ICommand BackCommand { get; }
        #endregion



        #region Constructors...

        public RoomEditorViewModel()
        {
            _hoteldbContext = new HoteldbContext();

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit, param => CanExecuteCommands);
            DeleteCommand = new RelayCommand(Delete, param => CanExecuteCommands);
            BackCommand = new RelayCommand(Back);
            
            ConstructRoomToShow();

        }

        #endregion


        #region Private Methods...
        private void ConstructRoomToShow()
        {
            var rooms = _hoteldbContext.Rooms.Include(room => room.RoomType).Where(room => room.Deleted == false).ToList();

            List<RoomToShow> roomsToShow = new List<RoomToShow>();

            foreach (var room in rooms)
            {
                float price;
                if (_hoteldbContext.PriceHistories.Where(price => price.RoomTypeId == room.RoomTypeId)
                    .FirstOrDefault() != null)
                {
                    price = _hoteldbContext.PriceHistories.Where(price => price.RoomTypeId == room.RoomTypeId)
                    .First().Price;
                }
                else
                {
                    price = 0.0f;
                }

                roomsToShow.Add(new RoomToShow()
                {
                    Number = room.Number,
                    Type = room.RoomType.Description,
                    Features = room.RoomType.Features,
                    Price = price
                });
            }


            foreach (var room in roomsToShow)
            {
                var imageIdByRoom = _hoteldbContext.RoomRoomImages.Include(x => x.RoomType)
                    .FirstOrDefault(x => x.RoomType.Description.Equals(room.Type));

                if (imageIdByRoom != null)
                    room.Image = _hoteldbContext.RoomImages.FirstOrDefault(image => image.Id == imageIdByRoom.RoomImageId).Picture;
            }

            RoomToEditList = new ObservableCollection<RoomToShow>(roomsToShow);
        }


        private void Add(object param)
        {
            AddOrEditRoomWindow window = new AddOrEditRoomWindow();

            window.ShowDialog();
        }
        
        private void Edit(object param)
        {
            if (SelectedRoom == null)
                return;

            AddOrEditRoomWindow window = new AddOrEditRoomWindow(SelectedRoom);
            
            window.ShowDialog();
        }

        private void Delete(object param)
        {
            if (SelectedRoom == null)
                return;

            var roomFromDatabase = _hoteldbContext.Rooms.Single(x => x.Number == SelectedRoom.Number);
            roomFromDatabase.Deleted = true;
            _hoteldbContext.SaveChanges();

            RoomToEditList.Remove(SelectedRoom);
        }

        private void Back(object param)
        {
            RoomEditorWindow.Close();
        }
        #endregion



    }
}

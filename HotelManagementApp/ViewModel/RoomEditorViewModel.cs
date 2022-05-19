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
            _hoteldbContext = new HoteldbContext();

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit, param => CanExecuteCommands);
            DeleteCommand = new RelayCommand(Delete, param => CanExecuteCommands);
            BackCommand = new RelayCommand(Back);

            string path = "../../../Images/image.jpg";

            RoomToEditList = new ObservableCollection<RoomToShow>()
            {
                new RoomToShow
                {
                    Type = "Single",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Number = 1
                },
                new RoomToShow
                {
                    Type = "Double",
                    Price = 2.45f,
                    Features = "AC, Watever",
                    Number = 2
                },
                new RoomToShow
                {
                    Type = "Single",
                    Features = "AC, Watever",
                    Price = 2.45f,
                    Number = 3
                },
                new RoomToShow
                {
                    Features = "AC, Watever",
                    Type = "Double",
                    Price = 2.45f,
                    Number = 4
                },
            };
            ConstructRoomToShow();

        }

        #endregion

        private void ConstructRoomToShow()
        {
            var rooms = _hoteldbContext.Rooms.Include(room => room.RoomType).ToList();

            List<RoomToShow> roomsToShow = new List<RoomToShow>();

            foreach (var room in rooms)
            {
                roomsToShow.Add(new RoomToShow()
                {
                    Number = room.Number,
                    Type = room.RoomType.Description,
                    Features = room.RoomType.Features,
                    Price = 0.0f
                });
            }


            foreach (var room in roomsToShow)
            {
                var imageIdByRoom = _hoteldbContext.RoomRoomImages.Include(x => x.RoomType)
                    .FirstOrDefault(x => x.RoomType.Description.Equals(room.Type));

                if(imageIdByRoom != null)
                    room.Image = _hoteldbContext.RoomImages.FirstOrDefault(image => image.Id == imageIdByRoom.RoomImageId).Picture;
            }

            RoomToEditList = new ObservableCollection<RoomToShow>(roomsToShow);
        }




        #region Private Methods...

        private void Add(object param)
        {
            AddOrEditRoomWindow window = new AddOrEditRoomWindow();

            App.Current.MainWindow.Hide();
            window.ShowDialog();
            App.Current.MainWindow.Show();
        }
        //daca trebe cumva schimbat la astea doua ca sa putem da back?
        private void Edit(object param)//enabled doar cand e un element selectat
        {
            AddOrEditRoomWindow window = new AddOrEditRoomWindow(SelectedRoom);

            App.Current.MainWindow.Hide();
            window.ShowDialog();
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

using HotelManagementApp.DataModels;
using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
using HotelManagementApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class AddOrEditOfferViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...

        private readonly HoteldbContext _hoteldbContext;

        private Visibility _isAddOfferWindow = Visibility.Hidden;
        private Visibility _isEditOfferWindow = Visibility.Hidden;

        private List<string> _roomTypes;
        private string _selectedRoomType;

        private string _offerName;
        private float _offerPrice;
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;

        private int _currentOfferId;
        #endregion


        #region Public Properties...
        public AddOrEditOfferWindow AddOrEditOfferWindow { get; set; }
        public Visibility IsAddOfferWindow
        {
            get { return _isAddOfferWindow; }
            set
            {
                _isAddOfferWindow = value;
                NotifyPropertyChanged(nameof(IsAddOfferWindow));
            }
        }


        public Visibility IsEditOfferWindow
        {
            get { return _isEditOfferWindow; }
            set
            {
                _isEditOfferWindow = value;
                NotifyPropertyChanged(nameof(IsEditOfferWindow));
            }
        }

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
        public string OfferName
        {
            get { return _offerName; }
            set
            {
                _offerName = value;
                NotifyPropertyChanged(nameof(OfferName));
            }
        }


        public float OfferPrice
        {
            get { return _offerPrice; }
            set
            {
                _offerPrice = value;
                NotifyPropertyChanged(nameof(OfferPrice));
            }
        }


        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyPropertyChanged(nameof(StartDate));
            }
        }


        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                NotifyPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand AddNewOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion




        #region Constructors...

        public AddOrEditOfferViewModel(OfferToShow? offerToShow = null)
        {
            _hoteldbContext = new HoteldbContext();
            RoomTypes = _hoteldbContext.RoomTypes.Select(roomType => roomType.Description).ToList();


            if (offerToShow != null)
            {
                IsEditOfferWindow = Visibility.Visible;
                OfferName = offerToShow.Name;
                OfferPrice = offerToShow.Price;
                StartDate = offerToShow.StartDate;
                EndDate = offerToShow.EndDate;
                SelectedRoomType = offerToShow.RoomType;
                _currentOfferId = offerToShow.Id;
            }
            else
            {
                IsAddOfferWindow = Visibility.Visible;
                SelectedRoomType = RoomTypes.FirstOrDefault();
            }

            AddNewOfferCommand = new RelayCommand(AddNewOffer);
            EditOfferCommand = new RelayCommand(EditOffer);
            CancelCommand = new RelayCommand(Exit);
        }

        private void Exit(object obj)
        {
            AddOrEditOfferWindow.Close();
        }

        private void EditOffer(object obj)
        {
            // 1. Get offer from db based on id
            Offer offer = _hoteldbContext.Offers.Single(offer => offer.Id == _currentOfferId);
            // 2. Updated offer from ui
            offer.Name = OfferName;
            offer.Price = OfferPrice;
            offer.StartDate = StartDate;
            offer.EndDate = EndDate;

            var roomOffer = _hoteldbContext.RoomOffers.Single(roomOffer => roomOffer.OfferId == _currentOfferId);

            roomOffer.RoomTypeId = _hoteldbContext.RoomTypes.Single(roomType => roomType.Description.Equals(SelectedRoomType)).Id;

            // 3. Save in db
            _hoteldbContext.SaveChanges();

            // 4. Redirect
            AddOrEditOfferWindow.Close();
        }

        private void AddNewOffer(object obj)
        {
            //1. Create the model (object from db) as Offer
            //2. Save object in db

            Offer offer = new Offer()
            {
                Name = OfferName,
                Price = OfferPrice,
                StartDate = StartDate,
                EndDate = EndDate
            };

            _hoteldbContext.Offers.Add(offer);
            _hoteldbContext.SaveChanges();

            RoomOffer roomOffer = new RoomOffer()
            {
                RoomTypeId = _hoteldbContext.RoomTypes.Single(roomType => roomType.Description.Equals(SelectedRoomType)).Id,
                OfferId = offer.Id
            };

            _hoteldbContext.RoomOffers.Add(roomOffer);
            _hoteldbContext.SaveChanges();

            //3 Redirect
            AddOrEditOfferWindow.Close();
        }

        #endregion


        #region Private Methods...

        #endregion
    }
}

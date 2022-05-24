using HotelManagementApp.DataModels;
using HotelManagementApp.Helpers;
using HotelManagementApp.Models;
using HotelManagementApp.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class OffersViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...

        private Visibility _isClientConnected;
        private Visibility _isAdminConnected;
        private Visibility _isEmployeeConnected;

        private ObservableCollection<OfferToShow> _offerToShowList;

        private readonly HoteldbContext _hoteldbContext;

        private OfferToShow _selectedOffer;

        #endregion

        #region Public Properties...
        public OffersWindow OffersWindow { get; set; }

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

        public ObservableCollection<OfferToShow> OfferToShowList
        {
            get { return _offerToShowList; }
            set
            {
                _offerToShowList = value;
                NotifyPropertyChanged(nameof(OfferToShowList));
            }
        }

        public ICommand SelectCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }

        public bool CanExecuteCommands { get; set; } = false;
        public OfferToShow SelectedOffer
        {
            get { return _selectedOffer; }
            set
            {
                _selectedOffer = value;

                if (_selectedOffer != null)
                {
                    CanExecuteCommands = true;
                }

                NotifyPropertyChanged(nameof(SelectedOffer));
            }
        }

        #endregion

        #region Constructors...

        public OffersViewModel()
        {
            SelectCommand = new RelayCommand(Select);
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit, param => CanExecuteCommands);
            DeleteCommand = new RelayCommand(Delete, param => CanExecuteCommands);
            BackCommand = new RelayCommand(Back);

            _hoteldbContext = new HoteldbContext();

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

            OfferToShowList = new ObservableCollection<OfferToShow>(ConstructRoomToShow());
        }

        

        #endregion

        #region Private Methods...

        private void Select(object param)
        {

        }

        private void Add(object param)
        {
            AddOrEditOfferWindow window = new AddOrEditOfferWindow();

            window.ShowDialog();
            OfferToShowList = new ObservableCollection<OfferToShow>(ConstructRoomToShow());
        }

        private void Edit(object param)
        {
            AddOrEditOfferWindow window = new AddOrEditOfferWindow(SelectedOffer);

            window.ShowDialog();
            OfferToShowList = new ObservableCollection<OfferToShow>(ConstructRoomToShow());
        }

        private void Delete(object param)
        {
            if (SelectedOffer == null)
                return;

            var offerFromDatabase = _hoteldbContext.Offers.First(offer => offer.Id == SelectedOffer.Id);
            var roomOffer = _hoteldbContext.RoomOffers.Single(roomOffer => roomOffer.OfferId == SelectedOffer.Id);

            offerFromDatabase.Deleted = roomOffer.Deleted = true;

            _hoteldbContext.SaveChanges();

            OfferToShowList.Remove(SelectedOffer);
        }

        private void Back(object param)
        {
            OffersWindow.Close();
        }

        private List<OfferToShow> ConstructRoomToShow()
        {
            var offers = _hoteldbContext.Offers.Include(offer => offer.RoomOffers).Where(offer => offer.Deleted == false).ToList();

            List<OfferToShow> offerToShow = new List<OfferToShow>();

            foreach (var offer in offers)
            {
                offerToShow.Add(new OfferToShow()
                {
                    Id = offer.Id,
                    Name = offer.Name,
                    Price = offer.Price,
                    StartDate = offer.StartDate,
                    EndDate = offer.EndDate,
                    RoomType = _hoteldbContext.RoomTypes.Single(roomType => roomType.Id == offer.RoomOffers.First().RoomTypeId).Description
                });
            }

            return offerToShow;
        }
        #endregion
    }
}

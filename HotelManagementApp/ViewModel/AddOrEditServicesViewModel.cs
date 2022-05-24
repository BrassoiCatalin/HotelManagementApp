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

    internal class AddOrEditServicesViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...

        private readonly HoteldbContext _hoteldbContext;

        private Visibility _isAddServiceWindow = Visibility.Hidden;
        private Visibility _isEditServiceWindow = Visibility.Hidden;

        private string _serviceDescription;
        private float _servicePrice;

        private int _currentServiceId;
        #endregion



        #region Public Properties...

        public Visibility IsAddServiceWindow
        {
            get { return _isAddServiceWindow; }
            set
            {
                _isAddServiceWindow = value;
                NotifyPropertyChanged(nameof(IsAddServiceWindow));

            }
        }

        public Visibility IsEditServiceWindow
        {
            get { return _isEditServiceWindow; }
            set
            {
                _isEditServiceWindow = value;
                NotifyPropertyChanged(nameof(IsEditServiceWindow));

            }
        }

        public AddOrEditServicesWindow AddOrEditServicesWindow { get; set; }

        public string ServiceDescription
        {
            get { return _serviceDescription; }
            set
            {
                _serviceDescription = value;
                NotifyPropertyChanged(nameof(ServiceDescription));
            }
        }

        public float ServicePrice
        {
            get { return _servicePrice; }
            set
            {
                _servicePrice = value;
                NotifyPropertyChanged(nameof(ServicePrice));
            }
        }

        public ICommand AddNewServiceCommand { get; }
        public ICommand EditServiceCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion



        #region Constructors...

        public AddOrEditServicesViewModel(ServicesToShow servicesToShow = null)
        {
            AddNewServiceCommand = new RelayCommand(AddNewExtra);
            EditServiceCommand = new RelayCommand(EditExtra);
            CancelCommand = new RelayCommand(Exit);

            _hoteldbContext = new HoteldbContext();

            if(servicesToShow != null)
            {
                IsEditServiceWindow = Visibility.Visible;
                ServiceDescription = servicesToShow.Description;
                ServicePrice = servicesToShow.Price;
                _currentServiceId = servicesToShow.Id;
            }
            else
            {
                IsAddServiceWindow = Visibility.Visible;
            }

        }

        #endregion



        #region Private Methods...

        private void Exit(object obj)
        {
            AddOrEditServicesWindow.Close();
        }

        private void EditExtra(object obj)
        {
            Extra extra = _hoteldbContext.Extras.Single(extra => extra.Id == _currentServiceId);

            extra.Description = ServiceDescription;
            extra.Price = ServicePrice;

            _hoteldbContext.Extras.Add(extra);
            _hoteldbContext.SaveChanges();

            AddOrEditServicesWindow.Close();
        }

        private void AddNewExtra(object obj)
        {
            Extra extra = new Extra()
            {
                Description = ServiceDescription,
                Price = ServicePrice
            };

            _hoteldbContext.Extras.Add(extra);
            _hoteldbContext.SaveChanges();

            AddOrEditServicesWindow.Close();
        }

        #endregion

    }
}

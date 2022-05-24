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
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class ServicesViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...

        private ObservableCollection<ServicesToShow> _servicesToShowList;

        private ServicesToShow _selectedService;

        private readonly HoteldbContext _hoteldbContext;


        #endregion


        #region Public Properties...

        public ObservableCollection<ServicesToShow> ServicesToShowList
        {
            get { return _servicesToShowList; }
            set
            {
                _servicesToShowList = value;
                NotifyPropertyChanged(nameof(ServicesToShowList));
            }
        }

        public bool CanExecuteCommands { get; set; } = false;
        public ServicesToShow SelectedService
        {
            get { return _selectedService; }
            set
            {
                _selectedService = value;

                if (_selectedService != null)
                {
                    CanExecuteCommands = true;
                }

                NotifyPropertyChanged(nameof(SelectedService));
            }
        }


        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }


        public ServicesWindow ServicesWindow { get; set; }
        #endregion



        #region Constructors...

        public ServicesViewModel()
        {
            _hoteldbContext = new HoteldbContext();

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit, param => CanExecuteCommands);
            DeleteCommand = new RelayCommand(Delete, param => CanExecuteCommands);
            BackCommand = new RelayCommand(Back);   

            ServicesToShowList = new ObservableCollection<ServicesToShow>(ConstructRoomToShow());
        }

        #endregion



        #region Private Methods...

        private void Add(object param)
        {
            AddOrEditServicesWindow window = new AddOrEditServicesWindow();

            window.ShowDialog();
            ServicesToShowList = new ObservableCollection<ServicesToShow>(ConstructRoomToShow());
        }

        private void Edit(object param)
        {
            if (SelectedService == null)
                return;

            AddOrEditServicesWindow window = new AddOrEditServicesWindow(SelectedService);

            window.ShowDialog();
            ServicesToShowList = new ObservableCollection<ServicesToShow>(ConstructRoomToShow());
        }

        private void Delete(object param)
        {
            if (SelectedService == null)
                return;

            var serviceFromDatabase = _hoteldbContext.Extras.Single(x => x.Id == SelectedService.Id);
            serviceFromDatabase.Deleted = true;
            _hoteldbContext.SaveChanges();

            ServicesToShowList.Remove(SelectedService);
        }

        private void Back(object param)
        {
            ServicesWindow.Close();
        }

        private List<ServicesToShow> ConstructRoomToShow()
        {
            var extras = _hoteldbContext.Extras.Include(extra => extra.ReservationExtras).Where(extra => extra.Deleted == false).ToList();

            List<ServicesToShow> result = new List<ServicesToShow>();

            foreach (var extra in extras)
            {
                result.Add(new ServicesToShow
                {
                    Id = extra.Id,
                    Description = extra.Description,
                    Price = extra.Price
                });
            }

            return result;
        }

        #endregion
    }
}

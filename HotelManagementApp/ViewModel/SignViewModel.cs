using HotelManagementApp.DataModels;
using HotelManagementApp.Helpers;
using HotelManagementApp.Services;
using HotelManagementApp.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class SignViewModel : NotifyPropertyViewModel
    {
        #region Private Fields...
        private readonly HoteldbContext _hoteldbContext;

        private string _emailText;
        private Visibility _isSignIn;
        private Visibility _isSignUp;


        #endregion

        #region Public Properties...
        public SignWindow SignWindow { get; set; }

        public string EmailText
        {
            get { return _emailText; }
            set
            {
                _emailText = value;
                NotifyPropertyChanged(nameof(EmailText));
            }
        }

        public Visibility IsSignIn
        {
            get { return _isSignIn; }
            set
            {
                _isSignIn = value;
                NotifyPropertyChanged(nameof(IsSignIn));
            }
        }

        public Visibility IsSignUp
        {
            get { return _isSignUp; }
            set
            {
                _isSignUp = value;
                NotifyPropertyChanged(nameof(IsSignUp));
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand LogInCommand { get; }

        #endregion

        #region Constructors...
        public SignViewModel()
        {
            _hoteldbContext = new HoteldbContext();

            RegisterCommand = new RelayCommand(Register);
            LogInCommand = new RelayCommand(LogIn);
        }


        #endregion

        #region Methods...

        private void Register(object param)
        {
            try
            {
                var password = ((PasswordBox)param).Password;
                UserValidator.CheckEmail(EmailText);
                UserValidator.CheckPassword(password);

                User user = new()
                {
                    Email = EmailText,
                    Password = password,
                    RoleId = _hoteldbContext.UserRoles
                    .Single(userRole => userRole.Role.Equals("Client")).Id
                };

                _hoteldbContext.Users.Add(user);
                _hoteldbContext.SaveChanges();

                //save user in static class
                ConnectedUser.User = user;

                //redirect to HotelView
                RedirectTo();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RedirectTo()
        {
            HotelWindow window = new HotelWindow();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            App.Current.MainWindow.Show();
        }

        private void LogIn(object param)
        {
            //verify if there is any email the same in db
            var result = _hoteldbContext.Users.SingleOrDefault(user => user.Email.Equals(EmailText));
            if(result == null)
            {
                MessageBox.Show("Email not found!");
                return;
            }

            //verify if the password match
            var password = ((PasswordBox)param).Password;

            if(!result.Password.Equals(password))
            {
                MessageBox.Show("Password doesn't match!");
                return;
            }

            //set user to be current user (static class)
            ConnectedUser.User = result;

            //redirect
            RedirectTo();

        }
        #endregion
    }
}

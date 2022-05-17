using HotelManagementApp.Helpers;
using HotelManagementApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
    internal class StartViewModel
    {
        public StartViewModel()
        {


            SignInCommand = new RelayCommand(SignIn);
            SignUpCommand = new RelayCommand(SignUp);
            ViewCommand = new RelayCommand(View);
            ExitCommand = new RelayCommand(Exit);
        }

        public ICommand SignInCommand { get; }
        public void SignIn(object param)
        {
            SignWindow window = new SignWindow();
            ((SignViewModel)window.DataContext).IsSignIn = System.Windows.Visibility.Visible;
            ((SignViewModel)window.DataContext).IsSignUp = System.Windows.Visibility.Hidden;

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            App.Current.MainWindow.Show();
        }

        public ICommand SignUpCommand { get; }
        public void SignUp(object param)
        {
            SignWindow window = new SignWindow();
            ((SignViewModel)window.DataContext).IsSignIn = System.Windows.Visibility.Hidden;
            ((SignViewModel)window.DataContext).IsSignUp = System.Windows.Visibility.Visible;

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            App.Current.MainWindow.Show();
        }

        public ICommand ViewCommand { get; }
        public void View(object param)
        {
            HotelWindow window = new HotelWindow();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = window;
            App.Current.MainWindow.Show();
        }

        public ICommand ExitCommand { get; }
        public void Exit(object param)
        {
            Environment.Exit(0);
        }
    }
}

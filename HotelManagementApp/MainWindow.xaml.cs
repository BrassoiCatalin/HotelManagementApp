﻿using HotelManagementApp.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HoteldbContext hotel = new HoteldbContext();
            //hotel.UserRoles.Add(new UserRole { Role = "Administrator" });
            //hotel.UserRoles.Add(new UserRole { Role = "Client" });
            //hotel.UserRoles.Add(new UserRole { Role = "Employee" });

            //var xx = hotel.UserRoles.FromSqlRaw("GetAllRoles").ToList();

            hotel.SaveChanges();
        }
    }
}
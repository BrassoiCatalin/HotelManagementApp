using HotelManagementApp.DataModels;
using HotelManagementApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace HotelManagementApp.Views
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            DataContext = new StartViewModel();
            string path = "../../../Images/image2.jpg";
            //AddImage(path);
            //CreateRoomTypes();
        }

        private void CreateRoomTypes()
        {
            HoteldbContext h = new HoteldbContext();
            h.RoomTypes.AddRange(
             new RoomType()
             {
                 Description = "Triple",
                 Features = "as the name might suggest, this room is equipped for three people to stay. The room will have a combination of either three twin beds, one double bed and a twin, or two double beds."
             },
            new RoomType()
            {
                Description = "Quad",
                Features = "a quad room is set up for four people to stay comfortably. This means the room will have two double beds. Some, however, may be set up dormitory-style with bunks or twins, so check with the property to make sure."
            }, new RoomType()
            {
                Description = "Queen",
                Features = "a room with a queen-sized bed. "
            }, new RoomType()
            {
                Description = "King",
                Features = "a room with a king-sized bed."
            }, new RoomType()
            {
                Description = "Twin",
                Features = " a room with two twin-sized beds."
            }, new RoomType()
            {
                Description = "Double-double",
                Features = " these rooms have two double beds (sometimes two queen beds) and are meant to accommodate two to four people, especially families traveling with young kids."
            }, new RoomType()
            {
                Description = "Studio",
                Features = "this type of room has a studio bed, e.g. a couch that can be converted into a bed. Some studios come with additional beds."
            }, new RoomType()
            {
                Description = "Suite",
                Features = "suites come in a few different sizes. A basic suite or executive suite comes with a separate living space connected to one or more bedrooms"
            }, new RoomType()
            {
                Description = "Apartment",
                Features = "aparthotels are offering these types of rooms, but they can also be found at other traditional hotel chains."
            },new RoomType()
            {
                Description = "Villa",
                Features = "most villas can be found at resorts. These kinds of rooms are actually stand-alone houses that have extra space and privacy. "
            }
            );

            h.SaveChanges();
        }

        private void AddImage(string path)
        {
            HoteldbContext h = new HoteldbContext();
            h.RoomImages.Add(new RoomImage()
            {
                Picture = File.ReadAllBytes(path),
            });
            h.SaveChanges();
        }
    }

}

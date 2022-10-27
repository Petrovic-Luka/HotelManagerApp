using DataManagement;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri($"{Environment.CurrentDirectory}/1.jpg"));
            base.Background = b;
        }

        private void btnGuests_Click(object sender, RoutedEventArgs e)
        {
            GuestsForm f = new GuestsForm();
            f.ShowDialog();
        }

        private void btnRooms_Click(object sender, RoutedEventArgs e)
        {
            RoomsForm f = new RoomsForm();
            f.ShowDialog();
        }

        private void btnReservations_Click(object sender, RoutedEventArgs e)
        {
            ReservationsForm f = new ReservationsForm();
            f.ShowDialog();
        }
    }
}

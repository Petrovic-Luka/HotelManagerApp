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
using System.Windows.Shapes;
using static DataManagement.DataManipulation;
namespace HotelManager
{
    /// <summary>
    /// Interaction logic for RoomsForm.xaml
    /// </summary>
    public partial class RoomsForm : Window
    {
        public RoomsForm()
        {
            InitializeComponent();
            ResetDisplay();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewRoom f = new();
            f.ShowDialog();
            ResetDisplay();
        }
        private void btnFees_Click(object sender, RoutedEventArgs e)
        {
            AlterFeesForm f = new();
            f.ShowDialog();
        }

        private async void ResetDisplay()
        {
            cmbRooms.ItemsSource = null;
            var list =await GetRoomsAsync();
            cmbRooms.ItemsSource = list.OrderBy(x => x.Room_Number);
            cmbRooms.DisplayMemberPath = "Display";
        }

    }
}

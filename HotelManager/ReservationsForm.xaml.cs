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
using DataManagement;
namespace HotelManager
{
    /// <summary>
    /// Interaction logic for ReservationsForm.xaml
    /// </summary>
    public partial class ReservationsForm : Window
    {
        public ReservationsForm()
        {
            InitializeComponent();
        }

        private void chDate_Checked(object sender, RoutedEventArgs e)
        {
            searchDate.IsEnabled = true;
        }

        private void chDate_Unchecked(object sender, RoutedEventArgs e)
        {
            searchDate.IsEnabled = false;
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(chDate.IsChecked==false)
            {
                ResetDisplay();
            }
            else if(searchDate.SelectedDate!=null)
            {
                cmbReservations.ItemsSource = null;
                cmbReservations.ItemsSource = await DataManipulation.GetReservationWithDateAsync(txtName.Text, txtRoom.Text, searchDate.SelectedDate.ToString().Split(" ")[0]);
                cmbReservations.DisplayMemberPath = "Display";
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            NewReservationForm f = new();
            f.ShowDialog();
            //TODO resetuj samo na sacuvanim izmenama =>dodaj event listener
            cmbReservations.ItemsSource = null;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(cmbReservations.SelectedItem==null)
            {
                MessageBox.Show("Select reservation");
                return;
            }
            try
            {
                ReservationModel r = (ReservationModel)cmbReservations.SelectedItem;
                DataManipulation.DeleteReservationAsync(r.Id);
                MessageBox.Show("Deleted");
                ResetDisplay();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private async void ResetDisplay()
        {
            cmbReservations.ItemsSource = null;
            cmbReservations.ItemsSource = await DataManipulation.GetReservation(txtName.Text, txtRoom.Text);
            cmbReservations.DisplayMemberPath = "Display";
        }
    }
}

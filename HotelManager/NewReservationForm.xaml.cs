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
    /// Interaction logic for NewReservationForm.xaml
    /// </summary>
    public partial class NewReservationForm : Window
    {
        public NewReservationForm()
        {
            InitializeComponent();
            SetUpDisplay();
        }

        private async void SetUpDisplay()
        {
            cmbRoom.ItemsSource = null;
            var list = await DataManipulation.GetRoomsAsync();
            cmbRoom.ItemsSource = list.OrderBy(x => x.Beds_Number);
            cmbRoom.DisplayMemberPath = "Display";
        }

        private async void btnName_Click(object sender, RoutedEventArgs e)
        {
            cmbGuests.ItemsSource = null;
            cmbGuests.ItemsSource = await DataManipulation.GetGuestsAsync(txtName.Text);
            cmbGuests.DisplayMemberPath = "Display";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var room = (RoomModel)cmbRoom.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Please select Room");
                return;
            }
            if (cmbGuests.SelectedItem == null)
            {
                MessageBox.Show("Please select Guest");
                return;
            }
            try
            {
                GuestModel g = (GuestModel)cmbGuests.SelectedItem;
                DataManipulation.SaveReservationAsync(g.Guest_Id, g.Name, room.Room_Number, startDate.Text, endDate.Text);
                MessageBox.Show("Saved");
                this.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}

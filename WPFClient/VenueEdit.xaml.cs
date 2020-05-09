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
using KoncertManager.BLL.DTOs;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for VenueEdit.xaml
    /// </summary>
    public partial class VenueEdit : UserControl
    {
        public ActionMode ActionMode { get; set; } = ActionMode.Create;
        public int EditedId { get; set; }
        public VenueEdit()
        {
            InitializeComponent();
        }

        private async void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            Venue venue = new Venue()
                {Name = tbName.Text, Address = tbAddress.Text, Capacity = int.Parse(tbCapacity.Text)};

            if (ActionMode == ActionMode.Create)
                await Communication.CreateVenueAsync(venue);
            else
                await Communication.UpdateVenueAsync(EditedId, venue);

            var window = (MainWindow) Window.GetWindow(this);
            await window.GetVenues();
            window.SetListSource();
            window.SetTbOutput();
        }
    }
}

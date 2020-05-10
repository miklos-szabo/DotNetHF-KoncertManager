using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ConcertEdit.xaml
    /// </summary>
    public partial class ConcertEdit : UserControl
    {
        public List<Band> Bands { get; set; }
        public List<Venue> Venues { get; set; }
        public ActionMode ActionMode { get; set; } = ActionMode.Create;
        public int EditedId { get; set; }
        public Concert EditedConcert { get; set; }
        public List<ComboBox> Boxes { get; set; }

        public ConcertEdit()
        {
            InitializeComponent();
            Boxes = new List<ComboBox> { cbBand0, cbBand1, cbBand2, cbBand3, cbBand4, cbBand5 };
        }

        public void PopulateComboBoxes()
        {
            Venues.ForEach(v => cbVenue.Items.Add(v));
            Bands.ForEach(b =>
                Boxes.ForEach(cb => cb.Items.Add(b)));
        }

        public List<Band> GetListOfBands()
        {
            //Ha többször ugyanazt kiválasztottuk, csak egyszer kerüljön be
            HashSet<Band> selectedBands = new HashSet<Band>();
            Boxes.ForEach(cb =>
            {
                if (cb.SelectedItem is Band band)
                    selectedBands.Add(band);
            });
            return selectedBands.ToList();
        }

        public void SetDataToEdit()
        {
            EditedId = EditedConcert.Id;
            cbVenue.SelectedItem = Venues.Find(v => v.Id == EditedConcert.VenueId);
            datePicker.DisplayDate = new DateTime(EditedConcert.Date.Year, EditedConcert.Date.Month, EditedConcert.Date.Day);
            datePicker.Text = datePicker.DisplayDate.ToString(CultureInfo.CurrentCulture);
            cbTickets.SelectedItem = EditedConcert.TicketsAvailable ? 
                cbTickets.Items.GetItemAt(0) : cbTickets.Items.GetItemAt(1);

            for (int i = 0; i < 5; i++)
            {
                if (EditedConcert.Bands.Count > i)
                    Boxes[i].SelectedItem = Bands.Find(b => b.Id == EditedConcert.Bands[i].Id);
            }
        }

        private async void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            if (!(cbVenue.SelectedItem is Venue venue)) return;

            Concert concert = new Concert
            {
                Bands = GetListOfBands(),
                Date = datePicker.DisplayDate,
                TicketsAvailable = cbTickets.SelectedItem == cbTickets.Items.GetItemAt(0),
                VenueId = venue.Id
            };

            if (ActionMode == ActionMode.Create)
                await Communication.CreateConcertAsync(concert);
            else
                await Communication.UpdateConcertAsync(EditedId, concert);

            var window = (MainWindow)Window.GetWindow(this);
            await window.GetConcerts();
            window.SetListSource();
            window.SetTbOutput();
        }
    }
}

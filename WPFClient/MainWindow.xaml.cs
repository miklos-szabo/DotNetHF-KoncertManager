using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Band> Bands { get; set; }
        public List<Venue> Venues { get; set; }
        public List<Concert> Concerts { get; set; }
        public Type CurrentView { get; set; }
        public string BandsAsString { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AtStart();
        }

        public async void AtStart()
        {
            tbOutput.Text = "Loading";
            await GetBands();
            await GetVenues();
            await GetConcerts();
            SetTbOutput();

            CurrentView = typeof(Concert);
            SetListSource();
        }

        private async void buttonBands_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading";
            await GetBands();
            SetTbOutput();

            CurrentView = typeof(Band);
            SetListSource();
        }

        private async void buttonVenues_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading";
            await GetVenues();
            SetTbOutput();

            CurrentView = typeof(Venue);
            SetListSource();
        }

        private async void buttonConcerts_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading";
            await GetConcerts();
            SetTbOutput();

            CurrentView = typeof(Concert);
            SetListSource();
        }
        private void buttonCreateNew_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentView == typeof(Band))
            {
                //Create mód az alapértelmezett, itt nem kell állítani semmit
                var bandEdit = new BandEdit();
                inputControl.Content = bandEdit;
            }
            else if (CurrentView == typeof(Venue))
            {
                var venueEdit = new VenueEdit();
                inputControl.Content = venueEdit;
            }
            else if (CurrentView == typeof(Concert))
            {
                var concertEdit = new ConcertEdit();
                concertEdit.Venues = Venues;
                concertEdit.Bands = Bands;
                concertEdit.PopulateComboBoxes();
                inputControl.Content = concertEdit;
            }
            
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbOutput.SelectedItem is Band band)
            {
                var bandEdit = new BandEdit
                {
                    ActionMode = ActionMode.Edit,
                    labelAction = {Content = "Edit Band"},
                    tbName = {Text = band.Name},
                    tbFormedIn = {Text = band.FormedIn.ToString()},
                    tbCountry = {Text = band.Country},
                    EditedId = band.Id
                };
                inputControl.Content = bandEdit;
            }
            else if (lbOutput.SelectedItem is Venue venue)
            {
                var venueEdit = new VenueEdit
                {
                    ActionMode = ActionMode.Edit,
                    LabelAction = {Content = "Edit Venue"},
                    tbName = {Text = venue.Name},
                    tbAddress = {Text = venue.Address},
                    tbCapacity = {Text = venue.Capacity.ToString()},
                    EditedId = venue.Id
                };
                inputControl.Content = venueEdit;
            }
            else if (lbOutput.SelectedItem is Concert concert)
            {
                var concertEdit = new ConcertEdit
                {
                    ActionMode = ActionMode.Edit,
                    labelAction = {Content = "Edit Concert"},
                    EditedConcert = concert,
                    Venues = Venues,
                    Bands = Bands
                };
                concertEdit.PopulateComboBoxes();
                concertEdit.SetDataToEdit();
                inputControl.Content = concertEdit;
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selectedItem in lbOutput.SelectedItems)
            {
                if (selectedItem is Band band)
                {
                    await Communication.DeleteBandAsync(band.Id);
                    SetTbOutput();
                }
                else if (selectedItem is Venue venue)
                {
                    await Communication.DeleteVenueAsync(venue.Id);
                    SetTbOutput();
                }
                else if (selectedItem is Concert concert)
                {
                    await Communication.DeleteConcertAsync(concert.Id);
                    SetTbOutput();
                }
            }

            if (CurrentView == typeof(Band))
                await GetBands();
            else if (CurrentView == typeof(Venue))
                await GetVenues();
            else if (CurrentView == typeof(Concert))
                await GetConcerts();
            SetListSource();
        }

        public void SetTbOutput()
        {
            tbOutput.Text = Communication.ResponseString;
        }

        public async Task GetConcerts()
        {
            Concerts = await Communication.GetConcertsAsync();
        }

        public async Task GetVenues()
        {
            Venues = await Communication.GetVenuesAsync();
        }

        public async Task GetBands()
        {
            Bands = await Communication.GetBandsAsync();
        }

        public void SetListSource()
        {
            if(CurrentView == typeof(Band))
                lbOutput.ItemsSource = Bands;
            else if (CurrentView == typeof(Venue))
                lbOutput.ItemsSource = Venues;
            else if (CurrentView == typeof(Concert))
                lbOutput.ItemsSource = Concerts;
        }
    }
}

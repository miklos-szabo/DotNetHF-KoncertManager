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
using System.Windows.Threading;
using KoncertManager.BLL.DTOs;
using Newtonsoft.Json;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Band> Bands { get; set; }   //Összes, adatbázisban levő együttes
        public List<Venue> Venues { get; set; } //Összes, adatbázisban levő helyszín
        public List<Concert> Concerts { get; set; } //Összes, adatbázisban levő koncert
        public Type CurrentView { get; set; }   //A jelenleg táblázatban levő elemek típusa
        public DispatcherTimer StatusTimer { get; set; }    //A státusz pár másodperc után visszavált az eredeti értékére

        public MainWindow()
        {
            InitializeComponent();
            StatusTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(3)};
            StatusTimer.Tick += statusTimer_tick;
            AtStart();
        }

        /**
         * Megnyitáskor betöltjük az adatbázist, és megjelenítjük a koncerteket
         */
        public async void AtStart()
        {
            tbStatus.Text = "Loading";
            await GetBands();   //Be kell tölteni az együtteseket és a helyszíneket is, mert ha koncertet 
            await GetVenues();      //szerkesztünk, a kiválasztásokhoz kell mindegyik elem
            await GetConcerts();
            //SortLists();

            CurrentView = typeof(Concert);
            SetListSource();
            SetViewToLists();   //A listát mutatjuk
            tbStatus.Text = "Concerts";
        }

        /**
         * Lekérdezzük és elmentjük az együtteseket, majd kirajzoljuk őket
         */
        private async void buttonBands_Click(object sender, RoutedEventArgs e)
        {
            SetViewToLists();
            tbStatus.Text = "Loading";
            await GetBands();

            CurrentView = typeof(Band);
            SetListSource();
            tbStatus.Text = "Bands";
        }

        /**
         * Lekérdezzük és elmentjük a helyszíneket, majd kirajzoljuk őket
         */
        private async void buttonVenues_Click(object sender, RoutedEventArgs e)
        {
            SetViewToLists();
            tbStatus.Text = "Loading";
            await GetVenues();

            CurrentView = typeof(Venue);
            SetListSource();
            tbStatus.Text = "Venues";
        }

        /**
         * Lekérdezzük és elmentjük a konceretket, majd kirajzoljuk őket
         */
        private async void buttonConcerts_Click(object sender, RoutedEventArgs e)
        {
            SetViewToLists();
            tbStatus.Text = "Loading";
            await GetConcerts();
            tbStatus.Text = "Concerts";

            CurrentView = typeof(Concert);
            SetListSource();
        }

        /**
         * A jelenleg kiválasztott típusból újat hozunk létre
         */
        private void buttonCreateNew_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentView == typeof(Band))
            {
                //Create mód az alapértelmezett, itt nem kell állítani semmit
                var bandEdit = new BandEdit();
                mainControl.Content = bandEdit;
            }
            else if (CurrentView == typeof(Venue))
            {
                var venueEdit = new VenueEdit();
                mainControl.Content = venueEdit;
            }
            else if (CurrentView == typeof(Concert))
            {
                var concertEdit = new ConcertEdit {Venues = Venues, Bands = Bands};
                concertEdit.PopulateComboBoxes();
                mainControl.Content = concertEdit;
            }
            SetViewToEdit();
        }

        /**
         * Szerkesztjük a kiválasztott elemet, a mezőket feltöltjük az eredeti értékekkel
         */
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
                mainControl.Content = bandEdit;
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
                mainControl.Content = venueEdit;
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
                mainControl.Content = concertEdit;
            }
            SetViewToEdit();
        }

        /**
         * Kitöröljük a kiválasztott elemet
         */
        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            bool deleteSuccess = false;
            foreach (var selectedItem in lbOutput.SelectedItems)
            {
                if (selectedItem is Band band)
                    deleteSuccess = await Communication.DeleteBandAsync(band.Id);
                else if (selectedItem is Venue venue)
                    deleteSuccess = await Communication.DeleteVenueAsync(venue.Id);
                else if (selectedItem is Concert concert)
                    deleteSuccess = await Communication.DeleteConcertAsync(concert.Id);
            }

            ResultDelete(deleteSuccess);

            if (CurrentView == typeof(Band))    //Frissítjük a listát
                await GetBands();
            else if (CurrentView == typeof(Venue))
                await GetVenues();
            else if (CurrentView == typeof(Concert))
                await GetConcerts();
            SetListSource();
        }

        /**
         * Lekérdezzük és elmentjük az együtteseket
         */
        public async Task GetBands()
        {
            Bands = await Communication.GetBandsAsync();
        }

        /**
         * Lekérdezzük és elmentjük a helyszíneket
         */
        public async Task GetVenues()
        {
            Venues = await Communication.GetVenuesAsync();
        }

        /**
         * Lekérdezzük és elmentjük a koncerteket
         */
        public async Task GetConcerts()
        {
            Concerts = await Communication.GetConcertsAsync();
        }

        /**
         * Beállítjuk a lista forrását a jelenleg kiválasztott típusra
         */
        public void SetListSource()
        {
            if(CurrentView == typeof(Band))
                lbOutput.ItemsSource = Bands;
            else if (CurrentView == typeof(Venue))
                lbOutput.ItemsSource = Venues;
            else if (CurrentView == typeof(Concert))
                lbOutput.ItemsSource = Concerts;
        }

        /**
         * A lista nézetet jelenítjük meg az edit helyett
         */
        public void SetViewToLists()
        {
            mainControl.Visibility = Visibility.Collapsed;
            lbOutput.Visibility = Visibility.Visible;
        }

        /**
         * Az edit nézetet jelenítjük meg a lista helyett
         */
        public void SetViewToEdit()
        {
            lbOutput.Visibility = Visibility.Collapsed;
            mainControl.Visibility = Visibility.Visible;
        }

        /**
         * Ha igaz a a paraméter, kiírjuk, hogy sikeres törlés
         */
        public void ResultDelete(bool result)
        {
            tbStatus.Text = result ? "Delete Successful!" : "Delete Failed!";
            tbStatus.Foreground = result ? Brushes.Green : Brushes.Red;
            StatusTimer.Start();
        }

        /**
         * Ha sikeres a létrehozás, kiírjuk
         */
        public void ResultCreate(bool result)
        {
            tbStatus.Text = result ? "Creation Successful!" : "Creation Failed!";
            tbStatus.Foreground = result ? Brushes.Green : Brushes.Red;
            StatusTimer.Start();
        }

        /**
         * Ha sikeres a szerkesztés, kiírjuk
         */
        public void ResultEdit(bool result)
        {
            tbStatus.Text = result ? "Edit Successful!" : "Edit Failed!";
            tbStatus.Foreground = result ? Brushes.Green : Brushes.Red;
            StatusTimer.Start();
        }

        /**
         * 3 másodperc után a státusz textblock visszaáll az eredeti értékére
         */
        private void statusTimer_tick(object sender, EventArgs e)
        {
            tbStatus.Foreground = Brushes.Black;
            tbStatus.Text = CurrentView.Name + "s";
            StatusTimer.Stop();
        }

        /**
         * Rendezzük a listákat - koncerteket dátum szerint, együtteseket és helyszíneket névsor szerint
         */
        public void SortLists()
        {
            Concerts = Concerts.OrderBy(c => c.Date).ToList();
            Bands = Bands.OrderBy(b => b.Name).ToList();
            Venues = Venues.OrderBy(v => v.Name).ToList();
        }

        /**
         * Rendezzük a listákat
         */
        private void buttonSort_Click(object sender, RoutedEventArgs e)
        {
            SortLists();
            SetListSource();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == string.Empty) return;

            if (CurrentView == typeof(Band))
                Bands = await Communication.GetFilteredBands($"filter=contains(Name,'{tbSearch.Text}')");
            else if (CurrentView == typeof(Venue))
                Venues = await Communication.GetFilteredVenues($"filter=contains(Name,'{tbSearch.Text}')"); //TODO
            else if (CurrentView == typeof(Concert))
                Concerts = await Communication.GetFilteredConcerts($"filter="); //TODO
            SetListSource();
        }
    }
}

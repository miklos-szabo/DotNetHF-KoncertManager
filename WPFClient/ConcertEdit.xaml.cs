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
        public List<Band> Bands { get; set; }   //Összes, adatbázisban levő együttes
        public List<Venue> Venues { get; set; } //Össze, adatbázisban levő helyszín
        public ActionMode ActionMode { get; set; } = ActionMode.Create; //Létrehozás, vagy szerkesztés
        public int EditedId { get; set; }   //Annak a koncertnek az ID-je, amit éppen szerkesztünk
        public Concert EditedConcert { get; set; }  //Az a koncert, amit éppen szerkesztünk
        public List<ComboBox> Boxes { get; set; }   //A 6 legördülő menü, amivel az együtteseket választjuk ki

        public ConcertEdit()
        {
            InitializeComponent();
            Boxes = new List<ComboBox> { cbBand0, cbBand1, cbBand2, cbBand3, cbBand4, cbBand5 };
        }

        /**
         * Legördülő menük feltöltése elemekkel
         */
        public void PopulateComboBoxes()
        {
            Venues.ForEach(v => cbVenue.Items.Add(v));
            Bands.ForEach(b =>
                Boxes.ForEach(cb => cb.Items.Add(b)));
        }

        /**
         * A legördülő menük adatai alapján listába szedjük a kiválasztott együtteseket
         */
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

        /**
         * Amikor szerkesztésbe fogunk, a mezőket feltöltjük a jelenlegi adatokkal
         */
        public void SetDataToEdit()
        {
            EditedId = EditedConcert.Id;
            cbVenue.SelectedItem = Venues.Find(v => v.Id == EditedConcert.VenueId);
            datePicker.DisplayDate = new DateTime(EditedConcert.Date.Year, EditedConcert.Date.Month, EditedConcert.Date.Day);
            datePicker.Text = datePicker.DisplayDate.ToString(CultureInfo.CurrentCulture);
            cbTickets.SelectedItem = EditedConcert.TicketsAvailable ? 
                cbTickets.Items.GetItemAt(0) : cbTickets.Items.GetItemAt(1);    //0: "True", 1: "False", jobb megoldást nem találtam

            for (int i = 0; i < 5; i++) //Együtteseket kiválasztó comboboxok elemeinek beállítása
            {
                if (EditedConcert.Bands.Count > i)
                    Boxes[i].SelectedItem = Bands.Find(b => b.Id == EditedConcert.Bands[i].Id);
            }
        }

        /**
         * Adataink alapján összeállítunk egy új koncertet, és elküljük a szervernek,
         * vagy létrehozással, vagy szerkesztéssel
         */
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

            var window = (MainWindow)Window.GetWindow(this);

            if (ActionMode == ActionMode.Create)
                window.ResultCreate(await Communication.CreateConcertAsync(concert));
            else
                window.ResultEdit(await Communication.UpdateConcertAsync(EditedId, concert)); 

            await window.GetConcerts();     //Frissítjük a koncertek listáját
            window.SetListSource();         //A lista forrása jó legyen
            window.SetViewToLists();        //A nézetet a listára állítjuk vissza, edit helyett
        }
    }
}

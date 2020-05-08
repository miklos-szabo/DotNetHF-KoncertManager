using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();
        public string ResponseString { get; set; }


        private async void buttonReadAll_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonCreateNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void buttonBands_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading";
            ResponseString = await client.GetStringAsync("http://localhost:53501/api/bands");
            tbOutput.Text = ResponseString;
            List<Band> bands = JsonConvert.DeserializeObject<List<Band>>(ResponseString);
            List<string> bandsOutput = new List<string>();
            bands.ForEach(b => bandsOutput.Add($"{b.Id} - {b.Name} - {b.FormedIn} - {b.Country}"));
            lbOutput.ItemsSource = bandsOutput;
        }

        private async void buttonVenues_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading";
            ResponseString = await client.GetStringAsync("http://localhost:53501/api/venues");
            tbOutput.Text = ResponseString;
            List<Venue> venues = JsonConvert.DeserializeObject<List<Venue>>(ResponseString);
            List<string> venuesOutput = new List<string>();
            venues.ForEach(v => venuesOutput.Add($"{v.Id} - {v.Name} - {v.Address} - {v.Capacity}"));
            lbOutput.ItemsSource = venuesOutput;
        }

        private async void buttonConcerts_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "Loading";
            ResponseString = await client.GetStringAsync("http://localhost:53501/api/concerts");
            tbOutput.Text = ResponseString;
            List<Concert> concerts = JsonConvert.DeserializeObject<List<Concert>>(ResponseString);
            List<string> concertOutput = new List<string>();

            concerts.ForEach(c => concertOutput.Add($"{c.Id} - {c.Date} - {c.TicketsAvailable} - {c.Venue.Name}"));
            lbOutput.ItemsSource = concertOutput;
        }
    }
}

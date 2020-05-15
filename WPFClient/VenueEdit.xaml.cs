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
        public ActionMode ActionMode { get; set; } = ActionMode.Create; //Létrehozás, vagy szerkesztés
        public int EditedId { get; set; }   //Az éppen szerkesztett helyszín Id-je
        public VenueEdit()
        {
            InitializeComponent();
        }

        /**
         * Létrehozunk egy új helyszínt, és elküldjük a szervernek vagy létrehozással, vagy szerkesztéssel
         */
        private async void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            Venue venue = new Venue()
                {Name = tbName.Text, Address = tbAddress.Text, Capacity = int.Parse(tbCapacity.Text)};

            var window = (MainWindow) Window.GetWindow(this);

            if (ActionMode == ActionMode.Create)
                window.ResultCreate(await Communication.CreateVenueAsync(venue)); 
            else
                window.ResultEdit(await Communication.UpdateVenueAsync(EditedId, venue)); 

            await window.GetVenues();   //Frissítjük a helyszínek listáját
            window.SetListSource();     //Jó legyen a lista forrása
            window.SetViewToLists();    //Visszatérünk a lista nézetére szerkesztésből
        }
    }
}

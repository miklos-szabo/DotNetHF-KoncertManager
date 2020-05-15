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
    /// Interaction logic for BandEdit.xaml
    /// </summary>
    public partial class BandEdit : UserControl
    {
        public ActionMode ActionMode { get; set; } = ActionMode.Create; //Létrehozás, vagy szerkesztés
        public int EditedId { get; set; }   //Az éppen szerkesztett együttes Id-je
        public BandEdit()
        {
            InitializeComponent();
        }

        /**
         * Új együttest hozunk létre, és elküldjük, vagy létrehozással, vagy szerkesztéssel
         */
        private async void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            Band band = new Band
                {Name = tbName.Text, FormedIn = int.Parse(tbFormedIn.Text), Country = tbCountry.Text};

            var window = (MainWindow) Window.GetWindow(this);

            if (ActionMode == ActionMode.Create)
                window.ResultCreate(await Communication.CreateBandAsync(band));
            else
                window.ResultEdit(await Communication.UpdateBandAsync(EditedId, band));

            await window.GetBands();    //Frissítjük az együttesek listáját
            window.SetListSource();     //Jó legyen a lista forrása
            window.SetViewToLists();    //Visszatérünk a lista nézetére szerkesztésből
        }
    }
}

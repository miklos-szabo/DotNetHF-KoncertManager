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
        public ActionMode ActionMode { get; set; } = ActionMode.Create;
        public int EditedId { get; set; }
        public BandEdit()
        {
            InitializeComponent();
        }

        private async void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            Band band = new Band
                {Name = tbName.Text, FormedIn = int.Parse(tbFormedIn.Text), Country = tbCountry.Text};

            if (ActionMode == ActionMode.Create)
                await Communication.CreateBandAsync(band);
            else
                await Communication.UpdateBandAsync(EditedId, band);

            var window = (MainWindow) Window.GetWindow(this);
            await window.GetBands();
            window.SetListSource();
            window.SetTbOutput();
        }
    }
}

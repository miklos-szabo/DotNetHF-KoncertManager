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
    /// Interaction logic for ConcertEdit.xaml
    /// </summary>
    public partial class ConcertEdit : UserControl
    {
        public List<Band> Bands { get; set; }
        public List<Venue> Venues { get; set; }
        public ActionMode ActionMode { get; set; } = ActionMode.Create;
        public int EditedId { get; set; }
        public ConcertEdit()
        {
            InitializeComponent();
        }

        public void PopulateComboBoxes()
        {
            Venues.ForEach(v => cbVenue.Items.Add(v));
            Bands.ForEach(b => cbBand0.Items.Add(b));
        }
    }
}

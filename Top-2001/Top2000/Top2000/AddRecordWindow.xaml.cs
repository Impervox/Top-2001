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
using System.Windows.Shapes;
using ClassLibrary;

namespace Top2000
{
    /// <summary>
    /// Interaction logic for AddRecordWindow.xaml
    /// </summary>
    public partial class AddRecordWindow : Window
    {
        public AddRecordWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        }

        private void txtPosition_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }
        private void FillComboBox()
        {
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(x => x).ToList();
            cbArtist.SelectedIndex = 0;
        }

        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: fill cbSongs with songs of this artist.
        }

        private void cbSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: this song should be added, only if song is not already in this year.
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            //TODO: add record to current -1 top2000 year(? or how do we want to do this?)
        }

        private void cbFirstLetter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBox();
        }
    }
}

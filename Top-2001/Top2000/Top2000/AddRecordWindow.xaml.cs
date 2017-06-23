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
            FillComboBox();
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
            cbYear.ItemsSource = DataProvider.GetYearsAndSongCount();
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(x => x).ToList();
            cbArtist.SelectedIndex = 0;
        }

        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbSong.ItemsSource = DataProvider.SongsOfArtist(cbArtist.SelectedValue.ToString());
            cbSong.SelectedIndex = 0;
        }

        private void cbSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: this song should be added, only if song is not already in this year.
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.AddRecord(cbArtist.SelectedValue.ToString(), cbSong.SelectedValue.ToString(), int.Parse(txtPosition.Text), int.Parse(cbYear.SelectedValue.ToString()));
        }

        private void cbFirstLetter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBox();
        }
    }
}

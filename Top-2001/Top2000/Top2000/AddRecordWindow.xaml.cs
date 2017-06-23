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
        Artist thisArtist;
        public AddRecordWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
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
            cbYear.SelectedIndex = 0;
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(x => x).ToList();
            cbArtist.SelectedIndex = 0;
        }

        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Artist artist in DataProvider.allArtists)
                if (artist.Name == (string)cbArtist.SelectedItem)
                    thisArtist = artist;
            cbSong.ItemsSource = DataProvider.SongsOfArtist(thisArtist.Name.ToString());
            cbSong.SelectedIndex = 0;
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            if (txtPosition.Text == "" || cbArtist.SelectedValue.ToString() == "" || cbSong.SelectedValue.ToString() == "" || cbYear.SelectedValue.ToString() == "")
                MessageBox.Show("Please fill in all the fields");
            else if (int.Parse(txtPosition.Text) < 1 && int.Parse(txtPosition.Text) > 2000)
                MessageBox.Show("oops");
            else
                try
                {
                    DataProvider.AddRecord(cbArtist.SelectedValue.ToString(), cbSong.SelectedValue.ToString(), int.Parse(txtPosition.Text), int.Parse(cbYear.SelectedValue.ToString()));
                }
                catch
                {
                    MessageBox.Show("There's already a song on this position or the song is already in the list.");
                }
        }

        private void cbFirstLetter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBox();
        }
    }
}

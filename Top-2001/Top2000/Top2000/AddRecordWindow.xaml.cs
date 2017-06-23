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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class AddRecordWindow : Window
    {
        /// <summary>
        /// The artist
        /// </summary>
        Artist thisArtist;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRecordWindow"/> class.
        /// </summary>
        public AddRecordWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
            FillComboBox();
        }

        /// <summary>
        /// Handles the PreviewTextInput event of the txtPosition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextCompositionEventArgs"/> instance containing the event data.</param>
        private void txtPosition_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }
        /// <summary>
        /// Fills the ComboBox.
        /// </summary>
        private void FillComboBox()
        {
            cbYear.ItemsSource = DataProvider.GetYearsAndSongCount();
            cbYear.SelectedIndex = 0;
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(x => x).ToList();
            cbArtist.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the cbArtist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Artist artist in DataProvider.allArtists)
                if (artist.Name == (string)cbArtist.SelectedItem)
                    thisArtist = artist;
            cbSong.ItemsSource = DataProvider.SongsOfArtist(thisArtist.Name.ToString());
            cbSong.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the btnAddRecord control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            int position = int.Parse(txtPosition.Text);
            if (txtPosition.Text == "" || cbArtist.SelectedValue.ToString() == "" || cbSong.SelectedValue.ToString() == "" || cbYear.SelectedValue.ToString() == "")
                MessageBox.Show("Please fill in all the fields");
            else if (position < 1 || position > 2000)
                MessageBox.Show("oops");
            else
                try
                {
                    DataProvider.AddRecord(cbArtist.SelectedValue.ToString(), cbSong.SelectedValue.ToString(), position, int.Parse(cbYear.SelectedValue.ToString()));
                }
                catch
                {
                    MessageBox.Show("There's already a song on this position or the song is already in the list.");
                }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the cbFirstLetter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cbFirstLetter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBox();
        }
    }
}

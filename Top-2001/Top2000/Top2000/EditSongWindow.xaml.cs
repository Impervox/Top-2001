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
using System.IO;
using ClassLibrary;
using System.Data.SqlClient;
using System.Data;

namespace Top2000
{
    /// <summary>
    /// Interaction logic for EditSongWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class EditSongWindow : Window
    {
        /// <summary>
        /// The this artist
        /// </summary>
        Artist thisArtist;
        /// <summary>
        /// The this song
        /// </summary>
        Song thisSong;
        /// <summary>
        /// Initializes a new instance of the <see cref="EditSongWindow"/> class.
        /// </summary>
        public EditSongWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
            FillData();
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        private void FillData()
        {
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(a => a).ToList();
            cbArtist.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the PreviewTextInput event of the txtYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextCompositionEventArgs"/> instance containing the event data.</param>
        private void txtYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the cbArtist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //fill cbSongs with songs of this artist.
            //get songs from artist this artist.
            foreach (Artist artist in DataProvider.allArtists)
                if (artist.Name == (string)cbArtist.SelectedItem)
                    thisArtist = artist;

            cbSong.ItemsSource = DataProvider.SongsOfArtist(thisArtist.Name);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the cbSong control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cbSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the song that will be edited.
            //fill the fields with this song's information if avaliable.
            foreach (Song song in DataProvider.allSongs)
                if (song.Title == (string)cbSong.SelectedItem)
                    thisSong = song;

            txtSong.Text = thisSong.Title;
            txtYear.Text = thisSong.Year.ToString();
            txtLyrics.Text = thisSong.Lyrics;
        }

        /// <summary>
        /// Handles the Drop event of the txtLyrics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        private void txtLyrics_Drop(object sender, DragEventArgs e)
        {
            string path;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if (droppedFilePaths.Length == 1)
                {
                    path = @"" + droppedFilePaths[0].ToString();
                    if (System.IO.Path.GetExtension(path) == ".txt")
                        txtLyrics.Text = File.ReadAllText(path);
                    else
                        MessageBox.Show("U kunt alleen een .txt bestand in dit veld droppen.", "Error");
                }
                else
                    MessageBox.Show("U kunt maximaal 1 bestand in dit veld droppen.", "Error");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEditSong control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEditSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSong.SelectedIndex > -1 && txtSong.Text != "" && txtYear.Text != "")
                {
                    DataProvider.EditSong(txtYear.Text, txtSong.Text, txtLyrics.Text, thisSong);
                    MessageBox.Show(String.Format("{0} aangepast.", thisSong.Title));
                    FillData();
                }
                else
                    MessageBox.Show("Vul A.U.B. de verplichte velden in.", "Ongeldig");
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException, "Error");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRemoveSong control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRemoveSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSong.SelectedIndex > -1 && txtSong.Text != "" && txtYear.Text != "")
                {
                    bool deletedSucesfully = true;
                    DataProvider.RemoveSong(thisSong, thisArtist);
                    foreach (Song song in DataProvider.allSongs)
                        if (song.Title == thisSong.Title)
                            deletedSucesfully = false; //het nummer is niet verwijderd.
                    if(deletedSucesfully)
                        MessageBox.Show(String.Format("Nummer {0} van {1} verwijderd.", thisSong.Title, thisArtist.Name), "Nummer verwijderd");
                    else
                        MessageBox.Show(String.Format("Nummer {0} van {1} is niet verwijderd.", thisSong.Title, thisArtist.Name), "Nummer niet verwijderd");
                    FillData();
                }
                else
                    MessageBox.Show("Vul A.U.B. de verplichte velden in.", "Ongeldig");
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException, "Error");
            }
        }

        /// <summary>
        /// Handles the PreviewDragOver event of the txtLyrics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        private void txtLyrics_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the cbFirstLetter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cbFirstLetter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillData();
        }
    }
}

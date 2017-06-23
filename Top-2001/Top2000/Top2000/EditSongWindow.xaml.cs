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
    public partial class EditSongWindow : Window
    {
        Artist thisArtist;
        Song ThisSong;
        public EditSongWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
            FillData();
        }

        private void FillData()
        {
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(a => a).ToList();
            cbArtist.SelectedIndex = 0;
        }

        private void txtYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //fill cbSongs with songs of this artist.
            //get songs from artist this artist.
            foreach (Artist artist in DataProvider.allArtists)
                if (artist.Name == (string)cbArtist.SelectedItem)
                    thisArtist = artist;

            cbSong.ItemsSource = DataProvider.SongsOfArtist(thisArtist.Name);
        }

        private void cbSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get the song that will be edited.
            //fill the fields with this song's information if avaliable.
            foreach (Song song in DataProvider.allSongs)
                if (song.Title == (string)cbSong.SelectedItem)
                    ThisSong = song;

            txtSong.Text = ThisSong.Title;
            txtYear.Text = ThisSong.Year.ToString();
            txtLyrics.Text = ThisSong.Lyrics;
        }

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

        private void btnEditSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSong.SelectedIndex > -1 && txtSong.Text != "" && txtYear.Text != "")
                {
                    DataProvider.EditSong(ThisSong);
                    MessageBox.Show(String.Format("{0} aangepast.", txtSong.Text));
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

        private void btnRemoveSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSong.SelectedIndex > -1 && txtSong.Text != "" && txtYear.Text != "")
                {
                    DataProvider.RemoveSong(ThisSong, thisArtist);
                    //TODO: Remove song procedure, if the song is not in a previous year.
                    MessageBox.Show(String.Format("Nummer {0} van {1} verwijderd.", ThisSong.Title, thisArtist.Name), "Nummer verwijderd");
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

        private void txtLyrics_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void cbFirstLetter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillData();
        }
    }
}

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

namespace Top2000
{
    /// <summary>
    /// Interaction logic for EditSongWindow.xaml
    /// </summary>
    public partial class EditSongWindow : Window
    {
        public EditSongWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
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
            //songs aanpassen naar songs van deze artiest.
        }

        private void cbSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //artiest invullen van dit nummer.
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
                if (txtSong.Text != "" && txtYear.Text != "")
                {
                    //TODO: Edit song procedure
                    MessageBox.Show("Nummer aangepast.");
                }
                else
                    MessageBox.Show("Vul A.U.B. de verplichte velden in.");
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException);
            }
        }

        private void btnRemoveSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSong.Text != "" && txtYear.Text != "")
                {
                    //TODO: Remove song procedure
                    MessageBox.Show("Nummer verwijderd.");
                }
                else
                    MessageBox.Show("Vul A.U.B. de verplichte velden in.");
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException);
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

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
    /// Interaction logic for EditArtistWindow.xaml
    /// </summary>
    public partial class EditArtistWindow : Window
    {
        public EditArtistWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            cbFirstLetter.SelectedIndex = 0;
            FillComboBox();
        }

        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO:changed artist selection, the artist that will be changed or removed.
        }

        private void txtBiography_Drop(object sender, DragEventArgs e)
        {
            string path;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if (droppedFilePaths.Length == 1)
                {
                    path = @"" + droppedFilePaths[0].ToString();
                    if (System.IO.Path.GetExtension(path) == ".txt")
                        txtBiography.Text = File.ReadAllText(path);
                    else
                        MessageBox.Show("U kunt alleen een .txt bestand in dit veld droppen.", "Error");
                }
                else
                    MessageBox.Show("U kunt maximaal 1 bestand in dit veld droppen.", "Error");
            }
        }

        private void txtUrl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //TODO: controlle op geldig email adress (optioneel).
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            //TODO: browse image implementation (optioneel).
        }

        private void btnEditArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtArtist.Text != "")
                {
                    //TODO: Edit artist procedure, artist can't have a song in a previous list.
                    MessageBox.Show("Artiest aangepast.");
                }
                else
                {
                    MessageBox.Show("Artiest naam is een verplicht veld.");
                }
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException);
            }
        }

        private void btnRemoveArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtArtist.Text != "")
                {
                    //TODO: Edit artist procedure, artist can't have songs
                    MessageBox.Show("Artiest verwijderd.");
                }
                else
                {
                    MessageBox.Show("Artiest naam is een verplicht veld.");
                }
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException);
            }
        }

        private void txtBiography_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void FillComboBox()
        {
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(x => x).ToList();
        }

        private void cbFirstLetter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBox();
        }
    }
}

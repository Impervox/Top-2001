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
using System.Text.RegularExpressions;

namespace Top2000
{
    /// <summary>
    /// Interaction logic for EditArtistWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class EditArtistWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditArtistWindow"/> class.
        /// </summary>
        public EditArtistWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
            FillComboBox();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the cbArtist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(String.IsNullOrEmpty((string)cbArtist.SelectedValue))
            {
                cbArtist.SelectedIndex = 0;
            }
            Artist artist = (from a in DataProvider.allArtists
                             where a.Name == cbArtist.SelectedItem.ToString()
                             select a).First();
            txtArtist.Text = artist.Name;
            txtBiography.Text = artist.Biography;
            txtUrl.Text = artist.Url;
        }

        /// <summary>
        /// Handles the Drop event of the txtBiography control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the btnEditArtist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEditArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtArtist.Text != "")
                {
                    string firstChar = Convert.ToString(txtArtist.Text[0]);
                    if (Regex.IsMatch(firstChar, "[A-Z0-9]"))
                    {
                        DataProvider.EditArtist(cbArtist.SelectedValue.ToString(), txtArtist.Text, txtUrl.Text, txtBiography.Text);
                        MessageBox.Show("Artiest aangepast.", "Succes");
                        FillComboBox();
                    }
                    else
                    {
                        MessageBox.Show("Begin de naam A.U.B. met een cijfer of een hoofdletters.");
                    }
                }
                else
                {
                    MessageBox.Show("Artiest naam is een verplicht veld.", "Ongeldig");
                }
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException, "Error");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRemoveArtist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRemoveArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbArtist.SelectedValue.ToString() != "")
                {
                    if (DataProvider.SongsOfArtist(cbArtist.SelectedValue.ToString()).Count == 0)
                    {
                        DataProvider.RemoveArtist(cbArtist.SelectedValue.ToString());
                        MessageBox.Show("Artiest verwijderd.", "Succes");
                        FillComboBox();
                    }
                    else
                        MessageBox.Show("U kunt een artiest niet verwijderen zolang hij nummers heeft.", "Ongeldig");
                }
                else
                {
                    MessageBox.Show("Selecteer A.U.B. een artiest", "Ongeldig");
                }
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException, "Error");
            }
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the PreviewDragOver event of the txtBiography control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        private void txtBiography_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Fills the ComboBox.
        /// </summary>
        private void FillComboBox()
        {
            cbArtist.ItemsSource = (from a in DataProvider.allArtists
                                    where a.Name.StartsWith(cbFirstLetter.SelectedValue.ToString())
                                    select a.Name).OrderBy(x => x).ToList();
            cbArtist.SelectedIndex = 0;
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

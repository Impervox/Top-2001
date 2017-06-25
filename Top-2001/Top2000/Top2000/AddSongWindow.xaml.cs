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
    /// Interaction logic for AddSongWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class AddSongWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSongWindow"/> class.
        /// </summary>
        public AddSongWindow()
        {
            InitializeComponent();
            cbFirstLetter.ItemsSource = DataProvider.GetFirstCharacters();
            cbFirstLetter.SelectedIndex = 0;
            FillComboBox();
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
                if(droppedFilePaths.Length == 1)
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
        /// Handles the Click event of the btnAddSong control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAddSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSong.Text != "" && txtYear.Text != "")
                {
                    string firstChar = Convert.ToString(txtSong.Text[0]);
                    if (Regex.IsMatch(firstChar, "[A-Z0-9]"))
                    {
                        DataProvider.CreateSong(cbArtist.SelectedValue.ToString(), txtSong.Text, int.Parse(txtYear.Text), txtLyrics.Text);
                        MessageBox.Show("Dit nummer is toegevoegd.", "Uitgevoerd.");
                    }
                    else
                    {
                        MessageBox.Show("Begin de titel A.U.B. met een cijfer of hoofdletter.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Vul A.U.B. de verplichte velden in.", "Error");
                }
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
            FillComboBox();
        }
    }
}

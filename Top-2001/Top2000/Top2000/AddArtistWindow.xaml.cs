using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Top2000
{
    /// <summary>
    /// Interaction logic for AddArtistWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class AddArtistWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddArtistWindow" /> class.
        /// </summary>
        public AddArtistWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Drop event of the txtBiography control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs" /> instance containing the event data.</param>
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
        /// Handles the Click event of the btnAddArtist control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void btnAddArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtArtist.Text != "")
                {
                    string firstChar = Convert.ToString(txtArtist.Text[0]);
                    if (Regex.IsMatch(firstChar, "[A-Z0-9]"))
                    {
                        DataProvider.CreateArtist(txtArtist.Text.ToString(), txtBiography.Text.ToString(), txtUrl.Text.ToString());
                        MessageBox.Show("Artiest toegevoegd.", "Succes");
                    }
                    else
                    {
                        MessageBox.Show("Begin de naam A.U.B. met een cijfer of hoofdletter.", "Error");
                    }
                }
                else
                    MessageBox.Show("Vul A.U.B. de verplichte velden in.", "Error");
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException, "Error");
            }
        }
    }
}

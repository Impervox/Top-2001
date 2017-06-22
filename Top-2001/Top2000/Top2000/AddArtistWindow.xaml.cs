using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Top2000
{
    /// <summary>
    /// Interaction logic for AddArtistWindow.xaml
    /// </summary>
    public partial class AddArtistWindow : Window
    {
        public AddArtistWindow()
        {
            InitializeComponent();
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

        private void btnAddArtist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtArtist.Text != "")
                {
                    DataProvider.CreateArtist(txtArtist.Text.ToString(), txtBiography.Text.ToString(), txtUrl.Text.ToString());
                    MessageBox.Show("Artiest toegevoegd.");
                }
                else
                    MessageBox.Show("Vul A.U.B. de verplichte velden in.");
            }
            catch
            {
                MessageBox.Show(DataProvider.errorException);
            }
        }
    }
}

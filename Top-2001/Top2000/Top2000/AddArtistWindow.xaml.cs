using ClassLibrary;
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
            //.txt document inslepen moet de textbox vullen met content van het bestand.
        }

        private void txtUrl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //url moet worden gecheckt op geldige url.
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
                    MessageBox.Show("Artiest naam is een verplicht veld.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

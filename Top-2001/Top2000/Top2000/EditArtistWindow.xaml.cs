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
        }

        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //pas gegevens van deze artiest aan.
        }

        private void txtBiography_Drop(object sender, DragEventArgs e)
        {
            //pas de biografie aan.
        }

        private void txtUrl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //pas de url aan.
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            //pas de foto aan.
        }

        private void btnEditArtist_Click(object sender, RoutedEventArgs e)
        {
            //pas artiest aan in tabel artiest.
        }

        private void btnRemoveArtist_Click(object sender, RoutedEventArgs e)
        {
            //verwijder artiest van tabel artiest als er geen nummers meer aan deze artiest gekoppelt zijn.
        }
    }
}

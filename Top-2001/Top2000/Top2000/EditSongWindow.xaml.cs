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
    /// Interaction logic for EditSongWindow.xaml
    /// </summary>
    public partial class EditSongWindow : Window
    {
        public EditSongWindow()
        {
            InitializeComponent();
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
            //pas lyrics aan.
        }

        private void btnIntro_Click(object sender, RoutedEventArgs e)
        {
            //intro muziek bestand selecteren.
        }

        private void btnEditSong_Click(object sender, RoutedEventArgs e)
        {
            //record aanpassen in songs alleen als die niet al in top2000 staat.
        }

        private void btnRemoveSong_Click(object sender, RoutedEventArgs e)
        {
            //verwijder nummer als die niet in de top2000 voorkomt.
        }
    }
}

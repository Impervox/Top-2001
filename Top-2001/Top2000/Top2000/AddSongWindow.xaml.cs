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
    /// Interaction logic for AddSongWindow.xaml
    /// </summary>
    public partial class AddSongWindow : Window
    {
        public AddSongWindow()
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

        private void txtLyrics_Drop(object sender, DragEventArgs e)
        {
            //Men moet een .txt bestand kunnen droppen in dit veld dat de content van dat bestand over neemt.
        }

        private void btnIntro_Click(object sender, RoutedEventArgs e)
        {
            //Er moet een geluids bestand geselecteerd en opgeslagen kunnen worden.
        }

        private void btnAddSong_Click(object sender, RoutedEventArgs e)
        {
            //Met deze data moet er een nieuw song aan tblSongs worden toegevoegd. jaar, titel en artiest.
        }
    }
}

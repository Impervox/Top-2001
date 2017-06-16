﻿using ClassLibrary;
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

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            //foto moet kunnen worden opgeslagen en geupload naar database.
        }

        private void btnAddArtist_Click(object sender, RoutedEventArgs e)
        {
            //voeg de artiest toe aan tbl artiesten. als naam niet al in de tabel staat.
            Artist a = new Artist(txtArtist.Text, txtBiography.Text, txtUrl.Text, txtBiography.Text);
            a.AddArtist(txtArtist.Text, txtBiography.Text, txtUrl.Text, txtBiography.Text);
        }
    }
}

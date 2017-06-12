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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;

namespace Top2000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbYear.ItemsSource = DataProvider.GetAllYears();
            cbYear.SelectedIndex = 0;
            txtPage.Text = 1 + "";
        }

        private void txtPage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void txtPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void cbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            int i;
            int year;
            if (int.TryParse(txtPage.Text, out i) && int.TryParse(cbYear.SelectedValue.ToString(), out year))
            {
                if (i > 80)
                    i = 80;
                if (i < 1)
                    i = 1;
                dgRecords.ItemsSource = DataProvider.loadData(year, (i * 25 - 24), (i * 25));
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            int i;
            if(int.TryParse(txtPage.Text, out i) && i != 80)
            {
                txtPage.Text = i + 1 + "";
            }
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            txtPage.Text = 80 + "";
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            int i;
            if (int.TryParse(txtPage.Text, out i) && i != 1)
            {
                txtPage.Text = i - 1 + "";
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            txtPage.Text = 1 + "";
        }
    }
}

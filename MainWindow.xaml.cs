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

namespace BackpackWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<BackpackInfo> BackpackInfos { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            BackpackInfos = new List<BackpackInfo>();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (IsVerified() == false) return;

            int.TryParse(priceBox.Text, out int price);
            int.TryParse(weightBox.Text, out int weight);

            var item = new BackpackInfo()
            {
                ID = BackpackInfos.Count(),
                Price = price,
                Weight = weight
            };
            BackpackInfos.Add(item);

            dataGridMain.Items.Add(item);
        }

        private void buttonSolve_Click(object sender, RoutedEventArgs e)
        {
            listOfID.Text = "";

            int.TryParse(capacityBox.Text, out int capacity);
            if (capacity == 0) return;

            var algoritm = new BackpackAlgoritm(BackpackInfos, capacity);

            int maxPriceItems = algoritm.GetMaxPrices();
            List<int> listID = algoritm.GetListOfID();

            maxProfitPrice.Text = maxPriceItems.ToString();
            foreach (var item in listID)
            {
                listOfID.Text += $"{item - 1}, ";
            }
            listOfID.Text = listOfID.Text.Remove(listOfID.Text.Length-2);
        }

        private bool IsVerified()
        {
            if (priceBox.Text.Length == 0 ||
                weightBox.Text.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}

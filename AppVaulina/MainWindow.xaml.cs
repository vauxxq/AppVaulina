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

namespace AppVaulina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            Wiindow.ProductsWindow productsWindow = new Wiindow.ProductsWindow();
            productsWindow.Show();
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            Wiindow.CategoriesWindow categoriesWindow = new Wiindow.CategoriesWindow();
            categoriesWindow.Show();
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Wiindow.SuppliersWindow suppliersWindow = new Wiindow.SuppliersWindow();
            suppliersWindow.Show();
        }

        private void btnSales_Click(object sender, RoutedEventArgs e)
        {
            Wiindow.SalesWindow salesWindow = new Wiindow.SalesWindow();
            salesWindow.Show();
        }

        private void btnWarehouses_Click(object sender, RoutedEventArgs e)
        {
            Wiindow.WarehousesWindow warehousesWindow = new Wiindow.WarehousesWindow();
            warehousesWindow.Show();
        }
    }
}
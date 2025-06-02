using AppVaulina.Classes;
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

namespace AppVaulina.Wiindow
{
    /// <summary>
    /// Логика взаимодействия для ProductAddEditWindow.xaml
    /// </summary>
    public partial class ProductAddEditWindow : Window
    {
        private Product currentProduct;

        public ProductAddEditWindow()
        {
            InitializeComponent();
            currentProduct = new Product(0, 0, 0, "");
            cbCategory.ItemsSource = App.AllCategories;
            cbWarehouse.ItemsSource = App.AllWarehouses;
        }

        public ProductAddEditWindow(Product product)
        {
            InitializeComponent();
            currentProduct = product;
            cbCategory.ItemsSource = App.AllCategories;
            cbWarehouse.ItemsSource = App.AllWarehouses;
            cbCategory.SelectedItem = App.AllCategories.Find(c => c.CategoryId == currentProduct.CategoryId);
            cbWarehouse.SelectedItem = App.AllWarehouses.Find(w => w.WarehouseId == currentProduct.WarehouseId);
            tbName.Text = currentProduct.Name;
            tbCost.Text = currentProduct.Cost.ToString();
            tbQuantity.Text = currentProduct.Quantity.ToString();
            tbDescription.Text = currentProduct.Description;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbCategory.SelectedItem == null || cbWarehouse.SelectedItem == null || string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbCost.Text) || string.IsNullOrWhiteSpace(tbQuantity.Text))
            {
                MessageBox.Show("Все поля, кроме описания, обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(tbCost.Text, out decimal cost) || cost <= 0)
            {
                MessageBox.Show("Стоимость должна быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(tbQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Количество должно быть неотрицательным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentProduct.CategoryId = (cbCategory.SelectedItem as Category)?.CategoryId ?? 0;
            currentProduct.WarehouseId = (cbWarehouse.SelectedItem as Warehouse)?.WarehouseId ?? 0;
            currentProduct.Name = tbName.Text;
            currentProduct.Cost = cost;
            currentProduct.Quantity = quantity;
            currentProduct.Description = tbDescription.Text;

            if (currentProduct.ProductId == 0)
            {
                if (App.AllProducts.Count == 0)
                    currentProduct.ProductId = 1;
                else
                    currentProduct.ProductId = App.AllProducts.Max(p => p.ProductId) + 1;
                App.AllProducts.Add(currentProduct);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
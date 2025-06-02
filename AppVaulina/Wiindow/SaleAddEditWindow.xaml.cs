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
    /// Логика взаимодействия для SaleAddEditWindow.xaml
    /// </summary>
    public partial class SaleAddEditWindow : Window
    {
        private Sale currentSale;

        public SaleAddEditWindow()
        {
            InitializeComponent();
            currentSale = new Sale(0, 0);
            cbProduct.ItemsSource = App.AllProducts;
            dpSaleDate.SelectedDate = DateTime.Now; // По умолчанию текущая дата
        }

        public SaleAddEditWindow(Sale sale)
        {
            InitializeComponent();
            currentSale = sale;
            cbProduct.ItemsSource = App.AllProducts;
            cbProduct.SelectedItem = App.AllProducts.Find(p => p.ProductId == currentSale.ProductId);
            tbQuantity.Text = currentSale.Quantity.ToString();
            dpSaleDate.SelectedDate = currentSale.SaleDate;
            tbSaleCost.Text = currentSale.SaleCost.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbProduct.SelectedItem == null || string.IsNullOrWhiteSpace(tbQuantity.Text) || string.IsNullOrWhiteSpace(tbSaleCost.Text) || !dpSaleDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(tbQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Количество должно быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(tbSaleCost.Text, out decimal saleCost) || saleCost <= 0)
            {
                MessageBox.Show("Стоимость продажи должна быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Product selectedProduct = cbProduct.SelectedItem as Product;
            if (selectedProduct.Quantity < quantity)
            {
                MessageBox.Show("Недостаточно товара на складе для этой продажи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Обновляем количество товара на складе
            selectedProduct.Quantity -= quantity;

            currentSale.ProductId = selectedProduct.ProductId;
            currentSale.Quantity = quantity;
            currentSale.SaleDate = dpSaleDate.SelectedDate.Value;
            currentSale.SaleCost = saleCost;

            if (currentSale.SaleId == 0)
            {
                if (App.AllSales.Count == 0)
                    currentSale.SaleId = 1;
                else
                    currentSale.SaleId = App.AllSales.Max(s => s.SaleId) + 1;
                App.AllSales.Add(currentSale);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
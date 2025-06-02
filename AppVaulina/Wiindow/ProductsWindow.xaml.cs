using AppVaulina.Classes;
using Microsoft.Win32;
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
using System.IO;

namespace AppVaulina.Wiindow
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            dgProducts.ItemsSource = null; // Очищаем DataGrid
            dgProducts.ItemsSource = App.AllProducts; // Загружаем список товаров
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductAddEditWindow addWindow = new ProductAddEditWindow();
            addWindow.ShowDialog();
            LoadProducts(); // Обновляем список после закрытия окна
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = dgProducts.SelectedItem as Product;
            if (selectedProduct != null)
            {
                Wiindow.ProductAddEditWindow editWindow = new Wiindow.ProductAddEditWindow(selectedProduct);
                editWindow.ShowDialog();
                LoadProducts(); // Обновляем список после закрытия окна
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = dgProducts.SelectedItem as Product;
            if (selectedProduct != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.AllProducts.Remove(selectedProduct);
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = "ProductsExport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    StringBuilder csvContent = new StringBuilder();
                    // Заголовки
                    csvContent.AppendLine("Номер товара,Категория,Склад,Наименование,Стоимость,Количество,Описание");
                    // Данные
                    foreach (var product in App.AllProducts)
                    {
                        string line = $"{product.ProductId},{product.CategoryId},{product.WarehouseId},\"{product.Name}\",\"{product.Cost}\",\"{product.Quantity}\",\"{product.Description}\"";
                        csvContent.AppendLine(line);
                    }

                    File.WriteAllText(saveFileDialog.FileName, csvContent.ToString(), Encoding.UTF8);
                    MessageBox.Show("Данные успешно экспортированы!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
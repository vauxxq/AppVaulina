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
    /// Логика взаимодействия для SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        public SalesWindow()
        {
            InitializeComponent();
            LoadSales();
        }

        private void LoadSales()
        {
            dgSales.ItemsSource = null; // Очищаем DataGrid
            dgSales.ItemsSource = App.AllSales; // Загружаем список продаж
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SaleAddEditWindow addWindow = new SaleAddEditWindow();
            addWindow.ShowDialog();
            LoadSales(); // Обновляем список после закрытия окна
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Sale selectedSale = dgSales.SelectedItem as Sale;
            if (selectedSale != null)
            {
                SaleAddEditWindow editWindow = new SaleAddEditWindow(selectedSale);
                editWindow.ShowDialog();
                LoadSales(); // Обновляем список после закрытия окна
            }
            else
            {
                MessageBox.Show("Выберите продажу для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Sale selectedSale = dgSales.SelectedItem as Sale;
            if (selectedSale != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эту продажу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.AllSales.Remove(selectedSale);
                    LoadSales();
                }
            }
            else
            {
                MessageBox.Show("Выберите продажу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = "SalesExport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    StringBuilder csvContent = new StringBuilder();
                    csvContent.AppendLine("Номер продажи,Номер товара,Количество,Дата продажи,Стоимость продажи");
                    foreach (var sale in App.AllSales)
                    {
                        string saleDate = sale.SaleDate.ToString("dd.MM.yyyy HH:mm");
                        string line = $"{sale.SaleId},{sale.ProductId},{sale.Quantity},\"{saleDate}\",\"{sale.SaleCost}\"";
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
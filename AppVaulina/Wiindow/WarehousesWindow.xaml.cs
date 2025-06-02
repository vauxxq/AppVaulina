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
    /// Логика взаимодействия для WarehousesWindow.xaml
    /// </summary>
    public partial class WarehousesWindow : Window
    {
        public WarehousesWindow()
        {
            InitializeComponent();
            LoadWarehouses();
        }

        private void LoadWarehouses()
        {
            dgWarehouses.ItemsSource = null; // Очищаем DataGrid
            dgWarehouses.ItemsSource = App.AllWarehouses; // Загружаем список складов
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WarehouseAddEditWindow addWindow = new WarehouseAddEditWindow();
            addWindow.ShowDialog();
            LoadWarehouses(); // Обновляем список после закрытия окна
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Warehouse selectedWarehouse = dgWarehouses.SelectedItem as Warehouse;
            if (selectedWarehouse != null)
            {
                WarehouseAddEditWindow editWindow = new WarehouseAddEditWindow(selectedWarehouse);
                editWindow.ShowDialog();
                LoadWarehouses(); // Обновляем список после закрытия окна
            }
            else
            {
                MessageBox.Show("Выберите склад для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Warehouse selectedWarehouse = dgWarehouses.SelectedItem as Warehouse;
            if (selectedWarehouse != null)
            {
                if (App.AllProducts.Any(p => p.WarehouseId == selectedWarehouse.WarehouseId))
                {
                    MessageBox.Show("Нельзя удалить склад, так как он используется в товарах.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (MessageBox.Show("Вы уверены, что хотите удалить этот склад?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.AllWarehouses.Remove(selectedWarehouse);
                    LoadWarehouses();
                }
            }
            else
            {
                MessageBox.Show("Выберите склад для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = "WarehousesExport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    StringBuilder csvContent = new StringBuilder();
                    csvContent.AppendLine("Номер склада,Номер поставщика,Количество на складе,Местоположение");
                    foreach (var warehouse in App.AllWarehouses)
                    {
                        string line = $"{warehouse.WarehouseId},{warehouse.SupplierId},{warehouse.StockQuantity},\"{warehouse.Location}\"";
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

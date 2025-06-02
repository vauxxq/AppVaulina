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
    /// Логика взаимодействия для SuppliersWindow.xaml
    /// </summary>
    public partial class SuppliersWindow : Window
    {
        public SuppliersWindow()
        {
            InitializeComponent();
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            dgSuppliers.ItemsSource = null; // Очищаем DataGrid
            dgSuppliers.ItemsSource = App.AllSuppliers; // Загружаем список поставщиков
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SupplierAddEditWindow addWindow = new SupplierAddEditWindow();
            addWindow.ShowDialog();
            LoadSuppliers(); // Обновляем список после закрытия окна
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Supplier selectedSupplier = dgSuppliers.SelectedItem as Supplier;
            if (selectedSupplier != null)
            {
                SupplierAddEditWindow editWindow = new SupplierAddEditWindow(selectedSupplier);
                editWindow.ShowDialog();
                LoadSuppliers(); // Обновляем список после закрытия окна
            }
            else
            {
                MessageBox.Show("Выберите поставщика для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Supplier selectedSupplier = dgSuppliers.SelectedItem as Supplier;
            if (selectedSupplier != null)
            {
                if (App.AllWarehouses.Any(w => w.SupplierId == selectedSupplier.SupplierId))
                {
                    MessageBox.Show("Нельзя удалить поставщика, так как он связан со складом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (MessageBox.Show("Вы уверены, что хотите удалить этого поставщика?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.AllSuppliers.Remove(selectedSupplier);
                    LoadSuppliers();
                }
            }
            else
            {
                MessageBox.Show("Выберите поставщика для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = "SuppliersExport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    StringBuilder csvContent = new StringBuilder();
                    csvContent.AppendLine("Номер поставщика,Наименование,Телефон,Почта,Адрес");
                    foreach (var supplier in App.AllSuppliers)
                    {
                        string line = $"{supplier.SupplierId},\"{supplier.Name}\",\"{supplier.Phone}\",\"{supplier.Email}\",\"{supplier.Address}\"";
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


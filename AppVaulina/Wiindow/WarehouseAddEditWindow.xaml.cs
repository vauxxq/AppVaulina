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
    /// Логика взаимодействия для WarehouseAddEditWindow.xaml
    /// </summary>
    public partial class WarehouseAddEditWindow : Window
    {
        private Warehouse currentWarehouse;

        public WarehouseAddEditWindow()
        {
            InitializeComponent();
            currentWarehouse = new Warehouse(0, 0);
            cbSupplier.ItemsSource = App.AllSuppliers;
        }

        public WarehouseAddEditWindow(Warehouse warehouse)
        {
            InitializeComponent();
            currentWarehouse = warehouse;
            cbSupplier.ItemsSource = App.AllSuppliers;
            cbSupplier.SelectedItem = App.AllSuppliers.Find(s => s.SupplierId == currentWarehouse.SupplierId);
            tbStockQuantity.Text = currentWarehouse.StockQuantity.ToString();
            tbLocation.Text = currentWarehouse.Location;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbSupplier.SelectedItem == null || string.IsNullOrWhiteSpace(tbStockQuantity.Text) || string.IsNullOrWhiteSpace(tbLocation.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(tbStockQuantity.Text, out int stockQuantity) || stockQuantity < 0)
            {
                MessageBox.Show("Количество на складе должно быть неотрицательным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentWarehouse.SupplierId = (cbSupplier.SelectedItem as Supplier)?.SupplierId ?? 0;
            currentWarehouse.StockQuantity = stockQuantity;
            currentWarehouse.Location = tbLocation.Text;

            if (currentWarehouse.WarehouseId == 0)
            {
                if (App.AllWarehouses.Count == 0)
                    currentWarehouse.WarehouseId = 1;
                else
                    currentWarehouse.WarehouseId = App.AllWarehouses.Max(w => w.WarehouseId) + 1;
                App.AllWarehouses.Add(currentWarehouse);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
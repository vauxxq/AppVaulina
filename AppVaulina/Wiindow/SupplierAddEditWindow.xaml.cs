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
    /// Логика взаимодействия для SupplierAddEditWindow.xaml
    /// </summary>
    public partial class SupplierAddEditWindow : Window
    {
        private Supplier currentSupplier;

        public SupplierAddEditWindow()
        {
            InitializeComponent();
            currentSupplier = new Supplier(0, "");
        }

        public SupplierAddEditWindow(Supplier supplier)
        {
            InitializeComponent();
            currentSupplier = supplier;
            tbName.Text = currentSupplier.Name;
            tbPhone.Text = currentSupplier.Phone;
            tbEmail.Text = currentSupplier.Email;
            tbAddress.Text = currentSupplier.Address;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Наименование поставщика обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentSupplier.Name = tbName.Text;
            currentSupplier.Phone = tbPhone.Text;
            currentSupplier.Email = tbEmail.Text;
            currentSupplier.Address = tbAddress.Text;

            if (currentSupplier.SupplierId == 0)
            {
                if (App.AllSuppliers.Count == 0)
                    currentSupplier.SupplierId = 1;
                else
                    currentSupplier.SupplierId = App.AllSuppliers.Max(s => s.SupplierId) + 1;
                App.AllSuppliers.Add(currentSupplier);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
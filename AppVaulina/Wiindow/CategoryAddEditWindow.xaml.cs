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
    /// Логика взаимодействия для CategoryAddEditWindow.xaml
    /// </summary>
    public partial class CategoryAddEditWindow : Window
    {
        private Category currentCategory;

        public CategoryAddEditWindow()
        {
            InitializeComponent();
            currentCategory = new Category(0, "");
        }

        public CategoryAddEditWindow(Category category)
        {
            InitializeComponent();
            currentCategory = category;
            tbName.Text = currentCategory.Name;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Наименование категории обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentCategory.Name = tbName.Text;

            if (currentCategory.CategoryId == 0)
            {
                if (App.AllCategories.Count == 0)
                    currentCategory.CategoryId = 1;
                else
                    currentCategory.CategoryId = App.AllCategories.Max(c => c.CategoryId) + 1;
                App.AllCategories.Add(currentCategory);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

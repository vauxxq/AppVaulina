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
using System.IO;
using Microsoft.Win32;

namespace AppVaulina.Wiindow
{
    /// <summary>
    /// Логика взаимодействия для CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        public CategoriesWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            dgCategories.ItemsSource = null; // Очищаем DataGrid
            dgCategories.ItemsSource = App.AllCategories; // Загружаем список категорий
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Wiindow.CategoryAddEditWindow addWindow = new Wiindow.CategoryAddEditWindow();
            addWindow.ShowDialog();
            LoadCategories(); // Обновляем список после закрытия окна
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Category selectedCategory = dgCategories.SelectedItem as Category;
            if (selectedCategory != null)
            {
                Wiindow.CategoryAddEditWindow editWindow = new Wiindow.CategoryAddEditWindow(selectedCategory);
                editWindow.ShowDialog();
                LoadCategories(); // Обновляем список после закрытия окна
            }
            else
            {
                MessageBox.Show("Выберите категорию для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Category selectedCategory = dgCategories.SelectedItem as Category;
            if (selectedCategory != null)
            {
                if (App.AllProducts.Any(p => p.CategoryId == selectedCategory.CategoryId))
                {
                    MessageBox.Show("Нельзя удалить категорию, так как она используется в товарах.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (MessageBox.Show("Вы уверены, что хотите удалить эту категорию?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.AllCategories.Remove(selectedCategory);
                    LoadCategories();
                }
            }
            else
            {
                MessageBox.Show("Выберите категорию для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = "CategoriesExport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    StringBuilder csvContent = new StringBuilder();
                    csvContent.AppendLine("Номер категории,Наименование");
                    foreach (var category in App.AllCategories)
                    {
                        string line = $"{category.CategoryId},\"{category.Name}\"";
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
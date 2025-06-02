using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AppVaulina
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<Classes.Product> AllProducts = new List<Classes.Product>();
        public static List<Classes.Category> AllCategories = new List<Classes.Category>();
        public static List<Classes.Supplier> AllSuppliers = new List<Classes.Supplier>();
        public static List<Classes.Sale> AllSales = new List<Classes.Sale>();
        public static List<Classes.Warehouse> AllWarehouses = new List<Classes.Warehouse>();
    }
}
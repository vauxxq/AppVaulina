using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVaulina.Classes
{
    public class Product
    {
        public int ProductId { get; set; } // Номер товара
        public int CategoryId { get; set; } // Номер категории
        public int WarehouseId { get; set; } // Номер склада
        public string Name { get; set; } // Наименование
        public decimal Cost { get; set; } // Стоимость
        public int Quantity { get; set; } // Количество
        public string Description { get; set; } // Описание

        public Product(int productId, int categoryId, int warehouseId, string name)
        {
            ProductId = productId;
            CategoryId = categoryId;
            WarehouseId = warehouseId;
            Name = name;
        }
    }
}
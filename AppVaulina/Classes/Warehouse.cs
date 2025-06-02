using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVaulina.Classes
{
    public class Warehouse
    {
        public int WarehouseId { get; set; } // Номер склада
        public int SupplierId { get; set; } // Номер поставщика
        public int StockQuantity { get; set; } // Количество на складе
        public string Location { get; set; } // Местоположение

        public Warehouse(int warehouseId, int supplierId)
        {
            WarehouseId = warehouseId;
            SupplierId = supplierId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVaulina.Classes
{
    public class Sale
    {
        public int SaleId { get; set; } // Номер продажи
        public int ProductId { get; set; } // Номер товара
        public int Quantity { get; set; } // Количество
        public DateTime SaleDate { get; set; } // Дата продажи
        public decimal SaleCost { get; set; } // Стоимость продажи

        public Sale(int saleId, int productId)
        {
            SaleId = saleId;
            ProductId = productId;
        }
    }
}
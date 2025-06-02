using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVaulina.Classes
{
    public class Supplier
    {
        public int SupplierId { get; set; } // Номер поставщика
        public string Name { get; set; } // Наименование
        public string Phone { get; set; } // Телефон
        public string Email { get; set; } // Почта
        public string Address { get; set; } // Адрес

        public Supplier(int supplierId, string name)
        {
            SupplierId = supplierId;
            Name = name;
        }
    }
}
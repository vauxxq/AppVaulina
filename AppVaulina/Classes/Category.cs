using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVaulina.Classes
{
    public class Category
    {
        public int CategoryId { get; set; } // Номер категории
        public string Name { get; set; } // Наименование

        public Category(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }
    }
}
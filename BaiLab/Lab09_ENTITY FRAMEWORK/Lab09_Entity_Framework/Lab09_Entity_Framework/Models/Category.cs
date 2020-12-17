﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab09_Entity_Framework.Models
{
    class Category
    {
        public int Id { get; set; }
         public int Name { get; set; }
        public CategoryType type { get; set; }
    }
    public enum CategoryType
    {
        Drink,
        Food
    }
}

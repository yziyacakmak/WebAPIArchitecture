﻿using App.Repositories.Products;
using Microsoft.AspNetCore.Mvc;

namespace App.Repositories.Categories
{
    public class Category: BaseEntity<int>,IAuditEntity
    {
        public string Name { get; set; } = default!;
        public List<Product>? Products { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

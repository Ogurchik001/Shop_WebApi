using System;
using System.Collections.Generic;

namespace Shop_WebApi.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

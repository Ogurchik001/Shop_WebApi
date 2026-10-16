using System;
using System.Collections.Generic;

namespace Shop_WebApi.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}

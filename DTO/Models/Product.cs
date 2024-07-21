using System;
using System.Collections.Generic;

namespace DTO.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public int? ModelId { get; set; }

    public int? DiscountId { get; set; }

    public int? Quantity { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual Model? Model { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

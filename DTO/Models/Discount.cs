using System;
using System.Collections.Generic;

namespace DTO.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public int? DiscountRate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

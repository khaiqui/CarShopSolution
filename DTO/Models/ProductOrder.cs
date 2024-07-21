using System;
using System.Collections.Generic;

namespace DTO.Models;

public partial class ProductOrder
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? Date { get; set; }

    public int? Amount { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}

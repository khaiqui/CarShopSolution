using System;
using System.Collections.Generic;

namespace DTO.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string? ModelName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

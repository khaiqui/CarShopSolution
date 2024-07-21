using System;
using System.Collections.Generic;

namespace DTO.Models;

public partial class UserService
{
    public int ServiceId { get; set; }

    public int? UserId { get; set; }

    public string? Message { get; set; }

    public DateOnly? Date { get; set; }

    public virtual User? User { get; set; }
}

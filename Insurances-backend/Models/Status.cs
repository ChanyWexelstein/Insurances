using System;
using System.Collections.Generic;

namespace Insurances_backend.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<NewInquiry> NewInquiries { get; set; } = new List<NewInquiry>();
}

using System;
using System.Collections.Generic;

namespace Insurances_backend.Models;

public partial class NewInquiry
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public double SalaryAmount { get; set; }

    public int StatusCode { get; set; }

    public virtual Status StatusCodeNavigation { get; set; } = null!;
}

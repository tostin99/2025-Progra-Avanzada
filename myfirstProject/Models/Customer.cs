﻿using System;
using System.Collections.Generic;

namespace myfirstProject.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Title { get; set; }

    public string? Suffix { get; set; }

    public string? CompanyName { get; set; }

    public string? SalesPerson { get; set; }

    public string? EmailAddress { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();
}

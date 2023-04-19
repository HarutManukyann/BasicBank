using FCBankBasicHelper.Attributes;
using System;
using System.Collections.Generic;

namespace FCBankBasicHelper.Models;

public partial class Phone
{
    public int UserId { get; set; }
    [Property]

    public string PhoneNumber { get; set; } = null!;

    public int Id { get; set; }

    public virtual User User { get; set; } = null!;
}

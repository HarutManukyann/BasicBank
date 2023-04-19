using FCBankBasicHelper.Attributes;
using System;
using System.Collections.Generic;

namespace FCBankBasicHelper.Models;

public partial class User
{
    public int Id { get; set; }
    [Property]

    public string Username { get; set; } = null!;
    [Property]

    public string Email { get; set; } = null!;   

    public string Password { get; set; } = null!;
    [Property]

    public string FirstName { get; set; } = null!;
    [Property]

    public string LastName { get; set; } = null!;

    public DateTime Birthday { get; set; }
    [Property]

    public string PassportNumber { get; set; } = null!;
    [Property]

    public string? Address { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Phone> Phones { get; } = new List<Phone>();

    public virtual ICollection<UserRolesMapping> UserRolesMappings { get; } = new List<UserRolesMapping>();
}

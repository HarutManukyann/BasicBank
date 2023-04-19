using System;
using System.Collections.Generic;

namespace FCBankBasicHelper.Models;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRolesMapping> UserRolesMappings { get; } = new List<UserRolesMapping>();
}

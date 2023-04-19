using System;
using System.Collections.Generic;

namespace FCBankBasicHelper.Models;

public partial class Token
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpire { get; set; }
}

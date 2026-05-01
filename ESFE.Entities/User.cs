using System;
using System.Collections.Generic;

namespace ESFE.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int RolId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserNickname { get; set; } = null!;

    public bool UserStatus { get; set; }

    
}

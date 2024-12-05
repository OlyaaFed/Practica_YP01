using System;
using System.Collections.Generic;

namespace WebApplication1;

public partial class Friendship
{
    public int FriendshipId { get; set; }

    public int? UserId { get; set; }

    public int? FriendId { get; set; }

    public bool? IsAccepted { get; set; }

    public virtual User? Friend { get; set; }

    public virtual User? User { get; set; }
}

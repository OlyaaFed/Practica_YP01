using System;
using System.Collections.Generic;

namespace CommunityBookLovers;

public partial class Friendship
{
    public int FriendshipId { get; set; }

    public int? UserId { get; set; }

    public int? FriendId { get; set; }

    public virtual User? Friend { get; set; }
    public bool IsAccepted { get; set; } = false;
    public virtual User? User { get; set; }
}

using System;
using System.Collections.Generic;

namespace WebApplication1;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateOnly? Date { get; set; }

    public string? ProfileimagePath { get; set; }

    public virtual ICollection<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();

    public virtual ICollection<Friendship> FriendshipFriends { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipUsers { get; set; } = new List<Friendship>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}

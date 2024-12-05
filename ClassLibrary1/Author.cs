﻿using System;
using System.Collections.Generic;

namespace ClassLibrary1;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? Name { get; set; }

    public DateOnly? Birth { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}

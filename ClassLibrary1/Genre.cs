﻿using System;
using System.Collections.Generic;

namespace ClassLibrary1;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}

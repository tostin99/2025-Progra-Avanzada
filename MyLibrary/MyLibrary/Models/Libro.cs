using System;
using System.Collections.Generic;

namespace MyLibrary.Models;

public partial class Libro
{
    public int LibroId { get; set; }

    public string? Titulo { get; set; }

    public string? Autor { get; set; }

    public virtual ICollection<Ejemplare> Ejemplares { get; set; } = new List<Ejemplare>();
}

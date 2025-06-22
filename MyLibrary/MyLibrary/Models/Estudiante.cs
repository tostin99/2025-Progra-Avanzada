using System;
using System.Collections.Generic;

namespace MyLibrary.Models;

public partial class Estudiante
{
    public int EstudianteId { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}

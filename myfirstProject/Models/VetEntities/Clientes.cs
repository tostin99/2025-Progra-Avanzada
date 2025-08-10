using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

public partial class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [StringLength(120)]
    public string Nombre { get; set; } = null!;

    [StringLength(25)]
    public string? Telefono { get; set; }

    [StringLength(120)]
    public string? Email { get; set; }

    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<Animales> Animales { get; set; } = new List<Animales>();
}

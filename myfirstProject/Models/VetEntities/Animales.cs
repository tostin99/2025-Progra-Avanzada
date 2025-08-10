using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

public partial class Animales
{
    [Key]
    public int AnimalId { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Sexo { get; set; }

    public int TipoAnimalId { get; set; }

    public int ClienteId { get; set; }

    [StringLength(500)]
    public string? Notas { get; set; }

    [InverseProperty("Animal")]
    public virtual ICollection<Citas> Citas { get; set; } = new List<Citas>();

    [ForeignKey("ClienteId")]
    [InverseProperty("Animales")]
    public virtual Clientes Cliente { get; set; } = null!;

    [ForeignKey("TipoAnimalId")]
    [InverseProperty("Animales")]
    public virtual TiposAnimales TipoAnimal { get; set; } = null!;
}

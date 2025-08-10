using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

[Index("Nombre", Name = "UQ__TiposAni__75E3EFCF6C6DFA93", IsUnique = true)]
public partial class TiposAnimales
{
    [Key]
    public int TipoAnimalId { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(250)]
    public string? Descripcion { get; set; }

    [InverseProperty("TipoAnimal")]
    public virtual ICollection<Animales> Animales { get; set; } = new List<Animales>();
}

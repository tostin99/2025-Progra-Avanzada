using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

public partial class Citas
{
    [Key]
    public long CitaId { get; set; }

    public int DoctorId { get; set; }

    public int AnimalId { get; set; }

    public DateTime Inicio { get; set; }

    public DateTime Fin { get; set; }

    public byte Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    [ForeignKey("AnimalId")]
    [InverseProperty("Citas")]
    public virtual Animales Animal { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("Citas")]
    public virtual Doctores Doctor { get; set; } = null!;

    [InverseProperty("Cita")]
    public virtual ICollection<HistorialCitas> HistorialCitas { get; set; } = new List<HistorialCitas>();
}

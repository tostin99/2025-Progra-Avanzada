using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

public partial class Doctores
{
    [Key]
    public int DoctorId { get; set; }

    [StringLength(120)]
    public string Nombre { get; set; } = null!;

    [StringLength(120)]
    public string? Especialidad { get; set; }

    public bool Activo { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<Citas> Citas { get; set; } = new List<Citas>();

    [InverseProperty("Doctor")]
    public virtual ICollection<HorariosDoctor> HorariosDoctor { get; set; } = new List<HorariosDoctor>();
}

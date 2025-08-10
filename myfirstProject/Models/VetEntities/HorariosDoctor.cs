using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

public partial class HorariosDoctor
{
    [Key]
    public int HorarioId { get; set; }

    public int DoctorId { get; set; }

    public byte DiaSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public byte DuracionMinutos { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("HorariosDoctor")]
    public virtual Doctores Doctor { get; set; } = null!;
}

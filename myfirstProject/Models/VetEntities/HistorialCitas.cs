using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myfirstProject.Models.VetEntities;

public partial class HistorialCitas
{
    [Key]
    public long HistorialId { get; set; }

    public long CitaId { get; set; }

    [StringLength(30)]
    public string Accion { get; set; } = null!;

    public DateTime? InicioAnt { get; set; }

    public DateTime? FinAnt { get; set; }

    public DateTime? InicioNuevo { get; set; }

    public DateTime? FinNuevo { get; set; }

    [StringLength(300)]
    public string? Nota { get; set; }

    public DateTime FechaRegistro { get; set; }

    [ForeignKey("CitaId")]
    [InverseProperty("HistorialCitas")]
    public virtual Citas Cita { get; set; } = null!;
}

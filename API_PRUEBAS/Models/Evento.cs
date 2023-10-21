using System;
using System.Collections.Generic;

namespace API_PRUEBAS.Models;

public partial class Evento
{
    public int Id { get; set; }

    public string? Evento1 { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioId { get; set; }

    public virtual Usuario? Usuario { get; set; }
}

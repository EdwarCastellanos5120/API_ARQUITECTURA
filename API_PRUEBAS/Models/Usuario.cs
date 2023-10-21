using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_PRUEBAS.Models;

public partial class Usuario
{
    public string Id { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? Clave { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}

using System;
using System.Collections.Generic;

namespace CrudAPICM.Models;

public partial class DestinoRuta
{
    public int IdDestino { get; set; }

    public string NombreDestino { get; set; } = null!;

    public bool Estatus { get; set; }

    public int IdRuta { get; set; }

    public string? RutaDestino { get; set; }

    //public virtual Ruta oRuta { get; set; } = null!;

    public virtual Ruta? oRuta { get; set; }
}

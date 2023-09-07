using System;
using System.Collections.Generic;

namespace CrudAPICM.Models;

public partial class Ruta
{
    public int IdRuta { get; set; }

    public string NombreRuta { get; set; } = null!;

    public string RutaInicio { get; set; } = null!;

    public bool Estatus { get; set; }

    public virtual ICollection<DestinoRuta> DestinoRuta { get; set; } = new List<DestinoRuta>();
}

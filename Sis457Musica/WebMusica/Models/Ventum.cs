using System;
using System.Collections.Generic;

namespace WebMusica.Models;

public partial class Ventum
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Direccion { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public int IdPais { get; set; }

    public int IdDepartamento { get; set; }

    public int IdCiudad { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Ciudad IdCiudadNavigation { get; set; } = null!;

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Pai IdPaisNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}

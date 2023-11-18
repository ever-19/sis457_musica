using CadMusica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMusica
{
    public class VentaDetalleCln
    {
        public static int insertar(VentaDetalle ventadetalle)
        {
            using (var context = new LabMusicaEntities())
            {
                context.VentaDetalle.Add(ventadetalle);
                context.SaveChanges();
                return ventadetalle.id;
            }
        }

        public static int actualizar(VentaDetalle ventadetalle)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.VentaDetalle.Find(ventadetalle.id);
                existente.cantidad = ventadetalle.cantidad;
                existente.precioUnitario = ventadetalle.precioUnitario;
                existente.precioTotal = ventadetalle.precioTotal;
                existente.tipoPago = ventadetalle.tipoPago;
                existente.idVenta = ventadetalle.idVenta;
                existente.idArticulo = ventadetalle.idArticulo;
                existente.usuarioRegistro = ventadetalle.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.VentaDetalle.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static VentaDetalle get(int id)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.VentaDetalle.Find(id);
            }
        }

        public static List<VentaDetalle> listar()
        {
            using (var context = new LabMusicaEntities())
            {
                return context.VentaDetalle.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paVentaDetalleListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.paVentaDetalleListar(parametro).ToList();
            }
        }
    }
}

using CadMusica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMusica
{
    public class ArticuloCln
    {
        public static int insertar(Articulo articulo)
        {
            using (var context = new LabMusicaEntities())
            {
                context.Articulo.Add(articulo);
                context.SaveChanges();
                return articulo.id;
            }
        }

        public static int actualizar(Articulo articulo)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.Articulo.Find(articulo.id);
                existente.codigo = articulo.codigo;
                existente.descripcion = articulo.descripcion;
                existente.unidadMedida = articulo.unidadMedida;
                existente.categoria = articulo.categoria;
                existente.precio = articulo.precio;
                existente.cantidadExistente = articulo.cantidadExistente;
                existente.usuarioRegistro = articulo.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.Articulo.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Articulo get(int id)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.Articulo.Find(id);
            }
        }

        public static List<Articulo> listar()
        {
            using (var context = new LabMusicaEntities())
            {
                return context.Articulo.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paArticuloListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.paArticuloListar(parametro).ToList();
            }
        }
    }
}



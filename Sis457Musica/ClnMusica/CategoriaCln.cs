using CadMusica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMusica
{
    public class CategoriaCln
    {
        public static int insertar(Categoria categoria)
        {
            using (var context = new LabMusicaEntities())
            {
                context.Categoria.Add(categoria);
                context.SaveChanges();
                return categoria.id;
            }
        }

        public static int actualizar(Categoria categoria)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.Categoria.Find(categoria.id);
                existente.nombre = categoria.nombre;

                existente.usuarioRegistro = categoria.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.Categoria.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Categoria get(int id)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.Categoria.Find(id);
            }
        }

        public static List<Categoria> listar()
        {
            using (var context = new LabMusicaEntities())
            {
                return context.Categoria.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paCategoriaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.paCategoriaListar(parametro).ToList();
            }
        }
    }
}

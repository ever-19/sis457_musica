using CadMusica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMusica
{
    public class AccesoAdminCln
    {

        public static AccesoAdmin validar2(string codigo)
        {
            using (var context = new LabMusicaEntities()) 
            {
                return context.AccesoAdmin
                    .Where(x => x.codigo == codigo)
                    .FirstOrDefault();
            }
        }

        public static int insertar(AccesoAdmin accesoadmin)
        {
            using (var context = new LabMusicaEntities())
            {
                context.AccesoAdmin.Add(accesoadmin);
                context.SaveChanges();
                return accesoadmin.id;
            }
        }

        public static int actualizar(AccesoAdmin articulo)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.AccesoAdmin.Find(articulo.id);
                existente.codigo = articulo.codigo;
                existente.usuarioRegistro = articulo.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.AccesoAdmin.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static AccesoAdmin get(int id)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.AccesoAdmin.Find(id);
            }
        }

        public static List<AccesoAdmin> listar()
        {
            using (var context = new LabMusicaEntities())
            {
                return context.AccesoAdmin.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paAccesoAdminListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.paAccesoAdminListar(parametro).ToList();
            }
        }
    }
}

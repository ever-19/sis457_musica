using CadMusica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMusica
{
    public class EmpleadoCln
    {
        public static int insertar(Empleado serie)
        {
            using (var context = new LabMusicaEntities())
            {
                context.Empleado.Add(serie);
                context.SaveChanges();
                return serie.id;
            }
        }

        public static int actualizar(Empleado empleado)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.Empleado.Find(empleado.id);
                existente.cedulaIdentidad = empleado.cedulaIdentidad;
                existente.nombre = empleado.nombre;
                existente.apellidoPaterno = empleado.apellidoPaterno;
                existente.apellidoMaterno = empleado.apellidoMaterno;
                existente.sexo = empleado.sexo;
                existente.fechaContrato = empleado.fechaContrato;
                existente.cargo = empleado.cargo;
                existente.celular = empleado.celular;
                existente.usuarioRegistro = empleado.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabMusicaEntities())
            {
                var existente = context.Empleado.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Empleado get(int id)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.Empleado.Find(id);
            }
        }

        public static List<Empleado> listar()
        {
            using (var context = new LabMusicaEntities())
            {
                return context.Empleado.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paEmpleadoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMusicaEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class LModulo
    {
        //Metodo Editar
        public static string Editar(int idmodulo, string nombre, string descripcion)
        {
            DModulo Obj = new DModulo();
            Obj.Idmodulo = idmodulo;
            Obj.Nombre = nombre;
            Obj.Descripcion = descripcion;
            return Obj.Editar(Obj);
        }

        //Metodo Mostrar

        public static DataTable Mostrar()
        {
            return new DModulo().Mostrar();
        }

        //Metodo Buscar

        public static DataTable Buscar(string textobuscar)
        {
            DModulo Obj = new DModulo();
            Obj.TextoBuscar = textobuscar;
            return Obj.Buscar(Obj);
        }
    }
}

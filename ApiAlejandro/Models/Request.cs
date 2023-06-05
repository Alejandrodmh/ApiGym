using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int id_compra { get; set; }
        public int id_producto { get; set; }
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string imagen { get; set; }
        public string descripcion { get; set; }
        public string contenido { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
    }
}
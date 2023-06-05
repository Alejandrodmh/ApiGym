using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Models
{
    public class Producto
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
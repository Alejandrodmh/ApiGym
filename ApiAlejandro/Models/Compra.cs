using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Models
{
    public class Compra
    {
        public int id_compra { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public decimal precio { get; set; }
        public int id_usuario { get; set; }
    }
}
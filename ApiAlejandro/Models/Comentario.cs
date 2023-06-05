using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Models
{
    public class Comentario
    {
        public int id_comentario { get; set; }
        public int id_producto { get; set; }
        public string contenido { get; set; }
        public DateTime fecha { get; set; }
        public int id_usuario { get; set; }
    }
}
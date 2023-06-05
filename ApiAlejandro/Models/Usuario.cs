using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAlejandro.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
    }
}
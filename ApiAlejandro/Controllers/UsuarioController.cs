using ApiAlejandro.Models;
using ApiAlejandro.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiAlejandro.Controllers
{
    [RoutePrefix("api/Gym")]
    public class UsuarioController : ApiController
    {
        private DTO_Usuario DTO = new DTO_Usuario();
        [HttpGet]
        [Route("GetUsuarios")]
        public List<Models.Usuario> GetUsuarios()
        {
            return DTO.GetUsuarios();
        }
        [HttpPost]
        [Route("GetEmail")]
        public Int64 VerificarEmail(Models.Request request)
        {
            return DTO.VerificarEmail(request);
        }
        [HttpPost]
        [Route("GetLogin")]
        public Usuario VerificarLogin(Models.Request request)
        {
            return DTO.verificarLogin(request);
        }

        [HttpPut]
        [Route("GetUsuarios")]
        public Models.Response putUsuario(Models.Request request)
        {
            return DTO.putUsuario(request);
        }

        
    }
}

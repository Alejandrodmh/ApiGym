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
    public class ComentarioController : ApiController
    {
        private DTO_Comentario DTO = new DTO_Comentario();
        [HttpGet]
        [Route("GetComentarios")]
        public List<Models.Comentario> GetComentarios()
        {
            return DTO.GetComentarios();
        }

        [HttpPut]
        [Route("GetComentarios")]
        public Models.Response putComentario(Models.Request request)
        {
            return DTO.putComentario(request);
        }

        [HttpPost]
        [Route("GetComentarios")]
        public Models.Response postComentario(Models.Request request)
        {
            return DTO.postComentario(request);
        }
        [HttpDelete]
        [Route("GetComentarios")]
        public Models.Response deleteComentario(Models.Request request)
        {
            return DTO.deleteComentario(request);
        }
    }
}

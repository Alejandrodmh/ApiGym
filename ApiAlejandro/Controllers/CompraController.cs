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
    public class CompraController : ApiController
    {
        private DTO_Compra DTO = new DTO_Compra();
        [HttpGet]
        [Route("GetCompras")]
        public List<Models.Compra> GetCompras()
        {
            return DTO.GetCompras();
        }

        [HttpPut]
        [Route("GetCompras")]
        public Models.Response putCompra(Models.Request request)
        {
            return DTO.putCompra(request);
        }

        
        [HttpDelete]
        [Route("GetCompras")]
        public Models.Response deleteCompra(Models.Request request)
        {
            return DTO.deleteCompra(request);
        }
    }
}

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
    public class ProductoController : ApiController
    {
        private DTO_Producto DTO = new DTO_Producto();
        [HttpGet]
        [Route("GetProductos")]
        public List<Models.Producto> GetProductos()
        {
            return DTO.GetProductos();
        }

        [HttpPut]
        [Route("GetProductos")]
        public Models.Response putProducto(Models.Request request)
        {
            return DTO.putProducto(request);
        }

        [HttpPost]
        [Route("GetProductos")]
        public Models.Response postProducto(Models.Request request)
        {
            return DTO.postProducto(request);
        }
        [HttpDelete]
        [Route("GetProductos")]
        public Models.Response deleteProducto(Models.Request request)
        {
            return DTO.deleteProducto(request);
        }
    }
}

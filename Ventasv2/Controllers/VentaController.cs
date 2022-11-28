using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Transactions;
using Ventasv2.Models;
using Ventasv2.Models.Request;
using Ventasv2.Models.Response;
using Ventasv2.Models.Services;

namespace Ventasv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private IVentaService _venta;

        public VentaController(IVentaService venta)
        {
            _venta = venta;
        }

        [HttpPost]

        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                //var venta = new VentaService();
                _venta.Add(model);
                respuesta.Exito = 1;                
            }
            catch(Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }
    }
}

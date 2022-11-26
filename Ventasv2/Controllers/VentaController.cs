using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Transactions;
using Ventasv2.Models;
using Ventasv2.Models.Request;
using Ventasv2.Models.Response;

namespace Ventasv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        [HttpPost]

        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {



                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {

                            var venta = new Ventum();
                            venta.Total = model.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                            venta.Fecha = DateTime.Now;
                            venta.IdCliente = model.IdCliente;
                            db.Venta.Add(venta);
                            db.SaveChanges();

                            foreach (var modelConcepto in model.Conceptos)
                            {
                                var concepto = new Models.Concepto();
                                concepto.Cantidad = modelConcepto.Cantidad;
                                concepto.IdProducto = modelConcepto.IdProducto;
                                concepto.PrecioUnitario = modelConcepto.PrecioUnitario;
                                concepto.IdVenta = venta.Id;
                                concepto.Importe = modelConcepto.Importe;
                                db.Conceptos.Add(concepto);
                                db.SaveChanges();
                            }

                            transaction.Commit();
                            respuesta.Exito = 1;

                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }       
                }
            }
            catch(Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }
    }
}

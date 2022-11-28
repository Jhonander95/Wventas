using Ventasv2.Models.Request;
using Ventasv2.Models.Response;

namespace Ventasv2.Models.Services
{
    public class VentaService : IVentaService
    {
        public void Add(VentaRequest model)
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
                            

                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw new Exception("Ocurrio una falla en la insercción");
                        }
                    }
                }
            

        }
    }
}

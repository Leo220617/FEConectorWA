using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CheckIn.API.Controllers
{
    [Authorize]
    public class ImpuestosController : ApiController
    {
        ModelCliente db;
        G G = new G();
        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var impuestos = db.Impuestos.ToList();
 
 

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, impuestos);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [Route("api/Impuestos/Consultar")]
        public async Task<HttpResponseMessage> GetOne([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var Impuestos = db.Impuestos.Where(a => a.id == id).FirstOrDefault();

                if (Impuestos == null)
                {
                    throw new Exception("Impuesto no existe");
                }


                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Impuestos);

            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Impuestos impuestos)
        {

            G.AbrirConexionAPP(out db);
            var t = db.Database.BeginTransaction();

            try
            {
                var Impuestos = db.Impuestos.Where(a => a.id == impuestos.id).FirstOrDefault();
                if (Impuestos == null)
                {
                    Impuestos = new Impuestos();
                    Impuestos.codigo = impuestos.codigo;
                    Impuestos.codigoTarifa = impuestos.codigoTarifa;
                    Impuestos.tarifa = impuestos.tarifa;
                    db.Impuestos.Add(Impuestos);
                    db.SaveChanges();

                    t.Commit();
                }
                else
                {
                    throw new Exception("Este impuesto YA existe");
                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                t.Rollback();
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();


                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPut]
        [Route("api/Impuestos/Actualizar")]
        public HttpResponseMessage Put([FromBody] Impuestos impuestos)
        {
            try
            {
                G.AbrirConexionAPP(out db);


                var Impuestos = db.Impuestos.Where(a => a.id == impuestos.id).FirstOrDefault();

                if (Impuestos != null)
                {
                    db.Entry(Impuestos).State = System.Data.Entity.EntityState.Modified;
                    Impuestos.codigo = impuestos.codigo;
                    Impuestos.codigoTarifa = impuestos.codigoTarifa;
                    Impuestos.tarifa = impuestos.tarifa;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Impuesto no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();

                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpDelete]
        [Route("api/Impuestos/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Impuestos = db.Impuestos.Where(a => a.id == id).FirstOrDefault();


                if (Impuestos != null)
                {
                    db.Impuestos.Remove(Impuestos);

                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Impuesto no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {


                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();


                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }


}
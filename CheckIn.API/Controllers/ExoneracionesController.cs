using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CheckIn.API.Controllers
{
    public class ExoneracionesController : ApiController
    {
        ModelCliente db;
        G G = new G();

        public HttpResponseMessage GetAll()
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Exoneraciones = db.Exoneraciones.ToList();
                G.CerrarConexionAPP(db);


                return Request.CreateResponse(System.Net.HttpStatusCode.OK, Exoneraciones);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);

            }
        }
        [Route("api/Exoneraciones/Consultar")]
        public HttpResponseMessage GetOne([FromUri] int ID)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                Exoneraciones exoneraciones = db.Exoneraciones.Where(a => a.id == ID).FirstOrDefault();

                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, exoneraciones);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);

            }

        }
   
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Exoneraciones exoneraciones)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                Exoneraciones Exoneracion = db.Exoneraciones.Where(a => a.id == exoneraciones.id).FirstOrDefault();
                if (Exoneracion == null)
                {
                    Exoneracion = new Exoneraciones();
                    Exoneracion.id = exoneraciones.id;
                    Exoneracion.TipoDoc = exoneraciones.TipoDoc;
                    Exoneracion.NumDoc = exoneraciones.NumDoc;
                    Exoneracion.NomInst = exoneraciones.NomInst;
                    Exoneracion.FechaEmision = exoneraciones.FechaEmision;
                    Exoneracion.PorExon = exoneraciones.PorExon;
                    db.Exoneraciones.Add(Exoneracion);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Ya existe una exoneración con este ID");
                }
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
        [Route("api/Exoneraciones/Actualizar")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Exoneraciones exoneraciones)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                Exoneraciones Exoneraciones = db.Exoneraciones.Where(a => a.id == exoneraciones.id).FirstOrDefault();
                if (Exoneraciones != null)
                {
                    db.Entry(Exoneraciones).State = System.Data.Entity.EntityState.Modified;
                    Exoneraciones.TipoDoc = exoneraciones.TipoDoc;
                    Exoneraciones.NumDoc = exoneraciones.NumDoc;
                    Exoneraciones.NomInst = exoneraciones.NomInst;
                    Exoneraciones.FechaEmision = exoneraciones.FechaEmision;
                    Exoneraciones.PorExon = exoneraciones.PorExon;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe una exoneración" +
                        " con este ID");
                }
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
        [Route("api/Exoneraciones/Eliminar")]
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                Exoneraciones Exoneraciones = db.Exoneraciones.Where(a => a.id == id).FirstOrDefault();
                if (Exoneraciones != null)
                {
                    db.Exoneraciones.Remove(Exoneraciones);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("No existe una exoneración con este ID");
                }
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
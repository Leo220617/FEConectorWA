﻿using CheckIn.API.Models;
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
    [Authorize]

    public class CorreosRecepcionController: ApiController
    {
        ModelCliente db;
        G G = new G();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Correos = db.CorreosRecepcion.ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    Correos = Correos.Where(a => a.RecepcionEmail.ToUpper().Contains(filtro.Texto.ToUpper())).ToList();
                }


                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.OK, Correos);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/CorreosRecepcion/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);



                var Correo = db.CorreosRecepcion.Where(a => a.id == id).FirstOrDefault();


                if (Correo == null)
                {
                    throw new Exception("Este correo no se encuentra registrado");
                }
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.OK, Correo);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] CorreosRecepcion correo)
        {
            try
            {

                G.AbrirConexionAPP(out db);

                var Correo = db.CorreosRecepcion.Where(a => a.id == correo.id).FirstOrDefault();

                if (Correo == null)
                {
                    Correo = new CorreosRecepcion();
                    Correo.RecepcionEmail = correo.RecepcionEmail;
                    Correo.RecepcionPassword = correo.RecepcionPassword;
                    Correo.RecepcionHostName = correo.RecepcionHostName;
                    Correo.RecepcionPort = correo.RecepcionPort;
                    Correo.RecepcionUltimaLecturaImap = DateTime.Now.AddMonths(1);
                    Correo.RecepcionUseSSL = correo.RecepcionUseSSL;


                    db.CorreosRecepcion.Add(Correo);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Este correo  YA existe");
                }

                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.OK, Correo);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("api/CorreosRecepcion/Actualizar")]
        public HttpResponseMessage Put([FromBody] CorreosRecepcion correo)
        {
            try
            {
                G.AbrirConexionAPP(out db);


                var Correo = db.CorreosRecepcion.Where(a => a.id == correo.id).FirstOrDefault();

                if (Correo != null)
                {
                    db.Entry(Correo).State = EntityState.Modified;
                    Correo.RecepcionEmail = correo.RecepcionEmail;
                    Correo.RecepcionPassword = correo.RecepcionPassword;
                    Correo.RecepcionHostName = correo.RecepcionHostName;
                    Correo.RecepcionPort = correo.RecepcionPort;
                    Correo.RecepcionUltimaLecturaImap = DateTime.Now.AddMonths(1);
                    Correo.RecepcionUseSSL = correo.RecepcionUseSSL;

                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Correo no existe");
                }
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.OK, Correo);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/CorreosRecepcion/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);


                var Correo = db.CorreosRecepcion.Where(a => a.id == id).FirstOrDefault();

                if (Correo != null)
                {


                    db.CorreosRecepcion.Remove(Correo);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Correo no existe");
                }
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
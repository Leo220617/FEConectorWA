﻿using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using CheckIn.API.Models.ModelMain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using Login = CheckIn.API.Models.ModelCliente.Login;

namespace CheckIn.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController: ApiController
    {

       ModelMain dbLogin = new ModelMain();
        ModelCliente db;
        G G = new G();


        [Route("api/Login/Compañias")]
        public async Task<HttpResponseMessage> GetCompañiasAsync([FromUri]  string Email = "")
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    throw new Exception("Se debe indicar el email");
                }

                var LicenciaUsuarios = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(Email.ToUpper())).ToList();
                if (LicenciaUsuarios == null)
                {
                    throw new Exception("Usuario no existe");
                }
                List<LicEmpresas> empresas = new List<LicEmpresas>();

                foreach(var item in LicenciaUsuarios)
                {
                    var emp = dbLogin.LicEmpresas.Where(a => a.CedulaJuridica == item.CedulaJuridica && a.Activo == true).FirstOrDefault();
                    empresas.Add(emp);
                }

                return Request.CreateResponse(HttpStatusCode.OK, empresas);

            }
            catch (Exception ex)
            {
                 

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [Route("api/Login/Conectar")]
        public async Task<HttpResponseMessage> GetLoginAsync([FromUri] string email, string clave, string CedulaJuridica = "")
        {
            try
            {
                if(string.IsNullOrEmpty(CedulaJuridica))
                {
                    throw new Exception("Se debe indicar el número de compañía a la que perteneces");
                }

                var LicenciaUsuarios = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(email.ToUpper()) && a.CedulaJuridica == CedulaJuridica).FirstOrDefault();
                if(LicenciaUsuarios == null)
                {
                    throw new Exception("Usuario no existe");
                }
                var Licencia = dbLogin.LicEmpresas.Where(a => a.CedulaJuridica == LicenciaUsuarios.CedulaJuridica).FirstOrDefault();

                if(Licencia == null)
                {
                    throw new Exception("Empresa no existe");
                }

                if(!Licencia.Activo.Value)
                {
                    throw new Exception("Empresa no se encuentra activa");
                }
                
                if(!LicenciaUsuarios.Activo.Value)
                {
                    throw new Exception("Este usuario no esta activo");
                }

               if(! BCrypt.Net.BCrypt.Verify(clave, LicenciaUsuarios.Clave))
                {
                    throw new Exception("Clave o Usuario incorrectos");
                }


                var BD = Licencia.CadenaConexionBD;

                db = new ModelCliente(BD);
                var token = TokenGenerator.GenerateTokenJwt(Licencia.CedulaJuridica, BD);

                DevolucionLogin de = new DevolucionLogin();
                var user = db.Login.Where(a => a.Email.ToUpper().Contains(LicenciaUsuarios.Email.ToUpper())).FirstOrDefault();

                if(user == null)
                {
                    throw new Exception("Usuario no existe");
                }


                var SeguridadModulos = db.SeguridadRolesModulos.Where(a => a.CodRol == user.idRol).ToList();
                var param = db.Parametros.FirstOrDefault();


                de.id = user.id ;
                de.idRol = user.idRol.Value;
                de.Email = LicenciaUsuarios.Email;
                de.CedulaJuridica = LicenciaUsuarios.CedulaJuridica;
                de.Nombre = user.Nombre;
                de.Activo = user.Activo.Value;
                de.Clave = LicenciaUsuarios.Clave;
                de.token = token;
                de.Seguridad = SeguridadModulos;
                
                return Request.CreateResponse(HttpStatusCode.OK, de);

            }
            catch (Exception ex)
            {
                
                G.GuardarTxt("ErrorLogin.txt", ex.Message + " => " + ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var Login = db.Login.ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    Login = Login.Where(a => a.Nombre.ToUpper().Contains(filtro.Texto.ToUpper()) || a.Email.ToUpper().Contains(filtro.Texto.ToUpper())).ToList();
                }
                

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Login);

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
        [Route("api/Login/Consultar")]
        public async Task<HttpResponseMessage> GetOne([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var Login = db.Login.Where(a => a.id == id).FirstOrDefault();

                if(Login == null)
                {
                    throw new Exception("Usuario no existe");
                }


                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Login);

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
        public HttpResponseMessage Post([FromBody] LoginViewModel usuario)
        {
            try
            {
                var Empresa = dbLogin.LicEmpresas.Where(a => a.CedulaJuridica == usuario.CedulaJuridica).FirstOrDefault();
                if(Empresa == null)
                {
                    throw new Exception("Empresa no existe");
                }

                db = new ModelCliente(Empresa.CadenaConexionBD);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            var t = db.Database.BeginTransaction();
            var d = dbLogin.Database.BeginTransaction();
            try
            {

                var User = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(usuario.Email.ToUpper()) && a.Activo == true).FirstOrDefault();
                var usuario1 = db.Login.Where(a => a.Email.ToUpper().Contains(usuario.Email.ToUpper()) && a.Activo == true).FirstOrDefault();
                if (usuario1 == null)
                {
                    User = new LicUsuarios();
                    User.Nombre = usuario.Nombre;
                    User.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                    User.Email = usuario.Email;
                    User.Activo = true;
                    User.CedulaJuridica = usuario.CedulaJuridica;

                    Login login = new Login();
                    login.Nombre = User.Nombre;
                    login.Clave = User.Clave;
                    login.Activo = true;
                    login.idRol = usuario.idRol;
                    login.Email = User.Email;
                   
                    db.Login.Add(login);

                    dbLogin.LicUsuarios.Add(User);
                    dbLogin.SaveChanges();
                    db.SaveChanges();

        

                    d.Commit();
                    t.Commit();
                }
                else
                {
                    throw new Exception("Este usuario YA existe");
                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                

                t.Rollback();
                d.Rollback();

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

        //Actualiza la contraseña del usuario 
        [HttpPut]
        [Route("api/Login/Actualizar")]
        public HttpResponseMessage Put([FromBody] LoginViewModel usuario)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var User = db.Login.Where(a => a.id == usuario.id).FirstOrDefault(); //a.Email.ToUpper().Contains(usuario.Email.ToUpper())
                var Usuario = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(User.Email.ToUpper()) && a.CedulaJuridica == usuario.CedulaJuridica).FirstOrDefault();

                if (Usuario != null && User != null)
                {
                    dbLogin.Entry(Usuario).State = EntityState.Modified;
                    db.Entry(User).State = EntityState.Modified;

                    if (!string.IsNullOrEmpty(usuario.Clave))
                    {

                        Usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                        User.Clave = Usuario.Clave;
                    }
       
                    if (!string.IsNullOrEmpty(usuario.Nombre))
                    {
                        Usuario.Nombre = usuario.Nombre;
                        User.Nombre = Usuario.Nombre;
                    }

                    if (!string.IsNullOrEmpty(usuario.Email))
                    {
                        Usuario.Email = usuario.Email;
                        User.Email = Usuario.Email;
                    }

                    if (usuario.idRol > 0)
                    {
                        User.idRol = usuario.idRol;
                    }
                
                
                    dbLogin.SaveChanges();
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Usuario no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message + " -> " + usuario.CedulaJuridica;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();

                G.CerrarConexionAPP(db);
                G.GuardarTxt("ErrorEditarUsuario.txt", ex.Message + " => " + ex.StackTrace);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/Login/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id, string CedulaJuridica)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var User = db.Login.Where(a => a.id == id).FirstOrDefault();
                var Usuario = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(User.Email.ToUpper()) && a.CedulaJuridica == CedulaJuridica).FirstOrDefault();


                if (Usuario != null && User != null)
                {

                    db.Entry(User).State = EntityState.Modified;
                    dbLogin.Entry(Usuario).State = EntityState.Modified;

                    if(Usuario.Activo.Value)
                    {
                        Usuario.Activo = false;
                        User.Activo = Usuario.Activo;

                    }
                    else
                    {
                        Usuario.Activo = true;
                        User.Activo = Usuario.Activo;
                    }



                    dbLogin.SaveChanges();
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Usuario no existe");
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

    internal class DevolucionLogin
    {
        public DevolucionLogin()
        {
        }
        public int id { get; set; }
        public int idRol { get; set; }
        public string CedulaJuridica { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string Clave { get; set; }
        public string token { get; set; }
        public List<SeguridadRolesModulos> Seguridad { get; set; }
    }
}
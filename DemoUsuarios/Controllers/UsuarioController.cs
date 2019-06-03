using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoUsuarios.Models.UsuarioModels;
using DemoUsuarios.Models.PersonaModels;
using System.Data.Entity;
using DemoUsuarios.Models;
using System.Collections;
using Newtonsoft.Json;

namespace DemoUsuarios.Controllers

{
    public class UsuarioController : Controller
    {
        private DemoUsuariosEntities db = new DemoUsuariosEntities();
        // GET: Usuario
        public ActionResult Usuario()
        {
         
                List<Personas> ListaPer = db.Personas.ToList();
                ViewBag.ListPersona = new SelectList(ListaPer, "IdPersona", "Nombres");

                List<Usuarios> ListUser = db.Usuarios.ToList();
                ViewBag.ListaUsuarios = ListUser;
        

            return View();
        }

        public JsonResult GetUserById(int UserId)
        {
         
                Usuarios model = db.Usuarios.Where(u => u.Id == UserId).SingleOrDefault();
                string value = string.Empty;
                value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
                       
        }
        public JsonResult SaveDataInDatabase(Usuario model)
        {
            var result = false;
            try
            {
                //Actualiza Usuarios
                if (model.Id > 0)
                {

                    Usuarios User = db.Usuarios.Find(model.Id);

                    User.Username = model.Username;
                    User.Password = FilterConfig.HASH256(model.Password);
                    User.IdPersona = model.IdPersona;
                    db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    result = true;

                }
                else
                {
                    //RegistraUsuarios
                    if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password) || model.IdPersona == 0)
                    {
                        result = false;
                    }
                    else
                    { 
                        var usuarioExiste = db.Usuarios.FirstOrDefault(u => u.Username == model.Username) != null ? true : false;
                        if (!usuarioExiste)
                        {
                            Usuarios User = new Usuarios();
                            User.Username = model.Username;
                            User.Password = FilterConfig.HASH256(model.Password);
                            User.Habilitado = 1;
                            User.IdPersona = model.IdPersona;
                            db.Usuarios.Add(User);
                            db.SaveChanges();
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }

                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteUser(int UserId)
        {
            bool result = false;
            Usuarios User = db.Usuarios.Find(UserId);
            if (User != null) { 
            db.Usuarios.Remove(User);
            db.SaveChanges();
            result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
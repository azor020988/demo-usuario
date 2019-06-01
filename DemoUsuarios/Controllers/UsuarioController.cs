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

namespace DemoUsuarios.Controllers

{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Usuario()
        {
            
            List<Usuario> listaUsuarios;
            

            using (DemoUsuariosEntities db = new DemoUsuariosEntities())
            {


                listaUsuarios = (from d in db.Usuarios
                                 select new Usuario
                                 {
                                     Id = d.Id,
                                     Username = d.Username,
                                     IdPersona = d.IdPersona

                             
                                               
                                 }).ToList();

            }

            return View(listaUsuarios);
        }


        public ActionResult Create()
        {
           
            Usuario usuarioModel = new Usuario();
            using (DemoUsuariosEntities db = new DemoUsuariosEntities())
            {
                usuarioModel.Persona = (from d in db.Personas
                                select new Persona
                                {
                                    IdPersona = d.IdPersona,
                                    Nombres = d.Nombres
                                }).ToList();
            }
                return View(usuarioModel);
            
        }
        [HttpPost]
        public ActionResult Create(Usuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using(DemoUsuariosEntities db = new DemoUsuariosEntities())
                    {

                        var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.Username == model.Username) != null ? true : false;
                        if (!usuarioExistente) { 

                        var usuario = new Usuarios();
                        usuario.Username = model.Username;
                        usuario.Password = FilterConfig.HASH256 (model.Password);
                        usuario.Habilitado = 1;
                        usuario.IdPersona = model.IdPersona;
                        db.Usuarios.Add(usuario);
                        db.SaveChanges();

                            return Redirect("~/Usuario/Usuario");
                        }

                        else
                        {
                            return Content("Usuario ya existe"); 
                        }

                        
                    }
                   
                }
                return View(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public ActionResult Update(int id)
        {
            Usuario usuarioModel = new Usuario();

            using (DemoUsuariosEntities db = new DemoUsuariosEntities())
            {
                var usuarios = db.Usuarios.Find(id);
                usuarioModel.Username = usuarios.Username;
                usuarioModel.Password = usuarios.Password;

                usuarioModel.Persona =(from d in db.Personas
                     select new Persona
                     {

                         IdPersona = d.IdPersona,
                         Nombres = d.Nombres
                     }).ToList();
            }
            return View(usuarioModel);
        }

        [HttpPost]
        public ActionResult Update(Usuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DemoUsuariosEntities db = new DemoUsuariosEntities())
                    {
                         var usuarios = db.Usuarios.Find(model.Id);
                        usuarios.Username = model.Username;
                        usuarios.Password = model.Password;
                        usuarios.Habilitado = model.Habilitado;
                        usuarios.IdPersona = model.IdPersona;
                        db.Entry(usuarios).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    return Redirect("~/Usuario/Usuario");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            Usuario usuarioModel = new Usuario();

            using (DemoUsuariosEntities db = new DemoUsuariosEntities())
            {
                
                var usuarios = db.Usuarios.Find(id);
                db.Usuarios.Remove(usuarios);
                db.SaveChanges();
                        
            }
            return Redirect("~/Usuario/Usuario");
        }
    }
}
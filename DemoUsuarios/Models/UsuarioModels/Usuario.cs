using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DemoUsuarios.Models.PersonaModels;


namespace DemoUsuarios.Models.UsuarioModels
{
    public class Usuario
    {
        public int Id { get; set; }
       
        public string Username { get; set; }
      
        public string Password { get; set; }
        public long Habilitado { get; set; }
      
        public int IdPersona { get; set; }
        
        public List <Persona> Persona { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoUsuarios.Models.PersonaModels
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public long Ci { get; set; }
        public int Direccion { get; set; }
        public int Telefono { get; set; }
    }
}
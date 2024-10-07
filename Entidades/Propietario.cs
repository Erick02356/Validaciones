using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Propietario
    {
        public int Id { get; set; }
        public long Cedula { get; set; }  
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Estado { get; set; }

    }
}

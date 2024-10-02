using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiPrueba.src.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int RUT { get; set; }

        public string email { get; set; } = string.Empty;

        public string genero { get; set; } = string.Empty;

        public string fechaNacimiento { get; set; } = string.Empty;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiPrueba.src.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null;
        public int RUT { get; set; }

        public string email { get; set; } = null;

        public string genero { get; set; } = null;
    }
}
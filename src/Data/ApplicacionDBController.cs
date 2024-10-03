using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiPrueba.src.Modelo;
using Microsoft.EntityFrameworkCore;

namespace apiPrueba.src.Data
{
    public class ApplicacionDBContext : DbContext 
    {
        public ApplicacionDBContext(DbContextOptions<ApplicacionDBContext> options) : base(options) // Asegurando que el constructor esté correcto
        {
        }

        public DbSet<Usuario> usuarios { get; set; }
    }
}

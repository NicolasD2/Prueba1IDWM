using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiPrueba.src.Modelo;
using Microsoft.EntityFrameworkCore;

namespace apiPrueba.src.Data
{
    public class ApplicacionDBController(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Usuario> usuario { get; set; }

    }
}
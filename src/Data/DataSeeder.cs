using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiPrueba.src.Modelo;
using Bogus;

namespace apiPrueba.src.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicacionDBContext>(); // Cambiado a Context
                var existingRuts = new HashSet<string>();

                if (!context.usuarios.Any())
                {
                    var userFaker = new Faker<Usuario>()
                        .RuleFor(u => u.Nombre, f => f.Person.FullName)
                        .RuleFor(u => u.RUT, f => int.Parse(GenerateRut(f, existingRuts)))
                        .RuleFor(u => u.email, f => f.Person.Email)
                        .RuleFor(u => u.genero, f => f.PickRandom("maculino", "femenino", "otro", "prefiero no decirlo"))
                        .RuleFor(u => u.fechaNacimiento, f => f.Person.DateOfBirth.ToString("yyyy-MM-dd"));

                    var users = userFaker.Generate(10);
                    context.usuarios.AddRange(users);
                    context.SaveChanges();
                }
            }
        }

        private static string GenerateRut(Faker faker, HashSet<string> existingRuts)
        {
            string rut;
            do
            {
                rut = new Faker().Random.Number(10000000, 99999999).ToString();
            } while (existingRuts.Contains(rut));
            existingRuts.Add(rut);
            return rut;
        }
    }
}

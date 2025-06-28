using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Data.Extensions
{
    public static class DbContextExtension
    {
        public static void InicializateJson<T,TDto>(this Dsw2025TpiContext context, string direccion, Func<TDto, T> factory) where T : class
        {
            if ( context.Set<T>().Any()) return;
            var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, direccion));
            var datosJson = JsonSerializer.Deserialize<List<TDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            if(datosJson is null || datosJson.Count is 0) return;

            var entidades = new List<T>();
            foreach (var dato in datosJson)
            {
                entidades.Add(factory(dato));
            }

            context.Set<T>().AddRange(entidades);
            context.SaveChanges();

        }
    }
}

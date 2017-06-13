#undef PASO_5
#define NO_DB

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data;
using Entidades;
using Servicios;

namespace Tests
{
  class Program
  {
    static void Main(string[] args)
    {
#if NO_DB
      ServiciosSeguridad seg = new ServiciosSeguridad(new NullMessenger());
      Usuario user;

      //  intentamos el login de un usuario y/o password inexistentes
      user = seg.Login("pirulo", "1234");

      if (user == null)
        Console.WriteLine(seg.Status());
      else
        Console.WriteLine("Usuario/password correctas");

      //  luego de un usuario y password correctos
      user = seg.Login("sara", "1234");
      if (user == null)
        Console.WriteLine(seg.Status());
      else
        Console.WriteLine("Usuario/password correctas");

      //  intentamos recuperar una contraseña
      seg.RecuperarPassword("sara", "gabrielaperezguerra@hotmail.com");

      //  PROBAR QUE LA CONTRASELÑA CAMBIO

#else
      OMBContext ctx = OMBContext.DB;

      AppDomain.CurrentDomain.UnhandledException += (o, e) => { ctx.Dispose(); Console.WriteLine("Excepcion"); };

      if (ctx.Database.Exists())
        Console.WriteLine("La base esta!");

#endif



#if PASO_5
      //  PASO 5 - Ingreso de algunas personas
      //
      Persona newPersona;
      Localidad rosario, perez;

      rosario = ctx.Localidades.Where(loc => loc.Nombre == "Rosario" && loc.Provincia.Nombre == "Santa Fe").FirstOrDefault();
      perez = ctx.Localidades.Where(loc => loc.Nombre == "Perez" && loc.Provincia.Nombre == "Santa Fe").FirstOrDefault();


      newPersona = new Persona()
      {
        Nombre = "Enrique Thedy",
        Localidad = rosario,
        Domicilio = "Mitre 509 Piso 5 Departamento 4",
        CodigoPostal = "S2000COK", 
        Documento = "18339577",
        TipoDocumento = ctx.TiposDeDocumento.FirstOrDefault(td => td.Descripcion == "DNI"),
        Nacimiento = new DateTime(1967, 4, 10),
        Sexo = Sexo.Masculino
      };
      ctx.Personas.Add(newPersona);

      //  newPersona = new Persona() { Nombres = "", Apellidos = "", Localidad = rosario };

      ctx.SaveChanges();
#endif

      Console.ReadLine();
    }
  }
}

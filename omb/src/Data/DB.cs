#define NO_DB

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;

namespace Data
{
#if NO_DB
  public class Database
  {
    public static Database DB { get; private set; }

    static Database()
    {
      DB = new Database();
    }

    private Database()
    {
      Personas = new List<Persona>()
      {
        new Persona() { Documento = "22555888", ID = Guid.NewGuid(), Email = "bonanseamartin@gmail.com", Nombre = "Martin Bonansea", Nacimiento = new DateTime(1900, 1, 1)},
        new Persona() { Documento = "22555888", ID = Guid.NewGuid(), Email = "gabrielaperezguerra@hotmail.com", Nombre = "Sara Gabriela Perez Guerra", Nacimiento = new DateTime(1900, 1, 1)},
        new Persona() { Documento = "22555888", ID = Guid.NewGuid(), Email = "jaquelina.ibarra@hotmail.com", Nombre = "Jaquelina Ibarra", Nacimiento = new DateTime(1900, 1, 1)},
        new Persona() { Documento = "22555888", ID = Guid.NewGuid(), Email = "samiestrellas@hotmail.com", Nombre = "Samanta Navarro", Nacimiento = new DateTime(1900, 1, 1)},
        new Persona() { Documento = "22555888", ID = Guid.NewGuid(), Email = "davorcristofer@hotmail.com", Nombre = "Davor Cristofer", Nacimiento = new DateTime(1900, 1, 1)},
        new Persona() { Documento = "22555888", ID = Guid.NewGuid(), Email = "villalba_mauro@hotmail.com", Nombre = "Mauro Villalba", Nacimiento = new DateTime(1900, 1, 1)},
        new Persona() { Documento = "22555888", ID = Guid.NewGuid(), Email = "mxm_ue@hotmail.com", Nombre = "Mariana Aquino", Nacimiento = new DateTime(1900, 1, 1)},
      };

      Usuarios = new List<Usuario>();

      foreach (Persona p in Personas)
      {
        Usuarios.Add(new Usuario(p.Nombre.Substring(0, p.Nombre.IndexOf(" ")).ToLower()) { Persona = p, Password = "1234" });
      }
    }

    public List<Persona> Personas { get; set; }
    public List<Usuario> Usuarios { get; set; }
    public List<Perfil> Perfiles { get; set; }
  }
#endif
}

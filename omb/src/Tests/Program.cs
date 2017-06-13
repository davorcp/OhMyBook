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

      Console.ReadLine();
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;

namespace Servicios
{
  /// <summary>
  /// Usado para casos de prueba o para implementar un default que no haga nada
  /// Es para no pasa null como argumento
  /// </summary>
  public class NullMessenger : IMessenger
  {
    public bool EnviarMensaje(string mensaje, Usuario destino)
    {
      Console.WriteLine("++++ No implementado");
      return true;
    }
  }
}

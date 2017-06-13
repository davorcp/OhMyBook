using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;

namespace Servicios
{
  public interface IMessenger
  {
    bool EnviarMensaje(string mensaje, Usuario destino);
  }
}

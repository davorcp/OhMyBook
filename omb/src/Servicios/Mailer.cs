using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;

namespace Servicios
{
  public class Mailer : IMessenger
  {
    private string _destinatario;

    public void AgregarDestinatario(string email)
    {
      _destinatario = email;
    }

    public bool EnviarMensaje(string mensaje)
    {
      Console.WriteLine("**** Enviando mail a {0}...", _destinatario);
      return true;
    }

    public bool EnviarMensaje(string mensaje, Usuario destino)
    {
      //AgregarDestinatario(destino.Email);
      EnviarMensaje(mensaje);

      return true;
    }
  }
}

#define NO_DB

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;
using Data;

namespace Servicios
{
  public class ServiciosSeguridad
  {
    private string _status;
    private IMessenger _mensajero;

    //  inyectamos la dependencia mediante el ctor
    //
    public ServiciosSeguridad(IMessenger msgr)
    {
      _mensajero = msgr;
    }

    /// <summary>
    /// El metodo Login retorna, o bien un usuario VALIDO o bien null, indicando que existe algun problema
    /// con la autenticacion
    /// Que problema hubo? podemos mostrar un mensaje generico o bien tener un metodo especifico que nos
    /// devuelva el error ocurrido
    /// </summary>
    /// <param name="user"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public Usuario Login(string user, string pwd)
    {
      Usuario result = null;
#if NO_DB
      Database db = Database.DB;
#else
      OMBContext db = OMBContext.DB;
#endif

      result = db.Usuarios.Find(usr => usr.Login == user && usr.Password == pwd);

      if (result == null)
      {
        //  es bueno colocar mensajes ambiguos y que el que ingresa no sepa si se coloco mal el 
        //  login o la password
        //  incrementar logins invalidos si el usuario existe
        //
        _status = "Credenciales invalidas, intente nuevamente...";
      }
      return result;
    }

    public Usuario NuevoUsuario(string login, string password, Persona persona)
    {
      return null;
    }

    public string Status()
    {
      return _status;
    }

    /// <summary>
    /// Si la combinacion de login y email coinciden entonces reseteamos la contraseña del usuario y
    /// se la enviamos mediante el implementador de IMessenger (correo, wasap, etc...)
    /// </summary>
    /// <param name="user"></param>
    /// <param name="email"></param>
    public void RecuperarPassword(string user, string email)
    {
      Usuario usuario = null;
#if NO_DB
      Database db = Database.DB;
#else
      OMBContext db = OMBContext.DB;
#endif

      _status = null;

      usuario = db.Usuarios.Find(usr => usr.Login == user && usr.Persona.Email == email);

      if (usuario != null)
      {
        //  llamar a una funcion que obtiene una nueva pass aleatoria
        string newPass = "qwerty2017";

        Console.WriteLine(">>>> Creada nueva contraseña {0} para {1}", newPass, usuario.Persona.Nombre);

        //  setear contraseña en usuario
        usuario.Password = newPass;

        Console.WriteLine(">>>> Actualizada DB de usuarios con nueva contraseña {0} a {1}", newPass, usuario.Persona.Nombre);

        //  No necesito mas este codigo porque la instancia del usuario ya esta recuperada desde la base de datos
        //
        //Usuario usr = new Usuario(user) { Password = newPass };

        //  Observar que quito toda dependencia de la clase concreta Mailer
        //  Dejo solo el "comportamiento" que debera tener cualquier clase que implemente IMessenger
        //
        //  Mailer mail = new Mailer();
        //  mail.AgregarDestinatario(email);
        //  mail.EnviarMensaje("Su nueva contraseña es qwerty2017");

        _mensajero.EnviarMensaje(string.Format("Su nueva contraseña es {0}", newPass), usuario);
      }
      _status = "Se envio un mail con la informacion solicitada";
    }
  }
}

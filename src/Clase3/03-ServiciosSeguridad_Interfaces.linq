<Query Kind="Program" />

/*
  Clase simple para representar un Usuario conectado al sistema
*/
public class Usuario
{
  public string Login;
  public string Email; 
  public string Password;

  public Usuario(string login) 
  {
    this.Login = login;
  }
}

/*
  Agregamos la posibilidad de enviar mail cuando tengo que recordar contraseña

  Eliminamos la dependencia de una clase concreta y la llevamos a una abstraccion (dependency inversion)
  Inyectamos la dependencia en el constructor
*/
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

  /*
    El metodo Login retorna, o bien un usuario VALIDO o bien null, indicando que existe algun problema
    con la autenticacion
    Que problema hubo? podemos mostrar un mensaje generico o bien tener un metodo especifico que nos
    devuelva el error ocurrido
  */
  public Usuario Login(string user, string pwd)
  {
    Usuario result = null;

    if ((user == "jperez" && pwd == "1234") || (user == "bgates" && pwd == "5678"))
    {
      result = new Usuario(user) { Password = pwd, Email = string.Format("{0}@gmail.com", user) };
    }
    else
    {
      //  es bueno colocar mensajes ambiguos y que el que ingresa no sepa si se coloco mal el 
      //  login o la password
      _status = "Credenciales invalidas...";
    }
    return result;
  }

  public string Status()
  {
    return _status;
  }

  /*
    Si la combinacion de login y email coinciden entonces reseteamos la contraseña del usuario y
    se la enviamos en un mail
  */
  public void RecuperarPassword(string user, string email)
  {
    _status = null;
    if (user == "jperez" && email == "jperez@gmail.com")
    {
      //  llamar a una funcion que obtiene una nueva pass aleatoria
      string newPass = "qwerty2017";
      Console.WriteLine(">>>> Creada nueva contraseña {0} para {1}", newPass, user);
      
      //  setear contraseña en usuario
      Console.WriteLine(">>>> Actualizada DB de usuarios con nueva contraseña {0} a {1}", newPass, user);

      Usuario usr = new Usuario(user) { Email = email, Password = newPass };

      //  Observar que quito toda dependencia de la clase concreta Mailer
      //  Dejo solo el "comportamiento" que debera tener cualquier clase que implemente IMessenger
      //
      //  Mailer mail = new Mailer();
      //  mail.AgregarDestinatario(email);
      //  mail.EnviarMensaje("Su nueva contraseña es qwerty2017");

      _mensajero.EnviarMensaje(string.Format("Su nueva contraseña es {0}", newPass), usr);
    }
    _status = "Se envio un mail con la informacion solicitada";
  }
}

public interface IMessenger
{ 
  bool EnviarMensaje(string mensaje, Usuario destino);
}

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
    AgregarDestinatario(destino.Email);
    EnviarMensaje(mensaje);
    
    return true;
  }
}

/*
  Usado para casos de prueba o para implementar un default que no haga nada
  Es para no pasa null como argumento
*/
public class NullMessenger : IMessenger
{
  public bool EnviarMensaje(string mensaje, Usuario destino)
  {
    Console.WriteLine("++++ No implementado");
    return true;
  }
}

public class WhatsappMessenger : IMessenger
{
  public bool EnviarMensaje(string mensaje, Usuario destino)
  {
    Console.WriteLine(">>>> Obtener telefono del usuario a partir del argumento");
    
    string telefono = "+5493413333222";
    
    Console.WriteLine("%%%% Enviando {2} por whatsapp a {0} Tel: {1}...", 
      destino.Login, telefono, mensaje);
    return true;
  }
}

void Main()
{
  //  Podemos cambiar de estrategia o de proveedor de mensajeria
  //  la clase ServiciosSeguridad ignora como se esta mandando el mensaje...
  //
  // ServiciosSeguridad seg = new ServiciosSeguridad(new NullMessenger());
  //  ServiciosSeguridad seg = new ServiciosSeguridad(new Mailer());
  ServiciosSeguridad seg = new ServiciosSeguridad(new WhatsappMessenger());
  
  seg.RecuperarPassword("jperez", "jperez@gmail.com");
  Console.WriteLine(seg.Status());

  Console.WriteLine("================================================================");
  seg.RecuperarPassword("jperez", "jperez@yahoo.com");
  Console.WriteLine(seg.Status());
}

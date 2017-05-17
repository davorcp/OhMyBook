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
  Eliminamos la dependencia de una clase concreta y la llevamos a una abstraccion (dependency inversion)
  Inyectamos la dependencia en el constructor
*/
public class ServiciosSeguridad
{
  private string _status;
  private IMessenger _mensajero;
  
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

    _status = null;
    if ((user == "jperez" && pwd == "1234") || (user == "bgates" && pwd == "5678"))
    {
      result = new Usuario(user) { Password = pwd };
    }
    else 
    {
      _status = "Credenciales incorrectas...";
    }
    return result;
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
      Usuario usr = new Usuario(user) { Email = email };
      
      Console.WriteLine(">>>> Crear nueva contraseña para {0}", user);
      Console.WriteLine(">>>> Enviar nueva contraseña {0} a {1}", "qwerty2017", email);
      
      //Mailer mail = new Mailer();
      //mail.AgregarDestinatario(email);
      //mail.EnviarMensaje("Su nueva contraseña es qwerty2017");
      
      _mensajero.EnviarMensaje("Su nueva contraseña es qwerty2017", usr);
    }
    _status = "Se envio un mail con la informacion solicitada"; 
  }

  public string Status() 
  {
    return _status;
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

public class NullMessenger : IMessenger
{
  public bool EnviarMensaje(string mensaje, Usuario destino)
  {
    Console.WriteLine("++++ No implementado");
    return true;
  }
}

void Main()
{
  // ServiciosSeguridad seg = new ServiciosSeguridad(new NullMessenger());
  ServiciosSeguridad seg = new ServiciosSeguridad(new Mailer());
  
  seg.RecuperarPassword("jperez", "jperez@gmail.com");
  Console.WriteLine(seg.Status());

  Console.WriteLine("================================================================");
  seg.RecuperarPassword("jperez", "jperez@yahoo.com");
  Console.WriteLine(seg.Status());
}
<Query Kind="Program" />

/*
  Agregamos composicion de clases: pensar por que no derivamos Usuario de Persona

*/
public string[] Origen = {
  "Bonansea;Martin;01/01/1900;77999888;bonanseamartin@gmail.com",
  "Perez Guerra;Sara Gabriela;01/01/1900;77999888;gabrielaperezguerra@hotmail.com",
  "Ibarra;Jaquelina;01/01/1900;77999888;jaquelina.ibarra@hotmail.com",
  "Navarro;Samanta;01/01/1900;77999888;samiestrellas@hotmail.com",
  "Cristofer;Davor;01/01/1900;77999888;davorcristofer@hotmail.com",
  "Villalba;Mauro;01/01/1900;77999888;villalba_mauro@hotmail.com",
  "Aquino;Mariana;01/01/1900;77999888;mxm_ue@hotmail.com"
};

public class Persona
{
  public Guid ID { get; set; }
  public string Nombre { get; set; }
  public DateTime Nacimiento { get; set; }
  public string Documento { get; set; }
  public string Email { get; set; }
    
  public Persona()
  {
    ID = Guid.NewGuid();
  }

  public static Persona Parse(string src)
  {
    Persona result = new Persona();
    string[] campos = src.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

    //  suponemos que esta todo bien...sin errores...
    //
    result.Nombre = string.Format("{0} {1}", campos[1].Trim(), campos[0].Trim());
    result.Documento = campos[3].Trim();
    result.Nacimiento = DateTime.Parse(campos[2].Trim());
    result.Email = campos[4].Trim();

    return result;
  }
}

public class Usuario
{
  public string Login { get; set; }
  public string Password { get; set; }
  public Persona Persona { get; set; }

  public Usuario(string login)
  {
    this.Login = login;
  }
}

void Main()
{
  Console.WriteLine("CREACION DE USUARIOS");
  
  CargarPersonas();
  personas.Dump();
  //
  Console.WriteLine("Ingrese usuario");
  Console.ReadLine();
  
  Console.WriteLine("Ingrese password");
  Console.ReadLine();

  MostrarPersonas();
  Console.WriteLine("Ingrese el numero de la persona a asociar");
  Console.ReadLine();
}


public void CargarPersonas()
{
}

public void MostrarPersonas()
{
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
      result = new Usuario(user) { Password = pwd };
    }
    else
    {
      //  es bueno colocar mensajes ambiguos y que el que ingresa no sepa si se coloco mal el 
      //  login o la password
      _status = "Credenciales invalidas...";
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

      Usuario usr = new Usuario(user) { Password = newPass };

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
    //AgregarDestinatario(destino.Email);
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

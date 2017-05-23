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

public class ServiciosSeguridad
{
  private string _status;
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
}



void Main()
{
  ServiciosSeguridad seg = new ServiciosSeguridad();
  Usuario usr;
  
  //  intento #1 ==> usuario/password incorrecto
  usr = seg.Login("bgates", "1234");
  if (usr == null)
    Console.WriteLine(seg.Status());
  else
    Console.WriteLine("Usuario conectado: {0} ; email: {1}", usr.Login, usr.Email);

  //  intento #2 ==> usuario/password correcto
  usr = seg.Login("jperez", "1234");
  if (usr == null)
    Console.WriteLine(seg.Status());
  else
    Console.WriteLine("Usuario conectado: {0} ; email: {1}", usr.Login, usr.Email);
}
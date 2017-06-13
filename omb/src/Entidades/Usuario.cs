using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
  public class Usuario
  {
    public string Login { get; set; }
    public string Password { get; set; }
    public Persona Persona { get; set; }

    public HashSet<Perfil> Perfiles { get; set; }

    public Usuario(string login)
    {
      this.Login = login;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
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
  }
}

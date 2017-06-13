﻿using System;
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

  /*
  public class Persona
  {
    public Guid IDPersona { get; set; }
    public string Nombre { get; set; }
    public DateTime Nacimiento { get; set; }
    public string Domicilio { get; set; }
    public string AmpliacionDomicilio { get; set; }
    public string CodigoPostal { get; set; }
    public string Documento { get; set; }
    public string Email { get; set; }
    public Sexo Sexo { get; set; }
    //
    public virtual Localidad Localidad { get; set; }
    public virtual TipoDocumento TipoDocumento { get; set; }

    public Persona()
    {
      //  necesario para SQL Compact
      IDPersona = Guid.NewGuid();
    }
  }
  */
}

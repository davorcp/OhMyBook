using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using Entidades;

namespace Data
{
#if DB
  public class OMBContext : DbContext
  {
    public DbSet<Localidad> Localidades { get; set; }
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<TipoDocumento> TiposDeDocumento { get; set; }

    public DbSet<Persona> Personas { get; set; }
    //  public DbSet<Usuario> Usuarios { get; set; }


    private StreamWriter _writer;

    public static OMBContext DB
    {
      get
      {
        if (_ctx == null)
          _ctx = new OMBContext();

        return _ctx;
      }
    }

    private static OMBContext _ctx;

    private OMBContext()
    {
      //Configuration.LazyLoadingEnabled = false;

#if CS5
      writer = File.CreateText(string.Format(@"{0}\{1}.LOG", Environment.CurrentDirectory, this.GetType().Name));
#else
      _writer = File.CreateText($@"{Environment.CurrentDirectory}\{this.GetType().Name}.LOG");
#endif

      Database.Log = _writer.WriteLine;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      _writer.Close();
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.Add(new ConfigurarProvincia());
      modelBuilder.Configurations.Add(new ConfigurarLocalidad());
      modelBuilder.Configurations.Add(new ConfigurarPersona());
      modelBuilder.Configurations.Add(new ConfigurarTiposDocumento());

      
      /*

      modelBuilder.Configurations.Add(new ConfigurarContacto());
      modelBuilder.Configurations.Add(new ConfigurarTipoContacto());

      modelBuilder.Configurations.Add(new ConfigurarUsuario());

      */
    }

  }

  public class ConfigurarLocalidad : EntityTypeConfiguration<Localidad>
  {
    public ConfigurarLocalidad()
    {
      this.ToTable("Localidades");
      this.HasKey(loc => loc.IDLocalidad);

      this.Property(loc => loc.IDLocalidad)
        .HasColumnName("ID_Localidad");
      this.Property(loc => loc.Nombre)
        .HasColumnName("Localidad");

      this.HasRequired(loc => loc.Provincia)
        .WithMany(prov => prov.Localidades)
        .Map(cfg => cfg.MapKey("ID_Provincia"));
    }
  }

  public class ConfigurarProvincia : EntityTypeConfiguration<Provincia>
  {
    public ConfigurarProvincia()
    {
      this.HasKey(prov => prov.IDProvincia);

      this.Property(prov => prov.Nombre)
        .HasColumnName("Provincia");

      this.Property(prov => prov.IDProvincia)
        .HasColumnName("ID_Provincia")
        .IsFixedLength()
        .HasMaxLength(1);
    }
  }

  public class ConfigurarTiposDocumento : EntityTypeConfiguration<TipoDocumento>
  {
    public ConfigurarTiposDocumento()
    {
      this.ToTable("TiposDeDocumento");
      this.HasKey(tipo => tipo.IDTipoDocumento);
      this.Property(tipo => tipo.IDTipoDocumento)
        .HasColumnName("ID_TipoDocumento");
    }
  }

  public class ConfigurarPersona : EntityTypeConfiguration<Persona>
  {
    public ConfigurarPersona()
    {
      this.HasKey(per => per.IDPersona);
      this.Property(per => per.IDPersona)
        .HasColumnName("ID_Persona");

      this.HasOptional(per => per.Localidad)
        .WithMany()
        .Map(cfg => cfg.MapKey("ID_Localidad"));

      this.HasOptional(per => per.TipoDocumento)
        .WithMany()
        .Map(cfg => cfg.MapKey("ID_TipoDocumento"));

      this.Property(per => per.Nacimiento)
        .HasColumnName("FechaNacimiento");
    }
  }


  /*

    public class ConfigurarContacto : EntityTypeConfiguration<Contacto>
    {
      public ConfigurarContacto()
      {
        this.HasKey(cont => cont.IDContacto);

        this.ToTable("Contactos");

        this.Property(cont => cont.IDContacto)
          .HasColumnName("ID_Contacto");

        this.HasRequired(cont => cont.Tipo)
          .WithMany()
          .Map(cfg => cfg.MapKey("ID_TipoContacto"));
      }
    }

    public class ConfigurarTipoContacto : EntityTypeConfiguration<TipoContacto>
    {
      public ConfigurarTipoContacto()
      {
        this.ToTable("TiposDeContacto");

        this.HasKey(tcon => tcon.IDTipoContacto);

        this.Property(tcon => tcon.RegExp)
          .HasColumnName("Validacion");

        this.Property(tcon => tcon.IDTipoContacto)
          .HasColumnName("ID_TipoContacto");
      }
    }

    public class ConfigurarUsuario : EntityTypeConfiguration<Usuario>
    {
      public ConfigurarUsuario()
      {
        this.ToTable("Usuarios");

        this.HasKey(usr => usr.Login);

        this.Property(usr => usr.PasswordExpiration)
          .HasColumnName("FechaExpiracionPassword");

        this.Property(usr => usr.LastFailLogin)
          .HasColumnName("FechaLastLoginBAD");

        this.Property(usr => usr.LastSuccessLogin)
          .HasColumnName("FechaLastLoginOK");

        this.HasRequired(usr => usr.Empleado)
          //  .WithOptional()
          .WithMany()
          .Map(cfg => cfg.MapKey("Legajo"));
      }
    }
  */

#endif

}

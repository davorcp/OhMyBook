<Query Kind="Program" />

public class Persona
{
  public string Nombre { get; set; }
  public DateTime Nacimiento { get; set; }
  public string Documento { get; set; }
  public string Email { get; set; }
}

public string[] Origen = {
  "Bonansea;Martin;01/01/1900;77999888;bonanseamartin@gmail.com",
  "Perez Guerra;Sara Gabriela;01/01/1900;77999888;gabrielaperezguerra@hotmail.com",
  "Ibarra;Jaquelina;01/01/1900;77999888;jaquelina.ibarra@hotmail.com",
  "Navarro;Samanta;01/01/1900;77999888;samiestrellas@hotmail.com",
  "Cristofer;Davor;01/01/1900;77999888;davorcristofer@hotmail.com",
  "Villalba;Mauro;01/01/1900;77999888;villalba_mauro@hotmail.com",
  "Aquino;Mariana;01/01/1900;77999888;mxm_ue@hotmail.com"
};

void Main()
{
}

public void Importar()
{
}

public void Exportar()
{
}

public Persona ParsePersona(string src)
{
  Persona result = new Persona();

  return result;
}


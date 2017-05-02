<Query Kind="Program" />


void Main()
{
  FileInfo fi = new FileInfo(@"E:\CURSOS\PTR_2017_01\local\src\Clase1\Clima en Rosario.csv");    //  por que @ ??

  if (fi.Exists)
  {
    Console.WriteLine($"Datos del archivo [{fi.FullName}]");
    Console.WriteLine($"Tamaño: {fi.Length}");
    Console.WriteLine($"Fecha/Hora de ultima modificacion: {fi.LastWriteTime}");
    //
    //  observar otros campos de FileInfo...diferencias entre fechas....
  }
  else
    Console.WriteLine("El archivo no existe!!");
}



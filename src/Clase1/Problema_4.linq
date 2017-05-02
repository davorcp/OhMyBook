<Query Kind="Program">
  <Reference Relative="CSV.dll">E:\CURSOS\PTR_2017_01\local\src\Clase1\CSV.dll</Reference>
  <Namespace>CSV</Namespace>
</Query>

void Main()
{
  FileInfo fi = new FileInfo(@"E:\CURSOS\PTR_2017_01\local\src\Clase1\Clima en Rosario.csv");
  int fila, col;

  fila = 10;
  col = 8;

  if (fi.Exists)
  {
    Console.WriteLine($"Datos del archivo [{fi.FullName}]");
    Console.WriteLine($"Tamaño: {fi.Length}");
    Console.WriteLine($"Fecha/Hora de ultima modificacion: {fi.LastWriteTime}");

    CSV.CSVFile csv = new CSV.CSVFile(fi);

    Console.WriteLine($"Lineas: {csv.Lineas} ; Max Columnas: {csv.MaxColumnas}");
    Console.WriteLine($"Elemento en la posicion [{fila}, {col}] ==> {csv.LeerCampo(fila, col)}");
  }
  else
    Console.WriteLine("El archivo no existe!!");
}



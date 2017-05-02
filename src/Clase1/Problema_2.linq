<Query Kind="Program" />


/*
*/
void Main()
{
  Console.WriteLine("Mostrar los primeros numeros pares menores a N");
  Console.WriteLine("Solucion COMPLICADA usando while + if");
  //  solicitamos el ingreso de la cantidad de numeros que queremos
  //
  Console.WriteLine("Por favor ingresar el limite de muestra");
  
  string ingreso = Console.ReadLine();
  
  int maximo = int.Parse(ingreso);
  
  int i = 1;

  while (i < maximo)  
  {
    if (i % 2 == 0)
      Console.WriteLine(i);
      
    i = i + 1;      
  }
}


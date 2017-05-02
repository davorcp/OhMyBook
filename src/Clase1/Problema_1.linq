<Query Kind="Program" />


/*
  Un bloque de comentario
*/
void Main()
{
  //  un comentario de una sola linea
  //
  Console.WriteLine("Mostrar los primeros 100 numeros pares (no consideramos el cero)");
  Console.WriteLine("Solucion sencilla usando while");
  
  int numero;
  
  numero = 1;
  while (numero <= 100)  //  tengo que asegurar la salida!!
  {
    Console.WriteLine(numero * 2);
    numero = numero + 1;      //  tambien tengo que asegurar que el contador se incremente
  }
}


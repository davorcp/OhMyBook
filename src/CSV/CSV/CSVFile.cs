using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV
{
  /// <summary>
  /// Simple clase para procesar de manera bastante rudimentaria un archivo de texto delimitado
  /// </summary>
  public class CSVFile
  {
    private FileInfo _file;
    private char _delim;

    /// <summary>
    /// Crea una nueva instancia de CSVFile, a partir de un archivo cuya informacion viene en FileInfo
    /// </summary>
    /// <param name="file"></param>
    /// <param name="delim"></param>
    public CSVFile(FileInfo file, char delim = ',')
    {
      string linea;
      int lineas, columnas;

      _file = file;
      _delim = delim;
      //
      TextReader rdr = _file.OpenText();

      lineas = columnas = 0;
      while ((linea = rdr.ReadLine()) != null)
      {
        lineas++;
        columnas = Math.Max(columnas, linea.Split(_delim).Length);
      }
      rdr.Close();
      Lineas = lineas;
      MaxColumnas = columnas;
    }

    public int Lineas { get; private set; }

    public int MaxColumnas { get; private set; }

    /// <summary>
    /// Obtiene el valor de una entrada del archivo
    /// Error si la fila supera el valor maximo, lo mismo la columna
    /// </summary>
    /// <param name="fila"></param>
    /// <param name="columna"></param>
    /// <returns></returns>
    public string LeerCampo(int fila, int columna)
    {
      string result = null;

      if (fila < Lineas && columna <= MaxColumnas)
      {
        TextReader rdr = _file.OpenText();

        string linea;
        
        //  buscamos la linea correspondiente...me posiciono en la linea anterior
        for (int i = 1; i < fila; i++)
          rdr.ReadLine();

        linea = rdr.ReadLine();
        result = linea.Split(_delim)[columna - 1];    //  recordar que empieza desde cero...
        rdr.Close();
      }
      return result;
    }
  }
}

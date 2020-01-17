using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static DynamicProgramming.Utilities;

namespace DynamicProgramming
{
  public class Instrument
  {
    public int Price { get; set; }
    public int Quality { get; set; }

    public Instrument(int price, int quality)
    {
      Price = price;
      Quality = quality;
    }

    public override string ToString() => $"<{Price}, {Quality}>";
  }

  public class KnapsackProblem
  {
    static int _limit = 10;
    static List<Instrument> _instruments = new List<Instrument>()
    {
      new Instrument(1, 5),
      new Instrument(2, 3),
      new Instrument(4, 5),
      new Instrument(2, 3),
      new Instrument(5, 2),
    };

    static List<Instrument> _selection = new List<Instrument>(); 
    static List<Instrument> _selection1 = new List<Instrument>(); 
    static List<Instrument> _selection2 = new List<Instrument>(); 
    static int[,] _matrix1 = new int[_instruments.Count, _limit];
    static int[,] _matrix2 = new int[_instruments.Count, _limit];
    static int[,] _matrix3 = new int[_instruments.Count, _limit];

    public KnapsackProblem()
    {
      WriteLine(
        "Knapsakc Problem (Dynamic Programming)\n" +
        "--------------------------------------\n"
      );

      // Print instruments.
      WriteLine($"LIMIT: {_limit}\n");
      WriteLine("INSTRUMENTS: \n");
      _instruments.ForEach((instrument) => WriteLine($"  - {instrument}"));

      // SOLUTION 1: Buy using the recursive way.
      var selected = new List<Instrument>();
      int solution1 = Method1_Recursive(_instruments, _instruments.Count - 1, _limit);
      WriteLine($"\nSOLUTION 1 (RECURSIVE WAY): {solution1}");
      // WriteLine($"\nSELECTION:");
      // selected.ForEach((instrument) => WriteLine($"  - {instrument}"));

      // SOLUTION 2: Buy using the brute force method.
      WriteLine("\nSOLUTION 2 (BRUTE FORCE):\n");
      (List<Instrument> selection2, int cumuledQuality, int cumuledPrice) = Method3_BruteForce(_instruments, _limit);
      WriteLine($"  - Cumuled Quality: {cumuledQuality}");
      WriteLine($"  - Cumuled Price: {cumuledPrice}");
      WriteLine("  - Selection: \n");
      selection2.ForEach((instrument) => WriteLine($"    - {instrument}"));

      // SOLUTION 3:
      Write("\nSOLUTION 3 (Using DP): ");
      int rows = _instruments.Count, columns = _limit;
      var matrix = new int[rows, columns];
      int solution3 = Method2_UsingDp(_instruments, matrix, rows - 1, _limit);
      WriteLine(solution3);
      
      // WriteLine("\n  - Matrix:");
      // for(int i = 0; i < rows; i++)
      //   for(int j = 0; j < columns; j++)
      //     if(matrix[i, j] != 0)
      //       WriteLine($"    - {matrix[i, j]}");

      // WriteLine("\n - Selection:");
      // _selection.ForEach((instrument) => WriteLine($"    - {instrument}")); 

      // SOLUTION 4:
      Write("\nSOLUTION 4 (Brilliant using DP): ");
      int solution4 = Method4_Brilliant(_instruments, _matrix1, _limit);
      Write(solution4);

      WriteLine("\n\n  - Matrix: \n");
      for(int i = 0; i < _instruments.Count; i++)
      {
        Write("      ");
        for(int j = 0; j < _limit; j++)
          Write($"{_matrix1[i, j]} {(j < _limit - 1 ? " " : "")}");
        
        WriteLine("");
      }

      ReadLine();
    }

    static int Method1_Recursive(List<Instrument> instruments, int i, int limitPrice)
    {
      int cumuledQuality;
      Instrument current = instruments[i];

      if(i == 0 || limitPrice == 0)
        cumuledQuality = 0;
      else if(current.Price > limitPrice) // Not take it.
        cumuledQuality = Method1_Recursive(instruments, i - 1, limitPrice);
      else // take it.
      {
        int n1 = Method1_Recursive(instruments, i - 1, limitPrice); // Take the next instrument as not taked.
        int n2 = current.Quality + Method1_Recursive(instruments, i - 1, limitPrice - current.Price);
        cumuledQuality = Maximum(n1, n2);
      }

      return cumuledQuality;
    }

    static int Method2_UsingDp(List<Instrument> instruments, int[,] matrix, int i, int limitPrice)
    {
      int cumuledQuality;
      Instrument current = instruments[i];

      if(matrix[i, limitPrice - 1] != 0)
        return matrix[i, limitPrice];

      if(i == 0 || limitPrice == 0)
        cumuledQuality = 0;
      else if(current.Price > limitPrice) // Not take it.
        cumuledQuality = Method2_UsingDp(instruments, matrix, i - 1, limitPrice);
      else // take it.
      {
        int n1 = Method2_UsingDp(instruments, matrix, i - 1, limitPrice); // Take the next instrument as not taked.
        int n2 = current.Quality + Method2_UsingDp(instruments, matrix, i - 1, limitPrice - current.Price);
        cumuledQuality = Maximum(n1, n2);
      }

      matrix[instruments.Count - 1, limitPrice - 1] = cumuledQuality;
      if(cumuledQuality != 0)
        _selection.Add(instruments[i]);

      return cumuledQuality;
    }

    static Tuple<List<Instrument>, int, int> Method3_BruteForce(List<Instrument> instruments, int maxPrice)
    {
      int cumuledQuality = 0, cumuledPrice = 0;
      var bestSelection = new List<Instrument>();

      for(int i = 1; i < Pow(2, instruments.Count); i++)
      {
        int sumPrice = 0, sumQuality = 0;
        var selection = new List<Instrument>();
        string representation = ConvertToBinaryString(i); // Binary representation.

        for(int j = 0; j < representation.Length; j++)
          if(representation[j] == '1')
          {
            selection.Add(instruments[j]);
            sumQuality += instruments[j].Quality;
            sumPrice += instruments[j].Price;
          }

        if((sumQuality > cumuledQuality) && (sumPrice <= maxPrice))
        {
          cumuledQuality = sumQuality;
          cumuledPrice = sumPrice;
          bestSelection = new List<Instrument>(selection);
        }
      }

      return new Tuple<List<Instrument>, int, int>(bestSelection, cumuledQuality, cumuledPrice);
    }

    static int Method4_Brilliant(List<Instrument> instruments, int[,] matrix, int maxWeight)
    {
      int rows = instruments.Count;

      for(int i = 1; i < instruments.Count; i++)
        for(int j = 0; j < maxWeight; j++)
        {
          if(instruments[i].Price > j)
            matrix[i, j] = matrix[i - 1, j];
          else
            matrix[i, j] = Maximum(matrix[i - 1, j], matrix[i - 1, j - instruments[i].Price] + instruments[i].Quality);
        }
 
      return matrix[rows - 1, maxWeight - 1];
    }
  }
}
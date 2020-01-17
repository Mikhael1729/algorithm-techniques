using static System.Math;
using static System.Console;
using static DynamicProgramming.Utilities;

namespace DynamicProgramming
{
  public class LegoPieces
  {
    public LegoPieces()
    {
      WriteLine(
        "\nLego Problem\n" +
        "--------------\n"
      );

      var costs = new int[10] { 1, 3, 7, 7, 10, 11, 15, 19, 21, 22 };
      var lengths = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

      var minimum2 = MinimumOfCost(lengths, costs, 100);
      WriteLine($"minimum: {minimum2}");
    }

    public static int MinimumOfCost(
      int[] lengths,
      int[] costs,
      int maxLength)
    {
      const int rows = 10;
      const int columns = 100;
      var memo = new int[rows, columns];
      int i, j;

      for(i = 1; i < rows; i++)
        for(j = 0; j < maxLength; j++)
        {
          if(costs[i] > j) // 
            memo[i,j] = memo[i - 1, j];
          else
            memo[i,j] =
              Maximum(memo[i-1, j], memo[i - 1, j] - costs[i] + lengths[i]);
        }

      return memo[rows, maxLength];
    }

    static int Maximum(int n1, int n2) => n1 > n2 ? n1 : n2;
  }
}

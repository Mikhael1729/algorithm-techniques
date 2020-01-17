using static System.Console;

namespace DynamicProgramming
{
  public class Fibonacci
  {
    public Fibonacci()
    { 
      WriteLine(
        "Fibonacci - using recursion\n" +
        "---------------------------\n"
      );

      int c, i = 0, n = 6;
      for(c = 1; c <= n; c++)
        Write(Solution2(i++) + (c < n ? ", " : ""));

      WriteLine(
        "\n\nFibonacci - using memoization\n" +
        "---------------------------\n"
      );

      c = 1;
      i = 0;
      for(; c <= n; c++)
        Write(FibonacciMemo(i++) + (c < n ? ", " : ""));
    }

    // Time = O(n^2), Space = O(n)
    static int Solution2(int n)
    {
      if(n <= 0)
        return 0;
      else if(n == 1)
        return 1;
      else
      {
        int prev = Solution2(n - 1);
        int next = Solution2(n - 2);

        // WriteLine($"prev: {prev}, next: {next}, sum: {prev + next}");
        return prev + next;
      }
    }

    // Time = O(n), Space = O(n)
    static int FibonacciMemo(int n)
    {
      var memo = new int[n + 1];
      return FibonacciMemo(n, memo);
    }

    static int FibonacciMemo(int n, int[] memo)
    {
      if(n <= 0)
        return 0;
      else if(n == 1)
        return 1;
      else if(memo[n] == 0)
        memo[n] = FibonacciMemo(n - 1) + FibonacciMemo(n -2);

      return memo[n];
    }
  }
}
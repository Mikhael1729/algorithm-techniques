using static System.Console;

namespace DynamicProgramming
{
  public class MatrixChainProblem
  {
    public MatrixChainProblem()
    {
      WriteLine(
        "Matrix-Chain Multiplication Problem\n" +
        "---------------------------------------\n"
      );

      // Recursive method.
      var matrices = new int[] { 10, 30, 5, 5,60 };
      var minimum = MatrixChainOrder(matrices);

      // Print the minum of multiplications needed.
      WriteLine($"mimum: {minimum}");
    }

    public static int MatrixChainOrder(int[] arr)
    {
      int matrixLength = arr.Length;
      int[,] m = new int[matrixLength, matrixLength];

      int i, j, k, L, cost;

      /* m[i, j] = Minimum number of scalar
      multiplications needed to compute the matrix A[i]A[i+1]...A[j] 
      = A[i..j] where dimension of A[i] is arr[i-1] x arr[i] */

      // cost is zero when multiplying 
      // one matrix. 
      //for (i = 1; i < matrixLength; i++) Esto no es necesario en C#
      //    m[i, i] = 0;

      // L is chain length. 
      for (L = 2; L < matrixLength; L++)
      {
        for (i = 1; i < matrixLength - L + 1; i++)
        {
          j = i + L - 1; // a=
          if (j == matrixLength)
            continue;

          m[i, j] = int.MaxValue;
          for (k = i; k <= j - 1; k++)
          {
            //cost/scalar multiplications 
            cost = m[i, k] + m[k + 1, j] + arr[i - 1] * arr[k] * arr[j];
            if (cost < m[i, j])
                m[i, j] = cost;
          }
        }
      }

      return m[1, matrixLength - 1];
    }
  }
}

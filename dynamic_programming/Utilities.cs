namespace DynamicProgramming
{
  public class Utilities
  {
    public Utilities()
    { }

    public static int Maximum(int n1, int n2) => n1 > n2 ? n1 : n2;
    
    public static string ConvertToBinaryString(int number)
    {
      int remainder;
      string result = string.Empty;
      while (number > 0)
      {
        remainder = number % 2;
        number /= 2;
        result = remainder.ToString() + result;
      }

      return result;
    }
  } 
}
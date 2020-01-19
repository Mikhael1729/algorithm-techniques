using System;

namespace DynamicProgramming
{
  public class Repartition
  {
    public Repartition()
    { }

    /* 
      1. Lista de personas con un valor sentimental.
      2. Hallar la cantidad mínima de chocolate para repartir, 
         tomando en cuenta lo siguiente: Si el compañero después de otro tiene un 
         mayor valor sentimental, darle más chocolate.
     */

    void ChocolateDistribution(int[] friends, int[] distribution, int quantity)
    { // Input: [1, 2, 1, 4, 5]
      // Output: [1, 2, 1, 3, 4] = 11

      // if (friends.Length == 0)
      //   return;


    }

    void SolveUsingBruteForce(int[] friends)
    {
      if(friends.Length == 0)
        return;

      // Store the distribution of chocolate for each friend.
      var distribution = new int[friends.Length];
      int size = friends.Length;


      // Organize the elements 
      var ranked = new int[friends.Length];
      Array.Copy(friends, ranked, friends.Length);

      // Distribute the first chocolate.
      distribution[0] = 1;

      // Store the current max quantity of chocolate given.
      int max = distribution[0];

      // Distribute the rest of the chocolate.
      for(int i = 0; i < ranked.Length - 1; i++)
      {
        if(friends[i + 1] > friends[i])
        {
          distribution[i + 1] = distribution[i] + 1;
        }
      }
    }
  }
}
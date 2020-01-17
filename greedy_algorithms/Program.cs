using static System.Console;
using System.Collections.Generic;
using System;

namespace greedy_algorithms
{
  class Program
  {
    static void Main(string[] args)
    {
      // Title.
      WriteLine(
        "Exchange Problem\n" +
        "----------------\n"
      );

      // Problem 1.
      var quantity = 36;
      List<int> change = FindMinimumCoins(quantity);

      WriteLine($"quantity: {quantity}");

      Write("change: ");
      for(int i = 0; i < change.Count; i ++)
        Write(change[i] + (i < change.Count - 1 ? ", " : ""));


      // Problem 2.
      WriteLine(
        "\n\nLexicographical Order Problem\n" +
        "-----------------------------\n"
      );

      var text = "BACADBA"; // DCBAABA
      string ordered = OrderLexicographically(text);

      WriteLine($"ordered: {ordered}");

      // Problem 3.
      WriteLine(
        "\n\nDomino Pilling\n" +
        "--------------\n"
      );

      int width = 3, height = 3;
      int dominoesQuantity = ComputeQuantityOfDominoes(width, height);

      WriteLine($"quantity of dominoes in a {width} x {height}: {dominoesQuantity}");

      // Problem 4.
      // WriteLine(
      //   "\n\nKnapsack Problem\n" +
      //   "-------------------\n"
      // );

      // var values = new int[] { 89, 90, 30, 50, 90, 79, 90, 10 };
      // var weights = new int[] { 123, 154, 258, 354, 365, 150, 95, 195 };

      // int result = KnapSack()
      // WriteLine($"result: {}"
    }

    static List<int> FindMinimumCoins(int quantity)
    {
      var coins = new int[] { 1, 5, 10, 25, 50, 100, 200, 500, 1000, 2000 };
      var change = new List<int>(); // Optimal General Solution.

      for(int i = coins.Length - 1; i >= 0; i--)
        while(quantity >= coins[i]) // Secure to rest always the most largest coin possible.
        {
          quantity -= coins[i]; // Rest the coin to the quantity.
          change.Add(coins[i]); // Register the coin.
        }

      return change;
    }

    static string OrderLexicographically(string text)
    {
      string ordered = "" + text[0]; // Optimal General Solution

      for(int i = 0; i < text.Length; i++)
      {
        if(text[i] >= ordered[0]) // Greedy Choice.
          ordered = text[i] + ordered;
        else
          ordered += text[i];
      }

      return ordered;
    }

    static int ComputeQuantityOfDominoes(int width, int height)
    {
      int domino = 2 * 1;
      int size = width * height;
      int quantity = 0; // [Optimal General Solution]

      // [Greedy substructure]
      while(size >= 2) // [Greedy Choice]
      {
        size -= domino;
        quantity++;
      }

      return quantity;
    }

    static int KnapSack(int capacity, int[] weight, int[] value, int itemsCount)
    {
       int[,] K = new int[itemsCount + 1, capacity + 1];
       for (int i = 0; i <= itemsCount; ++i)
       {
         for (int w = 0; w <= capacity; ++w)
         {
           if (i == 0 || w == 0)
             K[i, w] = 0;
           else if (weight[i - 1] <= w)
             K[i, w] = Math.Max(value[i - 1] + K[i - 1, w - weight[i - 1]], K[i - 1, w]);
           else
             K[i, w] = K[i - 1, w];
         }
       }
       return K[itemsCount, capacity];
    }
  }
}

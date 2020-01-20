using System;
using System.Collections.Generic;

namespace BacktrackingAndBranchAndBound
{
  public class Node
  {
    public int Level { get; set; } // Nivel en el árbol de decisión (el índice en el array)
    public int Profit { get; set; } // Beneficios de los nodos desde el nodo raíz hasta este en particular.
    public int Bound { get; set; } // Límite superior del máximo beneficio en el subtree de este nodo.
    public float Weight { get; set; }

    public override string ToString() =>
      $"N-<l:{Level}, p:{Profit}, b:{Bound}, w:{Weight}>";
  }

  public class Food : IComparable<Food>
  {
    public string Name { get; set; } // Name of the food.
    public int Value { get; set; } // How important is for me, my preference.
    public float Weight { get; set; } // Quantity of weight.
    public double Density
    {
      get => (double)Value / Weight;
    }

    public Food(string name, int value, int weight)
    {
      Name = name;
      Value = value;
      Weight = weight;
    }

    public override string ToString() => $"F-{Name}: <v:{Value}, w:{Weight}>";

    public int CompareTo(Food other) =>
      other == null ? 1 : Value.CompareTo(other.Value);
  }

  public class BranchAndBound
  {
    public BranchAndBound()
    {
      var menu = new Food[]
      {
        new Food("Hamburguesa", 1, 2),
        new Food("Lasaña", 6, 3),
        new Food("Pizza", 4, 5),
        new Food("Fritos", 7, 3),
      };

      Solve(10, menu, menu.Length); // Weight, Menu and Menu Lenght.
    }

    bool Compare(Food a, Food b) => a.Density > b.Density;

int Bound(Node node, int n, int maxWeight, Food[] arr)
{
  if(node.Weight >= maxWeight)
    return 0;
  
  int profitBound = node.Profit;
  int j = node.Level + 1;
  int totalWeight = (int) node.Weight;

  while((j < n) && totalWeight + arr[j].Weight <= maxWeight)
  {
    totalWeight += (int) arr[j].Weight;
    profitBound += arr[j].Value;
    j++;
  }

  if(j < n)
    profitBound += (int)((maxWeight - totalWeight) * arr[j].Value / arr[j].Weight);

  return profitBound;
}

int Solve(int W, Food[] arr, int n)
{
  // Sort array
  Array.Sort(arr);

  // Queue and first node.
  var Q = new Queue<Node>();
  var v = new Node();
  var u = new Node { Level = -1, Profit = 0, Weight = 0 };

  Q.Enqueue(u);

  int maxProfit = 0;
  while(Q.Count != 0)
  {
    u = Q.Dequeue();

    if(u.Level == -1)
      v.Level = 0;
    if(u.Level == n -1)
      continue;
    
    v.Level = u.Level + 1;
    v.Weight = u.Weight + arr[v.Level].Weight;
    v.Profit = u.Profit + arr[v.Level].Value;

    if(v.Weight <= W && v.Profit > maxProfit)
      maxProfit = v.Profit;

    v.Bound = Bound(v, n, W, arr);

    if(v.Bound > maxProfit)
      Q.Enqueue(v);

    v.Weight = u.Weight;
    v.Profit = u.Profit;
    v.Bound = Bound(v, n, W, arr);

    if(v.Bound > maxProfit)
      Q.Enqueue(v);
  }

  return maxProfit;
}
  }
}
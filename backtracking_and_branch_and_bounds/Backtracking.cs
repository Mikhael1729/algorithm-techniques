using System.Collections.Generic;
using static System.Console;
using System.Linq;

namespace BacktrackingAndBranchAndBound
{
  public class Backtracking
  {
    public Backtracking()
    {
    	// Representation
      var maze = new List<List<int>>(9)
			{
				new List<int>() { 1, 3 }, 	 // 0
				new List<int>() { 0, 2 }, 	 // 1
				new List<int>() { 1 }, 			 // 2
				new List<int>() { 0, 4, 6 }, // 3
				new List<int>() { 3, 5, 7 }, // 4
				new List<int>() { 4 },       // 5
				new List<int>() { 3 },       // 6
				new List<int>() { 4, 8 },    // 7
				new List<int>() { },         // 8
			};

			// Find the path that solves the maze.
			var path = SolveMaze(maze, 0, 8);

			// Print the path.
			path.ForEach((int point) => WriteLine(point));
		}

		static List<int> SolveMaze(
			List<List<int>> maze, 
			int start, 
			int finish)
		{
			// Store the current evaluating point.
			int current = start; 
			
			// Store the all visited points.
			var visited = new HashSet<int>() { current }; 
			
			// Store only the points who are part of the solution of the maze.
			var path = new List<int>() { current }; 

			// Until you reach the destination and there is a 
			// path that goes there.
			while(path.LastOrDefault() != finish && path.Count != 0)
			{
				int outletIndex = -1; // Store the last unvisited adjacent point. 

				// Evaluate the adjacent points until reach one unvisited.
				for(
					int i = 0; 
					i < maze[current].Count() && outletIndex == -1;
					i++) 
				{
					if(!visited.Contains(maze[current][i]))
						outletIndex = maze[current][i];
				}
				
				// GO TO THE NEXT POINT: If there is an unvisited point.
				if(outletIndex != -1)
				{
					path.Add(outletIndex);
					visited.Add(outletIndex);
				}
				// BACKTRACK: In the case all adjacent points of the 
				// current point are visited.
				else
					path.RemoveAt(path.Count - 1);
				
				current = path.LastOrDefault(); // Get the next point to travel.
			}

			return path;
		}
  
  }
}
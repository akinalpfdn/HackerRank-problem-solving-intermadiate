using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;



class Result
{

    /*
     * Complete the 'numberOfWays' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY roads as parameter.
     */

    public static int numberOfWays(List<List<int>> roads)
    {
        int cityCount = roads.Count + 1;
        int totalCount = 0;

        // Iterate over all possible combinations of three cities
        for (int city1 = 1; city1 <= cityCount; city1++)
        {
            for (int city2 = city1 + 1; city2 <= cityCount; city2++)
            {
                for (int city3 = city2 + 1; city3 <= cityCount; city3++)
                {
                    // Check if the distance between every pair of hotels is equal
                    if (IsDistanceEqual(city1, city2, city3, roads))
                    {
                        totalCount++;
                    }
                }
            }
        }

        return totalCount;
    }

    public static bool IsDistanceEqual(int city1, int city2, int city3, List<List<int>> roads)
    {
        int distance12 = CalculateDistance(city1, city2, roads);
        int distance13 = CalculateDistance(city1, city3, roads);
        int distance23 = CalculateDistance(city2, city3, roads);

        // Check if the distances between every pair of cities are equal
        return distance12 == distance13 && distance13 == distance23;
    }

    public static int CalculateDistance(int city1, int city2, List<List<int>> roads)
    {
        // Breadth-First Search to calculate the minimum number of roads to cross
        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();
        Dictionary<int, int> distance = new Dictionary<int, int>();

        queue.Enqueue(city1);
        visited.Add(city1);
        distance[city1] = 0;

        while (queue.Count > 0)
        {
            int currentCity = queue.Dequeue();

            if (currentCity == city2)
            {
                return distance[currentCity];
            }

            foreach (List<int> road in roads)
            {
                int startCity = road[0];
                int endCity = road[1];

                if (startCity == currentCity && !visited.Contains(endCity))
                {
                    queue.Enqueue(endCity);
                    visited.Add(endCity);
                    distance[endCity] = distance[currentCity] + 1;
                }
                else if (endCity == currentCity && !visited.Contains(startCity))
                {
                    queue.Enqueue(startCity);
                    visited.Add(startCity);
                    distance[startCity] = distance[currentCity] + 1;
                }
            }
        }

        // If the two cities are not connected, return a large value
        return int.MaxValue;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int roadsRows = Convert.ToInt32(Console.ReadLine().Trim());
        int roadsColumns = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> roads = new List<List<int>>();

        for (int i = 0; i < roadsRows; i++)
        {
            roads.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(roadsTemp => Convert.ToInt32(roadsTemp)).ToList());
        }

        int result = Result.numberOfWays(roads);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}

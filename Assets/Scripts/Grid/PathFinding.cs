using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor.Experimental.GraphView;
public static class PathFinding
{

    private static readonly int[][] Directions = new int[][]
    {
        new int[] {-1, 0}, new int[] {1, 0}, new int[] {0, -1}, new int[] {0, 1}
    };

    public static (Dictionary<(int, int), int>, Dictionary<(int, int), (int, int)>) FindShortestPaths(Tile[][] matrix, Tile start, int travelLimit)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        var distances = new Dictionary<(int, int), int>();
        var predecessors = new Dictionary<(int, int), (int, int)>();
        var priorityQueue = new SortedSet<(int cost, int x, int y)>(Comparer<(int, int, int)>.Create((a, b) => a.Item1 == b.Item1 ? (a.Item2 == b.Item2 ? a.Item3.CompareTo(b.Item3) : a.Item2.CompareTo(b.Item2)) : a.Item1.CompareTo(b.Item1)));

        distances[(start.x, start.y)] = 0;
        priorityQueue.Add((0, start.x, start.y));

        while (priorityQueue.Count > 0)
        {
            var (currentCost, x, y) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (currentCost > travelLimit) break;

            (int, int)? bestPredecessor = null; 

            foreach (var direction in Directions)
            {
                int newX = x + direction[0], newY = y + direction[1];
                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
                {
                    int newCost = currentCost + matrix[newX][newY].GetMovementCost();

                    if (newCost <= travelLimit && (!distances.ContainsKey((newX, newY)) || newCost < distances[(newX, newY)]))
                    {
                        priorityQueue.Add((newCost, newX, newY));
                        predecessors[(newX, newY)] = (x, y);
                        distances[(newX, newY)] = newCost;
                    }
                }
            }
        }

        return (distances, predecessors);
    }

    public static List<(int, int)> GetPath(Dictionary<(int, int), (int, int)> predecessors, (int, int) target)
    {
        var path = new List<(int, int)>();
        var current = target;

        while (predecessors.ContainsKey(current))
        {
            path.Add(current);
            current = predecessors[current];
        }
        path.Add(current); 
        path.Reverse(); 
        return path;
    }

}

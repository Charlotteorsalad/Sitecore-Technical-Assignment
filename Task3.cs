using System;
using System.Collections.Generic;

public class PathFinder // Core fields for pathfinding
{
    private readonly bool[,] field; // Minefield grid (true = bomb)
    private readonly int rows, cols; // Grid dimensions
    private readonly Dictionary<(int, int), bool> visited; // Track visited positions
    private readonly List<Position> path; // Totoshka's path
    private readonly List<Position> pathAlly; // Ally's path
    private readonly bool includeAlly; // Toggle Ally pathfinding

    public class Position // Position class for tracking coordinates
    {
        public int X { get; }
        public int Y { get; }
        public Position(int x, int y) { X = x; Y = y; }
        public override string ToString() => $"({X},{Y})";
    }

    public PathFinder(bool[,] bombs, bool withAlly = false) // Initialize pathfinder with minefield and ally option
    {
        field = bombs;
        rows = bombs.GetLength(0);
        cols = bombs.GetLength(1);
        includeAlly = withAlly;
        visited = new Dictionary<(int, int), bool>();
        path = new List<Position>();
        pathAlly = new List<Position>();
    }

    public (List<Position>, List<Position>) FindSafePath() // Find safe path through minefield
    {
        // Find valid starting column in first row
        int? startCol = null;
        for (int col = 0; col < cols; col++)
        {
            if (!field[0, col])
            {
                startCol = col;
                break;
            }
        }
        if (!startCol.HasValue) return (null, null);

        try
        {
            if (DFS(0, startCol.Value, -1, -1))
            {
                if (!includeAlly) return (new List<Position>(path), null);
                return (new List<Position>(path), new List<Position>(pathAlly));
            }
        }
        catch (Exception) // Handle collisions
        {
            return (null, null);
        }
        return (null, null);
    }

    private bool DFS(int row, int col, int allyRow, int allyCol) // DFS with backtracking for path finding
    {
        // Check boundaries and validity
        if (row < 0 || row >= rows || col < 0 || col >= cols ||
            visited.ContainsKey((row, col)) || field[row, col])
            return false;

        // Mark position and add to path
        visited[(row, col)] = true;
        path.Add(new Position(row, col));

        if (path.Count > 1 && includeAlly)
        {
            pathAlly.Add(new Position(allyRow, allyCol));
        }

        // Check if reached last row
        if (row == rows - 1)
            return true;

        // Try all 8 directions (including diagonals)
        var directions = new[] {
                new[] { 1, -1 }, new[] { 1, 0 }, new[] { 1, 1 },
                new[] { 0, -1 }, new[] { 0, 1 },
                new[] { -1, -1 }, new[] { -1, 0 }, new[] { -1, 1 }
            };

        // Recursive DFS in each direction
        for (int i = 0; i < directions.Length; i++)
        {
            int newRow = row + directions[i][0];
            int newCol = col + directions[i][1];
            if (DFS(newRow, newCol, row, col))
                return true;
        }

        // Backtrack if no valid path found
        if (pathAlly.Count > 0 && includeAlly)
        {
            pathAlly.RemoveAt(pathAlly.Count - 1);
        }
        path.RemoveAt(path.Count - 1);

        // Check for Totoshka-Ally collision
        if (path.Count > 0 && pathAlly.Count > 0 &&
            path[path.Count - 1].X == pathAlly[pathAlly.Count - 1].X &&
            path[path.Count - 1].Y == pathAlly[pathAlly.Count - 1].Y)
        {
            throw new Exception("Totoshka and Ally collided!");
        }

        return false;
    }
}

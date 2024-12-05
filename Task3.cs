using System;
using System.Collections.Generic;
using System.Linq;

public class PathFinder
{
    private readonly bool[,] field;
    private readonly int rows, cols;
    private readonly Dictionary<(int, int), bool> visited;
    private readonly List<Position> path;
    private readonly List<Position> pathAlly;
    private readonly bool includeAlly;

    public class Position
    {
        public int X { get; }
        public int Y { get; }
        public Position(int x, int y) { X = x; Y = y; }
        public override string ToString() => $"({X},{Y})";
    }

    public PathFinder(bool[,] bombs, bool withAlly = false)
    {
        field = bombs;
        rows = bombs.GetLength(0);
        cols = bombs.GetLength(1);
        includeAlly = withAlly;
        visited = new Dictionary<(int, int), bool>();
        path = new List<Position>();
        pathAlly = new List<Position>();
    }

    public (List<Position>, List<Position>) FindSafePath()
    {
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
        catch (Exception)
        {
            return (null, null);
        }
        return (null, null);
    }

    private bool DFS(int row, int col, int allyRow, int allyCol)
    {
        if (row < 0 || row >= rows || col < 0 || col >= cols ||
            visited.ContainsKey((row, col)) || field[row, col])
            return false;

        visited[(row, col)] = true;
        path.Add(new Position(row, col));

        if (path.Count > 1 && includeAlly)
        {
            pathAlly.Add(new Position(allyRow, allyCol));
        }

        if (row == rows - 1)
            return true;

        var directions = new[] {
                new[] { 1, -1 }, new[] { 1, 0 }, new[] { 1, 1 },
                new[] { 0, -1 }, new[] { 0, 1 },
                new[] { -1, -1 }, new[] { -1, 0 }, new[] { -1, 1 }
            };

        for (int i = 0; i < directions.Length; i++)
        {
            int newRow = row + directions[i][0];
            int newCol = col + directions[i][1];
            if (DFS(newRow, newCol, row, col))
                return true;
        }

        if (pathAlly.Count > 0 && includeAlly)
        {
            pathAlly.RemoveAt(pathAlly.Count - 1);
        }
        path.RemoveAt(path.Count - 1);

        if (path.Count > 0 && pathAlly.Count > 0 &&
            path[path.Count - 1].X == pathAlly[pathAlly.Count - 1].X &&
            path[path.Count - 1].Y == pathAlly[pathAlly.Count - 1].Y)
        {
            throw new Exception("Totoshka and Ally collided!");
        }

        return false;
    }
}

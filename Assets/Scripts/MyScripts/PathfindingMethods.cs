using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public static class PathfindingMethods
    {
        public static IEnumerable<Vector2Int> CreateDijkstraPath(byte[,] p_layout)
        {
            List<Vector2Int> m_traversables = GetTraversableTiles(p_layout);

            IPathFinder pathFinder = new Dijkstra(m_traversables);

            return pathFinder.FindPath(
                    GetMapCoordinatesOfType(p_layout, TileType.Start),
                    GetMapCoordinatesOfType(p_layout, TileType.End));
        }
        public static List<Vector2Int> GetTraversableTiles(byte[,] p_map)
        {
            List<Vector2Int> m_traversables = new List<Vector2Int>();

            for (int iRun = p_map.GetLength(0) - 1, i = 0; iRun >= 0; iRun--, i++)
            {
                for (int j = 0; j < p_map.GetLength(1); j++)
                {
                    if (TileMethods.IsWalkable(TileMethods.TypeById[p_map[iRun, j]]))
                    {
                        m_traversables.Add(new Vector2Int(j, i));
                    }
                }
            }

            return m_traversables;
        }

        public static Vector2Int GetMapCoordinatesOfType(byte[,] p_map, TileType p_type)
        {
            Vector2Int m_coordinates = Vector2Int.zero;

            for (int iRun = p_map.GetLength(0) - 1, i = 0; iRun >= 0; iRun--, i++)
            {
                for (int j = 0; j < p_map.GetLength(1); j++)
                {
                    if (TileMethods.TypeById[p_map[iRun,j]] == p_type )
                    {
                        m_coordinates = new Vector2Int(j, i);
                        break;
                    }
                }
            }

            return m_coordinates;
        }
    }
}
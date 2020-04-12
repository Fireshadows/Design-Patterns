using System;
using UnityEngine;
using System.Collections.Generic;
using Tools;
using System.Linq;

namespace AI
{
	//TODO: Implement IPathFinder using Dijsktra algorithm.
	public class Dijkstra : IPathFinder
	{
        private readonly HashSet<Vector2Int> m_rightPositions;

        public Dijkstra(IEnumerable<Vector2Int> p_rightPositions)
        {
            m_rightPositions = new HashSet<Vector2Int>(p_rightPositions);
        }

		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
            Dictionary<Vector2Int, Vector2Int?> m_ancestors = new Dictionary<Vector2Int, Vector2Int?>();
            m_ancestors.Add(start, null);
            Queue<Vector2Int> m_frontier = new Queue<Vector2Int>();
            m_frontier.Enqueue(start);

            while(m_frontier.Count > 0)
            {
                Vector2Int m_current = m_frontier.Dequeue();

                if (m_current == goal)
                {
                    break;
                }

                foreach (Vector2Int m_direction in DirectionTools.Dirs)
                {
                    Vector2Int m_next = m_current + m_direction;

                    if (m_rightPositions.Contains(m_next) && m_ancestors.ContainsKey(m_next) == false)
                    {
                        m_ancestors[m_next] = m_current;
                        m_frontier.Enqueue(m_next);
                    }
                }
            }

            if (m_ancestors.ContainsKey(goal))
            {
                List<Vector2Int> m_path = new List<Vector2Int>();

                for (Vector2Int? run = goal; run.HasValue; run = m_ancestors[run.Value])
                {
                    m_path.Add(run.Value);
                }
                m_path.Reverse();
                return m_path;
            }

            return Enumerable.Empty<Vector2Int>();
        }
	}    
}

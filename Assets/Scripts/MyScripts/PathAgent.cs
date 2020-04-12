using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    public List<Vector3> Path { get; set; }

    [SerializeField] private float m_movementSpeed = 6;

    public float MovementSpeed { get { return m_movementSpeed; } set { m_movementSpeed = value; } }

    private bool m_moving = false;

    public static List<Vector3> CreateWorldCoordinates(IEnumerable<Vector2Int> p_mapPath)
    {
        List<Vector3> m_worldPath = new List<Vector3>();
        List<Vector2Int> m_mapPath = p_mapPath.ToList();

        for (int i = 0; i < m_mapPath.Count; i++)
        {
            m_worldPath.Add(new Vector3(m_mapPath[i].x * 2, 0, m_mapPath[i].y * 2));
        }

        return m_worldPath;
    }

    public void NewPath()
    {
        if (Path == null)
            Path = new List<Vector3>();
        else
            Path.Clear();
        foreach (Vector3 m_coordinates in GameDirector.m_enemyPath)
        {
            Path.Add(m_coordinates);
        }
    }

    private void Update()
    {
        if (Path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path[0], m_movementSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, Path[0]) <= 0)
            {
                Path.RemoveAt(0);
            }
        }
    }
}

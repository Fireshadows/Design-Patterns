using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public static class LevelCreator
{
    public static BaseStorage CreateLayout(LevelData p_data, Dictionary<TileType, GameObject> p_tiles)
    {
        PlayerBase m_playerBase = null;
        GameObject m_enemyBase = null;
        for (int iRun = p_data.Layout.GetLength(0) - 1, i = 0; iRun >= 0; iRun--, i++)
        {
            for (int j = 0; j < p_data.Layout.GetLength(1); j++)
            {

                TileType m_type = TileMethods.TypeById[p_data.Layout[iRun,j]];
                GameObject m_prefab = null;
                m_prefab = p_tiles[m_type];
                if (!m_prefab)
                    continue;
                
                GameObject m_tile = Object.Instantiate(m_prefab, new Vector3(j * 2, 0, i * 2), Quaternion.identity);

                if (m_type == TileType.End)
                    m_playerBase = m_tile.GetComponent<PlayerBase>();
                else if (m_type == TileType.Start)
                    m_enemyBase = m_tile;
            }
        }
        return new BaseStorage(m_playerBase, m_enemyBase);
    }
    public static Dictionary<UnitType, GameObjectPool> CreateEnemies(LevelData p_data, Dictionary<UnitType, GameObject> p_enemies)
    {
        Dictionary<UnitType, GameObjectPool> m_pools = new Dictionary<UnitType, GameObjectPool>();

        int[] m_totalEnemies = new int[p_data.Waves.GetLength(1)];

        for (int i = 0; i < m_totalEnemies.Length; i++)
        {
            if (i > p_enemies.Count)
                break;
            m_pools[UnitMethods.TypeById[i]] = new GameObjectPool(1, p_enemies[UnitMethods.TypeById[i]], 1);
        }

        return m_pools;
    }
}

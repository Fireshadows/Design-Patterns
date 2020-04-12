using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class EnemySpawner : MonoBehaviour
{
    private Transform SpawnPoint { get; set; }
    private Dictionary<UnitType, GameObjectPool> m_enemyPools;

    private byte[,] Waves { get; set; }

    private float m_subWavePause = 2;
    private float m_wavePause = 3;

    public void Init(Transform p_spawnPoint, byte[,] p_waves, Dictionary<UnitType, GameObjectPool> p_poolDictionary)
    {
        SpawnPoint = p_spawnPoint;
        Waves = p_waves;
        m_enemyPools = p_poolDictionary;
    }
    public void StartSpawning()
    {
        StartCoroutine(nameof(SpawnEnemies));
    }
    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnEnemies()
    {
        for (int m_wave = 0; m_wave < Waves.GetLength(0); m_wave++)
        {
            for (int m_subWave = 0; m_subWave < Waves.GetLength(1); m_subWave++)
            {
                if (m_subWave > m_enemyPools.Count)
                {
                    Debug.Log("ERROR! Exceeded amount of enemy pools");
                    break;
                }

                for (int i = 0; i < Waves[m_wave, m_subWave]; i++)
                {
                    GameObject m_newEnemy = m_enemyPools[UnitMethods.TypeById[m_subWave]].Rent(false);
                    m_newEnemy.transform.position = SpawnPoint.position;
                    m_newEnemy.SetActive(true);
                    yield return new WaitForSeconds(m_subWavePause);
                }
            }
            yield return new WaitForSeconds(m_wavePause);
        }
    }
}

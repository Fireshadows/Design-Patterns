using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

[RequireComponent(typeof(EnemySpawner))]
public class GameDirector : MonoBehaviour
{
    [SerializeField] private TextAsset m_textAsset;
    [SerializeField] private TileData[] m_tileTypes;
    [SerializeField] private EnemyData[] m_enemyPrefabs;
    [SerializeField] private HealthDisplay m_healthDisplay;

    private Dictionary<TileType, GameObject> m_tileDictionary;
    private Dictionary<UnitType, GameObject> m_enemyDictionary;

    private LevelData m_levelData = default;

    private BaseStorage m_baseStorage;
    private EnemySpawner m_spawner;

    public static List<Vector3> m_enemyPath;

    void Start()
    {
        m_spawner = GetComponent<EnemySpawner>();

        CreateDictionaries();
        SetUp();

        m_spawner.StartSpawning();
    }

    private void CreateDictionaries()
    {
        m_tileDictionary = new Dictionary<TileType, GameObject>();
        foreach (TileData m_tileData in m_tileTypes)
            m_tileDictionary[m_tileData.m_type] = m_tileData.m_prefab;

        m_enemyDictionary = new Dictionary<UnitType, GameObject>();
        foreach (EnemyData m_enemyData in m_enemyPrefabs)
            m_enemyDictionary[m_enemyData.m_type] = m_enemyData.m_prefab;
    }

    private void SetUp()
    {
        if (m_textAsset)
        {
            m_levelData = TextReader.ProcessText(m_textAsset);


            if (m_tileDictionary.Count > 0)
            {
                m_baseStorage = LevelCreator.CreateLayout(m_levelData, m_tileDictionary);

                IEnumerable<Vector2Int> path = PathfindingMethods.CreateDijkstraPath(m_levelData.Layout);

                m_enemyPath = PathAgent.CreateWorldCoordinates(path);
                if (m_healthDisplay)
                {
                    m_baseStorage.PlayerBase.OnHealthChanged += m_healthDisplay.UpdateHealth;
                    m_baseStorage.PlayerBase.OnHealthChanged += GameOver;
                    m_healthDisplay.UpdateHealth(m_baseStorage.PlayerBase.Lives);
                }
                else
                    Debug.Log("ERROR! There's no health display!");
            }

            if (m_enemyDictionary.Count > 0)
            {
                m_spawner.Init(m_baseStorage.EnemyBase.transform, m_levelData.Waves, LevelCreator.CreateEnemies(m_levelData, m_enemyDictionary));
            }

        }
    }

    private void GameOver(int p_health)
    {
        if(p_health <= 0)
            m_spawner.StopSpawning();
    }
}

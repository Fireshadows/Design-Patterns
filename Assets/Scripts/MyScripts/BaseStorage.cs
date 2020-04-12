using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStorage
{
    public PlayerBase PlayerBase { get; }
    public GameObject EnemyBase { get; }

    public BaseStorage(PlayerBase p_playerBase, GameObject p_enemyBase)
    {
        PlayerBase = p_playerBase;
        EnemyBase = p_enemyBase;
    }
}

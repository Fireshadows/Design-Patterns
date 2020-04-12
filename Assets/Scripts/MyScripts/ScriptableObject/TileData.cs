using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Data", menuName = "Tile Data", order = 0)]
public class TileData : ScriptableObject
{
    public TileType m_type;
    public GameObject m_prefab;
}

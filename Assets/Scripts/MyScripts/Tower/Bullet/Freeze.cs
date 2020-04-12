using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Bullet
{
    private void Awake()
    {
        m_bulletType = BulletType.Freeze;
    }
}

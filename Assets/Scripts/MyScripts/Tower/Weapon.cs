using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public GameObject m_bullet;

    private GameObjectPool m_bulletPool;

    Tower User { get; set; } 

    public void Init(Tower p_user)
    {
        User = p_user;
        m_bulletPool = new GameObjectPool(5, m_bullet, 1);
    }

    public void Fire()
    {
        if (User) {
            GameObject m_rentedBullet = m_bulletPool.Rent(false);
            m_rentedBullet.transform.position = User.m_bulletSpawnPoint? User.m_bulletSpawnPoint.position : User.transform.position;

            m_rentedBullet.transform.LookAt(User.CurrentTarget);
            m_rentedBullet.SetActive(true);
        }
    }
}

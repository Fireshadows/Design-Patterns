using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    [SerializeField] private float m_bombRadius = 3;
    [SerializeField] private LayerMask m_layerMask;

    private bool m_blown = false;

    void Awake()
    {
        m_bulletType = BulletType.Bomb;
    }

    override public void OnEnable()
    {
        base.OnEnable();
        m_blown = false;
    }

    public void Explode()
    {
        if (m_blown)
            return;
        else m_blown = true;

        m_lifeTime = -1;

        Collider[] m_enemyColliders = Physics.OverlapSphere(transform.position, m_bombRadius, m_layerMask);

        if(m_enemyColliders.Length > 0)
        {
            foreach (Collider m_collider in m_enemyColliders)
            {
                m_collider.GetComponent<Boxymon>().TakeDamage((int)m_value);
            }
        }
    }
}

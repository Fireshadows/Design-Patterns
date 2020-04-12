using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Bomb,
    Freeze
}

public class Bullet : MonoBehaviour
{
    [System.NonSerialized] public BulletType m_bulletType;
    [Tooltip("Bomb Type = Damage, Freeze Type = Slow Duration")]
    public float m_value;

    protected float m_lifeDuration = 3;
    protected float m_lifeTime;

    public virtual void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 1200, ForceMode.Force);
        m_lifeTime = m_lifeDuration;
    }

    private void Update()
    {
        if (m_lifeTime > 0)
        {
            m_lifeTime -= Time.deltaTime;
            if (m_lifeTime <= 0)
                gameObject.SetActive(false);
        }
    }
}  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] public Transform m_bulletSpawnPoint;
    [SerializeField] private Weapon m_weapon;

    [SerializeField] private float m_fireCooldown = .25f;
    private float m_fireTime;

    private List<Transform> m_targets;
    public Transform CurrentTarget { get { return m_targets.Count > 0? m_targets[0] : null; } }

    void Start()
    {
        m_targets = new List<Transform>();

        m_weapon.Init(this);
    }

    void Update()
    {
        if (CurrentTarget) {
            if(!CurrentTarget.gameObject.activeSelf)
                m_targets.Remove(CurrentTarget);
        }

        if (m_fireTime > 0)
        {
            m_fireTime -= Time.deltaTime;
        }
        else
        {
            if (CurrentTarget)
                UseWeapon();
        }
    }
    void UseWeapon()
    {
        if (m_weapon != null)
        {
            m_weapon.Fire();
            m_fireTime = m_fireCooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(m_targets != null)
            m_targets.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if(m_targets != null)
            m_targets.Remove(other.transform);
    }
}

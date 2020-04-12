using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathAgent))]
public class Boxymon : MonoBehaviour
{
    [SerializeField] private int m_HP = 10;
    [SerializeField] private int m_damage = 10;
    [SerializeField] private int m_defense = 2;

    private int m_maxHP;
    private float m_freezeRecoveryTime;

    private PathAgent m_pathAgent;
    private float m_originalMovementSpeed;


    public bool Frozen { get { return m_freezeRecoveryTime > 0; } }

    private void Awake()
    {
        m_maxHP = m_HP;
        m_pathAgent = GetComponent<PathAgent>();
        m_originalMovementSpeed = m_pathAgent.MovementSpeed;
    }

    private void OnEnable()
    {
        m_HP = m_maxHP;
        m_pathAgent.NewPath();
        m_pathAgent.MovementSpeed = m_originalMovementSpeed;
        m_freezeRecoveryTime = 0;
    }

    private void Update()
    {
        if(Frozen)
        {
            m_freezeRecoveryTime -= Time.deltaTime;
            if (!Frozen)
                m_pathAgent.MovementSpeed = m_originalMovementSpeed;
        }
    }

    public void TakeDamage(int p_damage)
    {
        p_damage -= m_defense;
        if(p_damage > 0)
        {
            m_HP -= p_damage;
            if (m_HP <= 0)
                gameObject.SetActive(false);
        }
    }

    private void Freeze(float p_freezeRecoveryDuration)
    {
        m_freezeRecoveryTime = p_freezeRecoveryDuration;
        m_pathAgent.MovementSpeed /= 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            switch (collision.gameObject.GetComponent<Bullet>().m_bulletType)
            {
                case BulletType.Bomb:
                    collision.gameObject.GetComponent<Bomb>().Explode();
                    break;
                case BulletType.Freeze:
                    Freeze(collision.gameObject.GetComponent<Bullet>().m_value);
                    break;
                default:
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
}

using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int m_lives = 20;

    public int Lives { get { return m_lives; } }

    public event Action<int> OnHealthChanged;

    private void LoseLife()
    {
        m_lives--;
        OnHealthChanged.Invoke(m_lives);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            LoseLife();
            other.gameObject.SetActive(false);
        }
    }
}

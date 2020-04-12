using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Text m_text;
    public void UpdateHealth(int p_health)
    {
        if (p_health > 0)
            m_text.text = "Health: " + p_health.ToString();
        else
            m_text.text = "Game Over!";
    }
}

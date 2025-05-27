using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class HealthBar : MonoBehaviour
{
    public UnityEngine.UI.Image fillImage;

    public void SetMaxHealth(float maxHealth)
    {
        fillImage.fillAmount = 1f; // Full health
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        fillImage.fillAmount = currentHealth / maxHealth;
    }
}

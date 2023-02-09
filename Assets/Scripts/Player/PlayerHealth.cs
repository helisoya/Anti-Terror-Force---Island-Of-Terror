using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    private int health;

    [SerializeField]
    private int maxHealth;

    void Awake()
    {
        instance = this;
        health = maxHealth;
    }

    public void AddHealth(int val)
    {
        health = Mathf.Clamp(health + val, 0, maxHealth);
        PlayerUI.instance.SetHealthBarLength(health, maxHealth);

        if (health == 0)
        {
            PlayerUI.instance.FadeToBlackBeforeLoading(SceneManager.GetActiveScene().name);
        }
    }

    public bool IsAtMaxHealth()
    {
        return health == maxHealth;
    }
}

using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBar houseHealthBar;
    [SerializeField] private HealthBar playerHealthBar;

    public void SetHouseMaxHealth(float value)
    {
        houseHealthBar.MaxHealth = value;
    }

    public void SetHouseCurrentHealth(float value)
    {
        houseHealthBar.CurrentHealth = value;
    }

    public void SetPlayerMaxHealth(float value)
    {
        playerHealthBar.MaxHealth = value;
    }

    public void SetPlayerCurrentHealth(float value)
    {
        playerHealthBar.CurrentHealth = value;
    }
}

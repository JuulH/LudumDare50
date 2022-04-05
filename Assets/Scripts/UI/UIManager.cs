using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBar houseHealthBar;
    [SerializeField] private HealthBar playerHealthBar;

    [SerializeField] private GameObject houseHealthBackgroundBar;
    [SerializeField] private GameObject houseHealthBarObject;
    [SerializeField] private GameObject houseHealthIcon;
    [SerializeField] private GameObject houseHealthIconMovedLeft1;
    [SerializeField] private GameObject houseHealthIconMovedLeft2;
    [SerializeField] private GameObject houseHealthIconMovedLeft3;
    private GameObject _nextHealthIconPosition = null;

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

    public void IncreaseHouseHealthBarSize(float percentageToIncreaseBy)
    {
        IncreaseWidth(houseHealthBackgroundBar);
        IncreaseWidth(houseHealthBarObject);
        MoveLeft(houseHealthIcon);
    }

    private void MoveLeft(GameObject toMove)
    {
        if (_nextHealthIconPosition == null)
        {
            _nextHealthIconPosition = houseHealthIconMovedLeft1;
        }
        else if (_nextHealthIconPosition == houseHealthIconMovedLeft1)
        {
            _nextHealthIconPosition = houseHealthIconMovedLeft2;
        }
        else if (_nextHealthIconPosition == houseHealthIconMovedLeft2)
        {
            _nextHealthIconPosition = houseHealthIconMovedLeft3;
        }

        var rectTransform = toMove.GetComponent<RectTransform>();
        rectTransform.position = _nextHealthIconPosition.transform.position;
    }

    private void IncreaseWidth(GameObject toIncrease)
    {
        var rectTransform = toIncrease.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + 10, rectTransform.sizeDelta.y);
    }
}
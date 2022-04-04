using TMPro;
using UnityEngine;

public class UpgradeCost : MonoBehaviour
{
    private int _cost;
    private bool _isMaxUpgradeLimitReached;

    [SerializeField] private GameObject maxLimitReachedText;
    [SerializeField] private GameObject coinIcon;
    [SerializeField] private TextMeshProUGUI upgradeCostText;

    public void SetCost(int cost)
    {
        if (_isMaxUpgradeLimitReached) return;
        _cost = cost;
        upgradeCostText.text = "x " + cost;
    }

    public int Cost()
    {
        return _cost;
    }

    public void SetMaxUpgradeLimitReached()
    {
        _isMaxUpgradeLimitReached = true;
        maxLimitReachedText.SetActive(true);
        coinIcon.SetActive(false);
        upgradeCostText.gameObject.SetActive(false);
    }
}
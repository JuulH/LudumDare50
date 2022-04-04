using TMPro;
using UnityEngine;

public class UpgradeCost : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private int _cost = 0;

    public void SetCost(int cost)
    {
        _cost = cost;
        _text = GetComponent<TextMeshProUGUI>(); //Awake/Start isn't called in time for this method, do this at runtime
        _text.text = "x " + cost;
    }

    public int Cost()
    {
        return _cost;
    }
}

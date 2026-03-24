using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncreaseUpgradeCost : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text;
    private float cost = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCost()
    {
        cost++;
        text.text = "Cost: " + cost;
    }
}

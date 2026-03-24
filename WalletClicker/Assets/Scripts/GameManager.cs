using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float money = 0.0f;
    public TMP_Text moneyText;
    public GameObject wallet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString() + "¥";
    }
}

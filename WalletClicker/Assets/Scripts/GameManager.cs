using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float money = 0.0f;
    [SerializeField] public Wallet wallet;
    public TMP_Text moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString() + "¥";
    }

    public void AddWalletValue()
    {
        if (money > 0)
        {
            money--;
            wallet.walletValue++;
            Debug.Log("added click value. current value: " + wallet.walletValue);
            Debug.Log("current money: " + money);
        }
    }
}

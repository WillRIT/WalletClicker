using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float money = 0.0f;
    [SerializeField] public Wallet wallet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerEconomy : MonoBehaviour
{
    [SerializeField]
    private int playerMoney = 100;
    [SerializeField]
    private TextMeshProUGUI playerFundsText;
    // Start is called before the first frame update
    void Start()
    {
        playerFundsText.SetText("$" + playerMoney);
    }
    public void addFunds(int cash)
    {
        playerMoney += cash;
        updateFundsText();
    }

    public void subtractFunds(int cash)
    {
        playerMoney -= cash;
        updateFundsText();
    }

    public void updateFundsText()
    {
        playerFundsText.SetText("$" + playerMoney);
    }

    public bool canAfford(int price)
    {
        if(price <= playerMoney)
        {
            return true;
        }
        return false;
    }
}

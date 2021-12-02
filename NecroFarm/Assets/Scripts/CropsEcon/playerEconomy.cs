using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class playerEconomy : MonoBehaviour
{
    [SerializeField]
    private int playerMoney = 100;
    [SerializeField]
    private int numBones = 4;
    [SerializeField]
    private TextMeshProUGUI playerFundsText;
    [SerializeField]
    private TextMeshProUGUI playerBonesText;
    // Start is called before the first frame update
    void Start()
    {
        playerFundsText.SetText("$" + playerMoney);
        playerFundsText.SetText("Bones: " + numBones);
    }
    public void addFunds(int cash)
    {
        playerMoney += cash;
        UtilsClass.CreateWorldTextAdd(" +$" + cash, Mouse3D.GetMouseWorldPosition());
        updateFundsText();
    }

    public void subtractFunds(int cash)
    {
        playerMoney -= cash;
        UtilsClass.CreateWorldTextDeduct(" -$"+cash, Mouse3D.GetMouseWorldPosition());
        updateFundsText();
    }

    public void updateFundsText()
    {
        playerFundsText.SetText("$" + playerMoney);
    }

    public void updateBonesText()
    {
        playerBonesText.SetText("Bones: " + numBones);
    }

    public bool canAfford(int price)
    {
        if(price <= playerMoney)
        {
            return true;
        }
        return false;
    }

    public bool enoughBones()
    {
        if (numBones > 0)
        {
            return true;
        }
        return false;
    }

    public void addBones()
    {
        numBones ++;
        //UtilsClass.CreateWorldTextAdd(" + 1 Bone", Mouse3D.GetMouseWorldPosition());
        updateBonesText();
    }

    public void subtractBones()
    {
        numBones--;
        //UtilsClass.CreateWorldTextDeduct(" - 1 Bone", Mouse3D.GetMouseWorldPosition());
        updateBonesText();
    }
}

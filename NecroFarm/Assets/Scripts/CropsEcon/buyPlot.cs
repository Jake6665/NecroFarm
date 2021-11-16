using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;

public class buyPlot : MonoBehaviour
{
    [SerializeField]
    GameObject plot;
    [SerializeField]
    private int plotPrice = 1000;
    [SerializeField]
    GameObject gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM");
    }

    public void PurchasePlot()
    {
        if (gameManager.GetComponent<playerEconomy>().canAfford(plotPrice))
        {
            Instantiate(plot, new Vector3( this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            gameManager.GetComponent<playerEconomy>().subtractFunds(plotPrice);
            Destroy(this.gameObject);
        }
        else
        {
            UtilsClass.CreateWorldTextPopup("Cannot afford this!", Mouse3D.GetMouseWorldPosition());
        }

    }

}

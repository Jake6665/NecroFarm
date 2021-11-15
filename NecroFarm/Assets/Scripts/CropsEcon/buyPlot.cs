using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buyPlot : MonoBehaviour
{
    [SerializeField]
    GameObject gridSys;
    private bool buyMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gridSys.GetComponent<GridBuildingSystem>().CheckPlotState())
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 25))
                {
                    if (hit.collider.gameObject == this && hit.collider != null)
                    {
                        buyMenu = true;
                        return;
                    }
                    else
                    {
                        buyMenu = false;
                        return;
                    }

                }
            }
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    public GameObject inventory;
    public GameObject invPages;

    public GameObject[] tools;
    public Transform toolHandle;

    public int midIndex;

    private void Awake()
    {

    }


    public void InventoryHandling(int invType) // 0 = normal, 1 = Kitchen, 2 = Sawmill, 3 = Forge
    {

        switch (inventory.activeSelf)
        {
            case true:
                inventory.SetActive(false);
                GameManager.acc.curState = playerState.normal;
                GameManager.acc.CursorState(CursorLockMode.Locked, false);
                break;

            case false:
                switch (invType)
                {
                    case 0:
                        GameManager.acc.curState = playerState.inInv;
                        break;

                    case 1:
                        GameManager.acc.curState = playerState.atKitchen;
                        invPages.transform.Find("Crops").SetAsLastSibling();
                        break;

                    case 2:
                        GameManager.acc.curState = playerState.atSawmill;
                        invPages.transform.Find("Wood").SetAsLastSibling();
                        break;

                    case 3:
                        GameManager.acc.curState = playerState.atForge;
                        invPages.transform.Find("Rocks").SetAsLastSibling();
                        break;
                }
                inventory.SetActive(true);
                GameManager.acc.CursorState(CursorLockMode.Confined, true);

                break;
        }

    }

    public void FlipDirection(string letterInput)
    {
        switch (letterInput)
        {
            case "Q":
                invPages.transform.GetChild(0).GetComponent<InventoryManager>().FlipToPage("b");
                break;

            case "E":
                invPages.transform.GetChild(invPages.transform.childCount-1).GetComponent<InventoryManager>().FlipToPage("f");
                break;
        }
    }

    public void ToolRotation(int dir) // position Steps: 130f
    {
        

        switch (dir)
        {
            case 1:
                if(midIndex < 4)
                {
                    midIndex += dir;
                    toolHandle.position += new Vector3(0f, 130f * dir, 0f);
                }
                break;

            case -1:
                if (midIndex > 0)
                {
                    midIndex += dir;
                    toolHandle.position += new Vector3(0f, 130f * dir, 0f);
                }
                break;
        }

        ShowNearTools();

    }

    void ShowNearTools()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            if (i == midIndex - 1 || i == midIndex + 1)
            {
                float scale = .5f;
                tools[i].SetActive(true);
                tools[i].GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
            }
            else if (i == midIndex)
            {
                tools[i].SetActive(true);
                tools[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else
                tools[i].SetActive(false);
        }
    }


}

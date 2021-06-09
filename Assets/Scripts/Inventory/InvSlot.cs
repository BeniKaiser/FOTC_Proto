using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InvSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject drop_btn, sell1_btn, sellAll_btn, refine_btn, exit_btn;
    public GameObject[] buttons;

    public Item curItem;

    public void OnPointerClick(PointerEventData pointer)
    {
        if (curItem.amount > 0)
        {
            ButtonHandler();
        }
        else
            print("No Item Selected");

    }

    public void DropItem()
    {
        GameObject g = Resources.Load<GameObject>("Prefabs/" + curItem.resource_Folder + "/" + curItem.item_name);
        Instantiate(g, GameManager.acc.PM.player.transform.position + GameManager.acc.PM.player.transform.forward * 2f, Quaternion.identity);
        curItem.amount--;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curItem.amount.ToString();

        ResetSlotCheck();
        CloseButtons();
    }

    public void SpawnRefinedObj()
    {
        GameObject g = Resources.Load<GameObject>("Prefabs/Processed_Prefabs/" + curItem.refinded_Item_Name);
        Instantiate(g, GameManager.acc.curObject.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), Quaternion.identity);

        curItem.amount--;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curItem.amount.ToString();

        
        ResetSlotCheck();
        CloseButtons();
    }

    public void Sell1Item()
    {


        ResetSlotCheck();
    }

    public void SellAllItems()
    {


        ResetSlotCheck();
        CloseButtons();
    }

    public void CloseButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    void ButtonHandler()
    {
        switch (GameManager.acc.curState)
        {
            case playerState.inInv:
                drop_btn.SetActive(true);
                exit_btn.SetActive(true);
                break;

            case playerState.atKitchen:
                refine_btn.SetActive(true);
                exit_btn.SetActive(true);
                break;

            case playerState.atSawmill:
                refine_btn.SetActive(true);
                exit_btn.SetActive(true);
                break;

            case playerState.atForge:
                refine_btn.SetActive(true);
                exit_btn.SetActive(true);
                break;

            case playerState.inShop:
                sell1_btn.SetActive(true);
                sellAll_btn.SetActive(true);
                exit_btn.SetActive(true);
                break;

        }
    }

    void ResetSlotCheck()
    {
        if (curItem.amount == 0)
        {
            curItem = default;
            GetComponent<Image>().sprite = null;
            CloseButtons();
        }
        
    }
}

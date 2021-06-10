using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour, IPointerClickHandler
{
    public Item buyItem;

    public GameObject[] buttons;

    public void OnPointerClick(PointerEventData pointer)
    {
        if(buyItem.amount > 0)
        {
            GameManager.acc.UIL.CloseAllItemButtons();
            HandelButtons(true);
            GameManager.acc.UIL.itemName.text = buyItem.item_name;
            GameManager.acc.UIL.itemValue.text = buyItem.item_worth.ToString();
        }
            

    }

    public void Buy1Item()
    {
        if (GameLogic.pMoney >= buyItem.item_worth)
        {
            Item i = buyItem;
            i.amount = 1;
            InventoryManager.acc.AddItem(buyItem.curItem_type, i);

            GameLogic.pMoney -= buyItem.item_worth;
            buyItem.amount--;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buyItem.amount.ToString();
            ResetSlotCheck();
        }
        else
            print("not enough Money");
    }

    public void BuyAllItems()
    {
        if (GameLogic.pMoney >= buyItem.item_worth * buyItem.amount)
        {
            InventoryManager.acc.AddItem(buyItem.curItem_type, buyItem);

            GameLogic.pMoney -= buyItem.item_worth * buyItem.amount;
            buyItem.amount = 0;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buyItem.amount.ToString();
            ResetSlotCheck();
        }
        else
            print("not enough Money");
        HandelButtons(false);
    }

    void ResetSlotCheck()
    {
        if (buyItem.amount == 0)
        {
            buyItem = default;
            GetComponent<Image>().sprite = null;
            HandelButtons(false);

            GameManager.acc.UIL.itemName.text = "Select Item";
            GameManager.acc.UIL.itemValue.text = "";
        }

    }

    public void HandelButtons(bool open)
    {
        switch (open)
        {
            case true:
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(true);
                }
                break;

            case false:
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(false);
                }
                break;
        }
        
    }

}

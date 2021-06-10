using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager acc;

    public GameObject[] inventories; // 0 = Wood, 1 = Rocks, 2 = Crops, 3 = Seeds

    private void Awake()
    {
        acc = this;
    }

    public void AddToInventory(int invType, GameObject invAdd)
    {
        AddItem(invType, invAdd.GetComponent<DropProperties>().dropItem);
    }

    public void AddItem(int invIndex, Item invAdd)
    {
        // Check if object exists

        for (int i = 0; i < inventories[invIndex].transform.GetChild(0).childCount; i++)
        {
            print(i);
            if(inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.amount == 0)
            {
                inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem = invAdd;
                inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.amount = 1;

                //Visuals
                UpdateVisuals(inventories[invIndex].transform.GetChild(0).GetChild(i).gameObject, invAdd);

                break;
            }
            else if(inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.amount > 0)
            {
                if(inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.item_name == invAdd.item_name)
                {
                    inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.amount += invAdd.amount;

                    // Visuals
                    UpdateVisuals(inventories[invIndex].transform.GetChild(0).GetChild(i).gameObject, invAdd);
                    break;
                }
            }
            
        }

        

    }

    void UpdateVisuals(GameObject invSlot, Item invAdd)
    {
        invSlot.GetComponent<Image>().sprite = invAdd.item_Spr;
        invSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = invSlot.GetComponent<InvSlot>().curItem.amount.ToString();
    }




    public void FlipToPage(string dir)
    {
        switch (dir)
        {
            case "b":
                GameManager.acc.UIL.invPages.transform.GetChild(0).SetAsLastSibling();
                break;

            case "f":
                GameManager.acc.UIL.invPages.transform.GetChild(GameManager.acc.UIL.invPages.transform.childCount - 1).SetAsFirstSibling();
                break;
        }

    }

    
}

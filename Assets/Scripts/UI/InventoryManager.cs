using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] inventories; // 0 = Wood, 1 = Rocks, 2 = Crops, 3 = Seeds

    public void AddToInventory(int invType, GameObject invAdd)
    {

        switch (invType)
        {
            case 0:
                AddItem(invType, invAdd.GetComponent<DropProperties>().dropItem);
                break;
                
            case 1:
                AddItem(invType, invAdd.GetComponent<DropProperties>().dropItem);
                break;

            case 2:
                AddItem(invType, invAdd.GetComponent<DropProperties>().dropItem);
                break;

            case 3:
                AddItem(invType, invAdd.GetComponent<DropProperties>().dropItem);
                break;
        }
    }

    void AddItem(int invIndex, Item invAdd)
    {
        // Check if object exists
        Debug.LogError("Items Fall Down Sometimes!!");
        

        for (int i = 0; i < inventories[invIndex].transform.GetChild(0).childCount; i++)
        {


            if (inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.item_name == "")
            {
                inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem = invAdd;
                inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().amount = 1;

                //Visuals
                inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = 
                    inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.item_Spr;
                inventories[invIndex].transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                    inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().amount.ToString();
                break;

            }
            else if (inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.item_name != "")
                if (inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().curItem.item_name == invAdd.item_name)
                {
                    inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().amount++;

                    // Visuals
                    inventories[invIndex].transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    inventories[invIndex].transform.GetChild(0).GetChild(i).GetComponent<InvSlot>().amount.ToString();
                    break;
                }


        }
        
        
    }




    public void FlipToPage(string dir)
    {
        switch (dir)
        {
            case "f":
                transform.SetAsFirstSibling();
                break;

            case "b":
                transform.SetAsLastSibling();
                break;
        }

    }

    
}

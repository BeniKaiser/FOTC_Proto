using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PInventory : MonoBehaviour
{
    public GameObject[] quickSlots;
    public GameObject[] invSlots;
    public GameObject quickSlotSelect;

    public int quickSlotIndex; // 0 = axe, 1 = pickaxe

    
    void Start()
    {
        quickSlotSelect.transform.position = quickSlots[quickSlotIndex].transform.position;
    }

    public void MoveSelection(int scrollInput)
    {
        int scrollOutput = Mathf.Clamp(scrollInput, -1, 1);
        if (quickSlotIndex == 0 && scrollOutput == -1)
        {
            quickSlotIndex = quickSlots.Length - 1;
        }
        else if (quickSlotIndex == quickSlots.Length - 1 && scrollOutput == 1)
        {
            quickSlotIndex = 0;
        }
        else
            quickSlotIndex += scrollOutput;

        quickSlotSelect.transform.position = quickSlots[quickSlotIndex].transform.position;
    }

    public void AddToInv(GameObject item, int index, bool crop)
    {

        if(invSlots[index].GetComponent<InvSlot>().curItem == null)
        {
            invSlots[index].GetComponent<InvSlot>().curItem = item.GetComponent<ItemProperties>().itemSprite;
            invSlots[index].GetComponent<InvSlot>().itemName = item.GetComponent<ItemProperties>().itemName;
            if(item.GetComponent<ItemProperties>().itemNameOrg != null)
                invSlots[index].GetComponent<InvSlot>().itemNameOrg = item.GetComponent<ItemProperties>().itemNameOrg;
            invSlots[index].GetComponent<Image>().sprite = item.GetComponent<ItemProperties>().itemSprite;
            if(crop)
            {
                invSlots[index].GetComponent<InvSlot>().crop = true;
            }

            // Update Amount
            UpdateItemAmount(item, index, 1);
        }
        else if(index == invSlots.Length)
        {
            Debug.Log("Inventory Full");
        }
        else
        {
            if(invSlots[index].GetComponent<InvSlot>().itemName == item.GetComponent<ItemProperties>().itemName)
            {
                UpdateItemAmount(item, index, 1);
            }
            else
                AddToInv(item, index + 1, crop);

        }
    }

    public void UpdateItemAmount(GameObject item, int index, int changeVal)
    {
        if(changeVal == 1)
        {
            invSlots[index].GetComponent<InvSlot>().itemAmount++;
            invSlots[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = invSlots[index].GetComponent<InvSlot>().itemAmount.ToString();
        }
        else if(changeVal == -1)
        {
            invSlots[index].GetComponent<InvSlot>().itemAmount--;
            invSlots[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = invSlots[index].GetComponent<InvSlot>().itemAmount.ToString();
        }
    }

}

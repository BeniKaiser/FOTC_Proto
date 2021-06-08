using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InvSlot : MonoBehaviour, IPointerClickHandler
{
    public int amount;

    public Item curItem;

    public void OnPointerClick(PointerEventData pointer)
    {
        if (amount > 0)
        {
            
            switch (GameManager.acc.curState)
            {

                case playerState.atKitchen:
                    SpawnRefinedObj("Crop");
                    break;

                case playerState.atSawmill:
                    SpawnRefinedObj("Wood");
                    break;

                case playerState.atForge:
                    SpawnRefinedObj("Stone");
                    break;

                case playerState.inInv:
                    DropItem();
                    break;

            }
            
        }
        else
            print("nothing to Refine");

    }

    void DropItem()
    {
        GameObject g = Resources.Load<GameObject>("Prefabs/Drop_Prefabs/" + curItem.item_name);
        Instantiate(g, GameManager.acc.curObject.transform.position + PManager.player.transform.forward * 2f, Quaternion.identity);
        amount--;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = amount.ToString();

        if (amount == 0)
            ResetSlot();
    }

    void SpawnRefinedObj(string itemType)
    {
        if (curItem.curItem_type == itemType)
        {

            GameObject g = Resources.Load<GameObject>("Prefabs/Processed_Prefabs/" + curItem.refinded_Item_Name);
            Instantiate(g, GameManager.acc.curObject.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), Quaternion.identity);

            amount--;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = amount.ToString();

            if (amount == 0)
                ResetSlot();
        }
    }

    void ResetSlot()
    {
        curItem = default;
        GetComponent<Image>().sprite = null;
    }
}

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
        if (curItem.refinded_Item_Name != "" && amount > 0)
        {
            
            switch (GameManager.acc.curState)
            {

                case playerState.atKitchen:
                    if (curItem.curItem_type == "Crop")
                    {
                        GameObject g = Resources.Load<GameObject>("Prefabs/Processed_Prefabs/" + curItem.refinded_Item_Name);
                        Instantiate(g, GameManager.acc.curObject.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), Quaternion.identity);
                        print("Spawn Obj");
                    }
                    break;

                case playerState.atSawmill:
                    if (curItem.curItem_type == "Wood")
                    {

                        GameObject g = Resources.Load<GameObject>("Prefabs/Processed_Prefabs/" + curItem.refinded_Item_Name);
                        Instantiate(g, GameManager.acc.curObject.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), Quaternion.identity);
                        print("Spawn Obj");
                    }
                    break;

                case playerState.atForge:
                    if (curItem.curItem_type == "Stone")
                    {
                        GameObject g = Resources.Load<GameObject>("Prefabs/Processed_Prefabs/" + curItem.refinded_Item_Name);
                        Instantiate(g, GameManager.acc.curObject.transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), Quaternion.identity);
                        print("Spawn Obj");
                    }
                    break;

            }

            amount--;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = amount.ToString();
            if (amount == 0)
                ResetSlot();
        }
        else
            print("nothing to Refine");

    }

    void ResetSlot()
    {
        curItem = default;
        GetComponent<Image>().sprite = null;
    }
}

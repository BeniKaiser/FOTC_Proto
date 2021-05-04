using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemNameOrg;
    public string itemName;
    public Sprite curItem;
    public int itemAmount;

    public bool log;
    public bool ore;
    public bool crop;

    public void OnPointerClick(PointerEventData pointer)
    {
        switch (transform.parent.GetComponent<CurInvType>().curInvType)
        {
            case 0:
                NormalInv();
                break;
            case 1:
                ChooseResource(1);
                break;
            case 2:
                ChooseResource(2);
                break;
            case 3:
                ChooseResource(3);
                break;
            case 4:
                ChooseResource(4);
                break;
        }
        //Debug.Log(pointer.pointerEnter.name);
    }

    void ClickBase()
    {
        itemAmount--;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemAmount.ToString();


        // Remove Visuals

        if (itemAmount <= 0)
        {
            GetComponent<Image>().sprite = null;
            curItem = null;

            crop = false;
        }
    }

    void NormalInv() // 0 = normal InvOpen
    {
        

        // Spawn Object
        if(curItem != null)
        {
            ClickBase();
            GameObject drop = Resources.Load<GameObject>("Prefabs/" + itemName);
            Instantiate(drop, PManager.acc.transform.position + new Vector3(PManager.acc.transform.forward.x * 3, 0f, PManager.acc.transform.forward.z * 3), Quaternion.identity);
        }
        
    }

    void ChooseResource(int resType) // 1 = Farm, 2 = Sägewerk, 3 = Schmiede, 4 = Küche
    {
        switch (resType)
        {
            case 1:
                FarmRes();
                break;

            case 2:
                Debug.Log("use Sawmill");
                CutWood();
                break;

            case 3:
                Debug.Log("use Forge");
                break;

            case 4:
                Cook();
                break;
        }
        
        
    }

    void FarmRes()
    {
        if (curItem != null && crop && GameManager.acc.curInteractable.transform.childCount == 0)
        {
            ClickBase();
            GameObject crop = Resources.Load<GameObject>("Prefabs/" + itemNameOrg);
            GameObject curDrop = Instantiate(crop, GameManager.acc.curInteractable.transform.position, Quaternion.identity);
            curDrop.transform.parent = GameManager.acc.curInteractable.transform;
            GameManager.acc.VisibilitySwitch();
        }
        else if (!crop)
            Debug.Log("No Crop!");
        else if (GameManager.acc.curInteractable.transform.childCount > 0)
            Debug.Log("Occupied");
    }

    void Cook()
    {


        Debug.Log("Cook");
    }

    void CutWood()
    {
        if(log)
        {

        }
    }



    
}

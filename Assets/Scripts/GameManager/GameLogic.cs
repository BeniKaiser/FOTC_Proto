using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    public InventoryManager IM;

    public int[] tools; // 0 = axe, 1 = Pickaxe, 2 = hoe
    public GameObject activeFields;
    public static int pMoney;
    public TextMeshProUGUI pMoney_txt;

    public void CollectObject(GameObject curObject)
    {
        switch (curObject.tag)
        {
            // Resource Collection
            case "Wood":
                IM.AddToInventory(0, curObject);
                break;

            case "Stone":
                IM.AddToInventory(1, curObject);
                break;

            case "Crop":
                IM.AddToInventory(2, curObject);
                break;

            case "Seed":
                IM.AddToInventory(3, curObject);
                break;

        }


    }

    public void ManageMoney()
    {
        pMoney_txt.text = pMoney.ToString();
    }

    public void Interaction(GameObject curObject)
    {
        switch (curObject.tag)
        {
            // Resource Collection
            default:
                DamageRessource(curObject);
                break;

            // Verarbeitung
            case "Kitchen":
                GameManager.acc.UIL.InventoryHandling(1);
                break;

            case "Sawmill":
                GameManager.acc.UIL.InventoryHandling(2);
                break;

            case "Forge":
                GameManager.acc.UIL.InventoryHandling(3);
                break;

            case "Shop":
                GameManager.acc.UIL.InventoryHandling(4);
                GameManager.acc.UIL.shopOptions.SetActive(true);
                break;

            case "Quest":
                GameManager.acc.UIL.questGiver_UI.SetActive(true);
                GameManager.acc.curState = playerState.atQuestGiver;
                break;
        }


    }

    void DamageRessource(GameObject curObject)
    {
        if (curObject.tag == GameManager.acc.UIL.tools[GameManager.acc.UIL.midIndex].tag)
        {
            if (curObject.GetComponent<ResourcePropertie>() != null)
            {
                if (curObject.CompareTag("Crop") && !curObject.GetComponent<CropProperties>().ripe)
                {
                    print("not Ripe Yet");
                }
                else
                {
                    if (curObject.GetComponent<ResourcePropertie>().durability > 0)
                    {
                        curObject.GetComponent<ResourcePropertie>().durability--;
                    }
                    else if (curObject.GetComponent<ResourcePropertie>().durability == 0)
                    {
                        Instantiate(curObject.GetComponent<ResourcePropertie>().drop, curObject.transform.position + new Vector3(Random.Range(-2, 2), 0f, Random.Range(-2, 2)), Quaternion.identity);
                        Destroy(curObject);
                    }
                }
            }
            else
                print("wtf are you doing??");
            
            
        }
        else
            print("wrong Tool");
        
    }

    public void GrowCrops() // scale .5 = 0, scale 2 = 1;
    {
        for (int i = 0; i < activeFields.transform.childCount; i++)
        {
            if(activeFields.transform.GetChild(i).childCount > 0)
            {
                if(!activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().ripe)
                {
                    activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().curDay++;

                    // CropVisuals
                    activeFields.transform.GetChild(i).GetChild(0).localScale =
                        new Vector3(.5f, (2f / activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().daysToMature) * 
                        activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().curDay, .5f);

                    if (activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().curDay ==
                        activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().daysToMature)
                    {
                        activeFields.transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>().material =
                            activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().ripe_Mat;
                        activeFields.transform.GetChild(i).GetChild(0).GetComponent<CropProperties>().ripe = true;
                    }
                }
                
            }
        }
    }

}

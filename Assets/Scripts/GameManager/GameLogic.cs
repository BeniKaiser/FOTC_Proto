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

    [Header("Farm Stuff")]
    public LayerMask farm_Msk;
    public GameObject farm;
    public float maxDigDistance;
    public Item resourceCost;

    public GameObject slotWithItem;

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
                GameManager.acc.UIL.questGiver_UI.transform.Find("AcceptButton").gameObject.SetActive(curObject.GetComponent<QuestGiver>().quest_accepted ? false : true);
                GameManager.acc.UIL.questGiver_UI.transform.Find("DeliverButton").gameObject.SetActive(CheckForItem(curObject.GetComponent<QuestGiver>().requiredItem) ? true : false);
                GameManager.acc.curState = playerState.atQuestGiver;
                break;

            case "Farm":
                if(!curObject.GetComponent<FarmProperties>().occupied)
                    GameManager.acc.UIL.InventoryHandling(5);
                else
                {
                    if(GameManager.acc.UIL.tools[GameManager.acc.UIL.midIndex].CompareTag(curObject.tag))
                    {
                        //set material to watered Farm
                        curObject.GetComponent<FarmProperties>().watered = true;
                        curObject.GetComponent<MeshRenderer>().material = curObject.GetComponent<FarmProperties>().wateredFarm;
                    }
                }
                break;

            case "Ground":
                if (GameManager.acc.UIL.tools[GameManager.acc.UIL.midIndex].CompareTag(curObject.tag))
                {
                    //Check for Resources
                    if(CheckForItem(resourceCost))
                    {
                        //Check if other farm is too close
                        if (!Physics.CheckSphere(GameManager.acc.PM.Col.hit.point, .5f, farm_Msk))
                        {
                            slotWithItem.GetComponent<InvSlot>().curItem.amount -= resourceCost.amount;
                            slotWithItem.GetComponent<InvSlot>().ResetSlotCheck();


                            // Dig out Farm
                            if (Vector3.Distance(PManager.player.transform.position, GameManager.acc.PM.Col.hit.point) < maxDigDistance)
                            {
                                GameObject g = Instantiate(farm, GameManager.acc.PM.Col.hit.point - new Vector3(0f, .4f, 0f), PManager.player.transform.rotation);
                                g.transform.parent = activeFields.transform;
                            }

                        }
                    }
                    else
                    {
                        Debug.LogWarning("not Enough Resources");
                    }
                    

                }
                break;
        }


    }

    bool CheckForItem(Item item)
    {
        for (int i = 0; i < GameManager.acc.UIL.invPages.transform.childCount; i++)
        {
            for (int ni = 0; ni < GameManager.acc.UIL.invPages.transform.GetChild(i).GetChild(0).childCount; ni++)
            {
                if(GameManager.acc.UIL.invPages.transform.GetChild(i).GetChild(0).GetChild(ni).GetComponent<InvSlot>().curItem.amount != 0)
                {
                    Item plaItem = GameManager.acc.UIL.invPages.transform.GetChild(i).GetChild(0).GetChild(ni).GetComponent<InvSlot>().curItem;
                    if (item.item_name == plaItem.item_name && item.amount <= plaItem.amount)
                    {
                        print("itemFound");
                        slotWithItem = GameManager.acc.UIL.invPages.transform.GetChild(i).GetChild(0).GetChild(ni).gameObject;
                        if(GameManager.acc.curObject.CompareTag("Quest"))
                        {
                            if(GameManager.acc.curObject.GetComponent<QuestGiver>().quest_accepted)
                            {
                                GameManager.acc.UIL.questItem = item;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                            

                        return true;
                    }
                }
                
            }
        }
        return false;
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
                    if(activeFields.transform.GetChild(i).GetComponent<FarmProperties>().watered)
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
                            activeFields.transform.GetChild(i).GetChild(0).GetComponent<BoxCollider>().enabled = true;


                        }
                        // set field material to dry
                        activeFields.transform.GetChild(i).GetComponent<FarmProperties>().watered = false;
                        activeFields.transform.GetChild(i).GetComponent<MeshRenderer>().material = activeFields.transform.GetChild(i).GetComponent<FarmProperties>().dryFarm;
                    }
                    
                }
                
            }
        }
    }

}

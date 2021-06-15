using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILogic : MonoBehaviour
{
    public GameObject inventory;

    public TextMeshProUGUI itemName_text;

    [Header("Shop")]
    public GameObject buyInventory;
    public GameObject shopOptions;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemValue;

    [Header("Quests")]
    public GameObject questGiver_UI;
    public GameObject questLog;

    public GameObject questEntryParent;
    public GameObject questEntry_Pre;
    public List<Quest> activeQuests = new List<Quest>();

    public Item questItem;
    public GameObject slotWithQuestItem;

    [Header("Quest Infos")]
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;

    [Header("Balloon")]
    public GameObject balloonCanvas;



    [Space]
    public GameObject invPages;

    public GameObject[] tools;
    public Transform toolHandle;

    public int midIndex;

    private void Start()
    {
        ShowNearTools();
    }


    #region Quests

    public void QuestLogHandling()
    {
        switch (questLog.activeSelf)
        {
            case true:
                questLog.SetActive(false);
                GameManager.acc.curState = playerState.normal;
                GameManager.acc.CursorState(CursorLockMode.Locked, false);
                break;

            case false:
                questLog.SetActive(true);
                GameManager.acc.curState = playerState.inQuestLog;
                GameManager.acc.CursorState(CursorLockMode.Confined, true);
                break;
        }
    }

    public void AcceptQuest()
    {

        activeQuests.Add(GameManager.acc.curObject.GetComponent<QuestGiver>().quest);
        GameManager.acc.curObject.GetComponent<QuestGiver>().quest_accepted = true;
        print("accepted");

        GameObject g = Instantiate(questEntry_Pre, questEntryParent.transform);
        g.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, 250 - (100 * (questEntryParent.transform.childCount - 1)), 0f);
        g.GetComponent<QuestLogEntry>().curQuest = GameManager.acc.curObject.GetComponent<QuestGiver>().quest;

        // upadate Quest Giver UI
        questGiver_UI.SetActive(false);
        GameManager.acc.curState = playerState.normal;
        GameManager.acc.CursorState(CursorLockMode.Locked, false);
    }

    public void DeliverQuest()
    {
        print("quest Delivered");

        GameManager.acc.GL.slotWithItem.GetComponent<InvSlot>().curItem.amount -= questItem.amount;
        GameManager.acc.GL.slotWithItem.GetComponent<InvSlot>().ResetSlotCheck();

        GameManager.acc.curObject.GetComponent<QuestGiver>().DropRewards();
        GameManager.acc.curObject.GetComponent<QuestGiver>().quest_accepted = false;
        for (int i = 0; i < questEntryParent.transform.childCount; i++)
        {
            if(questEntryParent.transform.GetChild(i).GetComponent<QuestLogEntry>().curQuest.questName == GameManager.acc.curObject.GetComponent<QuestGiver>().quest.questName)
            {
                Destroy(questEntryParent.transform.GetChild(i).gameObject);
            }
        }
        GameManager.acc.UIL.questGiver_UI.SetActive(false);

        GameManager.acc.curState = playerState.normal;
        GameManager.acc.CursorState(CursorLockMode.Locked, false);
    }

    public void DeclineQuest()
    {
        print("declined");
        // Reset Quest Giver UI
        questGiver_UI.SetActive(false);
        GameManager.acc.curState = playerState.normal;
        GameManager.acc.CursorState(CursorLockMode.Locked, false);
    }

    #endregion

    #region Inventory

    public void InventoryHandling(int invType) // 0 = normal, 1 = Kitchen, 2 = Sawmill, 3 = Forge
    {

        switch (inventory.activeSelf)
        {
            case true:
                CloseInventory();
                break;

            case false:
                if(buyInventory.activeSelf)
                {
                    CloseInventory();
                }
                else
                {
                    switch (invType)
                    {
                        case 0:
                            GameManager.acc.curState = playerState.inInv;
                            break;

                        case 1:
                            GameManager.acc.curState = playerState.atKitchen;
                            invPages.transform.Find("Crops").SetAsLastSibling();
                            break;

                        case 2:
                            GameManager.acc.curState = playerState.atSawmill;
                            invPages.transform.Find("Wood").SetAsLastSibling();
                            break;

                        case 3:
                            GameManager.acc.curState = playerState.atForge;
                            invPages.transform.Find("Rocks").SetAsLastSibling();
                            break;

                        case 4:
                            GameManager.acc.curState = playerState.inShop;
                            break;

                        case 5:
                            GameManager.acc.curState = playerState.atFarm;
                            invPages.transform.Find("Seeds").SetAsLastSibling();
                            break;

                    }
                    inventory.SetActive(true);
                    GameManager.acc.CursorState(CursorLockMode.Confined, true);
                }
                

                break;
        }

    }

    public void FlipDirection(string letterInput)
    {
        switch (letterInput)
        {
            case "E":
                GameManager.acc.IM.FlipToPage("b");
                CloseAllItemButtons();
                break;

            case "Q":
                GameManager.acc.IM.FlipToPage("f");
                CloseAllItemButtons();
                break;
        }
    }

    void CloseInventory()
    {
        ResetInventory();

        inventory.SetActive(false);
        GameManager.acc.curState = playerState.normal;
        GameManager.acc.CursorState(CursorLockMode.Locked, false);
    }

    public void ToolRotation(int dir) // position Steps: 130f
    {
        

        switch (dir)
        {
            case 1:
                if(midIndex < 4)
                {
                    midIndex += dir;
                    toolHandle.position += new Vector3(0f, 130f * dir, 0f);
                }
                break;

            case -1:
                if (midIndex > 0)
                {
                    midIndex += dir;
                    toolHandle.position += new Vector3(0f, 130f * dir, 0f);
                }
                break;
        }

        ShowNearTools();

    }

    void ShowNearTools()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            if (i == midIndex - 1 || i == midIndex + 1)
            {
                float scale = .5f;
                tools[i].SetActive(true);
                tools[i].GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
            }
            else if (i == midIndex)
            {
                tools[i].SetActive(true);
                tools[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
            else
                tools[i].SetActive(false);
        }
    }

    public void SellItems()
    {
        buyInventory.SetActive(false);
        inventory.SetActive(true);
    }

    public void BuyItems()
    {
        buyInventory.SetActive(true);
        inventory.SetActive(false);

        for (int i = 0; i < GameManager.acc.curObject.GetComponent<ShopInventory>().shopInv.Count; i++)
        {

            Item shopItem = GameManager.acc.curObject.GetComponent<ShopInventory>().shopInv[i];

            buyInventory.transform.GetChild(0).GetChild(i).GetComponent<BuySlot>().buyItem = shopItem;
            buyInventory.transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = shopItem.item_Spr;
            buyInventory.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItem.amount.ToString();
        }
        GameManager.acc.curObject.GetComponent<ShopInventory>().shopInv.Clear();
    }

    public void CloseAllItemButtons()
    {
        // Close all Open InvSlot Options
        for (int i = 0; i < invPages.transform.childCount; i++)
        {
            for (int ni = 0; ni < invPages.transform.GetChild(i).GetChild(0).childCount; ni++)
            {
                invPages.transform.GetChild(i).GetChild(0).GetChild(ni).GetComponent<InvSlot>().CloseButtons();
            }
        }

        // Close all Buy Slot Options
        for (int i = 0; i < buyInventory.transform.GetChild(0).childCount; i++)
        {
            buyInventory.transform.GetChild(0).GetChild(i).GetComponent<BuySlot>().HandelButtons(false);
        }
    }

    void ResetInventory()
    {
        CloseAllItemButtons();

        //Close Shop Options
        shopOptions.SetActive(false);
        // Disable buyInv, enable normal inv
        SellItems();
    }

    #endregion

    public void ExitMap()
    {
        balloonCanvas.SetActive(false);
        GameManager.acc.curState = playerState.normal;
        GameManager.acc.CursorState(CursorLockMode.Locked, false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class QuestLogEntry : MonoBehaviour, IPointerClickHandler
{
    public Quest curQuest;

    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curQuest.questName;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = curQuest.questIsland;
    }

    public void OnPointerClick(PointerEventData pointer)
    {
        print("Show Quest Infos");
        GameManager.acc.UIL.questName.text = curQuest.questName;
        GameManager.acc.UIL.questDescription.text = curQuest.QuestDescription;
    }
}

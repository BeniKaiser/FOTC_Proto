using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public bool quest_accepted;

    public Quest quest;
    public Item requiredItem;

    public void DropRewards()
    {
        Instantiate(quest.reward_1, transform.position + transform.forward, Quaternion.identity);
        Instantiate(quest.reward_2, transform.position + transform.forward, Quaternion.identity);
    }
}

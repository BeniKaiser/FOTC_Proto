using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Quest
{
    public string questName;
    public string questIsland;
    [TextArea]
    public string QuestDescription;
    public GameObject reward_1, reward_2;
}

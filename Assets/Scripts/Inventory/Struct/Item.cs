using System;
using UnityEngine;

[Serializable]

public struct Item
{
    public int amount;

    public string item_name;
    public string resource_Folder;
    public int curItem_type; // 0 Wood, 1 Rocks, 2 Crops, 3 Seeds
    public string refinded_Item_Name;
    public Sprite item_Spr;
    public bool refined;

    public int item_worth;

}

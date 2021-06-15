using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropProperties : MonoBehaviour
{
    public Item dropItem;

    private void Start()
    {
        dropItem.item_name = gameObject.name;
    }
}
